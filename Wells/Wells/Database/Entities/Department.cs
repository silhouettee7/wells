using LinqToDB.Mapping;

namespace Wells.Database.Entities
{
    [Table("departments")]
    public class Department
    {
        [Column("id")] public int Id { get; set; }
        [Column("name")] public string Name { get; set; }
    }
}