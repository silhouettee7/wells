using System;
using LinqToDB.Mapping;

namespace Wells.Database.Entities
{
    [Table("measurements")]
    public class Measurement
    {
        [Column("id")] public int Id { get; set; }
        [Column("well_id")] public int WellId { get; set; }
        [Column("measurement_type_id")] public int MeasurementTypeId { get; set; }
        [Column("value")] public decimal Value { get; set; }
        [Column("measurement_time")] public DateTime MeasurementTime { get; set; }
    }
}