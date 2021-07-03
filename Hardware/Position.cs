using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class Position
    {
        public int PositionId { get; set; }
        public string Description { get; set; }

        public List<string> Retrieve()
        {
            Global global = new Global();
            List<string> positionList = new List<string>();
            string sql = "SELECT description FROM positions WHERE description != 'Administrator' ";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    positionList.Add(dr.GetString("description"));
                }
            }
            conn.Close();
            return positionList;
        }
        public int Retrieve(string description)
        {
            int positionId = 0;
            Global global = new Global();
            
            string sql = "SELECT position_id FROM positions WHERE description = @desc";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@desc", description);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                positionId = dr.GetInt32("position_id");
            }
            conn.Close();
            return positionId;
        }
        public Position Retrieve(int id)
        {
            Global global = new Global();
            Position position = new Position();
            string sql = "SELECT * FROM positions WHERE position_id = @id";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                position.PositionId = dr.GetInt32("position_id");
                position.Description = dr.GetString("description");
            }
            conn.Close();
            return position;
        }
        public bool InsertRecord()
        {
            try
            {
                Global global = new Global();
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                string sql = "INSERT INTO positions(description) VALUES(@desc) ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@desc", Description);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Message.ShowSuccess("Position Saved Successfully.", "Message");
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error has Occured: " + ex.Message, "Error");
                return false;
            }
            return true;
        }
    }
}
