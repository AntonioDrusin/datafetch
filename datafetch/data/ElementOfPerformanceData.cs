using System;

namespace datafetch.data
{
    internal class ElementOfPerformanceData
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public int StandardId { get; set; }
        public int Title { get; set; }
        public string Description { get; set; }
        public DateTime? OnDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public int Category { get; set; }
        public int MeasureOfSuccess { get; set; }
        public bool Documentation { get; set; }
        public bool Situational { get; set; }
        public bool DirectImpact { get; set; }
        public bool IsHospital { get; set; }
        public bool IsCriticalAccess { get; set; }
        public bool IsBehavioralHealth { get; set; }
        public bool IsLab { get; set; }
        public bool IsHomeCare { get; set; }
        public bool IsLongTermCare { get; set; }
        public String Scoring { get; set; }
        public string Code { get; set; }
    }
}