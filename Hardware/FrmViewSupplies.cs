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
    public partial class FrmViewSupplies : Form
    {
        public FrmViewSupplies()
        {
            InitializeComponent();
        }

        private void FrmViewSupplies_Load(object sender, EventArgs e)
        {
            Load_dgvItemSupplies(new ItemSupply().FilterByMovedToInventory(false));
            cboFilter.SelectedIndex = 1;
        }
        private void Load_dgvItemSupplies(List<ItemSupply> listItemSupplies)
        {
            //clear rows
            dgvItemSupplies.Rows.Clear();

            for (int i = 0; i < listItemSupplies.Count; i++)
            {
                string movedToInventory = listItemSupplies[i].IsAddedToStock ? "Yes" : "No";
                //string dateReceived = listItemSupplies[i].DateRecieved.ToString("yy-MM-dd") == DateTime.Now.ToString("yy-MM-dd") ? "Today" : listItemSupplies[i].DateRecieved.ToString("yyyy-MM-dd");
                //dateReceived = listItemSupplies[i].DateRecieved.ToString("yy-MM-dd") == DateTime.Now.AddDays(-1).ToString("yy-MM-dd") ? "Yesterday" : listItemSupplies[i].DateRecieved.ToString("yyyy-MM-dd");
                string dateReceived = "";
                if (listItemSupplies[i].DateRecieved.ToString("yy-MM-dd") == DateTime.Now.ToString("yy-MM-dd"))
                {
                    dateReceived = "Today";
                }
                else if (listItemSupplies[i].DateRecieved.ToString("yy-MM-dd") == DateTime.Now.AddDays(-1).ToString("yy-MM-dd"))
                {
                    dateReceived = "Yesterday";
                }
                else
                {
                    dateReceived = listItemSupplies[i].DateRecieved.ToString("yyyy-MM-dd");
                }
                dgvItemSupplies.Rows.Add(listItemSupplies[i].ItemSupplyId, listItemSupplies[i].Item.GetDescription(), listItemSupplies[i].Supplier.CompanyName, listItemSupplies[i].Price, listItemSupplies[i].Item.UnitCategory.Unit, listItemSupplies[i].Quantity, dateReceived, movedToInventory);
            }
            //col size
            dgvItemSupplies.Columns["colId"].Width = 100;
            dgvItemSupplies.Columns["colDescription"].Width = 200;
            dgvItemSupplies.Columns["colSupplier"].Width = 150;
            dgvItemSupplies.Columns["colUnit"].Width = 60;
            dgvItemSupplies.Columns["colQuantity"].Width = 70;
            //dgvItems.Columns["colUnit"].Width = 50;
            //dgvItems.Columns["colQty"].Width = 80;
            //if no rows
            if (dgvItemSupplies.Rows.Count > 0)
            {
                lblNoResult.Visible = false;
            }
            else
            {
                lblNoResult.Visible = true;
            }
        }

        private void btnNewSupply_Click(object sender, EventArgs e)
        {
            FrmAddSupply frmAddSupply = new FrmAddSupply();
            frmAddSupply.ShowDialog();
            if (cboFilter.Text == "All")
            {
                Load_dgvItemSupplies(new ItemSupply().Retrieve());
            }
            else
            {
                bool status = cboFilter.Text == "Yes" ? true : false;
                Load_dgvItemSupplies(new ItemSupply().FilterByMovedToInventory(status));
            }
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFilter.Text == "Yes")
            {
                Load_dgvItemSupplies(new ItemSupply().FilterByMovedToInventory(true));
            }
            else if (cboFilter.Text == "No")
            {
                Load_dgvItemSupplies(new ItemSupply().FilterByMovedToInventory(false));
            }
            else
            {
                Load_dgvItemSupplies(new ItemSupply().Retrieve());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Load_dgvItemSupplies(new ItemSupply().Search(txtSearch.Text));
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "" && cboFilter.Text != "All")
            {
                bool status = cboFilter.Text == "Yes" ? true : false;
                Load_dgvItemSupplies(new ItemSupply().FilterByMovedToInventory(status));
            }
            else
            {
                Load_dgvItemSupplies(new ItemSupply().Retrieve());
            }
        }

        private void dgvItemSupplies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9 && e.RowIndex > -1)
            {           
                if (MessageBox.Show("Are you sure you want to move this supply to inventory?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    ItemSupply itemSupply = new ItemSupply().Retrieve(dgvItemSupplies.Rows[e.RowIndex].Cells["colId"].Value.ToString());
                    if (itemSupply.Price != itemSupply.Item.Price)
                    {
                        if (MessageBox.Show("Price of the supply does not match to items in inventory. Do you want to proceed?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            if (dgvItemSupplies.Rows[e.RowIndex].Cells["colIsMoved"].Value.ToString() == "Yes")
                            {
                                Message.ShowError("Supply is already moved to inventory.", "Error");
                            }
                            else
                            {
                                //move to inventory
                                try
                                {
                                    itemSupply.MoveToInventory(itemSupply);
                                    Message.ShowSuccess("Supply is added to inventory.", "Success");
                                    Load_dgvItemSupplies(itemSupply.FilterByMovedToInventory(false));
                                }
                                catch (Exception)
                                {
                                    Message.ShowError("An error occured in transaction.", "Error");
                                }

                            }
                        }
                    }
                    else
                    {
                        itemSupply.MoveToInventory(itemSupply);
                        Message.ShowSuccess("Supply is added to inventory.", "Success");
                        Load_dgvItemSupplies(itemSupply.FilterByMovedToInventory(false));
                    }
                }
            }

            if (e.ColumnIndex == 8 && e.RowIndex > -1)
            {
                FrmViewSupply frmViewSupply = new FrmViewSupply(dgvItemSupplies.Rows[e.RowIndex].Cells["colId"].Value.ToString());
                frmViewSupply.ShowDialog();
                Load_dgvItemSupplies(new ItemSupply().FilterByMovedToInventory(false));
            }

        }
    }
}
