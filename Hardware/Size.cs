using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class Size
    {
        public int SizeId { get; set; }
        public string Description { get; set; }

        public List<string> Retrieve(int categoryId)
        {
            List<string> sizes = new List<string>();
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT sizes.description ";
            sql += "FROM sizes INNER JOIN category_size ";
            sql += "ON sizes.size_id = category_size.size_id ";
            sql += "WHERE category_size.category_id = @categoryId";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@categoryId",categoryId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    sizes.Add(dr.GetString("description"));
                }
            }
            conn.Close();
            return sizes;
        }
        public List<string> Retrieve()
        {
            List<string> sizes = new List<string>();
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM sizes";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    sizes.Add(dr.GetString("description"));
                }
            }
            conn.Close();
            return sizes;
        }
        public int Retrieve(string description)
        {
            int size_id;
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT size_id FROM sizes WHERE description = @desc";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@desc", description);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            size_id = dr.GetInt32("size_id");
            conn.Close();
            return size_id;
        }

    }
}
