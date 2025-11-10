using System;
using System.Diagnostics;
using System.IO;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Configuration;
using Wells.Abstractions;
using Wells.Database;
using Wells.FileExporters;

namespace Wells
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = SetConfiguration();
            using var appDataConnection = ConfigureAppDataConnection(configuration);
            var repository = new Repository(appDataConnection);
            var csvExporter = new CsvFileExporter();
            
            UploadDataToCsvBasedOnUserInputDate(repository, csvExporter);
        }

        static void UploadDataToCsvBasedOnUserInputDate(IRepository repository, IFileExporter csvFileExporter)
        {
            Console.WriteLine("Enter date for export (format: YYYY-MM-DD) or press Enter for all period:");
            var input = Console.ReadLine();

            DateTime? targetDate = null;
            if (!string.IsNullOrEmpty(input))
            {
                if (DateTime.TryParse(input, out DateTime parsedDate))
                {
                    targetDate = parsedDate.Date;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Exporting for all period.");
                }
            }

            try
            {
                var data = repository.GetMeasurementData(targetDate);
                csvFileExporter.ExportMeasurementData(data, targetDate);
                Console.WriteLine("Export completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static IConfiguration SetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
        static AppDataConnection ConfigureAppDataConnection(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSQL") ?? "";
            var options = new DataOptions()
                .UseConnectionString(ProviderName.PostgreSQL, connectionString)
                .UseTracing(TraceLevel.Verbose, OnTrace);
            
            var appDataConnection = new AppDataConnection(options);
            
            return appDataConnection;
        }

        static void OnTrace(TraceInfo traceInfo)
        {
            if (traceInfo.TraceInfoStep != TraceInfoStep.BeforeExecute) return;
            Console.WriteLine($"{traceInfo.SqlText}");
            Console.WriteLine("---");
        }
    }
}