using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class Customer
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public DateTime RecordDate { get; set; }

        public string GenerateID()
        {
            Global global = new Global();
            DateTime now = DateTime.Now;
            
            
            string newID = now.ToString("yy") + now.ToString("MM") + "0001";
            string sql = "SELECT customer_id FROM customers WHERE customer_id LIKE @id ";
            sql = sql + "ORDER BY customer_id DESC Limit 1";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", now.ToString("yy") + now.ToString("MM") + "%");
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            
          
            if (dr.HasRows)
            {
                dr.Read();
                newID = dr.GetString("customer_id").Substring(4);
                newID = (Convert.ToInt32(newID) + 1).ToString(now.ToString("yy") + now.ToString("MM") + "0000");
            }
            conn.Close();
            return newID;
        }

        public string GetFullName()
        {
            return LastName + ", " + FirstName;
        }

        public bool InsertRecord()
        {
            try
            {
                Global global = new Global();
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                string sql = "INSERT INTO customers(customer_id,first_name,last_name,address,contact_no,record_date) ";
                sql += "VALUES(@customerId,@fname,@lname,@add,@contact,@date) ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@customerId", CustomerId);
                cmd.Parameters.AddWithValue("@fname", FirstName);
                cmd.Parameters.AddWithValue("@lname", LastName);
                cmd.Parameters.AddWithValue("@add", Address);
                cmd.Parameters.AddWithValue("@contact", ContactNo);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Message.ShowSuccess("Customer Saved Successfully.","Message");
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
            }
            return true;
        }
        public List<Customer> Retrieve()
        {
            Global global = new Global();
            List<Customer> listCustomer = new List<Customer>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM customers";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Customer customer = new Customer();
                    customer.CustomerId = dr.GetString("customer_id");
                    customer.FirstName = dr.GetString("first_name");
                    customer.LastName = dr.GetString("last_name");
                    customer.Address = dr.GetString("address");
                    customer.ContactNo = dr.GetString("contact_no");
                    customer.RecordDate = dr.GetDateTime("record_date");
                    listCustomer.Add(customer);
                }
            }
            conn.Close();
            return listCustomer;
        }
        public Customer Retrieve(string customerId)
        {
            Global global = new Global();
            Customer customer = new Customer();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM customers WHERE customer_id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", customerId);
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
                customer.RecordDate = dr.GetDateTime("record_date");
            }
            conn.Close();
            return customer;
        }

        public List<Customer> Search(string keyword)
        {
            List<Customer> listCustomer = new List<Customer>();
            try
            {
                Global global = new Global();
                string sql = "SELECT * FROM customers WHERE first_name LIKE @keyword OR last_name LIKE @keyword ";
                sql += "OR customer_id = @id";
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                cmd.Parameters.AddWithValue("@id", keyword);
                conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerId = dr.GetString("customer_id");
                        customer.FirstName = dr.GetString("first_name");
                        customer.LastName = dr.GetString("last_name");
                        customer.Address = dr.GetString("address");
                        customer.ContactNo = dr.GetString("contact_no");
                        customer.RecordDate = dr.GetDateTime("record_date");
                        listCustomer.Add(customer);
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Ocurred: " + ex.Message, "Error");
            }
            return listCustomer;
        }
        public bool UpdateRecord()
        {
            try
            {
                Global global = new Global();
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                string sql = "UPDATE customers SET first_name = @fname, last_name = @lname, address = @add, contact_no = @contact ";
                sql += "WHERE customer_id = @customerId";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@customerId", CustomerId);
                cmd.Parameters.AddWithValue("@fname", FirstName);
                cmd.Parameters.AddWithValue("@lname", LastName);
                cmd.Parameters.AddWithValue("@add", Address);
                cmd.Parameters.AddWithValue("@contact", ContactNo);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Message.ShowSuccess("Customer Updated Successfully.", "Message");
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
            }
            return true;
        }

        public string Validate()
        {
            string errors = "";
            Validation validation = new Validation();

            if (validation.Is_Blank(FirstName))
            {
                errors += "First Name cannot be blank.\n";
            }
            else if (validation.HasLengthGreaterThan(FirstName, 35))
            {
                errors += "First Name character max length is 35.\n";
            }

            if (validation.Is_Blank(LastName))
            {
                errors += "Last Name cannot be blank.\n";
            }
            else if (validation.HasLengthGreaterThan(LastName, 35))
            {
                errors += "Last Name character max length is 35.\n";
            }

            if (validation.Is_Blank(Address))
            {
                errors += "Address cannot be blank.\n";
            }
            else if (validation.HasLengthGreaterThan(Address, 100))
            {
                errors += "Address character max length is 100.\n";
            }

            if (!validation.Is_Blank(ContactNo))
            {
                if (!validation.HasLength(ContactNo, 11))
                {
                    errors += "Please enter a valid Contact No.\n";
                }
            }

            return errors;
        }
    }
}
