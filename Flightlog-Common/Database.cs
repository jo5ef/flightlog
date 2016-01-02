using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Device.Location;

namespace OCactus.Flightlog.Common
{
    public class Database : IStore
    {
        private MySqlConnection db;
        private Dictionary<string, Airport> airports;

        public Database(string connectionString)
        {
            db = new MySqlConnection(connectionString);
            db.Open();

            LoadAirports();
        }

        private void LoadAirports()
        {
            airports = new Dictionary<string, Airport>();

            var cmd = new MySqlCommand("SELECT id, identifier, latitude, longitude FROM airport;", db);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var airport = new Airport
                    {
                        Id = reader.GetInt32(0),
                        IcaoCode = reader.GetString(1),
                        Location = new GeoCoordinate(reader.GetDouble(2), reader.GetDouble(3))
                    };

                    airports.Add(airport.IcaoCode, airport);
                }
            }
        }

        public int WriteFlight(Flight f)
        {
            var cmd = new MySqlCommand(@"INSERT INTO flight (pilot, tailnumber, type, crew,
                departure_airport, departure_time, arrival_airport, arrival_time,
                landings_day, landings_night, pic_time, dual_time, night_time, ifr_time, remarks)
                VALUES (1, @tailnumber, @type, @crew, @departure_airport, @departure_time,
                @arrival_airport, @arrival_time, @landings_day, @landings_night, @pic_time, @dual_time,
                @night_time, @ifr_time, @remarks);", db);

            cmd.Parameters.AddWithValue("tailnumber", f.Tailnumber);
            cmd.Parameters.AddWithValue("type", f.Model);
            cmd.Parameters.AddWithValue("crew", f.Crew);
            cmd.Parameters.AddWithValue("departure_airport", airports[f.DepartureAirport].Id);
            cmd.Parameters.AddWithValue("departure_time", f.DepartureTime);
            cmd.Parameters.AddWithValue("arrival_airport", airports[f.DestinationAirport].Id);
            cmd.Parameters.AddWithValue("arrival_time", f.ArrivalTime);
            cmd.Parameters.AddWithValue("landings_day", f.DayLandings);
            cmd.Parameters.AddWithValue("landings_night", f.NightLandings);
            cmd.Parameters.AddWithValue("pic_time", f.PICTime.TotalMinutes);
            cmd.Parameters.AddWithValue("dual_time", f.DualTime.TotalMinutes);
            cmd.Parameters.AddWithValue("night_time", f.NightTime.TotalMinutes);
            cmd.Parameters.AddWithValue("ifr_time", f.IFRTime.TotalMinutes);
            cmd.Parameters.AddWithValue("remarks", f.Remarks);

            return cmd.ExecuteNonQuery();
        }

        public IEnumerable<Flight> ReadFlights()
        {
            var cmd = new MySqlCommand(@"SELECT f.id, f.tailnumber, f.type, f.crew,
                dep.identifier AS departure_airport, f.departure_time, arr.identifier AS arrival_airport, f.arrival_time,
                f.landings_day, f.landings_night, f.pic_time, f.dual_time, f.night_time, f.ifr_time, f.remarks
                FROM flight f
	                JOIN airport dep ON dep.id = f.departure_airport
                    JOIN airport arr ON arr.id = f.arrival_airport;", db);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return ReadFlight(reader);
                }
            }
        }

        private Flight ReadFlight(MySqlDataReader reader)
        {
            return new Flight
            {
                Id = reader.GetInt32("id"),
                Tailnumber = reader.GetString("tailnumber"),
                Model = reader.GetString("type"),
                Crew = reader.GetString("crew"),
                DepartureAirport = reader.GetString("departure_airport"),
                DepartureTime = reader.GetDateTime("departure_time"),
                DestinationAirport = reader.GetString("arrival_airport"),
                ArrivalTime = reader.GetDateTime("arrival_time"),
                DayLandings = reader.GetInt32("landings_day"),
                NightLandings = reader.GetInt32("landings_night"),
                PICTime = TimeSpan.FromMinutes(reader.GetInt32("pic_time")),
                DualTime = TimeSpan.FromMinutes(reader.GetInt32("dual_time")),
                NightTime = TimeSpan.FromMinutes(reader.GetInt32("night_time")),
                IFRTime = TimeSpan.FromMinutes(reader.GetInt32("ifr_time")),
                Remarks = reader.GetString("remarks")
            };
        }
    }
}
