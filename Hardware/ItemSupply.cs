using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class ItemSupply
    {
        public string ItemSupplyId { get; set; }
        public Item Item { get; set; }
        public Supplier Supplier { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public DateTime DateRecieved { get; set; }
        public Staff Staff { get; set; }
        public bool IsAddedToStock { get; set; }

        public ItemSupply()
        {
            Item = new Item();
            Supplier = new Supplier();
            Staff = new Staff();
        }

        public string GenerateID()
        {
            Global global = new Global();
            DateTime now = DateTime.Now;
            string newID = now.ToString("yy") + now.ToString("MM") + "0001";
            string sql = "SELECT item_supply_id FROM item_supplies WHERE item_supply_id LIKE @id ";
            sql = sql + "ORDER BY item_supply_id DESC Limit 1";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", now.ToString("yy") + now.ToString("MM") + "%");
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                newID = dr.GetString("item_supply_id").Substring(4);
                newID = (Convert.ToInt32(newID) + 1).ToString(now.ToString("yy") + now.ToString("MM") + "0000");
            }
            conn.Close();
            return newID;
        }

        public bool InsertRecord()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            conn.Open();
            string sql = "INSERT INTO item_supplies(item_supply_id,item_id,supplier_id,price,quantity,date_recieved,staff_id,is_added_to_stock) ";
            sql += "VALUES(@itemSupplyId,@itemId,@supplierId,@price,@qty,@dateRecieved,@staffId,@isAdded)";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            MySqlTransaction tr = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = tr;
            cmd.Parameters.AddWithValue("@itemSupplyId", ItemSupplyId);
            cmd.Parameters.AddWithValue("@itemId", Item.ItemId);
            cmd.Parameters.AddWithValue("@supplierId", Supplier.SupplierID);
            cmd.Parameters.AddWithValue("@price", Price);
            cmd.Parameters.AddWithValue("@qty", Quantity);
            cmd.Parameters.AddWithValue("@dateRecieved", DateRecieved.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@staffId", Staff.StaffId);
            cmd.Parameters.AddWithValue("@isAdded", IsAddedToStock);
            try
            {
                
                cmd.ExecuteNonQuery();
                if (IsAddedToStock == true)
                {
                    sql = "UPDATE items SET quantity = quantity + @quantity ";
                    sql += "WHERE item_id = @item_id ";
                    sql += "LIMIT 1";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@quantity",Quantity);
                    cmd.Parameters.AddWithValue("@item_id", Item.ItemId);
                    cmd.ExecuteNonQuery();
                }
                tr.Commit();
            }
            catch (Exception)
            {
                tr.Rollback();
                throw new Exception();
            }
            conn.Close();
            return true;
        }
        public bool MoveToInventory(ItemSupply itemSupply)
        {
            if (itemSupply.IsAddedToStock == false)
            {
                Global global = new Global();
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                conn.Open();
                string sql = "UPDATE items SET quantity = quantity + @quantity WHERE item_id = @itemId LIMIT 1 ";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                MySqlTransaction tr = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = tr;
                cmd.Parameters.AddWithValue("@quantity", itemSupply.Quantity);
                cmd.Parameters.AddWithValue("@itemId", itemSupply.Item.ItemId);
                try
                {
                    cmd.ExecuteNonQuery();

                    sql = "UPDATE item_supplies SET is_added_to_stock = true ";
                    sql += "WHERE item_supply_id = @itemSupplyId ";
                    sql += "LIMIT 1 ";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@itemSupplyId", itemSupply.ItemSupplyId);
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                }
                catch (Exception)
                {
                    tr.Rollback();
                    throw new Exception();
                    //System.Windows.Forms.MessageBox.Show("Test");
                }
                conn.Close();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public List<ItemSupply> Retrieve()
        {
            Global global = new Global();
            List<ItemSupply> listItemSupply = new List<ItemSupply>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT item_supply_id,item_id,supplier_id,price,quantity,date_recieved,staff_id,is_added_to_stock ";
            sql += "FROM item_supplies ";
            sql += "ORDER BY is_added_to_stock, ";
            sql += "date_recieved DESC ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ItemSupply itemSupply = new ItemSupply();
                    itemSupply.ItemSupplyId = dr.GetString("item_supply_id");
                    itemSupply.Item = new Item().Retrieve(dr.GetString("item_id"));
                    itemSupply.Supplier = new Supplier().Retrieve(dr.GetInt32("supplier_id"));
                    itemSupply.Price = dr.GetDecimal("price");
                    itemSupply.Quantity = dr.GetDouble("quantity");
                    itemSupply.DateRecieved = dr.GetDateTime("date_recieved");
                    itemSupply.IsAddedToStock = dr.GetBoolean("is_added_to_stock");
                    listItemSupply.Add(itemSupply);
                }
            }
            conn.Close();
            return listItemSupply;
        }
        public ItemSupply Retrieve(string id)
        {
            Global global = new Global();
            ItemSupply itemSupply = new ItemSupply();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT item_supply_id,item_id,supplier_id,price,quantity,date_recieved,staff_id,is_added_to_stock ";
            sql += "FROM item_supplies ";
            sql += "WHERE item_supply_id = @id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                itemSupply.ItemSupplyId = dr.GetString("item_supply_id");
                itemSupply.Item = new Item().Retrieve(dr.GetString("item_id"));
                itemSupply.Supplier = new Supplier().Retrieve(dr.GetInt32("supplier_id"));
                itemSupply.Price = dr.GetDecimal("price");
                itemSupply.Quantity = dr.GetDouble("quantity");
                itemSupply.DateRecieved = dr.GetDateTime("date_recieved");
                itemSupply.IsAddedToStock = dr.GetBoolean("is_added_to_stock");
            }
            conn.Close();
            return itemSupply;
        }
        public int CountNewSupply()
        {
            Global global = new Global();
            int count = 0;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT COUNT(item_supply_id) AS 'count' FROM item_supplies WHERE is_added_to_stock = false";
            MySqlCommand cmd = new MySqlCommand(sql,conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            count = dr.GetInt32("count");
            conn.Close();
            return count;
        }
        public List<ItemSupply> Search(string keyword)
        {
            Global global = new Global();
            var arrKeyword = keyword.Split(new char[] { ' ' });
            List<ItemSupply> listItemSupply = new List<ItemSupply>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT item_supply_id,item_supplies.item_id,item_supplies.supplier_id,item_supplies.price,item_supplies.quantity,date_recieved,item_supplies.staff_id,is_added_to_stock ";
            sql += "FROM (((item_supplies INNER JOIN items ON item_supplies.item_id = items.item_id) ";
            sql += "INNER JOIN item_details ON item_details.item_id = items.item_id) ";
            sql += "INNER JOIN suppliers ON suppliers.supplier_id = item_supplies.supplier_id) ";

            //search logic
            sql += "WHERE item_name LIKE @keyword ";
            sql += "OR item_supplies.item_supply_id = @id ";
            sql += "OR item_details.detail_description LIKE @keyword ";
            sql += "OR suppliers.company_name LIKE @keyword ";
            for (int i = 0; i < arrKeyword.Length; i++)
            {
                sql += "OR item_details.detail_description LIKE @keyword" + i + " ";
            }
            sql += "GROUP BY item_supplies.item_supply_id ";
            sql += "ORDER BY is_added_to_stock ";
            

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
            cmd.Parameters.AddWithValue("@id", keyword);
            for (int i = 0; i < arrKeyword.Length; i++)
            {
                cmd.Parameters.AddWithValue("@keyword" + i, "%" + arrKeyword[i] + "%");
            }
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ItemSupply itemSupply = new ItemSupply();
                    itemSupply.ItemSupplyId = dr.GetString("item_supply_id");
                    itemSupply.Item = new Item().Retrieve(dr.GetString("item_id"));
                    itemSupply.Supplier = new Supplier().Retrieve(dr.GetInt32("supplier_id"));
                    itemSupply.Price = dr.GetDecimal("price");
                    itemSupply.Quantity = dr.GetDouble("quantity");
                    itemSupply.DateRecieved = dr.GetDateTime("date_recieved");
                    itemSupply.IsAddedToStock = dr.GetBoolean("is_added_to_stock");
                    listItemSupply.Add(itemSupply);
                }
            }
            conn.Close();
            return listItemSupply;
        }
        public List<ItemSupply> FilterByMovedToInventory(bool status)
        {
            Global global = new Global();
            List<ItemSupply> listItemSupply = new List<ItemSupply>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT item_supply_id,item_id,supplier_id,price,quantity,date_recieved,staff_id,is_added_to_stock ";
            sql += "FROM item_supplies ";
            sql += "WHERE is_added_to_stock = @status ";
            sql += "ORDER BY is_added_to_stock ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@status",status);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ItemSupply itemSupply = new ItemSupply();
                    itemSupply.ItemSupplyId = dr.GetString("item_supply_id");
                    itemSupply.Item = new Item().Retrieve(dr.GetString("item_id"));
                    itemSupply.Supplier = new Supplier().Retrieve(dr.GetInt32("supplier_id"));
                    itemSupply.Price = dr.GetDecimal("price");
                    itemSupply.Quantity = dr.GetDouble("quantity");
                    itemSupply.DateRecieved = dr.GetDateTime("date_recieved");
                    itemSupply.IsAddedToStock = dr.GetBoolean("is_added_to_stock");
                    listItemSupply.Add(itemSupply);
                }
            }
            conn.Close();
            return listItemSupply;
        }
        public bool UpdateRecord()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            
            string sql = "UPDATE item_supplies ";
            sql += "SET price = @price, quantity = @qty, date_recieved = @dateRecieved ";
            sql += "WHERE item_supply_id = @id ";
            sql += "LIMIT 1";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@id", ItemSupplyId);
            cmd.Parameters.AddWithValue("@price", Price);
            cmd.Parameters.AddWithValue("@qty", Quantity);
            cmd.Parameters.AddWithValue("@dateRecieved", DateRecieved.ToString("yyyy-MM-dd"));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }
        public string Validate()
        {
            string errors = "";
            Validation validation = new Validation();
            if (validation.Is_Blank(Item.ItemId))
            {
                errors += "Please select an item.\n";
            }
            if (Supplier.SupplierID == 0)
            {
                errors += "Please select the supplier.\n";
            }
            if (validation.IsNegativeValue(Price))
            {
                errors += "Please enter a valid price.\n";
            }
            if (!validation.IsGreaterThanZero(Quantity))
            {
                errors += "The quantity is not valid.\n";
            }
            return errors;
        }
    }
}
