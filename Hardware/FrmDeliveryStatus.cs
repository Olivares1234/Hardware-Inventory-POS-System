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
    public partial class FrmDeliveryStatus : Form
    {
        public FrmDeliveryStatus()
        {
            InitializeComponent();
        }
        private void Load_Deliveries()
        {
            dgvDeliveries.Rows.Clear();
            List<string> listDelivery = new DeliveryStatus().Retrieve();
            for (int i = 0; i < listDelivery.Count; i++)
            {
                dgvDeliveries.Rows.Add(listDelivery[i].ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DeliveryStatus delevery = new DeliveryStatus();
            delevery.Description = txtDescription.Text;
            Validation validation = new Validation();
            if (validation.Is_Blank(delevery.Description))
            {
                Message.ShowError("Input cannot be blank.", "Error");
            }
            else
            {
                try
                {
                    delevery.InsertRecord();
                    Message.ShowSuccess("New delivery Added.", "Message");
                    Load_Deliveries();
                    txtDescription.Clear();
                }
                catch (Exception ex)
                {
                    Message.ShowError("An Error Occured: " + ex.Message, "Error");
                }
            }
        }

        private void FrmDeliveryStatus_Load(object sender, EventArgs e)
        {
            Load_Deliveries();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
