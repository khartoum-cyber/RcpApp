using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RcpApp.Models;

namespace RcpApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please write path to folder with CSV files...");
                var csvPath = Console.ReadLine();
                Console.WriteLine("Searching for rcp files...");
                string[] files = Directory.GetFiles(csvPath, "*.csv");
            
                Console.WriteLine("Reading files and importing RCP.");
            
                var workDays = await ReadCsv(files);
            
                Console.WriteLine("All files read.");
                Console.WriteLine("RCPs:");
                workDays.ForEach(w => Console.WriteLine($"{w.KodPracownika}, {w.Data.Value.ToString("dd/MM/yyyy")}, {w.GodzinaWejscia}, {w.GodzinaWyjscia}"));
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        static async Task<List<WorkDay>> ReadCsv(string[] files)
        {
            List<WorkDay> workDays = new List<WorkDay>();
            
            foreach (var file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var values = line?.Split(';');

                        TimeSpan gWe;
                        TimeSpan gWy;

                        TimeSpan.TryParse(values?[2], out gWe);
                        TimeSpan.TryParse(values?[3], out gWy);
                        
                        WorkDay workDay = new WorkDay
                        {
                            KodPracownika = values?[0],
                            Data = DateTime.Parse(values?[1] ?? string.Empty),
                            GodzinaWejscia = gWe,
                            GodzinaWyjscia = gWy
                        };
                        
                        workDays.Add(workDay);
                    }
                }
            }

            return workDays;
        }
    }
}