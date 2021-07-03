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
    public partial class FrmAddEditTruck : Form
    {
        int truckId = 0;

        public FrmAddEditTruck()
        {
            InitializeComponent();
        }
        public FrmAddEditTruck(int id)
            : this()
        {
            this.truckId = id;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Truck truck = new Truck();
                truck.Description = txtDescription.Text;
                truck.PlateNo = txtPlateNo.Text;
                truck.Year = txtYear.Text;
                if (truck.Validate() != "")//Error
                {
                    Message.ShowError(truck.Validate(), "Please fix the error.");
                }
                else//no error
                {
                    //insert supplier

                    //this.Close();
                    if (truckId > 0)
                    {
                        //update
                        truck.TruckId = truckId;
                        truck.UpdateRecord();
                        Message.ShowSuccess("Truck updated successfully.", "Message");
                    }
                    else
                    {
                        //insert
                        truck.InsertRecord();
                        Message.ShowSuccess("Truck added successfully.", "Message");
                    }
                    this.Close();

                }

            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Occured: " + ex.Message, "Error");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddEditTruck_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(truckId.ToString());
            if (truckId > 0)//update mode
            {
                lblHeading.Text = "Update Truck";
                Truck truck = new Truck().Retrieve(truckId);
                txtDescription.Text = truck.Description;
                txtPlateNo.Text = truck.PlateNo;
                //MessageBox.Show(truck.Description);
                txtYear.Text = truck.Year;

            }
        }
    }
}
