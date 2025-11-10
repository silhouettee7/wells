using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using Wells.Abstractions;
using Wells.DataModels;

namespace Wells.Database
{
    public class Repository: IRepository
    {
        private readonly AppDataConnection _db;
        
        public Repository(AppDataConnection appDataConnection)
        {
            _db = appDataConnection;
        }

        public List<MeasurementData> GetMeasurementData(DateTime? targetDate)
        {
            var query = _db.Measurements
                .InnerJoin(_db.Wells, 
                    (m,w) => m.WellId == w.Id, 
                    (m,w) => new 
                    { 
                        Measurement = m, 
                        Well = w, 
                    })
                .InnerJoin(_db.Departments,
                    (mw, d) => mw.Well.DepartmentId == d.Id,
                    (mw, d) => new
                    {
                        Department = d,
                        mw.Well,
                        mw.Measurement,
                    })
                .InnerJoin(_db.MeasurementTypes,
                    (mwd,mt) => mt.Id == mwd.Measurement.MeasurementTypeId,
                    (mwd, mt) => new 
                    {
                        Department = mwd.Department.Name,
                        WellName = mwd.Well.Name,
                        MeasurementDate = mwd.Measurement.MeasurementTime.Date,
                        MeasurementType = mt.Name,
                        mwd.Measurement.Value,
                        mwd.Measurement.MeasurementTime
                    });
            if (targetDate.HasValue)
            {
                query = query
                    .Where(x => x.MeasurementDate == targetDate.Value);
            }
            
            var result = query
                .GroupBy(x => new
                {
                    x.Department,
                    x.WellName,
                    x.MeasurementDate,
                    x.MeasurementType
                })
                .Select(g => new MeasurementData
                {
                    Department = g.Key.Department,
                    WellName = g.Key.WellName,
                    MeasurementDate = g.Key.MeasurementDate,
                    MeasurementType = g.Key.MeasurementType,
                    MinValue = g.Min(x => x.Value),
                    MaxValue = g.Max(x => x.Value),
                    MeasurementCount = g.Count()
                })
                .OrderBy(x => x.Department)
                .ThenBy(x => x.WellName)
                .ThenBy(x => x.MeasurementDate)
                .ThenBy(x => x.MeasurementType)
                .ToList();

            return result;
        }
    }
}