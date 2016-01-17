using OCactus.Flightlog.Common;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;

namespace OCactus.Flightlog.Console
{
    internal class CSVImporter
    {
        public IEnumerable<Flight> ParseFlights(IEnumerable<string> lines)
        {
            foreach(var line in lines.Skip(1))
            {
                var parts = line.Split(new[] { ';' }, 14);
                var date = DateTime.Parse(parts[0]);

                yield return new Flight
                {
                    Tailnumber = parts[1],
                    Type = parts[2],
                    Crew = parts[3],
                    DepartureAirport = parts[4],
                    DepartureTime = MakeTime(date, parts[5]),
                    DestinationAirport = parts[6],
                    ArrivalTime = MakeTime(date, parts[7]),
                    DayLandings = int.Parse(parts[8]),
                    FlightTime = MakeTimespan(parts[9]),
                    PICTime = MakeTimespan(parts[10]),
                    DualTime = MakeTimespan(parts[11]),
                    Remarks = parts[12],
                    IFRTime = parts.Length > 13 ? MakeTimespan(parts[13]) : TimeSpan.FromSeconds(0)
                };
            }
        }

        public IEnumerable<Airport> ParseAirports(IEnumerable<string> lines)
        {
            foreach(var line in lines)
            {
                var parts = line.Split(new[] { ';' }, 3);

                yield return new Airport
                {
                    IcaoCode = parts[0],
                    Location = new GeoCoordinate(double.Parse(parts[1]), double.Parse(parts[2]))
                };
            }
        }

        private DateTime MakeTime(DateTime date, string time)
        {
            var parts = time.Split(new[] { ':' }, 2);
            return new DateTime(date.Year, date.Month, date.Day,
                int.Parse(parts[0]), int.Parse(parts[1]), 0);
        }

        private TimeSpan MakeTimespan(string time)
        {
            var parts = time.Split(new[] { ':' }, 2);
            return new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), 0);
        }
    }
}