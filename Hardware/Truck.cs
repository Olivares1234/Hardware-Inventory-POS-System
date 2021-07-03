using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class Truck
    {
        public int TruckId { get; set; }
        public string Description { get; set; }
        public string PlateNo { get; set; }
        public string Year { get; set; }


        public bool InsertRecord()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "INSERT INTO trucks(description,plate_no,year_created) VALUES(@description,@plateNo,@year)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@description", Description);
            cmd.Parameters.AddWithValue("@plateNo", PlateNo);
            cmd.Parameters.AddWithValue("@year", Year);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }
        public List<Truck> Retrieve()
        {
            Global global = new Global();
            List<Truck> listTruck = new List<Truck>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM trucks";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Truck truck = new Truck();
                    truck.TruckId = dr.GetInt32("truck_id");
                    truck.Description = dr.GetString("description");
                    truck.PlateNo = dr.GetString("plate_no");
                    truck.Year = dr.GetString("year_created");
                    listTruck.Add(truck);
                }
            }
            conn.Close();
            return listTruck;
        }
        public Truck Retrieve(int id)
        {
            Global global = new Global();
            Truck truck = new Truck();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM trucks WHERE truck_id = @id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                truck.TruckId = dr.GetInt32("truck_id");
                truck.Description = dr.GetString("description");
                truck.PlateNo = dr.GetString("plate_no");
                truck.Year = dr.GetString("year_created");
            }
            conn.Close();
            return truck;
        }
        public List<Truck> Retrieve(string keyword)
        {
            Global global = new Global();
            List<Truck> listTruck = new List<Truck>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM trucks WHERE description LIKE @keyword OR truck_id = @id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
            cmd.Parameters.AddWithValue("@id", keyword);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Truck truck = new Truck();
                    truck.TruckId = dr.GetInt32("truck_id");
                    truck.Description = dr.GetString("description");
                    truck.PlateNo = dr.GetString("plate_no");
                    truck.Year = dr.GetString("year_created");
                    listTruck.Add(truck);
                }
            }
            conn.Close();
            return listTruck;
        }
        public List<Truck> RetrieveAvailableTrucks(DateTime date, DateTime start, DateTime end)
        {
            Global global = new Global();
            List<Truck> listTruck = new List<Truck>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM trucks ";
            sql += "WHERE truck_id NOT IN(SELECT truck_id FROM deliveries WHERE ((deliveries.time_start BETWEEN @start AND @end) ";
            sql += "OR (deliveries.time_end BETWEEN @start AND @end)) ";
            sql += "AND deliveries.date = @date)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@start", start.ToString("HH:mm"));
            cmd.Parameters.AddWithValue("@end", end.ToString("HH:mm"));
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Truck truck = new Truck();
                    truck.TruckId = dr.GetInt32("truck_id");
                    truck.Description = dr.GetString("description");
                    truck.PlateNo = dr.GetString("plate_no");
                    truck.Year = dr.GetString("year_created");
                    listTruck.Add(truck);
                }
            }
            conn.Close();
            return listTruck;
        }
        public bool UpdateRecord()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "UPDATE trucks SET ";
            sql += "description = @description, ";
            sql += "plate_no = @plateNo, ";
            sql += "year_created = @year ";
            sql += "WHERE truck_id = @truckId ";
            sql += "LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@truckId",TruckId);
            cmd.Parameters.AddWithValue("@description", Description);
            cmd.Parameters.AddWithValue("@plateNo", PlateNo);
            cmd.Parameters.AddWithValue("@year", Year);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }
        public int CountTrucks()
        {
            Global global = new Global();
            int count = 0;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT COUNT(truck_id) AS 'count' FROM trucks";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            count = dr.GetInt32("count");
            conn.Close();
            return count;
        }
        public string Validate()
        {
            string errors = "";
            Validation validation = new Validation();
            if (validation.Is_Blank(Description))
            {
                errors += "Description cannot be blank.\n";
            }
            else if (!validation.HasLengthBetween(Description, 2, 45))
            {
                errors += "Descrition characters length must be between 2 and 45.\n";
            }

            if (validation.Is_Blank(PlateNo))
            {
                errors += "PlateNO cannot be blank.\n";
            }
            else if (validation.HasLengthGreaterThan(PlateNo, 100))
            {
                errors += "Address characters length limit is 100.\n";
            }

            if (validation.Is_Blank(Year))
            {
                errors += "Year cannot be blank.\n";
            }
            else if (validation.HasLengthGreaterThan(Year, 20))
            {
                errors += "Year length limit is 20.\n";
            }
            return errors;
        }


    }
}
