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
    public partial class FrmAddSupply : Form
    {
        Item item = new Item();
        Supplier supplier = new Supplier();
        public FrmAddSupply()
        {
            InitializeComponent();
        }

        private void btnSelectItem_Click(object sender, EventArgs e)
        {
            pnlSelectItem.Visible = true;
            pnlSupplier.Visible = false;
            Load_dgvItems(new Item().Retrieve());
        }

        private void FrmAddSupply_Load(object sender, EventArgs e)
        {
            Load_dgvItems(new Item().Retrieve());
            lblSupplyId.Text = "#" + new ItemSupply().GenerateID();
        }
        private void Load_dgvItems(List<Item> listItem)
        {
            //clear rows
            dgvItems.Rows.Clear();


            for (int i = 0; i < listItem.Count; i++)
            {
                dgvItems.Rows.Add(listItem[i].ItemId, listItem[i].GetDescription(),listItem[i].Category.Description);
            }
            //col size
            dgvItems.Columns["colId"].Width = 100;
            dgvItems.Columns["colDescription"].Width = 200;
            //if no rows
            if (dgvItems.Rows.Count > 0)
            {
                lblNoResult.Visible = false;
            }
            else
            {
                lblNoResult.Visible = true;
            }
        }
        private void Load_ItemInfo()
        {
            item = new Item().Retrieve(dgvItems.Rows[dgvItems.CurrentCell.RowIndex].Cells["colId"].Value.ToString());
            lblItemHeader.Text = "Item Selected (" + item.ItemId + ")";
            txtDescription.Text = item.GetDescription();
            lblCategory.Text = item.Category.Description;
            lblPrice.Text = item.Price.ToString();
            lblQuantity.Text = item.Quantity.ToString();
            lblUnit.Text = item.UnitCategory.Unit;
            lblVatable.Text = item.IsVatable ? "Yes" : "No";
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            Load_dgvItems(new Item().Search(txtSearchItem.Text));
        }

        private void txtSearchItem_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchItem.Text == "")
            {
                Load_dgvItems(new Item().Retrieve());
            }
        }

        private void txtSearchItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchItem.PerformClick();
            }
        }

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex > -1)
            {
                Load_ItemInfo();
                Message.ShowSuccess(item.ItemId + ": " + item.GetDescription() + " is Selected.","Message");
            }
        }

        private void txtDescription_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show(txtDescription.Text, txtDescription);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectSupplier_Click(object sender, EventArgs e)
        {
            pnlSelectItem.Visible = false;
            pnlSupplier.Visible = true;
            Load_dgvSuppliers(new Supplier().Retrieve());

        }
        private void Load_dgvSuppliers(List<Supplier> listSupplier)
        {
            //clear rows
            dgvSuppliers.Rows.Clear();


            for (int i = 0; i < listSupplier.Count; i++)
            {
                dgvSuppliers.Rows.Add(listSupplier[i].SupplierID, listSupplier[i].CompanyName,listSupplier[i].Address);
            }
            //col size
            dgvSuppliers.Columns["colSupplierId"].Width = 70;
            dgvSuppliers.Columns["colCompanyName"].Width = 200;
            //if no rows
            if (dgvSuppliers.Rows.Count > 0)
            {
                lblNoSupplier.Visible = false;
            }
            else
            {
                lblNoSupplier.Visible = true;
            }
        }

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex > -1)
            {
                Load_SupplierInfo();
                Message.ShowSuccess(lblCompanyName.Text + " is Selected.","Message");
            }
        }
        private void Load_SupplierInfo()
        {
            supplier = new Supplier().Retrieve(Convert.ToInt32(dgvSuppliers.Rows[dgvSuppliers.CurrentCell.RowIndex].Cells["colSupplierId"].Value.ToString()));
            lblCompanyName.Text = supplier.CompanyName;
            lblSupplierId.Text = supplier.SupplierID.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ItemSupply itemSupply = new ItemSupply();
                itemSupply.ItemSupplyId = itemSupply.GenerateID();
                itemSupply.Item.ItemId = item.ItemId;
                itemSupply.Supplier.SupplierID = supplier.SupplierID;
                itemSupply.Price = Convert.ToDecimal(txtPrice.Text);
                itemSupply.Quantity = Convert.ToDouble(nudQuantity.Value);
                itemSupply.DateRecieved = dtpDateRecieved.Value;
                itemSupply.Staff.StaffId = "20180901";
                if (chkAddToInventory.Checked)
                {
                    itemSupply.IsAddedToStock = true;
                }
                else
                {
                    itemSupply.IsAddedToStock = false;
                }


                if (itemSupply.Validate() == "")
                {
                    itemSupply.InsertRecord();
                    Message.ShowSuccess("Supply recorded successfully", "Message");
                    this.Close();
                }
                else
                {
                    Message.ShowError(itemSupply.Validate(), "Pease fix the following error");
                }
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Occured: " + ex.Message, "Error");
            }
        }

        private void chkAddToInventory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddToInventory.Checked)
            {
                if (item.Price > 0)
                {
                    if (item.Price != Convert.ToDecimal(txtPrice.Text))
                    {
                        if (!(MessageBox.Show("Are you sure you want to move directly this new supply to inventory? Previous price of item and price of the new supply does not match.", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes))
                        {
                            chkAddToInventory.Checked = false;
                        }
                    }
                }

            }
        }

        private void btnSearchSupplier_Click(object sender, EventArgs e)
        {
            Load_dgvSuppliers(new Supplier().Retrieve(txtSearchSupplier.Text));
        }

        private void txtSearchSupplier_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchSupplier.Text == "")
            {
                Load_dgvSuppliers(new Supplier().Retrieve());
            }
        }

        private void txtSearchSupplier_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchSupplier.PerformClick();
            }
        }

    }
}
