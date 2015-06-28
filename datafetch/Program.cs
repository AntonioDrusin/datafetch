using datafetch.progress;
using NPoco;

namespace datafetch
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var db =
                new Database(
                    "Data Source=ocashvrgdb02s.verge-solutions.com;Initial Catalog=vsuiteprodcopy;Persist Security Info=True;User ID=vsurveydb;Password=Caballer0", DatabaseType.SqlServer2008);
            var dataImporter = new BodyDataImporter(db, 1) {Progress = new ConsoleProgress()};
            dataImporter.ImportComplianceRules();
        }
    }
}