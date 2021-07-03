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
    public partial class FrmInventory : Form
    {
        public FrmInventory()
        {
            InitializeComponent();
        }

        private void FrmInventory_Load(object sender, EventArgs e)
        {
            Load_dgvItems(new Item().Retrieve());
            Load_cboFilterCategory();
            cboFilterCategory.SelectedIndex = 0;
            cboStockLevel.SelectedIndex = 0;
        }
        private void CountNewSupply()
        {
            int count = new ItemSupply().CountNewSupply();
            if (count == 0)
            {
                lblNewSupplyCount.Visible = false;
            }
            else
            {
                lblNewSupplyCount.Visible = true;
                lblNewSupplyCount.Text = count.ToString();
            }

        }

        private void Load_dgvItems(List<Item> listItem)
        {
            CountNewSupply();
            NotifyOfStock();
            //clear rows
            dgvItems.Rows.Clear();


            for (int i = 0; i < listItem.Count; i++)
            {
                string vatable = listItem[i].IsVatable ? "Yes" : "No";
                dgvItems.Rows.Add(listItem[i].ItemId, listItem[i].GetDescription(), listItem[i].Price, listItem[i].SRP,vatable,listItem[i].GetPriceWithVatString(), listItem[i].Quantity, listItem[i].UnitCategory.Unit, listItem[i].Category.Description);
                if (listItem[i].Quantity == 0)
                {
                    dgvItems.Rows[i].Cells[6].Style.ForeColor = Color.FromArgb(231, 76, 60);
                }
                else if (listItem[i].Quantity <= 15)
                {
                    dgvItems.Rows[i].Cells[6].Style.ForeColor = Color.FromArgb(230, 126, 34);
                }
                else
                {
                    dgvItems.Rows[i].Cells[6].Style.ForeColor = Color.FromArgb(39, 174, 96);
                }
                if (i % 2 == 0)
                {
                    dgvItems.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    dgvItems.Rows[i].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control);
                }
            }
            //dgvItems.Rows[0].Cells[0].Style.ForeColor = Color.Red;
            //col size
            dgvItems.Columns["colId"].Width = 80;
            dgvItems.Columns["colDescription"].Width = 180;
            dgvItems.Columns["colPrice"].Width = 80;
            dgvItems.Columns["colSRP"].Width = 80;
            dgvItems.Columns["colBtnInfo"].Width = 100;
            dgvItems.Columns["colUnit"].Width = 60;
            dgvItems.Columns["colQty"].Width = 60;
            dgvItems.Columns["colVatable"].Width = 60;
            //if no rows
            if (dgvItems.Rows.Count > 0)
            {
                lblNoResult.Visible = false;
            }
            else
            {
                lblNoResult.Visible = true;
            }
            lblCountShow.Text = "Showing " + dgvItems.RowCount + " of " + new Item().CountItems() + " Items";

        }


        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            FrmAddUnit frmAddUnit = new FrmAddUnit();
            frmAddUnit.ShowDialog();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            FrmAddItem frmAddEditItem = new FrmAddItem();
            frmAddEditItem.ShowDialog();
            Load_dgvItems(new Item().Retrieve());
        }

        private void cboFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFilterCategory.Text == "All")
            {
                Load_dgvItems(new Item().Retrieve());
            }
            else 
            {
                Load_dgvItems(new Item().RetrieveByCategory(new Category().Retrieve(cboFilterCategory.Text)));
            }
        }
        private void Load_cboFilterCategory()
        {
            cboFilterCategory.Items.AddRange(new Category().Retrieve().ToArray());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Load_dgvItems(new Item().Search(txtSearch.Text));
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                Load_dgvItems(new Item().Retrieve());
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            FrmAddCategory frmAddCategory = new FrmAddCategory();
            frmAddCategory.ShowDialog();
        }

        private void btnNewSupply_Click(object sender, EventArgs e)
        {
            FrmAddSupply frmAddSupply = new FrmAddSupply();
            frmAddSupply.ShowDialog();
            Load_dgvItems(new Item().Retrieve());
        }

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9 && e.RowIndex > -1)
            {
                FrmViewItem frmViewItem = new FrmViewItem(dgvItems.Rows[e.RowIndex].Cells["colId"].Value.ToString());
                frmViewItem.ShowDialog();
                Load_dgvItems(new Item().Retrieve());
            }
        }

        private void btnViewSupplies_Click(object sender, EventArgs e)
        {
            FrmViewSupplies frmViewSupplies = new FrmViewSupplies();
            frmViewSupplies.ShowDialog();
            Load_dgvItems(new Item().Retrieve());
        }

        private void btnAddAttribute_Click(object sender, EventArgs e)
        {
            FrmAddAttribute frmAddAttribute = new FrmAddAttribute();
            frmAddAttribute.ShowDialog();
        }

        private void btnBrokenItem_Click(object sender, EventArgs e)
        {
            FrmBrokenItem frmBrokenItem = new FrmBrokenItem();
            frmBrokenItem.ShowDialog(this);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cboStockLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStockLevel.SelectedIndex == 0)
            {
                Load_dgvItems(new Item().Retrieve());
            }
            else if (cboStockLevel.Text == "Low stock")
            {
                Load_dgvItems(new Item().Retrieve(1));
            }
            else if (cboStockLevel.Text == "Out of stock")
            {
                Load_dgvItems(new Item().Retrieve(0));
            }
            else if (cboStockLevel.Text == "In stock")
            {
                Load_dgvItems(new Item().Retrieve(2));
            }
        }
        private void NotifyOfStock()
        {
            int outOfStock = new Item().Retrieve(0).Count;
            int lowStock = new Item().Retrieve(1).Count;
            lblOutOfStock.Text = outOfStock.ToString();
            lblLowStock.Text = lowStock.ToString();
        }

        private void lblLowStock_Click(object sender, EventArgs e)
        {

        }

        private void lblNewSupplyCount_Click(object sender, EventArgs e)
        {

        }

    }
}
