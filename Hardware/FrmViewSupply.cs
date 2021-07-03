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
    public partial class FrmViewSupply : Form
    {
        string supplyId;
        ItemSupply itemSupply = new ItemSupply();
        public FrmViewSupply()
        {
            InitializeComponent();
        }

        public FrmViewSupply(string supplyId) : this()
        {
            this.supplyId = supplyId;
        }

        private void FrmViewSupply_Load(object sender, EventArgs e)
        {
            
            Load_ItemSupplyDetails();
        }
        private void Load_ItemSupplyDetails()
        {
            //item
            itemSupply = itemSupply.Retrieve(supplyId);

            lblSupplyId.Text = itemSupply.ItemSupplyId;
            lblItemId.Text = itemSupply.Item.ItemId;
            lblDescription.Text = itemSupply.Item.GetDescription();
            lblAvailableStocks.Text = itemSupply.Item.Quantity.ToString();
            lblUnit.Text = itemSupply.Item.UnitCategory.Unit;
            lblCurrentPrice.Text = itemSupply.Item.Price.ToString();
            lblCategory.Text = itemSupply.Item.Category.Description;
            //supplier
            lblSupplierId.Text = itemSupply.Supplier.SupplierID.ToString();
            lblCompanyName.Text = itemSupply.Supplier.CompanyName;
            lblAddress.Text = itemSupply.Supplier.Address;
            lblContactNo.Text = itemSupply.Supplier.ContactNo;
            //info
            lblPrice.Text = itemSupply.Price.ToString();
            lblQuantity.Text = itemSupply.Quantity.ToString();
            lblDate.Text = itemSupply.DateRecieved.ToString("yyyy-MM-dd");
            lblAdded.Text = itemSupply.IsAddedToStock.ToString();

            if (itemSupply.IsAddedToStock == false)
            {
                btnMoveToInventory.Visible = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMoveToInventory_Click(object sender, EventArgs e)
        {
            try
            {
                itemSupply.MoveToInventory(itemSupply);
                Message.ShowSuccess("Supply Added to inventory","Message");
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmUpdateSupply frmUpdateSupply = new FrmUpdateSupply(lblSupplyId.Text);
            frmUpdateSupply.ShowDialog();
            Load_ItemSupplyDetails();
        }
    }
}
