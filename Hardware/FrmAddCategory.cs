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
    public partial class FrmAddCategory : Form
    {
        public FrmAddCategory()
        {
            InitializeComponent();
        }

        private void FrmAddCategory_Load(object sender, EventArgs e)
        {
            Load_dgvCategories();
        }
        private void Load_dgvCategories()
        {
            dgvCategories.Rows.Clear();
            List<string> listCategory = new Category().Retrieve();
            for (int i = 0; i < listCategory.Count; i++)
            {
                dgvCategories.Rows.Add(listCategory[i].ToString());
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Description = txtDescription.Text;
            Validation validation = new Validation();
            if (validation.Is_Blank(category.Description))
            {
                Message.ShowError("Input cannot be blank.", "Error");
            }
            else 
            {
                try
                {
                    category.InsertRecord();
                    Message.ShowSuccess("New Category Added.", "Message");
                    Load_dgvCategories();
                    txtDescription.Clear();
                }
                catch (Exception ex)
                {
                    Message.ShowError("An Error Occured: " + ex.Message, "Error");
                }
            }
        }
    }
}
