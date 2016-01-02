using OCactus.Flightlog.Common;
using System.Collections.Generic;
using System.Web.Http;

namespace OCactus.Flightlog.Web.Controllers
{
    public class FlightController : ApiController
    {
        private Database db;

        public FlightController()
        {
            db = new Database("Server=localhost;Database=flightlog;Uid=flightlog;Pwd=flightlog;");
        }

        public IEnumerable<Flight> Get()
        {
            return db.ReadFlights();
        }
    }
}
