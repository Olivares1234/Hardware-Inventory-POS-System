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
    public partial class FrmEditCustomer : Form
    {
        string customerId = "";
        public FrmEditCustomer()
        {
            InitializeComponent();
        }
        public FrmEditCustomer(string customerId) : this()
        {
            this.customerId = customerId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.CustomerId = txtCustomerId.Text;
            customer.LastName = txtLName.Text;
            customer.FirstName = txtFName.Text;
            customer.Address = txtAddress.Text;
            customer.ContactNo = txtContact.Text;
            if (customer.Validate() == "")
            {
                customer.UpdateRecord();
                this.Close();
            }
            else
            {
                Message.ShowError(customer.Validate(),"Error");
            }
            
        }

        private void FrmEditCustomer_Load(object sender, EventArgs e)
        {
            Customer customer = new Customer().Retrieve(customerId);
            txtCustomerId.Text = customer.CustomerId;
            txtFName.Text = customer.FirstName;
            txtLName.Text = customer.LastName;
            txtAddress.Text = customer.Address;
            txtContact.Text = customer.ContactNo;
        }
    }
}
