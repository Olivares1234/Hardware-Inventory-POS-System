using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;


namespace Hardware_MS
{
    class Supplier
    {
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }


        public bool InsertRecord()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "INSERT INTO suppliers(company_name,address,contact_no) VALUES(@companyName,@address,@contactNo)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@companyName", CompanyName);
            cmd.Parameters.AddWithValue("@address", Address);
            cmd.Parameters.AddWithValue("@contactNo",ContactNo);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }
        public List<Supplier> Retrieve()
        {
            Global global = new Global();
            List<Supplier> listSupplier = new List<Supplier>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM suppliers";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Supplier supplier = new Supplier();
                    supplier.SupplierID = dr.GetInt32("supplier_id");
                    supplier.CompanyName = dr.GetString("company_name");
                    supplier.Address = dr.GetString("address");
                    supplier.ContactNo = dr.GetString("contact_no");
                    listSupplier.Add(supplier);
                }
            }
            conn.Close();
            return listSupplier;
        }
        public Supplier Retrieve(int id)
        {
            Global global = new Global();
            Supplier supplier = new Supplier();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM suppliers WHERE supplier_id = @id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                supplier.SupplierID = dr.GetInt32("supplier_id");
                supplier.CompanyName = dr.GetString("company_name");
                supplier.Address = dr.GetString("address");
                supplier.ContactNo = dr.GetString("contact_no");
            }
            conn.Close();
            return supplier;
        }
        public List<Supplier> Retrieve(string keyword)
        {
            Global global = new Global();
            List<Supplier> listSupplier = new List<Supplier>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM suppliers WHERE company_name LIKE @keyword OR supplier_id = @id ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
            cmd.Parameters.AddWithValue("@id", keyword);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Supplier supplier = new Supplier();
                    supplier.SupplierID = dr.GetInt32("supplier_id");
                    supplier.CompanyName = dr.GetString("company_name");
                    supplier.Address = dr.GetString("address");
                    supplier.ContactNo = dr.GetString("contact_no");
                    listSupplier.Add(supplier);
                }
            }
            conn.Close();
            return listSupplier;
        }
        public bool UpdateRecord()
        {
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "UPDATE suppliers SET ";
            sql += "company_name = @companyName, ";
            sql += "address = @address, ";
            sql += "contact_no = @contactNo ";
            sql += "WHERE supplier_id = @supplierId ";
            sql += "LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@supplierId", SupplierID);
            cmd.Parameters.AddWithValue("@companyName", CompanyName);
            cmd.Parameters.AddWithValue("@address", Address);
            cmd.Parameters.AddWithValue("@contactNo", ContactNo);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }
        public int CountSuppliers()
        {
            Global global = new Global();
            int count = 0;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT COUNT(supplier_id) AS 'count' FROM suppliers";
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
            string errors = "";
            Validation validation = new Validation();
            if (validation.Is_Blank(CompanyName))
            {
                errors += "Company name cannot be blank.\n";
            }
            else if (!validation.HasLengthBetween(CompanyName, 2, 45))
            {
                errors += "Company name characters length must be between 2 and 45.\n";
            }

            if (validation.Is_Blank(Address))
            {
                errors += "Address cannot be blank.\n";
            }
            else if (validation.HasLengthGreaterThan(Address, 100))
            {
                errors += "Address characters length limit is 100.\n";
            }

            if (validation.Is_Blank(ContactNo))
            {
                errors += "Contact number cannot be blank.\n";
            }
            else if (validation.HasLengthGreaterThan(ContactNo, 20))
            {
                errors += "Contact characters length limit is 20.\n";
            }
            return errors;
        }


    }
}

