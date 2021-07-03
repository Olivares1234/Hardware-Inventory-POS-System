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
    public partial class FrmUpdateSupply : Form
    {
        ItemSupply itemSupply = new ItemSupply();
        string itemSupplyId;
        public FrmUpdateSupply()
        {
            InitializeComponent();
        }
        public FrmUpdateSupply(string itemSupplyId) : this()
        {
            this.itemSupplyId = itemSupplyId;
        }

        private void FrmUpdateSupply_Load(object sender, EventArgs e)
        {
            itemSupply = itemSupply.Retrieve(itemSupplyId);
            lblSupplyId.Text = itemSupply.ItemSupplyId;
            txtPrice.Text = itemSupply.Price.ToString();
            nudQuantity.Value = Convert.ToDecimal(itemSupply.Quantity);
            dtpDateRecieved.Value = itemSupply.DateRecieved;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            itemSupply.Price = Convert.ToDecimal(txtPrice.Text);
            itemSupply.Quantity = Convert.ToDouble(nudQuantity.Value);
            itemSupply.DateRecieved = dtpDateRecieved.Value;
            try
            {
                if (itemSupply.Validate() == "")
                {
                    itemSupply.UpdateRecord();
                    Message.ShowSuccess("Item Supply Information updated successfully.", "Message");
                    this.Close();
                }
                else
                {
                    Message.ShowError(itemSupply.Validate(), "Please fix the following error");
                }
            }
            catch (Exception)
            {
                Message.ShowError("An Error Occured","Error");
            }
            
        }
    }
}
