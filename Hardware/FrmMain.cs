using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Hardware_MS
{
    public partial class FrmMain : Form
    {
        Button[] btnNavs = { };
        Panel[] pnlActiveIndicator = { };
        Form[] forms = { };
        Staff staff = new Staff();
        public FrmMain()
        {
            InitializeComponent();
            
        }
        private void SetActive(Button btnNav, Panel pnlActive, Form frmContent)
        {
            //Clear content of main panel
            pnlMain.Controls.Clear();

            //Set the background color of button navs to default
            for (int i = 0; i < btnNavs.Length; i++)
            {
                btnNavs[i].BackColor = Color.FromArgb(52, 73, 94);
            }
            //Set all pnlIndicator to invisible
            for (int i = 0; i < pnlActiveIndicator.Length; i++)
            {
                pnlActiveIndicator[i].Visible = false;
            }
            //set the background color of actie button
            btnNav.BackColor = Color.FromArgb(34, 48, 61);
            //show the active indicator
            pnlActive.Visible = true;
            Functions functions = new Functions();
            functions.OpenForm(frmContent, pnlMain);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            btnNavs = new Button[] { btnDashboard, btnTransaction,btnDelivery, btnSupplier, btnInventory, btnStaff,btnAccount };
            pnlActiveIndicator = new Panel[] { pnlActDashboard, pnlActTransaction,pnlActDelivery, pnlActSupplier, pnlActInventory, pnlActStaff,pnlActAccount };
            forms = new Form[] { new FrmDashboard(), new FrmTransaction(), new FrmDelivery(), new FrmSupplier(), new FrmInventory(), new FrmStaffs() };

            //FrmDashboard frmDashboard = new FrmDashboard();
            //SetActive(btnDashboard, pnlActDashboard, frmDashboard);
            staff = staff.Retrieve(LoginInfo.StaffId);
            txtName.Text = staff.GetFullName();
            txtPosition.Text = staff.Position.Description;
            Load_Privileges();
            //MessageBox.Show(new PositionPrivilege().GetPrivileges(staff.Position.PositionId).Count.ToString());
            try
            {
                if (File.Exists("../../img/" + staff.StaffId + ".png"))
                {
                    picProfile.Image = Image.FromFile("../../img/" + staff.StaffId + ".png");
                }
                else
                {
                    if (staff.Sex == "Male")
                    {
                        picProfile.Image = Hardware_MS.Properties.Resources.user_prof;
                    }
                    else
                    {
                        picProfile.Image = Hardware_MS.Properties.Resources.user__2_;
                    }
                }
            }
            catch(Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
            }
            //btnLogout.Size = new System.Drawing.Size(0,0);
            btnAccount.Enabled = true;
        }

        private void Load_Privileges()
        {
            List<string> privileges = new PositionPrivilege().GetPrivileges(staff.Position.PositionId);

            if (privileges.Count > 0)
            {
                for (int i = 0; i < btnNavs.Length; i++)
                {
                    if (btnNavs[i].Text == privileges[0])
                    {
                        //FrmDashboard frmDashboard = new FrmDashboard();
                        SetActive(btnNavs[i], pnlActiveIndicator[i], forms[i]);
                        break;
                    }
                }
            }
            for (int i = 0; i < privileges.Count; i++)
            {
                //if (privileges[i] == "Dashboard")
                //{
                //    btnDashboard.Enabled = true;
                //}
                //if (privileges[i] == "Transaction")
                //{
                //    btnTransaction.Enabled = true;
                //}
                //if (privileges[i] == "Supplier")
                //{
                //    btnSupplier.Enabled = true;
                //}
                //if (privileges[i] == "Inventory")
                //{
                //    btnInventory.Enabled = true;
                //}
                //if (privileges[i] == "Staff")
                //{
                //    btnStaff.Enabled = true;
                //}
                for (int btn = 0; btn < btnNavs.Length; btn++)
                {
                    if (btnNavs[btn].Text == privileges[i])
                    {
                        btnNavs[btn].Enabled = true;
                    }
                }
                btnAccount.Enabled = true;
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FrmDashboard frmDashboard = new FrmDashboard();
            SetActive(btnDashboard, pnlActDashboard, frmDashboard);
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            FrmTransaction frmTransaction = new FrmTransaction();
            SetActive(btnTransaction,pnlActTransaction,frmTransaction);
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            FrmSupplier frmSupplier = new FrmSupplier();
            SetActive(btnSupplier, pnlActSupplier, frmSupplier);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            FrmInventory frmInventory = new FrmInventory();
            SetActive(btnInventory,pnlActInventory,frmInventory);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            FrmStaffs frmStaff = new FrmStaffs();
            SetActive(btnStaff,pnlActStaff,frmStaff);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                LoginInfo.StaffId = "";
                FrmLogin frmLogin = new FrmLogin();
                this.Hide();
                frmLogin.ShowDialog();
                this.Close();
            }
        }

        private void FrmMain_KeyUp(object sender, KeyEventArgs e)
        {
            //flowLayoutPanel1.Controls.Add(btnDashboard);
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            FrmAccount frmAccount = new FrmAccount();
            SetActive(btnAccount, pnlActAccount, frmAccount);
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            FrmDelivery frmDelivery = new FrmDelivery();
            SetActive(btnDelivery, pnlActDelivery, frmDelivery);
        }

    }
}
