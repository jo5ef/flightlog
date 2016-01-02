using System.Device.Location;

namespace OCactus.Flightlog.Common
{
    public class Airport
    {
        public int Id { get; set; }

        public string IcaoCode { get; set; }

        public GeoCoordinate Location { get; set; }
    }
}