using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class Sale
    {
        public string SaleId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public Staff Staff { get; set; }
        public string OrNo { get; set; }
        public decimal VATable { get; set; }
        public decimal VAT_tax { get; set; }
        public List<SaleDetail> SaleDetails { get; set; }

        public Sale()
        {
            Staff = new Staff();
            SaleDetails = new List<SaleDetail>();
        }

        public string GenerateID()
        {
            Global global = new Global();
            DateTime now = DateTime.Now;
            string newID = now.ToString("yy") + now.ToString("MM") + "0001";
            string sql = "SELECT sale_id FROM sales WHERE sale_id LIKE @id ";
            sql = sql + "ORDER BY sale_id DESC Limit 1";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", now.ToString("yy") + now.ToString("MM") + "%");
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                newID = dr.GetString("sale_id").Substring(4);
                newID = (Convert.ToInt32(newID) + 1).ToString(now.ToString("yy") + now.ToString("MM") + "0000");
            }
            conn.Close();
            return newID;
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
                //insert into sales table
                sql = "INSERT INTO sales(sale_id,date_time,total_price,staff_id,or_number,vatable,vat_tax) ";
                sql += "VALUES(@sale_id,@dt,@total,@staff_id,@or_no,@vatable,@vat_tax) ";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@sale_id",SaleId);
                cmd.Parameters.AddWithValue("@dt",DateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@total",TotalPrice);
                cmd.Parameters.AddWithValue("@staff_id",Staff.StaffId);
                cmd.Parameters.AddWithValue("@or_no", OrNo);
                cmd.Parameters.AddWithValue("@vatable", VATable);
                cmd.Parameters.AddWithValue("@vat_tax", VAT_tax);
                cmd.ExecuteNonQuery();
                //insert sale details
                for (int i = 0; i < SaleDetails.Count; i++)
                {
                    sql = "INSERT INTO sale_details(sale_id,item_id,price,quantity,discount_percentage,is_vatable) ";
                    sql += "VALUES(@sale_id,@item_id,@price,@qty,@discount,@vatable) ";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@sale_id",SaleId);
                    cmd.Parameters.AddWithValue("@item_id",SaleDetails[i].Item.ItemId);
                    cmd.Parameters.AddWithValue("@price",SaleDetails[i].Item.GetPriceWithVat());
                    cmd.Parameters.AddWithValue("@qty", SaleDetails[i].Quantity);
                    cmd.Parameters.AddWithValue("@discount",SaleDetails[i].DiscountPercentage);
                    cmd.Parameters.AddWithValue("@vatable",SaleDetails[i].IsVatable);
                    cmd.ExecuteNonQuery();
                    //decrease quantity stocks
                    sql = "UPDATE items SET quantity = quantity - @qty ";
                    sql += "WHERE item_id = @item_id";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }


                trans.Commit();
                conn.Close();
                Message.ShowSuccess("Transaction saved successfully.","Message");
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
            }
            return true;
        }
        public double ComputeTotalQty()
        {
            double total = 0;
            for (int i = 0; i < SaleDetails.Count; i++)
            {
                total += SaleDetails[i].Quantity;
            }
            return total;
        }
        public Sale Retrieve(string id)
        {
            Global global = new Global();
            Sale sale = new Sale();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM sales WHERE sale_id = @id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sale.SaleId = dr.GetString("sale_id");
                sale.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                sale.TotalPrice = dr.GetDecimal("total_price");
                sale.DateTime = dr.GetDateTime("date_time");
                sale.OrNo = dr.GetString("or_number");
                sale.VATable = dr.GetDecimal("vatable");
                sale.VAT_tax = dr.GetDecimal("vat_tax");
                sale.SaleDetails = new SaleDetail().Retrieve(sale.SaleId);
            }
            conn.Close();
            return sale;
        }
        public Sale RetrieveByORNo(string orNo)
        {
            Global global = new Global();
            Sale sale = new Sale();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM sales WHERE or_number = @orNo ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@orNo", orNo);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sale.SaleId = dr.GetString("sale_id");
                sale.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                sale.TotalPrice = dr.GetDecimal("total_price");
                sale.DateTime = dr.GetDateTime("date_time");
                sale.OrNo = dr.GetString("or_number");
                sale.VATable = dr.GetDecimal("vatable");
                sale.VAT_tax = dr.GetDecimal("vat_tax");
                sale.SaleDetails = new SaleDetail().Retrieve(sale.SaleId);
            }
            conn.Close();
            return sale;
        }

        public bool CheckIfExisting(string OrNo)
        {
            Global global = new Global();
            bool existing = false;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT sale_id FROM sales WHERE or_number = @orNo";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@orNo", OrNo);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                existing = true;
            }
            conn.Close();
            return existing;
        }

        public List<Sale> Retrieve()
        {
            Global global = new Global();
            List<Sale> listSale = new List<Sale>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM sales ORDER BY date_time DESC ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Sale sale = new Sale();
                    sale.SaleId = dr.GetString("sale_id");
                    sale.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    sale.TotalPrice = dr.GetDecimal("total_price");
                    sale.DateTime = dr.GetDateTime("date_time");
                    sale.OrNo = dr.GetString("or_number");
                    sale.VATable = dr.GetDecimal("vatable");
                    sale.VAT_tax = dr.GetDecimal("vat_tax");
                    sale.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    sale.SaleDetails = new SaleDetail().Retrieve(sale.SaleId);
                    listSale.Add(sale);
                }
            }
            conn.Close();
            return listSale;
        }

        public List<Sale> Retrieve(DateTime dateOfSale)
        {
            Global global = new Global();
            List<Sale> listSale = new List<Sale>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM sales WHERE date_time LIKE @date ORDER BY date_time DESC";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@date", dateOfSale.ToString("yyyy-MM-dd") + "%");
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Sale sale = new Sale();
                    sale.SaleId = dr.GetString("sale_id");
                    sale.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    sale.TotalPrice = dr.GetDecimal("total_price");
                    sale.DateTime = dr.GetDateTime("date_time");
                    sale.OrNo = dr.GetString("or_number");
                    sale.VATable = dr.GetDecimal("vatable");
                    sale.VAT_tax = dr.GetDecimal("vat_tax");
                    sale.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    sale.SaleDetails = new SaleDetail().Retrieve(sale.SaleId);
                    listSale.Add(sale);
                }
            }
            conn.Close();
            return listSale;
        }
        public decimal ComputeTotalSalesToday()
        {
            List<Sale> listSale = new Sale().Retrieve(DateTime.Now);
            decimal total = 0;
            for (int i = 0; i < listSale.Count; i++)
            {
                total += listSale[i].TotalPrice;
            }
            return total;
        }
        public List<Sale> Retrieve(DateTime dateFrom,DateTime dateTo)
        {
            Global global = new Global();
            List<Sale> listSale = new List<Sale>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM sales WHERE date_time BETWEEN @dateFrom AND @dateTo ORDER BY date_time DESC";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@dateFrom", dateFrom.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@dateTo", dateTo.ToString("yyyy-MM-dd") + " 23:59");
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Sale sale = new Sale();
                    sale.SaleId = dr.GetString("sale_id");
                    sale.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    sale.TotalPrice = dr.GetDecimal("total_price");
                    sale.DateTime = dr.GetDateTime("date_time");
                    sale.OrNo = dr.GetString("or_number");
                    sale.VATable = dr.GetDecimal("vatable");
                    sale.VAT_tax = dr.GetDecimal("vat_tax");
                    sale.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    sale.SaleDetails = new SaleDetail().Retrieve(sale.SaleId);
                    listSale.Add(sale);
                }
            }
            conn.Close();
            return listSale;
        }
        public List<Sale> RetrieveByStaff(string staffId)
        {
            Global global = new Global();
            List<Sale> listSale = new List<Sale>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM sales WHERE staff_id = @staffId ORDER BY date_time DESC";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@staffId", staffId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Sale sale = new Sale();
                    sale.SaleId = dr.GetString("sale_id");
                    sale.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    sale.TotalPrice = dr.GetDecimal("total_price");
                    sale.DateTime = dr.GetDateTime("date_time");
                    sale.OrNo = dr.GetString("or_number");
                    sale.VATable = dr.GetDecimal("vatable");
                    sale.VAT_tax = dr.GetDecimal("vat_tax");
                    sale.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    sale.SaleDetails = new SaleDetail().Retrieve(sale.SaleId);
                    listSale.Add(sale);
                }
            }
            conn.Close();
            return listSale;
        }

        public int CountSales()
        {
            Global global = new Global();
            int count = 0;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT COUNT(sale_id) AS 'count' FROM sales";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            count = dr.GetInt32("count");
            conn.Close();
            return count;
        }
        public int CountSales(DateTime dateOfSale)
        {
            Global global = new Global();
            int count = 0;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT COUNT(sale_id) AS 'count' FROM sales WHERE date_time LIKE @date";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@date", dateOfSale.ToString("yyyy-MM-dd") + "%");
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            count = dr.GetInt32("count");
            conn.Close();
            return count;
        }
    }
}
