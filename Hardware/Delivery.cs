using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class Delivery
    {
        public int DeliveryId { get; set; }
        public Sale Sale { get; set; }
        public DeliveryStatus Status { get; set; }
        public Truck Truck { get; set; }
        public Staff Staff { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }      
        public decimal Fee { get; set; }
        public DateTime DateRecorded { get; set; }
        //staffs
        public List<DeliveryStaff> ListDeliveryStaff { get; set; }

        public Delivery()
        {
            Sale = new Sale();
            Status = new DeliveryStatus();
            Truck = new Truck();
            Staff = new Staff();
            ListDeliveryStaff = new List<DeliveryStaff>();
        }

        public int GenerateID()
        {
            Global global = new Global();
            
            string sql = "SELECT delivery_id FROM deliveries ORDER BY delivery_id DESC LIMIT 1";
            int newID = 1;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                newID = dr.GetInt32("delivery_id") + 1;
            }
            conn.Close();
            return newID;
        }

        public bool InsertRecord()
        {
            Global global = new Global();
            DeliveryId = GenerateID();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            conn.Open();
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {
                string sql = "INSERT INTO deliveries(delivery_id,sale_id,delivery_address,date,time_start,time_end,delivery_fee,truck_id,staff_id,date_recorded) ";
                sql += "VALUES(@delivery_id,@sale_id,@delivery_address,@date,@time_start,@time_end,@fee,@truck_id,@staff_id,@date_recorded) ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@delivery_id",DeliveryId);
                cmd.Parameters.AddWithValue("@sale_id",Sale.SaleId);
                cmd.Parameters.AddWithValue("@delivery_address", DeliveryAddress);
                cmd.Parameters.AddWithValue("@date", Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@time_start",TimeStart.ToString("HH:mm"));
                cmd.Parameters.AddWithValue("@time_end", TimeEnd.ToString("HH:mm"));
                //cmd.Parameters.AddWithValue("@status", TimeEnd.ToString("hh:mm"));
                cmd.Parameters.AddWithValue("@fee", Fee);
                cmd.Parameters.AddWithValue("@truck_id", Truck.TruckId);
                cmd.Parameters.AddWithValue("@staff_id",Staff.StaffId);
                cmd.Parameters.AddWithValue("@date_recorded", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.ExecuteNonQuery();
                for (int i = 0; i < ListDeliveryStaff.Count; i++)
                {
                    sql = "INSERT INTO delivery_staff(staff_id,delivery_id,role) ";
                    sql += "VALUES(@staff_id,@delivery_id,@role) ";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@staff_id",ListDeliveryStaff[i].Staff.StaffId);
                    cmd.Parameters.AddWithValue("@delivery_id",DeliveryId);
                    cmd.Parameters.AddWithValue("@role", ListDeliveryStaff[i].Staff.Position.Description);
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                conn.Close();
                Message.ShowSuccess("Delivery Recorded Successfully", "Success");

            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
                trans.Rollback();
                return false;
            }
            return true;
        }
        public List<Delivery> Retrieve()
        {
            Global global = new Global();
            List<Delivery> listDelivery = new List<Delivery>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM deliveries ORDER BY date DESC ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Delivery delivery = new Delivery();
                    delivery.DeliveryId = dr.GetInt32("delivery_id");
                    delivery.Sale = new Sale().Retrieve(dr.GetString("sale_id"));
                    delivery.DeliveryAddress = dr.GetString("delivery_address");
                    delivery.Date = dr.GetDateTime("date");
                    delivery.TimeStart = DateTime.Parse(dr.GetString("time_start"));
                    delivery.TimeEnd = DateTime.Parse(dr.GetString("time_end"));
                    delivery.Status = new DeliveryStatus().Retrieve(dr.GetInt32("status_id"));
                    delivery.Fee = dr.GetDecimal("delivery_fee");
                    delivery.Truck = new Truck().Retrieve(dr.GetInt32("truck_id"));
                    delivery.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    delivery.DateRecorded = dr.GetDateTime("date_recorded");
                    delivery.ListDeliveryStaff = new DeliveryStaff().Retrieve(delivery.DeliveryId);
                    listDelivery.Add(delivery);
                }
            }
            conn.Close();
            return listDelivery;
        }
        public List<Delivery> Retrieve(DateTime date)
        {
            Global global = new Global();
            List<Delivery> listDelivery = new List<Delivery>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM deliveries WHERE date = @date ORDER BY date DESC ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@date",date.ToString("yyyy-MM-dd"));
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Delivery delivery = new Delivery();
                    delivery.DeliveryId = dr.GetInt32("delivery_id");
                    delivery.Sale = new Sale().Retrieve(dr.GetString("sale_id"));
                    delivery.DeliveryAddress = dr.GetString("delivery_address");
                    delivery.Date = dr.GetDateTime("date");
                    delivery.TimeStart = DateTime.Parse(dr.GetString("time_start"));
                    delivery.TimeEnd = DateTime.Parse(dr.GetString("time_end"));
                    delivery.Status = new DeliveryStatus().Retrieve(dr.GetInt32("status_id"));
                    delivery.Fee = dr.GetDecimal("delivery_fee");
                    delivery.Truck = new Truck().Retrieve(dr.GetInt32("truck_id"));
                    delivery.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    delivery.DateRecorded = dr.GetDateTime("date_recorded");
                    delivery.ListDeliveryStaff = new DeliveryStaff().Retrieve(delivery.DeliveryId);
                    listDelivery.Add(delivery);
                }
            }
            conn.Close();
            return listDelivery;
        }
        public Delivery Retrieve(int deliveryId)
        {
            Global global = new Global();
            Delivery delivery = new Delivery();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM deliveries WHERE delivery_id = @id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id",deliveryId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                delivery.DeliveryId = dr.GetInt32("delivery_id");
                delivery.Sale = new Sale().Retrieve(dr.GetString("sale_id"));
                delivery.DeliveryAddress = dr.GetString("delivery_address");
                delivery.Date = dr.GetDateTime("date");
                delivery.TimeStart = DateTime.Parse(dr.GetString("time_start"));
                delivery.TimeEnd = DateTime.Parse(dr.GetString("time_end"));
                delivery.Status = new DeliveryStatus().Retrieve(dr.GetInt32("status_id"));
                delivery.Fee = dr.GetDecimal("delivery_fee");
                delivery.Truck = new Truck().Retrieve(dr.GetInt32("truck_id"));
                delivery.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                delivery.DateRecorded = dr.GetDateTime("date_recorded");
                delivery.ListDeliveryStaff = new DeliveryStaff().Retrieve(delivery.DeliveryId);
            }
            conn.Close();
            return delivery;
        }
        public decimal GetDeliveryFee(string saleId)
        {
            Global global = new Global();
            decimal fee = 0;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT delivery_fee FROM deliveries WHERE sale_id = @id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", saleId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                fee = dr.GetDecimal("delivery_fee");
            }
            conn.Close();
            return fee;
        }
        public bool UpdateStatus(int id,int status)
        {
            
            try
            {
                Global global = new Global();
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                conn.Open();
                string sql = "UPDATE deliveries SET status_id = @status WHERE delivery_id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters.AddWithValue("@status",status);
                cmd.ExecuteNonQuery();
                conn.Close();
                Message.ShowSuccess("Delivery Status Updated Successfully", "Success");

            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
                return false;
            }
            return true;
        }
        public int CountDelivery(DateTime date)
        {
            Global global = new Global();
            int count = 0;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT COUNT(delivery_id) AS 'count' FROM deliveries WHERE date = @date ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@date",date.ToString("yyyy-MM-dd"));
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                count = dr.GetInt32("count");
            }
            conn.Close();
            return count;
        }
    }
}
