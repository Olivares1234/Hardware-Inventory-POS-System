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
    public partial class FrmAddUnit : Form
    {
        public FrmAddUnit()
        {
            InitializeComponent();
        }

        private void FrmAddUnit_Load(object sender, EventArgs e)
        {
            Load_dgvItemUnits(new UnitCategory().RetrieveAll());
            chkWhole.Checked = true;
        }
        private void Load_dgvItemUnits(List<UnitCategory> listUnitCategory)
        {
            dgvItemUnits.Rows.Clear();
            for (int i = 0; i < listUnitCategory.Count; i++)
            {
                string wholeInSale = listUnitCategory[i].WholeInSale ? "Yes" : "No";
                dgvItemUnits.Rows.Add(listUnitCategory[i].Description,listUnitCategory[i].Unit,wholeInSale);
                if (i % 2 == 0)
                {
                    dgvItemUnits.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    dgvItemUnits.Rows[i].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Validation validation = new Validation();
                UnitCategory unitCategory = new UnitCategory();
                unitCategory.Description = txtDescription.Text;
                unitCategory.Unit = txtUnit.Text;
                if (chkWhole.Checked)
                {
                    unitCategory.WholeInSale = true;
                }
                else
                {
                    unitCategory.WholeInSale = false;
                }

                if (validation.Is_Blank(txtDescription.Text) || validation.Is_Blank(txtUnit.Text))
                {
                    Message.ShowError("Inputs cannot be blank.", "Error");
                }
                else
                {
                    unitCategory.InsertRecord();
                    txtDescription.Clear();
                    txtUnit.Clear();
                    Message.ShowSuccess("New unit added successfully", "Message");
                    Load_dgvItemUnits(new UnitCategory().RetrieveAll());
                }
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error occured: " + ex.Message, "Error");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
