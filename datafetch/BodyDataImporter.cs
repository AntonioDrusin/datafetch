using System.Collections.Generic;
using System.IO;
using datafetch.data;
using datafetch.progress;
using Newtonsoft.Json;
using NPoco;

namespace datafetch
{
    internal class BodyDataImporter
    {
        private readonly int _bodyId;
        private readonly IDatabase _db;
        private IProgress _progress;

        public BodyDataImporter(IDatabase db, int bodyId)
        {
            _db = db;
            _bodyId = bodyId;
        }

        public IProgress Progress
        {
            get { return _progress ?? new NullProgress(); }
            set { _progress = value; }
        }

        public void ImportComplianceRules()
        {
            ImportChapters();
        }

        private void ImportChapters()
        {
            _progress.StartTask("Importing chapters", GetChapterCount());
            CreateJsonFile(GetChapterData(), "chapters.json");

            _progress.StartTask("Importing Standards", GetStandardCount());
            CreateJsonFile(GetStandardData(), "standards.json");

            _progress.StartTask("Importing Elements Of Performance", GetElementOfPerformanceCount());
            CreateJsonFile(GetElementOfPerformanceData(), "ep.json");
        }

        private void CreateJsonFile<T>(IEnumerable<T> collection, string fileName)
        {
            using (var sw = new StreamWriter(fileName))
            {
                bool first = true;
                sw.WriteLine("[");
                foreach (T e in collection)
                {
                    if (!first)
                    {
                        sw.Write(",");
                    }
                    first = false;
                    sw.WriteLine(JsonConvert.SerializeObject(e));
                    _progress.Progress("");
                }
                sw.WriteLine("]");
                sw.Close();
            }
        }

        private int GetChapterCount()
        {
            return _db.ExecuteScalar<int>("select count(*) from stjcahochapter where stbody_id = @0", _bodyId);
        }

        private IEnumerable<ChapterData> GetChapterData()
        {
            return
                _db.Query<ChapterData>(
                    "select * from stjcahochapter where stbody_id = @0 and deleted = 0 and expired = 0", _bodyId);
        }

        private IEnumerable<StandardData> GetStandardData()
        {
            return
                _db.Query<StandardData>(@"
                        select s.stjcahochapter_id chapterid, s.title, s.description, s.Code, s.standard_code standardCode, s.sortorder, s.rational, s.ambulatorycare isambulatorycare, s.behavorialhealth isbehavioralhealth, s.hospital ishospital, s.introduction  
                        from stjcahostandard as s 
                        inner join stjcahochapter as c on c.id = s.stjcahochapter_id
                        where stbody_id = @0 and c.deleted = 0 and s.deleted = 0 and c.expired = 0
                ", _bodyId);
        }

        private int GetStandardCount()
        {
            return
                _db.ExecuteScalar<int>(@"
                        select count(*)  
                        from stjcahostandard as s 
                        inner join stjcahochapter as c on c.id = s.stjcahochapter_id
                        where stbody_id = 1 and c.deleted = 0 and s.deleted = 0 and c.expired = 0
                ", _bodyId);
        }

        private IEnumerable<ElementOfPerformanceData> GetElementOfPerformanceData()
        {
            return _db.Query<ElementOfPerformanceData>(@"
                select ep.id, s.stjcahochapter_id chapterid, s.id standardid, 
                ep.title, ep.description, ep.ondate, ep.changedate, ep.jcahocategory category, jcahomos MeasureOfSuccess, jcahodocumentation documentation, jcahosituational situational,
                jcahoDirectImpact DirectImpact, ep.hospital ishospital, criticalaccess iscriticalaccess, behavioralhealth isbehavioralhealth, lab islab, homecare ishomecare, longtermcare islongtermcare, scoring,
                c.code + '.' + s.code + '.' + cast( ep.title as nvarchar(max)) code
                from stjcahostandardelement ep 
                inner join stjcahostandard as s on ep.stjcahostandard_id = s.id
                inner join stjcahochapter as c on c.id = s.stjcahochapter_id
                where stbody_id = 1 and c.deleted = 0 and s.deleted = 0 and c.expired = 0
                and ep.deleted = 0
            ", _bodyId);
        }

        private int GetElementOfPerformanceCount()
        {
            return _db.ExecuteScalar<int>(@"
                select count(*)
                from stjcahostandardelement ep 
                inner join stjcahostandard as s on ep.stjcahostandard_id = s.id
                inner join stjcahochapter as c on c.id = s.stjcahochapter_id
                where stbody_id = 1 and c.deleted = 0 and s.deleted = 0 and c.expired = 0
                and ep.deleted = 0
            ", _bodyId);
        }
    }
}