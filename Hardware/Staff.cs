using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Hardware_MS
{
    class Staff
    {
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Position Position { get; set; }
        public string ContactNo { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public bool IsEmployed { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateHired { get; set; }

        public Staff()
        {
            Position = new Position();
        }

        public string GetFullName()
        {
            return LastName + ", " + FirstName + " " + MiddleName[0].ToString().ToUpper() + "."; 
        }
        public List<Staff> Retrieve()
        {
            Global global = new Global();
            List<Staff> listStaff = new List<Staff>();
            string sql = "SELECT * FROM Staffs ";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Staff staff = new Staff();
                    staff.StaffId = dr.GetString("staff_id");
                    staff.FirstName = dr.GetString("first_name");
                    staff.LastName = dr.GetString("last_name");
                    staff.MiddleName = dr.GetString("middle_name");
                    staff.Position = new Position().Retrieve(dr.GetInt32("position_id"));
                    staff.ContactNo = dr.GetString("contact_no");
                    staff.BirthDate = dr.GetDateTime("birthdate");
                    staff.Address = dr.GetString("address");
                    staff.Sex = ToSexString(dr.GetInt32("sex"));
                    staff.IsEmployed = dr.GetBoolean("is_employed");
                    staff.UserName = dr.GetString("username");
                    staff.Password = dr.GetString("password");
                    staff.DateHired = dr.GetDateTime("date_hired");
                    listStaff.Add(staff);
                }
            }
            conn.Close();
            return listStaff;          
        }
        public List<Staff> RetrieveByPosition(int position)
        {
            Global global = new Global();
            List<Staff> listStaff = new List<Staff>();
            string sql = "SELECT * FROM Staffs WHERE position_id = @pos ";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pos",position);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Staff staff = new Staff();
                    staff.StaffId = dr.GetString("staff_id");
                    staff.FirstName = dr.GetString("first_name");
                    staff.LastName = dr.GetString("last_name");
                    staff.MiddleName = dr.GetString("middle_name");
                    staff.Position = new Position().Retrieve(dr.GetInt32("position_id"));
                    staff.ContactNo = dr.GetString("contact_no");
                    staff.BirthDate = dr.GetDateTime("birthdate");
                    staff.Address = dr.GetString("address");
                    staff.Sex = ToSexString(dr.GetInt32("sex"));
                    staff.IsEmployed = dr.GetBoolean("is_employed");
                    staff.UserName = dr.GetString("username");
                    staff.Password = dr.GetString("password");
                    staff.DateHired = dr.GetDateTime("date_hired");
                    listStaff.Add(staff);
                }
            }
            conn.Close();
            return listStaff;
        }
        public List<Staff> RetrieveBySex(int sex)
        {
            Global global = new Global();
            List<Staff> listStaff = new List<Staff>();
            string sql = "SELECT * FROM Staffs WHERE sex = @sex ";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@sex", sex.ToString());
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Staff staff = new Staff();
                    staff.StaffId = dr.GetString("staff_id");
                    staff.FirstName = dr.GetString("first_name");
                    staff.LastName = dr.GetString("last_name");
                    staff.MiddleName = dr.GetString("middle_name");
                    staff.Position = new Position().Retrieve(dr.GetInt32("position_id"));
                    staff.ContactNo = dr.GetString("contact_no");
                    staff.BirthDate = dr.GetDateTime("birthdate");
                    staff.Address = dr.GetString("address");
                    staff.Sex = ToSexString(dr.GetInt32("sex"));
                    staff.IsEmployed = dr.GetBoolean("is_employed");
                    staff.UserName = dr.GetString("username");
                    staff.Password = dr.GetString("password");
                    staff.DateHired = dr.GetDateTime("date_hired");
                    listStaff.Add(staff);
                }
            }
            conn.Close();
            return listStaff;
        }
        public List<Staff> RetrieveByStatus(bool status)
        {
            Global global = new Global();
            List<Staff> listStaff = new List<Staff>();
            string sql = "SELECT * FROM Staffs WHERE is_employed = @status ";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@status", status);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Staff staff = new Staff();
                    staff.StaffId = dr.GetString("staff_id");
                    staff.FirstName = dr.GetString("first_name");
                    staff.LastName = dr.GetString("last_name");
                    staff.MiddleName = dr.GetString("middle_name");
                    staff.Position = new Position().Retrieve(dr.GetInt32("position_id"));
                    staff.ContactNo = dr.GetString("contact_no");
                    staff.BirthDate = dr.GetDateTime("birthdate");
                    staff.Address = dr.GetString("address");
                    staff.Sex = ToSexString(dr.GetInt32("sex"));
                    staff.IsEmployed = dr.GetBoolean("is_employed");
                    staff.UserName = dr.GetString("username");
                    staff.Password = dr.GetString("password");
                    staff.DateHired = dr.GetDateTime("date_hired");
                    listStaff.Add(staff);
                }
            }
            conn.Close();
            return listStaff;
        }
        public Staff Retrieve(string staffId)
        {
            Global global = new Global();
            Staff staff = new Staff();
            string sql = "SELECT * FROM Staffs WHERE staff_id = @staffId";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@staffId",staffId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                staff.StaffId = dr.GetString("staff_id");
                staff.FirstName = dr.GetString("first_name");
                staff.LastName = dr.GetString("last_name");
                staff.MiddleName = dr.GetString("middle_name");
                staff.Position = new Position().Retrieve(dr.GetInt32("position_id"));
                staff.ContactNo = dr.GetString("contact_no");
                staff.BirthDate = dr.GetDateTime("birthdate");
                staff.Address = dr.GetString("address");
                staff.Sex = ToSexString(dr.GetInt32("sex"));
                staff.IsEmployed = dr.GetBoolean("is_employed");
                staff.UserName = dr.GetString("username");
                staff.Password = dr.GetString("password");
                staff.DateHired = dr.GetDateTime("date_hired");
            }
            conn.Close();
            return staff;
        }
        public string RetrieveByName(string fullname)//returns the id of staff
        {
            Global global = new Global();
            string staff_id = "";
            string sql = "SELECT * FROM hardwaredb.Staffs WHERE CONCAT(last_name, ', ', first_name, ' ', SUBSTRING(middle_name,1,1), '.') = @staff";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@staff", fullname);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                staff_id = dr.GetString("staff_id");
            }
            conn.Close();
            return staff_id;
        }
        public List<Staff> RerieveAvailableStaff(DateTime date,DateTime start, DateTime end)
        {
            Global global = new Global();
            List<Staff> listStaff = new List<Staff>();
            string sql = "SELECT staffs.staff_id,first_name,last_name,middle_name,position_id FROM staffs ";
            sql += "WHERE staff_id NOT IN(SELECT delivery_staff.staff_id FROM delivery_staff INNER JOIN deliveries ";
            sql += "ON deliveries.delivery_id = delivery_staff.delivery_id ";
            sql += "WHERE ((deliveries.time_start BETWEEN @start AND @end) ";
            sql += "OR (deliveries.time_end BETWEEN @start AND @end)) ";
            sql += "AND deliveries.date = @date) ";
            sql += "AND position_id IN('3','6') AND is_employed = true";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@date",date.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@start",start.ToString("HH:mm"));
            cmd.Parameters.AddWithValue("@end", end.ToString("HH:mm"));
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Staff staff = new Staff();
                    staff.StaffId = dr.GetString("staff_id");
                    staff.FirstName = dr.GetString("first_name");
                    staff.LastName = dr.GetString("last_name");
                    staff.MiddleName = dr.GetString("middle_name");
                    staff.Position = new Position().Retrieve(dr.GetInt32("position_id"));
                    listStaff.Add(staff);
                }
            }
            conn.Close();
            return listStaff;  
        }
        public string ToSexString(int sexNum)
        {
            string result = "";
            if (sexNum == 0)
            {
                result = "Male";
            }
            else if (sexNum == 1)
            {
                result = "Female";
            }
            return result;
        }
        public int ToSexInt(string sexString)
        {
            int result = 0;
            if (sexString == "Male")
            {
                result = 0;
            }
            else if (sexString == "Female")
            {
                result = 1;
            }
            return result;
        }

        public string GenerateID()
        {
            Global global = new Global();
            DateTime now = DateTime.Now;
            string newID = now.ToString("yyyy") + now.ToString("MM") + "01";
            string sql = "SELECT staff_id FROM staffs WHERE staff_id LIKE @id ";
            sql = sql + "ORDER BY staff_id DESC Limit 1";
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", now.ToString("yyyy") + now.ToString("MM") + "%");
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                newID = dr.GetString("staff_id").Substring(6);
                newID = (Convert.ToInt32(newID) + 1).ToString(now.ToString("yyyy") + now.ToString("MM") + "00");
            }
            conn.Close();
            return newID;
        }
        public bool InsertRecord()
        {
            try
            {
                Global global = new Global();
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                string sql = "INSERT INTO staffs(staff_id,first_name,last_name,middle_name,position_id,contact_no,birthdate,sex,address,date_hired,username,password) ";
                sql += "VALUES(@id,@fn,@ln,@mn,@pos,@con,@bd,@sex,@add,@dh,@user,@pass) ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", StaffId);
                cmd.Parameters.AddWithValue("@fn", FirstName);
                cmd.Parameters.AddWithValue("@ln", LastName);
                cmd.Parameters.AddWithValue("@mn", MiddleName);
                cmd.Parameters.AddWithValue("@pos", Position.PositionId);
                cmd.Parameters.AddWithValue("@con", ContactNo);
                cmd.Parameters.AddWithValue("@bd", BirthDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@sex", ToSexInt(Sex));
                cmd.Parameters.AddWithValue("@add", Address);
                cmd.Parameters.AddWithValue("@dh", DateHired.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@user", UserName);
                cmd.Parameters.AddWithValue("@pass", Password);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Message.ShowSuccess("Staff Saved Successfully.","Message");
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error has Occured: " + ex.Message,"Error");
                return false;
            }
            return true;
        }
        public bool Login()
        {
            bool result = false;
            Global global = new Global();
            try
            {

                string sql = "SELECT staff_id FROM staffs WHERE BINARY username = @username AND BINARY password = @pass AND is_employed = true";
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", UserName);
                cmd.Parameters.AddWithValue("@pass", Password);
                conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    StaffId = dr.GetString("staff_id");
                    result = true;
                }
                conn.Close();              
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error has Occured: " + ex.Message,"Error");
            }
            return result;
        }
        public List<Staff> Search(string keyword)
        {
            List<Staff> listStaff = new List<Staff>();
            try
            {
                Global global = new Global();              
                string sql = "SELECT * FROM Staffs WHERE first_name LIKE @keyword OR last_name LIKE @keyword ";
                sql += "OR middle_name LIKE @keyword ";
                sql += "OR staff_id = @id";
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
                        Staff staff = new Staff();
                        staff.StaffId = dr.GetString("staff_id");
                        staff.FirstName = dr.GetString("first_name");
                        staff.LastName = dr.GetString("last_name");
                        staff.MiddleName = dr.GetString("middle_name");
                        staff.Position = new Position().Retrieve(dr.GetInt32("position_id"));
                        staff.ContactNo = dr.GetString("contact_no");
                        staff.BirthDate = dr.GetDateTime("birthdate");
                        staff.Address = dr.GetString("address");
                        staff.Sex = ToSexString(dr.GetInt32("sex"));
                        staff.IsEmployed = dr.GetBoolean("is_employed");
                        staff.UserName = dr.GetString("username");
                        staff.Password = dr.GetString("password");
                        staff.DateHired = dr.GetDateTime("date_hired");
                        listStaff.Add(staff);
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Ocurred: " + ex.Message, "Error");
            }
            return listStaff;
        }
        public bool UpdateRecord()
        {
            try
            {
                Global global = new Global();
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                //string sql = "INSERT INTO staffs(staff_id,first_name,last_name,middle_name,position_id,contact_no,birthdate,sex,address,date_hired,username,password) ";
                //sql += "VALUES(@id,@fn,@ln,@mn,@pos,@con,@bd,@sex,@add,@dh,@user,@pass) ";
                string sql = "UPDATE staffs SET ";
                sql += "first_name = @fn, ";
                sql += "last_name = @ln, ";
                sql += "middle_name = @mn, ";
                sql += "position_id = @pos, ";
                sql += "contact_no = @con, ";
                sql += "birthdate = @bd, ";
                sql += "sex = @sex, ";
                sql += "address = @add, ";
                sql += "date_hired = @dh, ";
                sql += "password = @pass, ";
                sql += "username = @user, ";
                sql += "is_employed = @employed ";
                sql += "WHERE staff_id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", StaffId);
                cmd.Parameters.AddWithValue("@fn", FirstName);
                cmd.Parameters.AddWithValue("@ln", LastName);
                cmd.Parameters.AddWithValue("@mn", MiddleName);
                cmd.Parameters.AddWithValue("@pos", Position.PositionId);
                cmd.Parameters.AddWithValue("@con", ContactNo);
                cmd.Parameters.AddWithValue("@bd", BirthDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@sex", ToSexInt(Sex));
                cmd.Parameters.AddWithValue("@add", Address);
                cmd.Parameters.AddWithValue("@dh", DateHired.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@user", UserName);
                cmd.Parameters.AddWithValue("@pass", Password);
                cmd.Parameters.AddWithValue("@employed",IsEmployed);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Message.ShowSuccess("Employee Updated Successfully.", "Message");
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error has Occured: " + ex.Message, "Error");
                return false;
            }
            return true;
        }

        public bool UpdateAccount()
        {
            try
            {
                Global global = new Global();
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                string sql = "UPDATE staffs SET ";
                sql += "password = @pass, ";
                sql += "username = @user ";
                sql += "WHERE staff_id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", StaffId);
                cmd.Parameters.AddWithValue("@user", UserName);
                cmd.Parameters.AddWithValue("@pass", Password);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Message.ShowSuccess("Account Updated Successfully.", "Message");
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error has Occured: " + ex.Message, "Error");
                return false;
            }
            return true;
        }
        public int Count()
        {
            Global global = new Global();
            int count = 0;
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT COUNT(staff_id) AS 'count' FROM staffs";
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
            Validation validation = new Validation();
            string errors = "";
            if (validation.Is_Blank(FirstName))
            {
                errors += "First Name isn't valid.\n";
            }
            else if (validation.HasLengthGreaterThan(FirstName, 35))
            {
                errors += "First Name characters length limit is 35.\n";
            }

            //lname
            if (validation.Is_Blank(LastName))
            {
                errors += "Last Name isn't valid.\n";
            }
            else if (validation.HasLengthGreaterThan(LastName, 35))
            {
                errors += "Last Name characters length limit is 35.\n";
            }
            //mname
            if (validation.Is_Blank(MiddleName))
            {
                errors += "Middle Name isn't valid.\n";
            }
            else if (validation.HasLengthGreaterThan(MiddleName, 35))
            {
                errors += "Middle Name characters length limit is 35.\n";
            }
            //contact no
            if (validation.Is_Blank(ContactNo))
            {
                errors += "Mobile number isn't valid.\n";
            }
            else if (!validation.HasLength(ContactNo,11))
            {
                errors += "Please enter a valid contact no.\n";
            }
            //address
            if (validation.Is_Blank(Address))
            {
                errors += "Address isn't valid.\n";
            }
            else if (validation.HasLengthGreaterThan(Address,100))
            {
                errors += "Address characters length limit is 100.\n";
            }
            //username
            if (validation.Is_Blank(UserName))
            {
                errors += "User Name isn't validk.\n";
            }
            else if (validation.HasLengthGreaterThan(UserName, 20))
            {
                errors += "Username characters length limit is 20.\n";
            }
            //password
            if (validation.Is_Blank(Password))
            {
                errors += "Password isn't valid.\n";
            }
            else if (validation.HasLengthGreaterThan(Password, 20))
            {
                errors += "Password characters length limit is 20.\n";
            }
            return errors;
        }
        public string ValidateAccount()
        {
            Validation validation = new Validation();
            string errors = "";
            //username
            if (validation.Is_Blank(UserName))
            {
                errors += "User Name isn't valid.\n";
            }
            else if (validation.HasLengthGreaterThan(UserName, 20))
            {
                errors += "Username characters length limit is 20.\n";
            }
            //password
            if (validation.Is_Blank(Password))
            {
                errors += "Password isn't valid.\n";
            }
            else if (validation.HasLengthGreaterThan(Password, 20))
            {
                errors += "Password characters length limit is 20.\n";
            }
            return errors;
        }
    }
}
