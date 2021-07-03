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
    public partial class FrmAddItem : Form
    {
        Item newItem = new Item();
        public FrmAddItem()
        {
            InitializeComponent();
        }

        private void FrmAddEditItem_Load(object sender, EventArgs e)
        {
            GenerateNewId();
            Load_cboUnit();          
            Load_cboCategory();
            Load_dgvDetails();
            Load_cboItemAttribute();
            
        }
        private void GenerateNewId()
        {
            txtItemId.Text = new Item().GenerateID();
        }
        private void Load_cboUnit()
        {
            cboUnit.Items.AddRange(new UnitCategory().Retrieve().ToArray());
            if (cboUnit.Items.Count > 0)
            {
                cboUnit.SelectedIndex = 0;
            }
            else
            {
                cboUnit.Items.Add("No unit available");
            }
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
        private void Load_dgvDetails()
        {
            dgvDetails.Columns["colBtnRemove"].Width = 80;
            lblNoResult.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            lblNoResult.Visible = false;
            Validation validation = new Validation();
            string errors = "";

            if (validation.Is_Blank(cboItemAttribute.Text))
            {
                errors += "Item Attribute cannot be blank.\n";
            }
            if (validation.Is_Blank(txtDetailDesc.Text))
            {
                errors += "Detail Desc cannot be blank.\n";
            }
            else if (validation.HasLengthGreaterThan(txtDetailDesc.Text, 45))
            {
                errors += "Detail Desc characters length must be less than 45.";
            }


            if (errors == "")
            {
                ItemDetail itemDetail = new ItemDetail();
                ItemAttribute itemAttribute = new ItemAttribute();
                //itemDetail.DetailName = txtDetailName.Text;
                //itemDetail.ItemAttribute.AttributeId = new ItemAttribute().Retrieve(cboItemAttribute.Text);
                itemDetail.ItemAttribute = itemAttribute.Retrieve(itemAttribute.Retrieve(cboItemAttribute.Text));
                itemDetail.DetailDescription = txtDetailDesc.Text;
                itemDetail.ItemId = txtItemId.Text;
                newItem.ItemDetails.Add(itemDetail);
                dgvDetails.Rows.Add(itemDetail.ItemAttribute.Description, txtDetailDesc.Text);
                txtDetailDesc.Clear();
                cboItemAttribute.Items.RemoveAt(cboItemAttribute.SelectedIndex);//remove attribute
                //cboItemAttribute.SelectedIndex = 0;
            }
            else
            {
                Message.ShowError(errors,"Please fix the following: ");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                newItem.ItemId = txtItemId.Text;
                newItem.ItemName = txtItemName.Text;
                newItem.Price = Convert.ToDecimal(txtPrice.Text);
                newItem.Category.CategoryId = new Category().Retrieve(cboCategory.Text);
                newItem.UnitCategory.UnitCategoryId = new UnitCategory().Retrieve(cboUnit.Text);
                newItem.SRP = Convert.ToDecimal(txtSRP.Text);
                newItem.IsVatable = chkVatable.Checked;

                if (newItem.Validate() == "")
                {
                    //no error

                    newItem.InsertRecord();
                    Message.ShowSuccess("Item Saved Successfully.", "Message");
                    this.Close();


                }
                else
                {
                    Message.ShowError(newItem.Validate(), "Please fix the following: ");
                }
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Occured: " + ex.Message, "Error");
            }
        }

        private void dgvDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex > -1)
            {
                cboItemAttribute.Items.Add(dgvDetails.Rows[e.RowIndex].Cells["colName"].Value.ToString());
                newItem.RemoveDetail(dgvDetails.Rows[e.RowIndex].Cells["colName"].Value.ToString(), dgvDetails.Rows[e.RowIndex].Cells["colDesc"].Value.ToString());
                dgvDetails.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void txtSRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions functions = new Functions();
            functions.AllowNumbersOnly(sender,e);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions functions = new Functions();
            functions.AllowNumbersOnly(sender, e);
        }
        private void Load_cboItemAttribute()
        {
            //cboItemAttribute.DataSource = new ItemAttribute().Retrieve();
            List<string> listItemAttribute = new ItemAttribute().Retrieve();
            for (int i = 0; i < listItemAttribute.Count; i++)
            {
                cboItemAttribute.Items.Add(listItemAttribute[i]);
            }
            cboItemAttribute.SelectedIndex = 0;
        }
    }
}
