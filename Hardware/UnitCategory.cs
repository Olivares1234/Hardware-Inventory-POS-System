using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class UnitCategory
    {
        public int UnitCategoryId { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public bool WholeInSale { get; set; }

        public bool InsertRecord()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "INSERT INTO unit_categories(description, unit,whole_in_sale) ";
            sql += "VALUES(@desc, @unit,@whole)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@desc", Description);
            cmd.Parameters.AddWithValue("@unit", Unit);
            cmd.Parameters.AddWithValue("@whole", WholeInSale);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public List<string> Retrieve()
        {
            List<string> unitCategories = new List<string>();
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT description FROM unit_categories";
            MySqlCommand cmd = new MySqlCommand(sql,conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    unitCategories.Add(dr.GetString("description"));
                }
            }
            conn.Close();
            return unitCategories;
        }
        public List<UnitCategory> RetrieveAll()
        {
            List<UnitCategory> unitCategories = new List<UnitCategory>();
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM unit_categories";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    UnitCategory unitCategory = new UnitCategory();
                    unitCategory.UnitCategoryId = dr.GetInt32("unit_category_id");
                    unitCategory.Description = dr.GetString("description");
                    unitCategory.Unit = dr.GetString("unit");
                    unitCategory.WholeInSale = dr.GetBoolean("whole_in_sale");
                    unitCategories.Add(unitCategory);
                }
            }
            conn.Close();
            return unitCategories;
        }
        public int Retrieve(string description)
        {
            int unit_category_id;
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT unit_category_id FROM unit_categories WHERE description = @desc";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@desc",description);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            unit_category_id = dr.GetInt32("unit_category_id");
            conn.Close();
            return unit_category_id;
        }

    }
}
