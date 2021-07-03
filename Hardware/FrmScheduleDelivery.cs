using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hardware_MS
{
    public partial class FrmScheduleDelivery : Form
    {
        public FrmScheduleDelivery()
        {
            InitializeComponent();
        }

        private void FrmScheduleDelivery_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now.AddHours(0.5);
            //MessageBox.Show(DateTime.Now.ToString("hh:mm"));
            DisplayAvailableStaff();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            //Validation validation = new Validation();
            //if (!validation.IsValidDateSchedule(dtpDate.Value))
            //{
            //    Message.ShowError("Date is not valid","Error");
            //    dtpDate.Value = DateTime.Now;
            //}
            DisplayAvailableStaff();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            //Validation validation = new Validation();
            //if (!validation.IsValidTimeSchedule(dtpFrom.Value))
            //{
            //    Message.ShowError("From time is not valid", "Error");
            //    dtpFrom.Value = DateTime.Now;
            //}
            ComputeTimeRange();
            DisplayAvailableStaff();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpFrom.Value > dtpTo.Value)
            //{
            //    Message.ShowError("To time is not valid", "Error");
            //    dtpTo.Value = DateTime.Now.AddMinutes(30);
            //}
            ComputeTimeRange();
            DisplayAvailableStaff();
        }
        private void ComputeTimeRange()
        {
            TimeSpan timeRange =  dtpTo.Value - dtpFrom.Value;
            if (timeRange.Hours > 0)
            {
                lblTimeRange.Text = timeRange.Hours + " hour(s) and " + timeRange.Minutes.ToString() + " minute(s) ";
            }
            else
            {
                lblTimeRange.Text = timeRange.Minutes.ToString() + " minute(s) ";
            }
            
        }
        private void DisplayAvailableStaff()
        {
            List<Staff> listStaff = new Staff().RerieveAvailableStaff(dtpDate.Value, dtpFrom.Value, dtpTo.Value);
            dgvAvailableStaff.Rows.Clear();
            for (int i = 0; i < listStaff.Count; i++)
            {
                dgvAvailableStaff.Rows.Add(listStaff[i].StaffId,listStaff[i].GetFullName(),listStaff[i].Position.Description);
            }
        }

    }
}
