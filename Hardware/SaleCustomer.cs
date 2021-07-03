using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class SaleCustomer
    {
        public int SaleCustomerId { get; set; }
        public Sale Sale { get; set; }
        public Customer Customer { get; set; }

        public SaleCustomer()
        {
            Sale = new Sale();
            Customer = new Customer();
        }
               
        public bool InsertRecord()
        {
            try
            {
                Global global = new Global();
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                
                string sql = "INSERT INTO sales_customer(customer_id,sale_id) ";
                sql += "VALUES(@cust_id, @sale_id)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cust_id",Customer.CustomerId);
                cmd.Parameters.AddWithValue("@sale_id",Sale.SaleId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
            }
            return true;
        }

        public Customer GetCustomer(string saleId)
        {
            Global global = new Global();
            Customer customer = new Customer();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT customers.customer_id,first_name,last_name,address,contact_no FROM sales_customer INNER JOIN customers ";
            sql += "ON sales_customer.customer_id = customers.customer_id WHERE sales_customer.sale_id = @sale_id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@sale_id", saleId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                customer.CustomerId = dr.GetString("customer_id");
                customer.FirstName = dr.GetString("first_name");
                customer.LastName = dr.GetString("last_name");
                customer.Address = dr.GetString("address");
                customer.ContactNo = dr.GetString("contact_no");
            }
            conn.Close();
            return customer;
        }
    }
}
