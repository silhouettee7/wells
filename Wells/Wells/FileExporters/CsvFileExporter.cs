using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Wells.Abstractions;
using Wells.DataModels;

namespace Wells.FileExporters
{
    public class CsvFileExporter: IFileExporter
    {
        public void ExportMeasurementData(List<MeasurementData> data, DateTime? targetDate)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string dateSuffix = targetDate?.ToString("yyyyMMdd") ?? "all";
            string fileName = $"measurement_export_{dateSuffix}_{timestamp}.csv";

            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                writer.WriteLine("Department;WellName;MeasurementDate;MeasurementType;MinValue;MaxValue;MeasurementCount");

                foreach (var item in data)
                {
                    writer.WriteLine($"{item.Department};{item.WellName};{item.MeasurementDate:yyyy-MM-dd};{item.MeasurementType};{item.MinValue};{item.MaxValue};{item.MeasurementCount}");
                }
            }
            Console.WriteLine($"File saved: {fileName}");
        }
    }
}