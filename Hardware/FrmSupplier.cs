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
    public partial class FrmSupplier : Form
    {
        public FrmSupplier()
        {
            InitializeComponent();
        }

        //private void btnAddSupplier_Click(object sender, EventArgs e)
        //{
        //    FrmAddEditSupplier frmAddSupplier = new FrmAddEditSupplier();
        //    frmAddSupplier.Text = "Add new supplier";
        //    frmAddSupplier.ShowDialog();
        //    Load_dgvSuppliers(new Supplier().Retrieve());
        //}

        private void FrmSupplier_Load(object sender, EventArgs e)
        {
            List<Supplier> listSuppliers = new Supplier().Retrieve();
            Load_dgvSuppliers(listSuppliers);
            ////testing
            ////UnitCategory unitCategory = new UnitCategory();
            ////MessageBox.Show(unitCategory.Retrieve("Kilo Gram").ToString());
            //Item item = new Item();
            ////item.ItemId = item.GenerateID();
            ////item.ItemName = "Concrete Nail";
            ////item.Price = 100.00M;
            ////item.QuantityPerUnit = 50;
            ////item.UnitCategory.UnitCategoryId = 1;
            ////item.InsertRecord();
            //MessageBox.Show(item.Retrieve("item1800002").UnitCategory.Unit);
            //ItemSupply itemSupply = new ItemSupply();
            //MessageBox.Show(itemSupply.GenerateID());
        }
        private void Load_dgvSuppliers(List<Supplier> listSuppliers)
        {
            //clear rows
            dgvSuppliers.Rows.Clear();
            

            for (int i = 0; i < listSuppliers.Count; i++)
            {
                dgvSuppliers.Rows.Add(listSuppliers[i].SupplierID, listSuppliers[i].CompanyName, listSuppliers[i].Address,listSuppliers[i].ContactNo);
                if (i % 2 == 0)
                {
                    dgvSuppliers.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    dgvSuppliers.Rows[i].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control);
                }
            }
            //col size
            dgvSuppliers.Columns["colId"].Width = 50;
            dgvSuppliers.Columns["colContactNo"].Width = 100;
            dgvSuppliers.Columns["colCompanyName"].Width = 200;
            dgvSuppliers.Columns["colAddress"].Width = 200;
            //dgvSuppliers.Columns["colBtnView"].Width = 100;
            dgvSuppliers.Columns["colBtnEdit"].Width = 100;
            //if no rows
            if (!(dgvSuppliers.Rows.Count > 0))
            {
                lblNoResult.Visible = true;
            }
            else
            {
                lblNoResult.Visible = false;
            }
            lblCountShow.Text = "Showing " + dgvSuppliers.RowCount + " of " + new Supplier().CountSuppliers() + " Suppliers";
            
        }

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 4 && e.RowIndex != -1)
            //{
            //    FrmViewSupplier frmViewSupplier = new FrmViewSupplier(Convert.ToInt32(dgvSuppliers.Rows[e.RowIndex].Cells["colId"].Value));
            //    frmViewSupplier.ShowDialog();
            //}
            if (e.ColumnIndex == 4 && e.RowIndex != -1)
            {
                FrmAddEditSupplier frmAddEditSupplier = new FrmAddEditSupplier(Convert.ToInt32(dgvSuppliers.Rows[e.RowIndex].Cells["colId"].Value));
                frmAddEditSupplier.Text = "Edit Supplier Information";
                frmAddEditSupplier.ShowDialog();
                Load_dgvSuppliers(new Supplier().Retrieve());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Supplier> listSupplier = new Supplier().Retrieve(txtSearch.Text);
            Load_dgvSuppliers(listSupplier);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                Load_dgvSuppliers(new Supplier().Retrieve());
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            FrmAddEditSupplier frmAddSupplier = new FrmAddEditSupplier();
            frmAddSupplier.Text = "Add new supplier";
            frmAddSupplier.ShowDialog();
            Load_dgvSuppliers(new Supplier().Retrieve());
        }


    }
}
