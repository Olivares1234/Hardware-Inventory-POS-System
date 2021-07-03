using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class DeliveryStatus
    {
        public int StatusId { get; set; }
        public string Description { get; set; }

        public bool InsertRecord()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "INSERT INTO deliver_status(description) ";
            sql += "VALUES(@desc)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@desc", Description);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return true;
        }
        public List<string> Retrieve()
        {
            List<string> deliveries = new List<string>();
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT description FROM deliver_status";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    deliveries.Add(dr.GetString("description"));
                }
            }
            conn.Close();
            return deliveries;
        }
        public DeliveryStatus Retrieve(int id)
        {
            DeliveryStatus deliveryStatus = new DeliveryStatus();
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM deliver_status WHERE status_id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                deliveryStatus.StatusId = dr.GetInt32("status_id");
                deliveryStatus.Description = dr.GetString("description");
            }
            conn.Close();
            return deliveryStatus;
        }
        public int Retrieve(string description)
        {
            int statusId;
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT status_id FROM deliver_status WHERE description = @desc";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@desc", description);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            statusId = dr.GetInt32("status_id");
            conn.Close();
            return statusId;
        }
    }
}
