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
    public partial class FrmViewSupplier : Form
    {
        int supplierId;
        public FrmViewSupplier()
        {
            InitializeComponent();
        }
        public FrmViewSupplier(int supplierId) : this()
        {
            this.supplierId = supplierId;
        }

        private void FrmViewSupplier_Load(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier().Retrieve(supplierId);
            lblSupplierId.Text = supplier.SupplierID.ToString();
            lblCompanyName.Text = supplier.CompanyName;
            lblCompanyAddress.Text = supplier.Address;
            lblContactNo.Text = supplier.ContactNo;
        }
    }
}
