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
    public partial class FrmAddEditSupplier : Form
    {
        int supplierId = 0;

        public FrmAddEditSupplier()
        {
            InitializeComponent();
        }
        public FrmAddEditSupplier(int id) : this()
        {
            this.supplierId = id;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Supplier supplier = new Supplier();
                supplier.CompanyName = txtCompanyName.Text;
                supplier.Address = txtCompanyAddress.Text;
                supplier.ContactNo = txtContactNo.Text;
                if (supplier.Validate() != "")//Error
                {
                    Message.ShowError(supplier.Validate(), "Please fix the error.");
                }
                else//no error
                {
                    //insert supplier

                    //this.Close();
                    if (supplierId > 0)
                    {
                        //update
                        supplier.SupplierID = supplierId;
                        supplier.UpdateRecord();
                        Message.ShowSuccess("Supplier updated successfully.", "Message");
                    }
                    else
                    {
                        //insert
                        supplier.InsertRecord();
                        Message.ShowSuccess("Supplier added successfully.", "Message");
                    }
                    this.Close();

                }
                
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Occured: " + ex.Message,"Error");
            }
        }

        private void FrmAddEditSupplier_Load(object sender, EventArgs e)
        {
            if (supplierId > 0)//update mode
            {
                lblHeading.Text = "Update Supplier";
                Supplier supplier = new Supplier();
                supplier = supplier.Retrieve(supplierId);
                txtCompanyName.Text = supplier.CompanyName;
                txtCompanyAddress.Text = supplier.Address;
                txtContactNo.Text = supplier.ContactNo;
            }
        }
    }
}
