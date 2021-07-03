using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class DeliveryStaff
    {
        public Delivery Delivery { get; set; }
        public Staff Staff { get; set; }
        public string Role { get; set; }

        public List<DeliveryStaff> Retrieve(int id)
        {
            Global global = new Global();
            List<DeliveryStaff> listDeliveryStaff = new List<DeliveryStaff>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM delivery_staff WHERE delivery_id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DeliveryStaff deliveryStaff = new DeliveryStaff();
                    deliveryStaff.Staff = new Staff().Retrieve(dr.GetString("staff_id"));
                    deliveryStaff.Role = dr.GetString("role");
                    listDeliveryStaff.Add(deliveryStaff);
                }
            }
            conn.Close();
            return listDeliveryStaff;
        }
    }
}
