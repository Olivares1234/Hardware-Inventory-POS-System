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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Validation validation = new Validation();
            if (validation.Is_Blank(txtUser.Text) || validation.Is_Blank(txtPassword.Text))
            {
                Message.ShowError("Fill up all username and password to login.","Error");
            }
            else
            {
                Staff staff = new Staff();
                staff.UserName = txtUser.Text;
                staff.Password = txtPassword.Text;
                bool result = staff.Login();
                if (result == true)
                {
                    FrmMain frmMain = new FrmMain();
                    LoginInfo.StaffId = staff.StaffId;
                    this.Hide();
                    frmMain.ShowDialog();               
                    this.Close();
                }
                else
                {
                    Message.ShowError("Incorrect Username or Password.","Error");
                }
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtUser;
            txtUser.Focus();
        }

        private void txtUser_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}
