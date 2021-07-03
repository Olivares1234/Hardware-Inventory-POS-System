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
    public partial class FrmDelivery : Form
    {
        public FrmDelivery()
        {
            InitializeComponent();
        }

        private void FrmDelivery_Load(object sender, EventArgs e)
        {
            HideHeader_tabContol1();
            Load_dgvDeliveries(new Delivery().Retrieve(dtpDate.Value));
            cboChooseFilter.SelectedIndex = 1;
            
        }
        private void HideHeader_tabContol1()
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmScheduleDelivery frmScheduleDelivery = new FrmScheduleDelivery();
            frmScheduleDelivery.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmTruck frmTruck = new FrmTruck();
            frmTruck.ShowDialog();
            //MessageBox.Show(DateTime.Now.ToString("HH:mm"));
        }

        private void btnAddAttribute_Click(object sender, EventArgs e)
        {
            FrmDeliveryStatus frmDeliver = new FrmDeliveryStatus();
            frmDeliver.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmDeliveryScheduling frmDeliveryScheduling = new FrmDeliveryScheduling("18100017");
            frmDeliveryScheduling.ShowDialog();
        }
        private void Load_dgvDeliveries(List<Delivery> listDelivery)
        {
            dgvDeliveries.Rows.Clear();
            for (int i = 0; i < listDelivery.Count; i++)
            {
                dgvDeliveries.Rows.Add(listDelivery[i].DeliveryId, listDelivery[i].Sale.SaleId,listDelivery[i].Date.ToString("MMM dd yyyy"),
                    listDelivery[i].TimeStart.ToString("hh:mm tt"), listDelivery[i].TimeEnd.ToString("hh:mm tt"), listDelivery[i].Fee.ToString("N2"),
                    listDelivery[i].Truck.Description, listDelivery[i].DeliveryAddress ,listDelivery[i].Status.Description);
            }
            dgvDeliveries.Columns[0].Width = 50;
            dgvDeliveries.Columns[3].Width = 90;
            dgvDeliveries.Columns[4].Width = 90;
            dgvDeliveries.Columns[5].Width = 90;
            dgvDeliveries.Columns[6].Width = 140;
            dgvDeliveries.Columns[7].Width = 180;
        }

        private void dgvDeliveries_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 9)
            {
                Load_cboStatus(new DeliveryStatus().Retrieve());
                pnlUpdateStatus.Show();
                lblDeliveryId.Text = dgvDeliveries.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            else if (e.RowIndex > -1 && e.ColumnIndex == 10)
            {
                tabControl1.SelectedIndex = 1;
                Load_deliveryInfo();
            }
        }
        private void Load_cboStatus(List<string> listStatus)
        {
            cboStatus.Items.Clear();
            for (int i = 0; i < listStatus.Count; i++)
            {
                cboStatus.Items.Add(listStatus[i]);
            }
            cboStatus.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlUpdateStatus.Hide();
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            Delivery delivery = new Delivery();
            delivery.UpdateStatus(Convert.ToInt32(lblDeliveryId.Text),new DeliveryStatus().Retrieve(cboStatus.Text));
            Load_dgvDeliveries(new Delivery().Retrieve());
            pnlUpdateStatus.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
        private void Load_deliveryInfo() 
        {
            Delivery delivery = new Delivery().Retrieve(Convert.ToInt32(dgvDeliveries.Rows[dgvDeliveries.CurrentCell.RowIndex].Cells[0].Value.ToString()));
            txtDeliveyId.Text = delivery.DeliveryId.ToString();
            txtSaleId.Text = delivery.Sale.SaleId;
            txtOrNo.Text = delivery.Sale.OrNo;
            txtDeliveryAddress.Text = delivery.DeliveryAddress;
            lblStatus.Text = delivery.Status.Description;
            txtDate.Text = delivery.Date.ToString("MMM dd yyyy");
            txtTimeStart.Text = delivery.TimeStart.ToString("hh:mm tt");
            txtTimeEnd.Text = delivery.TimeEnd.ToString("hh:mm tt");
            lblFee.Text = delivery.Fee.ToString("N2");
            lblTruckUsed.Text = delivery.Truck.Description;
            dgvStaff.Rows.Clear();
            for (int i = 0; i < delivery.ListDeliveryStaff.Count; i++)
            {
                dgvStaff.Rows.Add(delivery.ListDeliveryStaff[i].Staff.StaffId, delivery.ListDeliveryStaff[i].Staff.GetFullName(), delivery.ListDeliveryStaff[i].Role);
            }
            dgvStaff.Columns[0].Width = 90;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void cboChooseFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChooseFilter.SelectedIndex == 0)
            {
                Load_dgvDeliveries(new Delivery().Retrieve());
            }
            else if (cboChooseFilter.SelectedIndex == 1)
            {
                Load_dgvDeliveries(new Delivery().Retrieve(dtpDate.Value));
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            Load_dgvDeliveries(new Delivery().Retrieve(dtpDate.Value));
        }
    }
}
