using System;
using System.Collections.Generic;
using Wells.DataModels;

namespace Wells.Abstractions
{
    public interface IFileExporter
    {
        void ExportMeasurementData(List<MeasurementData> data, DateTime? targetDate);
    }
}