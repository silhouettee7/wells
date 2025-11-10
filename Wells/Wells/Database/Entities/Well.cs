using LinqToDB.Mapping;

namespace Wells.Database.Entities
{
    [Table("wells")]
    public class Well
    {
        [Column("id")] public int Id { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("department_id")] public int DepartmentId { get; set; }
    }
}