using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class ItemReturn
    {
        public int ItemReturnId { get; set; }
        public Item Item { get; set; }
        public double Quantity { get; set; }
        public string ReasonForReturn { get; set; }
        public string ItemCondition { get; set; }
        public decimal ReturnAmount { get; set; }
        public bool IsReturnedToStock { get; set; }
        public string ReturnId { get; set; }
        public decimal ItemPrice { get; set; }

        public ItemReturn()
        {
            Item = new Item();
        }

        public List<ItemReturn> Retrieve()
        {
            Global global = new Global();
            List<ItemReturn> listItemReturn = new List<ItemReturn>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM item_return";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ItemReturn itemReturn = new ItemReturn();
                    itemReturn.ItemReturnId = dr.GetInt32("item_return_id");
                    itemReturn.Item = new Item().Retrieve(dr.GetString("item_id"));
                    itemReturn.Quantity = dr.GetDouble("quantity");
                    itemReturn.ReasonForReturn = dr.GetString("reason_for_return");
                    itemReturn.ItemCondition = dr.GetString("item_condition");
                    itemReturn.ReturnAmount = dr.GetDecimal("return_amount");
                    itemReturn.IsReturnedToStock = dr.GetBoolean("is_returned_to_stocks");
                    itemReturn.ReturnId = dr.GetString("return_id");
                    itemReturn.ItemPrice = dr.GetDecimal("item_price");
                    listItemReturn.Add(itemReturn);
                }
            }
            conn.Close();
            return listItemReturn;
        }
        public List<ItemReturn> Retrieve(string returnId)
        {
            Global global = new Global();
            List<ItemReturn> listItemReturn = new List<ItemReturn>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM item_return WHERE return_id = @id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id",returnId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ItemReturn itemReturn = new ItemReturn();
                    itemReturn.ItemReturnId = dr.GetInt32("item_return_id");
                    itemReturn.Item = new Item().Retrieve(dr.GetString("item_id"));
                    itemReturn.Quantity = dr.GetDouble("quantity");
                    itemReturn.ReasonForReturn = dr.GetString("reason_for_return");
                    itemReturn.ItemCondition = dr.GetString("item_condition");
                    itemReturn.ReturnAmount = dr.GetDecimal("return_amount");
                    itemReturn.IsReturnedToStock = dr.GetBoolean("is_returned_to_stocks");
                    itemReturn.ReturnId = dr.GetString("return_id");
                    itemReturn.ItemPrice = dr.GetDecimal("item_price");
                    listItemReturn.Add(itemReturn);
                }
            }
            conn.Close();
            return listItemReturn;
        }

        public ItemReturn Retrieve(int item_return_id)
        {
            Global global = new Global();
            ItemReturn itemReturn = new ItemReturn();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM item_return WHERE item_return_id = @id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", item_return_id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                itemReturn.ItemReturnId = dr.GetInt32("item_return_id");
                itemReturn.Item = new Item().Retrieve(dr.GetString("item_id"));
                itemReturn.Quantity = dr.GetDouble("quantity");
                itemReturn.ReasonForReturn = dr.GetString("reason_for_return");
                itemReturn.ItemCondition = dr.GetString("item_condition");
                itemReturn.ReturnAmount = dr.GetDecimal("return_amount");
                itemReturn.IsReturnedToStock = dr.GetBoolean("is_returned_to_stocks");
                itemReturn.ReturnId = dr.GetString("return_id");
                itemReturn.ItemPrice = dr.GetDecimal("item_price");
            }
            conn.Close();
            return itemReturn;
        }

        public bool ReturnToInventory()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            conn.Open();
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {

                string sql = "UPDATE item_return SET is_returned_to_stocks = true WHERE item_return_id = @id LIMIT 1 ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id",ItemReturnId);
                cmd.ExecuteNonQuery();

                //add the item to inventory
                sql = "UPDATE items SET quantity = quantity + @qty WHERE item_id = @item_id LIMIT 1";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@item_id",Item.ItemId);
                cmd.Parameters.AddWithValue("@qty",Quantity);
                cmd.ExecuteNonQuery();
                trans.Commit();
                conn.Close();
                Message.ShowSuccess("Item Successfully Return to Inventory.","Message");
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
                trans.Rollback();
                return false;
            }
            return true;
        }
    }
}
