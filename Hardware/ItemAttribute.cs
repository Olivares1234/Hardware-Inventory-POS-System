using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Hardware_MS
{
    class ItemAttribute
    {
        public int AttributeId { get; set; }
        public string Description { get; set; }


        public List<string> Retrieve()
        {
            Global global = new Global();
            List<string> attributeList = new List<string>();
            string sql = "SELECT description FROM item_attributes ";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    attributeList.Add(dr.GetString("description"));
                }
            }
            conn.Close();
            return attributeList;
        }

        public List<string> RetrieveAvailable(string id)
        {
            Global global = new Global();
            List<string> attributeList = new List<string>();
            string sql = "SELECT description FROM item_attributes WHERE attribute_id NOT IN (SELECT attribute_id FROM item_details WHERE item_id = @id) ";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    attributeList.Add(dr.GetString("description"));
                }
            }
            conn.Close();
            return attributeList;
        }

        public int Retrieve(string description)
        {
            int attributeId = 0;
            Global global = new Global();

            string sql = "SELECT attribute_id FROM item_attributes WHERE description = @desc";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@desc", description);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                attributeId = dr.GetInt32("attribute_id");
            }
            conn.Close();
            return attributeId;
        }
        public ItemAttribute Retrieve(int id)
        {
            Global global = new Global();
            ItemAttribute itemAttribute = new ItemAttribute();
            string sql = "SELECT * FROM item_attributes WHERE attribute_id = @id";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                itemAttribute.AttributeId = dr.GetInt32("attribute_id");
                itemAttribute.Description = dr.GetString("description");
            }
            conn.Close();
            return itemAttribute;
        }

        public bool InsertRecord()
        {
            try
            {
                Global global = new Global();
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                string sql = "INSERT INTO item_attributes(description) ";
                sql += "VALUES(@desc)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@desc", Description);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Message.ShowSuccess("Attribute Saved Successfully.","Message");
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: "+ ex.Message,"Error");
            }

            return true;
        }
    }
}
