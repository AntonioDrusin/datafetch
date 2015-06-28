namespace datafetch.data
{
    internal class StandardData
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StandardCode { get; set; }
        public string Rational { get; set; }
        public bool IsAmbulatoryCare { get; set; }
        public bool IsBehavioralHealth { get; set; }
        public bool IsHospital { get; set; }
        public string Introduction { get; set; }
    }
}