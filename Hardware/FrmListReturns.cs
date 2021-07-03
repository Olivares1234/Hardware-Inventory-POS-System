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
    public partial class FrmListReturns : Form
    {
        List<Return> listReturnPrint = new List<Return>();
        public FrmListReturns()
        {
            InitializeComponent();
        }

        private void FrmListReturns_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(new Return().Retrieve().Count.ToString());
            HideHeader_tabContol1();
            Load_dgvReturns(new Return().Retrieve(DateTime.Now));
            cboChooseFilter.SelectedIndex = 1;
            Load_cboSelectStaff();
        }
        private void Load_dgvReturns(List<Return> listReturn)
        {
            dgvReturns.Rows.Clear();
            listReturnPrint = listReturn;
            for (int i = 0; i < listReturn.Count; i++)
            {
                dgvReturns.Rows.Add(listReturn[i].ReturnId,listReturn[i].OrRefNumber,listReturn[i].DateTime,
                    listReturn[i].Staff.GetFullName(),listReturn[i].ListItemReturn.Count,listReturn[i].TotalAmountReturned.ToString("N2"));
            }
            dgvReturns.Columns[0].Width = 80;
            dgvReturns.Columns[1].Width = 150;
            dgvReturns.Columns[2].Width = 200;
            dgvReturns.Columns[3].Width = 150;
            if (dgvReturns.Rows.Count > 0)
            {
                lblNoResult.Visible = false;
                lblNoResult.BringToFront();
            }
            else
            {
                lblNoResult.Visible = true;
            }
            lblCountShow.Text = "Showing " + dgvReturns.Rows.Count + " of " + new Return().Count() + " return sales";
        }
        private void HideHeader_tabContol1()
        {
            //implementation for hiding tablControl headers
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        private void cboChooseFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChooseFilter.SelectedIndex == 0)
            {
                Load_dgvReturns(new Return().Retrieve());
                pnlFilterByDate.Hide();
                pnlFilterStaff.Hide();
                pnlDateBetween.Hide();
            }
            else if (cboChooseFilter.SelectedIndex == 1)
            {
                //filter by date
                pnlFilterByDate.Show();
                pnlFilterStaff.Hide();
                pnlDateBetween.Hide();
                dtpFilterReturns.Value = DateTime.Now;
            }
            else if (cboChooseFilter.SelectedIndex == 2)
            {
                //filter by staff
                pnlFilterByDate.Hide();
                pnlFilterStaff.Show();
                pnlDateBetween.Hide();
                cboSelectStaff.SelectedIndex = 0;
            }
            else if (cboChooseFilter.SelectedIndex == 3)
            {
                pnlFilterByDate.Hide();
                pnlFilterStaff.Hide();
                pnlDateBetween.Show();
                dtpFrom.Value = DateTime.Now.AddDays(-1);
                dtpTo.Value = DateTime.Now;
            }
        }

        private void dtpFilterReturns_ValueChanged(object sender, EventArgs e)
        {
            Load_dgvReturns(new Return().Retrieve(dtpFilterReturns.Value));
        }
        private void Load_cboSelectStaff()
        {
            List<Staff> listStaff = new Staff().Retrieve();
            for (int i = 0; i < listStaff.Count; i++)
            {
                cboSelectStaff.Items.Add(listStaff[i].GetFullName());
            }
        }

        private void cboSelectStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSelectStaff.Text == "All")
            {
                Load_dgvReturns(new Return().Retrieve());
            }
            else
            {
                Load_dgvReturns(new Return().RetrieveByStaff(new Staff().RetrieveByName(cboSelectStaff.Text)));
            }
        }

        private void dgvReturns_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 6)
            {
                tabControl1.SelectedIndex = 1;
                LoadReturnInfo(dgvReturns.Rows[e.RowIndex].Cells[0].Value.ToString());
                //MessageBox.Show(dgvReturns.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
        private void LoadReturnInfo(string returnId)
        {
            Return saleReturn = new Return().Retrieve(returnId);
            txtReturnId.Text = saleReturn.ReturnId;
            txtOrNo.Text = saleReturn.OrRefNumber;
            txtDateTime.Text = saleReturn.DateTime.ToString();
            txtP4Staff.Text = saleReturn.Staff.GetFullName();
            txtNoOfItems.Text = saleReturn.ListItemReturn.Count.ToString();
            txtTotalAmt.Text = saleReturn.TotalAmountReturned.ToString("N2");
            txtP4TotalQty.Text = saleReturn.ComputeTotalQty().ToString();
            dgvReturnItem.Rows.Clear();
            for (int i = 0; i < saleReturn.ListItemReturn.Count; i++)
            {
                string returned = saleReturn.ListItemReturn[i].IsReturnedToStock ? "Yes" : "No";
                dgvReturnItem.Rows.Add(saleReturn.ListItemReturn[i].Item.ItemId, saleReturn.ListItemReturn[i].Item.GetDescription(),saleReturn.ListItemReturn[i].Item.UnitCategory.Unit,
                    saleReturn.ListItemReturn[i].Quantity, saleReturn.ListItemReturn[i].ReturnAmount, (Convert.ToDecimal(saleReturn.ListItemReturn[i].Quantity) * saleReturn.ListItemReturn[i].ReturnAmount).ToString("N2"),
                    saleReturn.ListItemReturn[i].ReasonForReturn, saleReturn.ListItemReturn[i].ItemCondition, returned, saleReturn.ListItemReturn[i].ItemReturnId);
            }
            dgvReturnItem.Columns[0].Width = 80;
            dgvReturnItem.Columns[1].Width = 120;
            dgvReturnItem.Columns[2].Width = 50;
            dgvReturnItem.Columns[3].Width = 50;
            dgvReturnItem.Columns[4].Width = 100;
            dgvReturnItem.Columns[5].Width = 100;
            dgvReturnItem.Columns[6].Width = 120;
            dgvReturnItem.Columns[7].Width = 100;
        }

        private void dgvReturnItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 10)
            {
                if (dgvReturnItem.Rows[e.RowIndex].Cells[8].Value.ToString() == "Yes")
                {
                    Message.ShowError("Item is already moved to inventory", "Error");
                }
                else
                {
                    //MessageBox.Show("Test");
                    //MessageBox.Show(dgvReturnItem.Rows[e.RowIndex].Cells[9].Value.ToString());
                    if (MessageBox.Show("Do you want this item to return to inventory?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    {
                        ItemReturn itemReturn = new ItemReturn().Retrieve(Convert.ToInt32(dgvReturnItem.Rows[e.RowIndex].Cells[9].Value.ToString()));
                        itemReturn.ReturnToInventory();
                        LoadReturnInfo(txtReturnId.Text);
                    }
                }
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            Load_dgvReturns(new Return().Retrieve(dtpFrom.Value, dtpTo.Value));
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            Load_dgvReturns(new Return().Retrieve(dtpFrom.Value,dtpTo.Value));
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvReturns.Rows.Count > 0)
            {
                FrmPrintReturns frmPrintReturns = new FrmPrintReturns();
                if (cboChooseFilter.SelectedIndex == 1)
                {
                    frmPrintReturns = new FrmPrintReturns(dtpFilterReturns.Value);
                }
                else if (cboChooseFilter.SelectedIndex == 2)
                {
                    frmPrintReturns = new FrmPrintReturns(new Staff().RetrieveByName(cboSelectStaff.Text));
                    if (cboSelectStaff.Text == "All")
                    {
                        frmPrintReturns = new FrmPrintReturns();
                    }
                }
                else if (cboChooseFilter.SelectedIndex == 3)
                {
                    frmPrintReturns = new FrmPrintReturns(dtpFrom.Value, dtpTo.Value);
                }
                frmPrintReturns.ShowDialog();
            }
            else
            {
                Message.ShowError("No data.","Error");
            }
        }
    }
}
