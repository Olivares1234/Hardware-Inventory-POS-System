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
    public partial class FrmAddBrokenItem : Form
    {
        int brokenItemId = 0;
        Item item = new Item();
        BrokenItem broken = new BrokenItem();
        public FrmAddBrokenItem()
        {
            InitializeComponent();
        }

        public FrmAddBrokenItem(int id)
            : this()
        {
            this.brokenItemId = id;
        }

        private void Load_itemInfo()
        {
            Item item = new Item().Retrieve(dgvBrokenItem.Rows[dgvBrokenItem.CurrentCell.RowIndex].Cells["colItemCode"].Value.ToString());
            lblItemCode.Text = item.ItemId;
            lblItemName.Text = item.GetDescription();
            lblPrice.Text = item.GetPriceWithVat().ToString("N2");
            lblQty.Text = item.Quantity.ToString();
            lblSRP.Text = item.SRP.ToString();
            lblUnit.Text = item.UnitCategory.Unit;
            lblCategory.Text = item.Category.Description;
        }

        private void SaveTransaction()
        {
            try
            {
                if (!(new Validation().Is_Blank(lblItemName.Text)))
                {
                    if (nupQty.Value > 0)
                    {
                        if (!(new Validation().Is_Blank(txtDescription.Text)))
                        {
                            BrokenItem brokenItem = new BrokenItem();
                            brokenItem.Item.ItemId = lblItemCode.Text;
                            brokenItem.Description = txtDescription.Text;
                            brokenItem.Quantity = Convert.ToDouble(nupQty.Value);
                            brokenItem.Price = Convert.ToDecimal(lblPrice.Text);
                            brokenItem.Date = DateTime.Now;
                            brokenItem.InsertRecord();

                            FrmAddBrokenItem frmBroken = new FrmAddBrokenItem();
                            this.Close();
                        }
                        else
                        {
                            Message.ShowError("The comment cannot be blank.", "Error");
                        }
                    }
                    else
                    {
                        Message.ShowError("The quantity isn't valid.", "Error");
                    }
                }
                else
                {
                    Message.ShowError("Please select item.", "Error");
                }
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message,"Error");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveTransaction();
        }

        private void dgvBrokenItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 && e.RowIndex > -1)
            {
                Load_itemInfo();
                //MessageBox.Show("Select successfully!");
                dgvBrokenItem.Visible = false;
                txtSearchItem.Clear();
                //txtSearchItem.Enabled = false;
            }
        }

        private void txtSearchItem_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchItem.Text == "")
            {
                dgvBrokenItem.Visible = false;
                lblNoItem.Visible = false;
            }
            else
            {
                dgvBrokenItem.Visible = true;
                Load_dgvBrokenItem(new Item().Search(txtSearchItem.Text));
            }     
        }
        private void Load_dgvBrokenItem(List<Item> listItem)
        {
            dgvBrokenItem.Rows.Clear();
            dgvBrokenItem.BringToFront();
            for (int i = 0; i < listItem.Count; i++)
            {
                dgvBrokenItem.Rows.Add(listItem[i].ItemId, listItem[i].GetDescription(), listItem[i].GetPriceWithVat().ToString("N2"), listItem[i].Quantity, listItem[i].SRP, listItem[i].UnitCategory.Unit);
            }
            if (dgvBrokenItem.Rows.Count > 0)
            {
                lblNoItem.Visible = false;
            }
            else
            {
                lblNoItem.Visible = true;
            }
            dgvBrokenItem.Columns[0].Width = 80;
            dgvBrokenItem.Columns[1].Width = 120;
            dgvBrokenItem.Columns[3].Width = 50;
            dgvBrokenItem.Columns[5].Width = 70;
            lblNoItem.BringToFront();
        }

        private void FrmAddBrokenItem_Load(object sender, EventArgs e)
        {

        }
    }
}
