using LinqToDB.Mapping;

namespace Wells.Database.Entities
{
    [Table("measurement_types")]
    public class MeasurementType
    {
        [Column("id")] public int Id { get; set; }
        [Column("name")] public string Name { get; set; }
    }
}