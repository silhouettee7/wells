using System;
using System.Collections.Generic;
using Wells.DataModels;

namespace Wells.Abstractions
{
    public interface IRepository
    {
        List<MeasurementData> GetMeasurementData(DateTime? targetDate);
    }
}