using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class BrokenItem
    {
        public int BrokenItemId { get; set; }
        public Item Item { get; set; }
        //public string ItemId { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public BrokenItem()
        {
            Item = new Item();
        }
        public bool InsertRecord()
        {
            Global global = new Global();
            string sql = "";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            conn.Open();
            MySqlTransaction trans = conn.BeginTransaction();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            try
            {
                sql = "INSERT INTO broken_items(broken_item_id,item_id,quantity,description,price,date_time) ";
                sql += "VALUES(@bkid,@item_id,@qty,@desc,@price,@date) ";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@bkid", BrokenItemId);
                cmd.Parameters.AddWithValue("@item_id", Item.ItemId);
                cmd.Parameters.AddWithValue("@qty", Quantity);
                cmd.Parameters.AddWithValue("@desc", Description);
                cmd.Parameters.AddWithValue("@price", Price);
                cmd.Parameters.AddWithValue("@date", Date.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
                //update item qty...
                sql = "UPDATE items SET quantity = quantity - @qty ";
                sql += "WHERE item_id = @item_id";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                trans.Commit();
                conn.Close();
                Message.ShowSuccess("Transaction saved successfully.", "Message");
            }
            catch (Exception e)
            {
                trans.Rollback();
                Message.ShowError("An Error Has Occured: " + e.Message, "Error");
            }
            return true;
        }
        public List<BrokenItem> Retrieve()
        {
            Global global = new Global();
            List<BrokenItem> listBrokenItem = new List<BrokenItem>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM broken_items ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    BrokenItem brokenItem = new BrokenItem();
                    brokenItem.BrokenItemId = dr.GetInt32("broken_item_id");
                    brokenItem.Item  = new Item().Retrieve(dr.GetString("item_id"));
                    brokenItem.Quantity = dr.GetDouble("quantity");
                    brokenItem.Price = dr.GetDecimal("Price");
                    brokenItem.Description = dr.GetString("description");
                    brokenItem.Date = dr.GetDateTime("date_time");
                    listBrokenItem.Add(brokenItem);
                }
            }
            conn.Close();
            return listBrokenItem;
        }
        public BrokenItem Retrieve(int id)
        {
            Global global = new Global();
            BrokenItem brokenItem = new BrokenItem();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM broken_items WHERE broken_item_id = @id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                brokenItem.BrokenItemId = dr.GetInt32("broken_item_id");
                brokenItem.Item.ItemId = dr.GetString("item_id");
                brokenItem.Quantity = dr.GetDouble("quantity");
                brokenItem.Price = dr.GetDecimal("Price");
                brokenItem.Description = dr.GetString("description");
                brokenItem.Date = dr.GetDateTime("date_time");
            }
            conn.Close();
            return brokenItem;

        }
        public int CountBrokenItems()
        {
            Global global = new Global();
            int count = 0;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT COUNT(broken_item_id) AS 'count' FROM broken_items";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            count = dr.GetInt32("count");
            conn.Close();
            return count;
        }

    }
}
