using System.Device.Location;

namespace OCactus.Flightlog.Common
{
    public class Airport
    {
        public string IcaoCode { get; set; }

        public GeoCoordinate Location { get; set; }
    }
}