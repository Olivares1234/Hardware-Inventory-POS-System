using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class Privilege
    {
        public int PrivilegeId { get; set; }
        public string Description { get; set; }


        public int Retrieve(string description)
        {
            int privilegeId;
            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT privilege_id FROM privileges WHERE description = @desc";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@desc", description);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            privilegeId = dr.GetInt32("privilege_id");
            conn.Close();
            return privilegeId;
        }
    }
}
