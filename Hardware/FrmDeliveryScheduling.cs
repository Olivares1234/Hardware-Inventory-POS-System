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
    public partial class FrmDeliveryScheduling : Form
    {
        Panel[] pnlSteps = new Panel[] { };
        List<Staff> ListSelectedStaff = new List<Staff>();
        Truck selectedTruck = new Truck();
        string saleId = "";
        public FrmDeliveryScheduling()
        {
            InitializeComponent();
        }
        public FrmDeliveryScheduling(string sale_id) :this()
        {
            saleId = sale_id;
        }

        private void FrmDeliveryScheduling_Load(object sender, EventArgs e)
        {
            HideHeader_tabContol1();
            pnlSteps = new Panel[] {pnlStep1,pnlStep2,pnlStep3,pnlStep4 };
            dtpDate.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddMinutes(30);
            dtpTo.Value = DateTime.Now.AddHours(1);
            ComputeTimeRange();
        }
        private void HideHeader_tabContol1()
        {
            //implementation for hiding tablControl headers
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }
        private void ComputeTimeRange()
        {
            TimeSpan timeRange = dtpTo.Value - dtpFrom.Value;
            if (timeRange.Hours > 0)
            {
                lblTimeRange.Text = timeRange.Hours + " hour(s) and " + timeRange.Minutes.ToString() + " minute(s) ";
            }
            else
            {
                lblTimeRange.Text = timeRange.Minutes.ToString() + " minute(s) ";
            }

        }

        private void btnStep1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            ChangeStep(0);
        }

        private void btnStep2_Click(object sender, EventArgs e)
        {
            if (dtpFrom.Value < DateTime.Now || dtpTo.Value < dtpFrom.Value)
            {
                Message.ShowError("The time schedule is invalid.", "Error");
            }
            else if ((dtpTo.Value - dtpFrom.Value).Minutes < 29)
            {
                Message.ShowError("The time range minumum is 30 minutes","Error");
            }
            else if ((dtpTo.Value - dtpFrom.Value).Hours > 5)
            {
                Message.ShowError("The time range maximum is 5 hours","Error");
            }
            else if (dtpDate.Value.Date < DateTime.Now.Date || (dtpDate.Value.Date - DateTime.Now.Date).Days > 2)
            {
                Message.ShowError("Invalid Date", "Error");
            }
            else
            {
                dgvSelectedStaff.Rows.Clear();
                ListSelectedStaff.Clear();
                selectedTruck = new Truck();
                txtTruckId.Text = "--";
                txtModel.Text = "--";
                tabControl1.SelectedIndex = 1;
                ChangeStep(1);
                LoadCustomerInfo();
            }
        }
        private void LoadCustomerInfo()
        {
            //Customer customer = new Customer().Retrieve(new Sale().Retrieve(saleId).);
            Customer customer = new SaleCustomer().GetCustomer(saleId);
            txtCustomerName.Text = customer.GetFullName();
            txtCustomerAddress.Text = customer.Address;
            txtContact.Text = customer.ContactNo;
            txtDescription.Text = customer.Address;
        }
        private void ChangeStep(int activeStep)
        {
            for (int i = 0; i < pnlSteps.Length; i++)
            {
                pnlSteps[i].BackColor = Color.FromKnownColor(KnownColor.Control);
            }
            pnlSteps[activeStep].BackColor = Color.FromArgb(98, 220, 150);
        }

        private void btnStep3_Click(object sender, EventArgs e)
        {
            
            if (nudFee.Value < 0)
            {
                Message.ShowError("Invalid Delivery fee.", "Error");
            }
            else
            {
                tabControl1.SelectedIndex = 2;
                ChangeStep(2);
                Load_dgvTrucks();
            }
            
        }
        private void Load_dgvTrucks()
        {
            dgvTrucks.Rows.Clear();
            List<Truck> listTruck = new Truck().RetrieveAvailableTrucks(dtpDate.Value, dtpFrom.Value, dtpTo.Value);
            for (int i = 0; i < listTruck.Count; i++)
            {
                dgvTrucks.Rows.Add(listTruck[i].TruckId,listTruck[i].Description);
            }
            dgvTrucks.Columns[0].Width = 90;
            dgvTrucks.Columns[2].Width = 100;
        }

        private void btnStep4_Click(object sender, EventArgs e)
        {
            if (txtTruckId.Text == "--")
            {
                Message.ShowError("Please select a delivery truck.","Error");
            }
            else
            {
                tabControl1.SelectedIndex = 3;
                ChangeStep(3);
                DisplayAvailableStaff();
                dgvAvailableStaff.Columns[3].Width = 90;
            }

        }

        private void btnBackStep3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            ChangeStep(2);
        }

        private void btnBackStep2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            ChangeStep(1);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
        private void DisplayAvailableStaff()
        {
            List<Staff> listStaff = new Staff().RerieveAvailableStaff(dtpDate.Value, dtpFrom.Value, dtpTo.Value);
            dgvAvailableStaff.Rows.Clear();
            for (int i = 0; i < listStaff.Count; i++)
            {
                dgvAvailableStaff.Rows.Add(listStaff[i].StaffId, listStaff[i].GetFullName(), listStaff[i].Position.Description);
            }
        }

        private void dgvAvailableStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void Load_dgvSelectedStaff()
        {
            dgvSelectedStaff.Rows.Clear();
            for (int i = 0; i < ListSelectedStaff.Count; i++)
            {
                dgvSelectedStaff.Rows.Add(ListSelectedStaff[i].StaffId,ListSelectedStaff[i].GetFullName(),ListSelectedStaff[i].Position.Description);
            }
            dgvSelectedStaff.Columns[3].Width = 90;
        }
        private int FindStaff(string staffId)
        {
            int pos = -1;
            for (int i = 0; i < ListSelectedStaff.Count; i++)
            {
                if (ListSelectedStaff[i].StaffId == staffId)
                {
                    pos = i;
                    break;
                }
            }
            return pos;
        }

        private void dgvAvailableStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex > -1)
            {
                if (FindStaff(dgvAvailableStaff.Rows[e.RowIndex].Cells[0].Value.ToString()) > -1)
                {
                    Message.ShowError("Staff is already selected","Error");
                }
                else if (ListSelectedStaff.Count >= 5)
                {
                    Message.ShowError("Staff maximum is 5.","Maximum");
                }
                else
                {
                    Staff staff = new Staff().Retrieve(dgvAvailableStaff.Rows[e.RowIndex].Cells[0].Value.ToString());
                    ListSelectedStaff.Add(staff);
                    Load_dgvSelectedStaff();
                }
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (ListSelectedStaff.Count <= 2)
            {
                Message.ShowError("The selected staff must have 1 driver and 2 to 4 helpers.","Error");
            }
            else if (CountDriver() != 1)
            {
                Message.ShowError("Only 1 Driver is required for delivery operation.", "Error");
            }
            else
            {
                //ready to save
                Delivery delivery = new Delivery();
                delivery.Sale.SaleId = saleId;
                delivery.Truck.TruckId = selectedTruck.TruckId;
                delivery.Staff.StaffId = LoginInfo.StaffId;
                delivery.DeliveryAddress = txtDescription.Text;
                delivery.TimeStart = dtpFrom.Value;
                delivery.TimeEnd = dtpTo.Value;
                delivery.Date = dtpDate.Value;
                delivery.Fee = nudFee.Value;
                List<DeliveryStaff> listDeliveryStaff = new List<DeliveryStaff>();
                for (int i = 0; i < ListSelectedStaff.Count; i++)
                {
                    DeliveryStaff deliveryStaff = new DeliveryStaff();
                    deliveryStaff.Staff = ListSelectedStaff[i];
                    deliveryStaff.Role = ListSelectedStaff[i].Position.Description;
                    listDeliveryStaff.Add(deliveryStaff);
                }
                delivery.ListDeliveryStaff = listDeliveryStaff;
                //Insert record
                delivery.InsertRecord();
                this.Close();
            }
        }

        private int CountDriver()
        {
            int count = 0;
            for (int i = 0; i < ListSelectedStaff.Count; i++)
            {
                if (ListSelectedStaff[i].Position.Description == "Truck Driver")
                {
                    count++;
                }
            }
            return count;
        }

        private void dgvSelectedStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ListSelectedStaff.RemoveAt(FindStaff(dgvSelectedStaff.Rows[e.RowIndex].Cells[0].Value.ToString()));
            Load_dgvSelectedStaff();
        }

        private void dgvTrucks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 2)
            {
                selectedTruck = new Truck().Retrieve(Convert.ToInt32(dgvTrucks.Rows[e.RowIndex].Cells[0].Value.ToString()));
                txtTruckId.Text = selectedTruck.TruckId.ToString();
                txtModel.Text = selectedTruck.Description;
            }
        }

        private void txtModel_TextChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        
    }
}
