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
    public partial class FrmBrokenItem : Form
    {
        BrokenItem brokenItem = new BrokenItem();
        public FrmBrokenItem()
        {
            InitializeComponent();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            FrmAddBrokenItem frmAddBrokenItem = new FrmAddBrokenItem();
            frmAddBrokenItem.ShowDialog(this);
            Load_dvgBroken(new BrokenItem().Retrieve());
        }
        private void Load_dvgBroken(List<BrokenItem> listBrokenitem)
        {
            dgvBroken.Rows.Clear();
            for (int i = 0; i < listBrokenitem.Count; i++)
            {
                dgvBroken.Rows.Add(listBrokenitem[i].BrokenItemId, listBrokenitem[i].Item.GetDescription(), listBrokenitem[i].Quantity, listBrokenitem[i].Date.ToShortDateString(), listBrokenitem[i].Price, listBrokenitem[i].Description);
            }
            Load_lblCountShow1();
            dgvBroken.Columns[0].Width = 50;
            dgvBroken.Columns[1].Width = 150;
            dgvBroken.Columns[2].Width = 70;
            //if no rows
            if (!(dgvBroken.Rows.Count > 0))
            {
                lblNoResult.Visible = true;
            }
            else
            {
                lblNoResult.Visible = false;
            }
            lblCountShow.Text = "Showing " + dgvBroken.RowCount + " of " + new BrokenItem().CountBrokenItems() + " BrokenItems";
            
        }

        private void FrmBrokenItem_Load(object sender, EventArgs e)
        {
            Load_dvgBroken(new BrokenItem().Retrieve());
        }

        private void btnPrintBroken_Click(object sender, EventArgs e)
        {
            FrmPrintBrokenItem brokenitemreport = new FrmPrintBrokenItem();
            brokenitemreport.ShowDialog();

        }
        private void Load_lblCountShow1()
        {

            //lblCountShow1.Text = "Showing " + dgvBroken.Rows.Count + " of " + brokenItem. + " sales.";
        }
    }
}
