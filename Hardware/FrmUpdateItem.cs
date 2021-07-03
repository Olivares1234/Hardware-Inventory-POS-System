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
    public partial class FrmUpdateItem : Form
    {
        string itemId = "";
        public FrmUpdateItem()
        {
            InitializeComponent();
        }
        public FrmUpdateItem(string itemId) : this()
        {
            this.itemId = itemId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmUpdateItem_Load(object sender, EventArgs e)
        {
            Item item = new Item().Retrieve(itemId);
            Load_cboCategory();
            txtItemName.Text = item.ItemName;
            txtPrice.Text = item.Price.ToString("N2");
            txtSRP.Text = item.SRP.ToString("N2");
            cboCategory.Text = item.Category.Description;
            chkVatable.Checked = item.IsVatable;
            
        }
        private void Load_cboCategory()
        {
            cboCategory.Items.AddRange(new Category().Retrieve().ToArray());
            if (cboCategory.Items.Count > 0)
            {
                cboCategory.SelectedIndex = 0;
            }
            else
            {
                cboCategory.Items.Add("No category available");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Item item = new Item();
                item.ItemId = itemId;
                item.ItemName = txtItemName.Text;
                item.Price = Convert.ToDecimal(txtPrice.Text);
                item.SRP = Convert.ToDecimal(txtSRP.Text);
                item.IsVatable = chkVatable.Checked;
                item.Category.CategoryId = new Category().Retrieve(cboCategory.Text);

                if (item.Validate() == "")
                {
                    item.UpdateRecord();
                    Message.ShowSuccess("Item updated successfully", "Message");
                    this.Close();
                }
                else
                {
                    Message.ShowError(item.Validate(), "Error");
                }
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Occured: " + ex.Message, "Error");
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions functions = new Functions();
            functions.AllowNumbersOnly(sender,e);
        }

        private void txtSRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions functions = new Functions();
            functions.AllowNumbersOnly(sender, e);
        }
    }
}
