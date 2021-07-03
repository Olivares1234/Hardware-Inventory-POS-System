using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace Hardware_MS
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            Sale sale = new Sale();
            Delivery delivery = new Delivery();
            lblSales.Text = sale.ComputeTotalSalesToday().ToString("N2");
            lblDelivery.Text = delivery.CountDelivery(DateTime.Now).ToString();

            timer1.Start();
            labelTime.Text = DateTime.Now.ToLongTimeString();
          
           

            Global global = new Global();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT SUM(total_price) as total, DATE_FORMAT(date_time, '%b -%y') AS month FROM sales GROUP BY month order by date_time ";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr;

            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    this.chart1.Series["Monthly"].Points.AddXY(dr.GetString("month"), dr.GetDouble("total"));
                    this.chart1.Series[0].IsValueShownAsLabel = true;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            Global global1 = new Global();
            MySqlConnection conn1 = new MySqlConnection(global1.connectionString);
            string sql1 = "SELECT SUM(total_price) as totalS, YEAR(date_time) as year FROM sales GROUP BY year";

            MySqlCommand cmd1 = new MySqlCommand(sql1, conn1);
            MySqlDataReader dr1;

            try
            {
                conn1.Open();
                dr1 = cmd1.ExecuteReader();

                while (dr1.Read())
                {
                    this.chart2.Series["Yearly"].Points.AddXY(dr1.GetString("year"), dr1.GetDouble("totalS"));
                    this.chart2.Series[1].IsValueShownAsLabel = true;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Global global2 = new Global();
            MySqlConnection conn2 = new MySqlConnection(global2.connectionString);
            string sql2 = "SELECT SUM(total_price) AS total, DATE_FORMAT(date_time, '%b -%d -%y') AS date FROM sales GROUP BY date order by date_time";

            MySqlCommand cmd2 = new MySqlCommand(sql2, conn2);
            MySqlDataReader dr2;

            try
            {
                conn2.Open();
                dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                    this.chart3.Series["Daily"].Points.AddXY(dr2.GetString("date"), dr2.GetDouble("total"));
                    this.chart3.Series[2].IsValueShownAsLabel = true;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }


    }
}
