using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class Category
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }


        public bool InsertRecord()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "INSERT INTO categories(description) ";
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
            List<string> categories = new List<string>();
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT description FROM categories";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    categories.Add(dr.GetString("description"));
                }
            }
            conn.Close();
            return categories;
        }
        public int Retrieve(string description)
        {
            int categoryId;
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT category_id FROM categories WHERE description = @desc";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@desc", description);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            categoryId = dr.GetInt32("category_id");
            conn.Close();
            return categoryId;
        }
    }
}
