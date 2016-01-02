using System;

namespace OCactus.Flightlog.Common
{
    public class Flight
    {
        public int Id { get; set; }

        public string Tailnumber { get; set; }

        public string Model { get; set; }

        public string Crew { get; set; }

        public string DepartureAirport { get; set; }

        public string DestinationAirport { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public int DayLandings { get; set; }

        public int NightLandings { get; set; }

        public TimeSpan Flighttime { get; set; }

        public TimeSpan PICTime { get; set; }

        public TimeSpan DualTime { get; set; }

        public TimeSpan NightTime { get; set; }

        public TimeSpan IFRTime { get; set; }

        public string Remarks { get; set; }
    }
}
