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
    public partial class FrmTruck : Form
    {
        public FrmTruck()
        {
            InitializeComponent();
        }

        private void Load_dgvTruck(List<Truck> listTrucks)
        {
            //clear rows
            dgvTruck.Rows.Clear();


            for (int i = 0; i < listTrucks.Count; i++)
            {
                dgvTruck.Rows.Add(listTrucks[i].TruckId, listTrucks[i].Description, listTrucks[i].PlateNo, listTrucks[i].Year);
                if (i % 2 == 0)
                {
                    dgvTruck.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    dgvTruck.Rows[i].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control);
                }
            }
            //col size
            //dgvTruck.Columns["colId"].Width = 50;
            //dgvTruck.Columns["colDescription"].Width = 100;
            //dgvTruck.Columns["colPlateNo"].Width = 200;
            //dgvTruck.Columns["colYear"].Width = 200;
            ////dgvSuppliers.Columns["colBtnView"].Width = 100;
            //dgvTruck.Columns["colBtnEdit"].Width = 100;
            //if no rows
            if (!(dgvTruck.Rows.Count > 0))
            {
                lblNoResult.Visible = true;
            }
            else
            {
                lblNoResult.Visible = false;
            }
            lblCountShow.Text = "Showing " + dgvTruck.RowCount + " of " + new Truck().CountTrucks() + " trucks";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddEditTruck frmAddEditTruck = new FrmAddEditTruck();
            frmAddEditTruck.Text = "Add new truck";
            frmAddEditTruck.ShowDialog();
            Load_dgvTruck(new Truck().Retrieve());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<Truck> listTruck = new Truck().Retrieve(txtSearch.Text);
                Load_dgvTruck(listTruck);
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error has occured: " + ex.Message,"Error");
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                Load_dgvTruck(new Truck().Retrieve());
            }
        }

        private void FrmTruck_Load(object sender, EventArgs e)
        {
            List<Truck> listTrucks = new Truck().Retrieve();
            Load_dgvTruck(listTrucks);
        }

        private void dgvTruck_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex != -1)
            {
                FrmAddEditTruck frmAddEditTruck = new FrmAddEditTruck(Convert.ToInt32(dgvTruck.Rows[e.RowIndex].Cells["colId"].Value));
                frmAddEditTruck.Text = "Edit Truck Information";
                frmAddEditTruck.ShowDialog();
                Load_dgvTruck(new Truck().Retrieve());
            }
        }
    }
}
