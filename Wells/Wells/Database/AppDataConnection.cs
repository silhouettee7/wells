using LinqToDB;
using LinqToDB.Data;
using Wells.Database.Entities;

namespace Wells.Database
{
    public class AppDataConnection : DataConnection
    {
        public AppDataConnection(DataOptions options) : base(options) { }
        
        public ITable<Department> Departments => this.GetTable<Department>();
        public ITable<Well> Wells => this.GetTable<Well>();
        public ITable<MeasurementType> MeasurementTypes => this.GetTable<MeasurementType>();
        public ITable<Measurement> Measurements => this.GetTable<Measurement>();
    }
}