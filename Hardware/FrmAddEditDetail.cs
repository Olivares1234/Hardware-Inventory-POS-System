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
    public partial class FrmAddEditDetail : Form
    {
        int detailId = 0;
        string itemId = "";
        public FrmAddEditDetail()
        {
            InitializeComponent();
        }
        public FrmAddEditDetail(int detailId) : this()
        {
            this.detailId = detailId;
            
        }
        public FrmAddEditDetail(string itemId) : this()
        {
            this.itemId = itemId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddEditDetail_Load(object sender, EventArgs e)
        {
            //cboItemAttribute.DataSource = new ItemAttribute().Retrieve();
            Load_cboItemAttribute();
            if (detailId != 0)//update
            {
                Load_cboItemAttribute();
                //cboItemAttribute.Items.Clear();
                //cboItemAttribute.DataSource = new ItemAttribute().RetrieveAvailable(new ItemDetail().Retrieve(detailId).ItemId);
                ItemDetail itemDetail = new ItemDetail().Retrieve(detailId);
                //txtDetailName.Text = itemDetail.ItemAttribute.Description;
                cboItemAttribute.Items.Add(itemDetail.ItemAttribute.Description);
                cboItemAttribute.Text = itemDetail.ItemAttribute.Description;
                txtDetailDesc.Text = itemDetail.DetailDescription;
                lblHeader.Text = "Edit Detail";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Validation validation = new Validation();
            ItemDetail itemDetail = new ItemDetail();
            itemDetail.DetailDescription = txtDetailDesc.Text;
            itemDetail.ItemAttribute.AttributeId = new ItemAttribute().Retrieve(cboItemAttribute.Text);
            itemDetail.DetailId = detailId;
            itemDetail.ItemId = itemId;
            if (validation.Is_Blank(cboItemAttribute.Text) || validation.Is_Blank(txtDetailDesc.Text))
            {
                Message.ShowSuccess("Inputs cannot be blank", "Please fix the error");
            }
            else//no error, ready to update or insert
            {
                try
                {
                    if (detailId != 0)//update
                    {                     
                        itemDetail.UpdateRecord();
                        Message.ShowSuccess("Detail updated successfully.", "Message");
                        this.Close();
                    }
                    else//insert
                    {
                        itemDetail.InsertRecord();
                        Message.ShowSuccess("Detail added successfully.", "Message");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    Message.ShowError("An Error Occured: " + ex.Message, "Error");
                }
            }
        }

        private void Load_cboItemAttribute()
        {
            cboItemAttribute.Items.Clear();
            List<string> listAttribute = new ItemAttribute().RetrieveAvailable(itemId);

            if (detailId != 0)
            {
                listAttribute = new ItemAttribute().RetrieveAvailable(new ItemDetail().Retrieve(detailId).ItemId);
            }
            
            for (int i = 0; i < listAttribute.Count; i++)
            {
                cboItemAttribute.Items.Add(listAttribute[i]);
            }
            //cboItemAttribute.SelectedIndex = 0;
        }
    }
}
