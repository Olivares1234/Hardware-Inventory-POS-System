using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class PositionPrivilege
    {
        public int PositionPrivilegeId { get; set; }
        public Position Position { get; set; }
        public Privilege Privilege { get; set; }
        public bool IsGranted { get; set; }

        public PositionPrivilege()
        {
            Position = new Position();
            Privilege = new Privilege();
        }

        public List<string> GetPrivileges(int positionId)
        {
            List<string> privileges = new List<string>();
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT privileges.description FROM privileges INNER JOIN position_privilege ON privileges.privilege_id = position_privilege.privilege_id WHERE position_id = @id  && is_granted = true";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id",positionId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    privileges.Add(dr.GetString("description"));
                }
            }
            conn.Close();
            return privileges;
        }

        public bool SavePrivileges()
        {
            try
            {
                Global global = new Global();
                MySqlConnection conn = new MySqlConnection(global.connectionString);
                conn.Open();
                string sql = "SELECT * FROM position_privilege WHERE position_id = @position_id AND privilege_id = @privilege_id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@position_id", Position.PositionId);
                cmd.Parameters.AddWithValue("@privilege_id", Privilege.PrivilegeId);

                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)//update
                {
                    dr.Read();
                    int id = dr.GetInt32("position_privilege_id");
                    conn.Close();
                    conn.Open();
                    sql = "UPDATE position_privilege SET is_granted = @granted WHERE position_privilege_id = @id";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@granted", IsGranted);
                    cmd.ExecuteNonQuery();
                }
                else//insert
                {
                    conn.Close();
                    conn.Open();
                    sql = "INSERT INTO position_privilege(privilege_id,position_id,is_granted) VALUES(@priv_id,@pos_id,@granted)";
                    cmd = new MySqlCommand(sql,conn);
                    cmd.Parameters.AddWithValue("@priv_id",Privilege.PrivilegeId);
                    cmd.Parameters.AddWithValue("@pos_id",Position.PositionId);
                    cmd.Parameters.AddWithValue("@granted", IsGranted);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Oocured" + ex.Message, "Error");
            }
            return true;
        }
    }
}
