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
    public partial class FrmAccount : Form
    {
        Staff staff = new Staff();
        public FrmAccount()
        {
            InitializeComponent();
        }

        private void FrmAccount_Load(object sender, EventArgs e)
        {
            Load_StaffInfo();
        }
        private void Load_StaffInfo()
        {
            staff = new Staff().Retrieve(LoginInfo.StaffId);
            txt4Id.Text = staff.StaffId;
            txt4FName.Text = staff.FirstName;
            txt4LName.Text = staff.LastName;
            txt4MName.Text = staff.MiddleName;
            txt4Sex.Text = staff.Sex;
            txt4DOB.Text = staff.BirthDate.ToString("yyyy-MM-dd");
            txt4DateHired.Text = staff.DateHired.ToString("yyyy-MM-dd");
            txt4Position.Text = staff.Position.Description;
            txt4Address.Text = staff.Address;
            txt4ContactNo.Text = staff.ContactNo;
            txtUserName.Text = staff.UserName;
            txtPassword.Text = staff.Password;
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShow.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            HandleControls(true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            HandleControls(false);
            txtUserName.Text = staff.UserName;
            txtPassword.Text = staff.Password;
        }
        private void HandleControls(bool status)
        {
            btnCancel.Visible = status;
            btnSave.Visible = status;
            txtUserName.ReadOnly = !status;
            txtPassword.ReadOnly = !status;
            btnUpdate.Visible = !status;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Staff staff = new Staff();
            staff.StaffId = LoginInfo.StaffId;
            staff.Password = txtPassword.Text;
            staff.UserName = txtUserName.Text;
            if (staff.ValidateAccount() == "")
            {
                staff.UpdateAccount();
                
                HandleControls(false);
            }
            else
            {
                Message.ShowError(staff.ValidateAccount(), "Error");
            }
            staff = staff.Retrieve(LoginInfo.StaffId);
            txtUserName.Text = staff.UserName;
            txtPassword.Text = staff.Password;
        }
    }
}
