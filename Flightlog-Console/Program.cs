using OCactus.Flightlog.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OCactus.Flightlog.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvImporter = new CSVImporter();
            var flights = csvImporter.ParseFlights(File.ReadAllLines("sample.csv"));

            WriteTime("total", flights.Sum(f => f.Flighttime.TotalMinutes));
            WriteTime("pic", flights.Sum(f => f.PICTime.TotalMinutes));
            WriteTime("dual", flights.Sum(f => f.DualTime.TotalMinutes));
            WriteTime("ifr", flights.Sum(f => f.IFRTime.TotalMinutes));

            System.Console.WriteLine($"landings: {flights.Sum(f => f.DayLandings)}");

            /*
            var invalidFlightTime = flights.Where(f => f.ArrivalTime - f.DepartureTime != f.Flighttime);
            System.Console.WriteLine($"{invalidFlightTime.Count()} invalid flights:");
            */

            var airports = new Dictionary<string, Airport>();
            foreach(var a in csvImporter.ParseAirports(File.ReadAllLines("airports.csv")))
            {
                airports.Add(a.IcaoCode, a);
            }

            System.Console.WriteLine("cross country flights:");
            var xcTime = TimeSpan.FromDays(0);

            foreach(var f in flights.Where(f => Distance(f, airports) >= 50))
            {
                System.Console.WriteLine($"{f.DepartureAirport}->{f.DestinationAirport}, {Distance(f, airports)}");
                xcTime += f.Flighttime;
            }

            WriteTime("cross country", xcTime.TotalMinutes);
        }

        static double Distance(Flight f, Dictionary<string, Airport> airports)
        {
            var from = airports[f.DepartureAirport];
            var to = airports[f.DestinationAirport];
            return from.Location.GetDistanceTo(to.Location) / 1852.0;
        }

        static void WriteTime(string title, double time)
        {
            var t = TimeSpan.FromMinutes(time);
            System.Console.WriteLine($"{title} time: {(int)t.TotalHours} h {t.Minutes} min");
        }
    }
}
