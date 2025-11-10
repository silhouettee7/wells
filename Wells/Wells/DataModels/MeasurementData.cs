using System;

namespace Wells.DataModels
{
    public class MeasurementData
    {
        public string Department { get; set; }
        public string WellName { get; set; }
        public DateTime MeasurementDate { get; set; }
        public string MeasurementType { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public int MeasurementCount { get; set; }
    }
}