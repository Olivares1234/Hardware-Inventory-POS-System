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
    public partial class FrmAddAttribute : Form
    {
        public FrmAddAttribute()
        {
            InitializeComponent();
        }

        private void FrmAddAttribute_Load(object sender, EventArgs e)
        {
            Load_dgvAttributes(new ItemAttribute().Retrieve());
        }
        private void Load_dgvAttributes(List<string> listAttribute)
        {
            dgvAttributes.Rows.Clear();
            for (int i = 0; i < listAttribute.Count; i++)
            {
                dgvAttributes.Rows.Add(listAttribute[i]);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Validation validation = new Validation();

            if (validation.Is_Blank(txtAttribute.Text))
            {
                Message.ShowError("Input cannot be blank.", "Error");
            }
            else
            {
                ItemAttribute itemAttribute = new ItemAttribute();
                itemAttribute.Description = txtAttribute.Text;
                itemAttribute.InsertRecord();
                txtAttribute.Clear();
                Load_dgvAttributes(new ItemAttribute().Retrieve());
            }
        }
    }
}
