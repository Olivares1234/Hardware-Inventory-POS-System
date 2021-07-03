using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class ItemDetail
    {
        public int DetailId { get; set; }
        public string ItemId { get; set; }
        public string DetailDescription { get; set; }
        public ItemAttribute ItemAttribute { get; set; }

        public ItemDetail()
        {
            ItemAttribute = new ItemAttribute();
        }

        public bool InsertRecord()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "INSERT INTO item_details(attribute_id,detail_description,item_id) ";
            sql += "VALUES(@attribute_id,@description,@itemId)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@attribute_id", ItemAttribute.AttributeId);
            cmd.Parameters.AddWithValue("@description", DetailDescription);
            cmd.Parameters.AddWithValue("@itemId", ItemId);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }
        public bool UpdateRecord()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "UPDATE item_details SET attribute_id = @attribute_id, detail_description = @desc ";
            sql += "WHERE detail_id = @id ";
            sql += "LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", DetailId);
            cmd.Parameters.AddWithValue("@attribute_id", ItemAttribute.AttributeId);
            cmd.Parameters.AddWithValue("@desc", DetailDescription);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }
        public List<ItemDetail> Retrieve(string itemId)
        {
            Global global = new Global();
            List<ItemDetail> listItemDetail = new List<ItemDetail>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM item_details ";
            sql += "WHERE item_id = @itemId";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@itemId",itemId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ItemDetail itemDetail = new ItemDetail();
                    itemDetail.DetailId = dr.GetInt32("detail_id");
                    itemDetail.ItemAttribute = new ItemAttribute().Retrieve(dr.GetInt32("attribute_id"));
                    itemDetail.DetailDescription = dr.GetString("detail_description");
                    listItemDetail.Add(itemDetail);
                }
            }
            conn.Close();
            return listItemDetail;
        }
        public ItemDetail Retrieve(int detailId)
        {
            Global global = new Global();
            ItemDetail itemDetail = new ItemDetail();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM item_details ";
            sql += "WHERE detail_id = @detailId";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@detailId", detailId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                itemDetail.ItemId = dr.GetString("item_id");
                itemDetail.DetailId = dr.GetInt32("detail_id");
                itemDetail.ItemAttribute = new ItemAttribute().Retrieve(dr.GetInt32("attribute_id"));
                itemDetail.DetailDescription = dr.GetString("detail_description");
            }
            conn.Close();
            return itemDetail;
        }


    }
}
