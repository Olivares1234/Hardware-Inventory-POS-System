using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class Return
    {
        public string ReturnId { get; set; }
        public DateTime DateTime { get; set; }
        public Staff Staff { get; set; }
        public decimal TotalAmountReturned { get; set; }
        public string OrRefNumber { get; set; }
        public List<ItemReturn> ListItemReturn { get; set; }

        public Return()
        {
            Staff = new Staff();
            ListItemReturn = new List<ItemReturn>();
        }

        public string GenerateID()
        {
            Global global = new Global();
            DateTime now = DateTime.Now;
            string newID = now.ToString("yy") + now.ToString("MM") + "0001";
            string sql = "SELECT return_id FROM returns WHERE return_id LIKE @id ";
            sql = sql + "ORDER BY return_id DESC Limit 1";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", now.ToString("yy") + now.ToString("MM") + "%");
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                newID = dr.GetString("return_id").Substring(4);
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
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {             
                string sql = "INSERT INTO returns(return_id,date_time,staff_id,total_amount_returned,or_number_reference) ";
                sql += "VALUES(@returnId,NOW(),@staffId,@total,@or) ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@returnId",ReturnId);
                cmd.Parameters.AddWithValue("@staffId", Staff.StaffId);
                cmd.Parameters.AddWithValue("@total", TotalAmountReturned);
                cmd.Parameters.AddWithValue("@or", OrRefNumber);
                cmd.ExecuteNonQuery();
                for (int i = 0; i < ListItemReturn.Count; i++)
                {
                    sql = "INSERT INTO item_return(item_id,quantity,reason_for_return,item_condition,return_amount,return_id,item_price) ";
                    sql += "VALUES(@itemId,@qty,@reason,@condition,@returnAmt,@returnId,@itemPrice) ";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@itemId",ListItemReturn[i].Item.ItemId);
                    cmd.Parameters.AddWithValue("@qty", ListItemReturn[i].Quantity);
                    cmd.Parameters.AddWithValue("@reason", ListItemReturn[i].ReasonForReturn);
                    cmd.Parameters.AddWithValue("@condition", ListItemReturn[i].ItemCondition);
                    cmd.Parameters.AddWithValue("@returnAmt", ListItemReturn[i].ReturnAmount);
                    cmd.Parameters.AddWithValue("@returnId", ListItemReturn[i].ReturnId);
                    cmd.Parameters.AddWithValue("@itemPrice", ListItemReturn[i].ItemPrice);
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                conn.Close();
                Message.ShowSuccess("Sale Return Recorded Successfully", "Success");

            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
                trans.Rollback();
                return false;
            }
            return true;
        }
        public bool CheckIfAlreadyReturned(string orNo)
        {
            Global global = new Global();
            bool existing = false;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT or_number_reference FROM returns WHERE or_number_reference = @orNo";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@orNo", orNo);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                existing = true;
            }
            conn.Close();
            return existing;
        }
        public List<Return> Retrieve()
        {
            Global global = new Global();
            List<Return> listReturn = new List<Return>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM returns ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Return saleReturn = new Return();
                    saleReturn.ReturnId = dr.GetString("return_id");
                    saleReturn.DateTime = dr.GetDateTime("date_time");
                    saleReturn.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    saleReturn.TotalAmountReturned = dr.GetDecimal("total_amount_returned");
                    saleReturn.OrRefNumber = dr.GetString("or_number_reference");
                    saleReturn.ListItemReturn = new ItemReturn().Retrieve(saleReturn.ReturnId);
                    listReturn.Add(saleReturn);
                }
            }
            conn.Close();
            return listReturn;
        }
        public Return Retrieve(string returnId)
        {
            Global global = new Global();
            Return saleReturn = new Return();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM returns WHERE return_id =  @id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id",returnId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    
                    saleReturn.ReturnId = dr.GetString("return_id");
                    saleReturn.DateTime = dr.GetDateTime("date_time");
                    saleReturn.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    saleReturn.TotalAmountReturned = dr.GetDecimal("total_amount_returned");
                    saleReturn.OrRefNumber = dr.GetString("or_number_reference");
                    saleReturn.ListItemReturn = new ItemReturn().Retrieve(saleReturn.ReturnId);
                }
            }
            conn.Close();
            return saleReturn;
        }
        public double ComputeTotalQty()
        {
            double total = 0;
            for (int i = 0; i < ListItemReturn.Count; i++)
            {
                total += ListItemReturn[i].Quantity;
            }
            return total;
        }
        public List<Return> Retrieve(DateTime date)
        {
            Global global = new Global();
            List<Return> listReturn = new List<Return>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM returns WHERE date_time LIKE @date ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@date",date.ToString("yyyy-MM-dd") + "%");
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Return saleReturn = new Return();
                    saleReturn.ReturnId = dr.GetString("return_id");
                    saleReturn.DateTime = dr.GetDateTime("date_time");
                    saleReturn.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    saleReturn.TotalAmountReturned = dr.GetDecimal("total_amount_returned");
                    saleReturn.OrRefNumber = dr.GetString("or_number_reference");
                    saleReturn.ListItemReturn = new ItemReturn().Retrieve(saleReturn.ReturnId);
                    listReturn.Add(saleReturn);
                }
            }
            conn.Close();
            return listReturn;
        }
        public List<Return> Retrieve(DateTime dateFrom, DateTime dateTo)
        {
            Global global = new Global();
            List<Return> listReturn = new List<Return>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM returns WHERE date_time BETWEEN @dateFrom AND @dateTo ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            //cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd") + "%");
            cmd.Parameters.AddWithValue("@dateFrom", dateFrom.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@dateTo", dateTo.ToString("yyyy-MM-dd") + " 23:59");
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Return saleReturn = new Return();
                    saleReturn.ReturnId = dr.GetString("return_id");
                    saleReturn.DateTime = dr.GetDateTime("date_time");
                    saleReturn.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    saleReturn.TotalAmountReturned = dr.GetDecimal("total_amount_returned");
                    saleReturn.OrRefNumber = dr.GetString("or_number_reference");
                    saleReturn.ListItemReturn = new ItemReturn().Retrieve(saleReturn.ReturnId);
                    listReturn.Add(saleReturn);
                }
            }
            conn.Close();
            return listReturn;
        }
        public List<Return> RetrieveByStaff(string staffId)
        {
            Global global = new Global();
            List<Return> listReturn = new List<Return>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM returns WHERE staff_id = @staff ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@staff", staffId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Return saleReturn = new Return();
                    saleReturn.ReturnId = dr.GetString("return_id");
                    saleReturn.DateTime = dr.GetDateTime("date_time");
                    saleReturn.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    saleReturn.TotalAmountReturned = dr.GetDecimal("total_amount_returned");
                    saleReturn.OrRefNumber = dr.GetString("or_number_reference");
                    saleReturn.ListItemReturn = new ItemReturn().Retrieve(saleReturn.ReturnId);
                    listReturn.Add(saleReturn);
                }
            }
            conn.Close();
            return listReturn;
        }
        public int Count()
        {
            Global global = new Global();
            int count = 0;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT COUNT(return_id) AS 'count' FROM returns";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            count = dr.GetInt32("count");
            conn.Close();
            return count;
        }
        public int Count(DateTime date)
        {
            Global global = new Global();
            int count = 0;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT COUNT(return_id) AS 'count' FROM returns WHERE date_time LIKE @date";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@date",date.ToString("yyyy-MM-dd") + "%");
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            count = dr.GetInt32("count");
            conn.Close();
            return count;
        }

    }
}
