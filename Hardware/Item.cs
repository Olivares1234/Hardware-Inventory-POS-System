using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class Item
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal SRP { get; set; }
        public double Quantity { get; set; }
        public UnitCategory UnitCategory { get; set; }
        public Category Category { get; set; }
        public bool IsVatable { get; set; }
        public List<ItemDetail> ItemDetails { get; set; }

        public Item()
        {
            UnitCategory = new UnitCategory();
            Category = new Category();
            ItemDetails = new List<ItemDetail>();
        }

        public string GenerateID()
        {
            Global global = new Global();
            DateTime now = DateTime.Now;
            string newID = now.ToString("yy") + now.ToString("MM") + "0001";
            string sql = "SELECT item_id FROM items WHERE item_id LIKE @id ";
            sql = sql + "ORDER BY item_id DESC Limit 1";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", now.ToString("yy") + now.ToString("MM") + "%");
            
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                newID = dr.GetString("item_id").Substring(4);
                newID = (Convert.ToInt32(newID) + 1).ToString(now.ToString("yy") + now.ToString("MM") + "0000");
            }
            conn.Close();
            return newID;
        }
        public void AddDetail(ItemDetail detail)
        {
            ItemDetails.Add(detail);
        }
        public void RemoveDetail(string detailName, string detailDesc)
        {
            for (int i = 0; i < ItemDetails.Count; i++)
            {
                if (detailName == ItemDetails[i].ItemAttribute.Description && detailDesc == ItemDetails[i].DetailDescription)
                {
                    ItemDetails.RemoveAt(i);
                    break;
                }
            }
        }
        public string GetDescription()
        {
            string desc = ItemName;
            if (ItemDetails.Count > 0)
            {
                desc += " - ";
                for (int i = 0; i < ItemDetails.Count; i++)
                {
                    desc += ItemDetails[i].DetailDescription;
                    if (i == ItemDetails.Count - 1)
                        continue;
                    desc += ", ";
                }
            }       
            return desc;
        }

        public string GetPriceWithVatString()
        {
            Global global = new Global();
            string price = Price.ToString("N2");

            if (IsVatable)
            {
                price = (Price + (Price * global.vatTaxPercentage)).ToString("N2") + "V";
            }
            return price;

        }
        public decimal GetPriceWithVat()
        {
            Global global = new Global();
            decimal price = Price;

            if (IsVatable)
            {
                price = (Price + (Price * global.vatTaxPercentage));
            }
            return price;

        }

        public bool InsertRecord()
        {
            try
            {
                Global global = new Global();
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                conn.Open();
                string sql = "INSERT INTO items(item_id,item_name,price,quantity,unit_category_id,category_id,SRP,is_vatable) ";
                sql += "VALUES(@itemId,@name,@price,@qty,@unit,@category,@srp,@is_vatable)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@itemId", ItemId);
                cmd.Parameters.AddWithValue("@name", ItemName);
                cmd.Parameters.AddWithValue("@price", Price);
                cmd.Parameters.AddWithValue("@qty", Quantity);
                cmd.Parameters.AddWithValue("@unit", UnitCategory.UnitCategoryId);
                cmd.Parameters.AddWithValue("@category", Category.CategoryId);
                cmd.Parameters.AddWithValue("@srp", SRP);
                cmd.Parameters.AddWithValue("@is_vatable", IsVatable);
                cmd.ExecuteNonQuery();
                for (int i = 0; i < ItemDetails.Count; i++)
                {
                    ItemDetails[i].InsertRecord();
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
                return false;
            }
            return true;
        }

        public List<Item> Retrieve()
        {
            Global global = new Global();
            List<Item> listItems = new List<Item>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT item_id,item_name,price,quantity,unit_categories.unit,unit_categories.whole_in_sale, categories.description as category,SRP,is_vatable ";
            sql += "FROM ((items INNER JOIN unit_categories ";
            sql += "ON items.unit_category_id = unit_categories.unit_category_id) ";
            sql += "INNER JOIN categories ON items.category_id = categories.category_id) ";
            sql += "ORDER BY item_id DESC";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Item item = new Item();
                    item.ItemId = dr.GetString("item_id");
                    item.ItemName = dr.GetString("item_name");
                    item.Price = dr.GetDecimal("price");
                    item.Quantity = dr.GetDouble("quantity");
                    item.UnitCategory.Unit = dr.GetString("unit");
                    item.UnitCategory.WholeInSale = dr.GetBoolean("whole_in_sale");
                    item.Category.Description = dr.GetString("category");
                    item.ItemDetails = new ItemDetail().Retrieve(dr.GetString("item_id"));
                    item.SRP = dr.GetDecimal("SRP");
                    item.IsVatable = dr.GetBoolean("is_vatable");
                    listItems.Add(item);
                }
            }
            conn.Close();
            return listItems;
        }
        public List<Item> Retrieve(int stockLevel)// 0 - Out of stock | 1- Low stock | 2- In stock
        {
            Global global = new Global();
            List<Item> listItems = new List<Item>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT item_id,item_name,price,quantity,unit_categories.unit,unit_categories.whole_in_sale, categories.description as category,SRP,is_vatable ";
            sql += "FROM ((items INNER JOIN unit_categories ";
            sql += "ON items.unit_category_id = unit_categories.unit_category_id) ";
            sql += "INNER JOIN categories ON items.category_id = categories.category_id) ";
            if (stockLevel == 0)//Out of stock
            {
                sql += "WHERE items.quantity <= 0 ";
            }
            else if (stockLevel == 1)//Low stock
            {
                sql += "WHERE items.quantity > 0 AND items.quantity <= 15 ";
            }
            else if (stockLevel == 2)//In Stock
            {
                sql += "WHERE items.quantity > 15 ";
            }
            sql += "ORDER BY item_id DESC ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Item item = new Item();
                    item.ItemId = dr.GetString("item_id");
                    item.ItemName = dr.GetString("item_name");
                    item.Price = dr.GetDecimal("price");
                    item.Quantity = dr.GetDouble("quantity");
                    item.UnitCategory.Unit = dr.GetString("unit");
                    item.UnitCategory.WholeInSale = dr.GetBoolean("whole_in_sale");
                    item.Category.Description = dr.GetString("category");
                    item.ItemDetails = new ItemDetail().Retrieve(dr.GetString("item_id"));
                    item.SRP = dr.GetDecimal("SRP");
                    item.IsVatable = dr.GetBoolean("is_vatable");
                    listItems.Add(item);
                }
            }
            conn.Close();
            return listItems;
        }

        public Item Retrieve(string id)
        {
            Global global = new Global();
            Item item = new Item();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT item_id,item_name,price,quantity,unit_categories.unit,unit_categories.whole_in_sale, categories.description as category,SRP,is_vatable ";
            sql += "FROM ((items INNER JOIN unit_categories ";
            sql += "ON items.unit_category_id = unit_categories.unit_category_id) ";
            sql += "INNER JOIN categories ON items.category_id = categories.category_id) ";
            sql += "WHERE item_id = @itemId";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@itemId", id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                item.ItemId = dr.GetString("item_id");
                item.ItemName = dr.GetString("item_name");
                item.Price = dr.GetDecimal("price");
                item.Quantity = dr.GetDouble("quantity");
                item.UnitCategory.Unit = dr.GetString("unit");
                item.UnitCategory.WholeInSale = dr.GetBoolean("whole_in_sale");
                item.Category.Description = dr.GetString("category");
                item.ItemDetails = new ItemDetail().Retrieve(dr.GetString("item_id"));
                item.SRP = dr.GetDecimal("SRP");
                item.IsVatable = dr.GetBoolean("is_vatable");
            }
            conn.Close();
            return item;
        }

        public List<Item> Search(string keyword)
        {
            Global global = new Global();
            var arrKeyword = keyword.Split(new char[] { ' ' });
            List<Item> listItems = new List<Item>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT items.item_id,item_name,price,quantity,unit_categories.unit, categories.description as category,SRP,is_vatable ";
            sql += "FROM (((items INNER JOIN unit_categories ";
            sql += "ON items.unit_category_id = unit_categories.unit_category_id) ";
            sql += "INNER JOIN categories ON items.category_id = categories.category_id) ";
            sql += "INNER JOIN item_details ON item_details.item_id = items.item_id) ";

            //search logic
            sql += "WHERE item_name LIKE @keyword ";
            sql += "OR items.item_id = @id ";
            sql += "OR item_details.detail_description LIKE @keyword ";
            for (int i = 0; i < arrKeyword.Length; i++)
            {
                sql += "OR item_details.detail_description LIKE @keyword" + i + " ";
            }
            sql += "GROUP BY items.item_id ";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
            cmd.Parameters.AddWithValue("@id", keyword);
            for (int i = 0; i < arrKeyword.Length; i++)
            {
                cmd.Parameters.AddWithValue("@keyword"+i, "%" + arrKeyword[i] + "%");
            }
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Item item = new Item();
                    item.ItemId = dr.GetString("item_id");
                    item.ItemName = dr.GetString("item_name");
                    item.Price = dr.GetDecimal("price");
                    item.Quantity = dr.GetDouble("quantity");
                    item.UnitCategory.Unit = dr.GetString("unit");
                    item.Category.Description = dr.GetString("category");
                    item.ItemDetails = new ItemDetail().Retrieve(dr.GetString("item_id"));
                    item.SRP = dr.GetDecimal("SRP");
                    item.IsVatable = dr.GetBoolean("is_vatable");
                    listItems.Add(item);
                }
            }
            conn.Close();
            return listItems;
        }

        public List<Item> RetrieveByCategory(int categoryId)
        {
            Global global = new Global();
            List<Item> listItems = new List<Item>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT item_id,item_name,price,quantity,unit_categories.unit, categories.description as category,SRP,is_vatable ";
            sql += "FROM ((items INNER JOIN unit_categories ";
            sql += "ON items.unit_category_id = unit_categories.unit_category_id) ";
            sql += "INNER JOIN categories ON items.category_id = categories.category_id) ";
            sql += "WHERE items.category_id = @categoryId ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@categoryId",categoryId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Item item = new Item();
                    item.ItemId = dr.GetString("item_id");
                    item.ItemName = dr.GetString("item_name");
                    item.Price = dr.GetDecimal("price");
                    item.Quantity = dr.GetDouble("quantity");
                    item.UnitCategory.Unit = dr.GetString("unit");
                    item.Category.Description = dr.GetString("category");
                    item.ItemDetails = new ItemDetail().Retrieve(dr.GetString("item_id"));
                    item.SRP = dr.GetDecimal("SRP");
                    item.IsVatable = dr.GetBoolean("is_vatable");
                    listItems.Add(item);
                }
            }
            conn.Close();
            return listItems;
        }

        public void UpdateRecord()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "UPDATE items SET ";
            sql += "item_name = @itemName, ";
            sql += "price = @price, ";
            sql += "category_id = @categoryId, ";
            sql += "SRP = @srp, ";
            sql += "is_vatable = @is_vatable ";
            sql += "WHERE item_id = @id ";
            sql += "LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", ItemId);
            cmd.Parameters.AddWithValue("@itemName", ItemName);
            cmd.Parameters.AddWithValue("@price", Price);
            cmd.Parameters.AddWithValue("@categoryId", Category.CategoryId);
            cmd.Parameters.AddWithValue("@srp", SRP);
            cmd.Parameters.AddWithValue("@is_vatable", IsVatable);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public int CountItems()
        {
            Global global = new Global();
            int count = 0;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT COUNT(item_id) AS 'count' FROM items";
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
            string error = "";
            Validation validation = new Validation();
            if (validation.Is_Blank(ItemName))
            {
                error += "Item Name cannot be blank.\n";
            }
            else if (!(validation.HasLengthBetween(ItemName, 2, 45)))
            {
                error += "Item Name characters length must be between 2 and 45.\n";
            }
            if (!validation.IsValidPrice(Price))
            {
                error += "Price must be a valid value.\n";
            }
            if (validation.IsNegativeValue(Quantity))
            {
                error += "Quantity must be a valid value.\n";
            }
            return error;
        }
    }
}
