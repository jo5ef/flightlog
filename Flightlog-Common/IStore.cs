using System.Collections.Generic;

namespace OCactus.Flightlog.Common
{
    public interface IStore
    {
        int WriteFlight(Flight f);

        IEnumerable<Flight> ReadFlights();
    }
}