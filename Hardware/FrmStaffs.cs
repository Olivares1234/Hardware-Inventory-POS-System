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
    public partial class FrmStaffs : Form
    {
        Staff staff = new Staff();
        bool isAdd = true;
        CheckBox[] chkPrivileges = new CheckBox[] { };
        int selectedPositionId = 0;
        public FrmStaffs()
        {
            InitializeComponent();
        }

        private void FrmStaffs_Load(object sender, EventArgs e)
        {
            HideHeader_tabContol1();
            Load_dgvStaffs(new Staff().Retrieve());
            chkPrivileges = new CheckBox[] { chkDashboard, chkTransaction, chkSupplier, chkInventory, chkStaff,chkDelivery };
            cboChooseFilter.SelectedIndex = 0;
            Load_cboFilterPosition(new Position().Retrieve());
            //MessageBox.Show(new Staff().ToSexInt("Female").ToString());
        }
        private void HideHeader_tabContol1()
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }
        private void Load_dgvStaffs(List<Staff> listStaff)
        {
            dgvStaffs.Rows.Clear();
            for (int i = 0; i < listStaff.Count; i++)
            {
                dgvStaffs.Rows.Add(listStaff[i].StaffId,listStaff[i].GetFullName(),listStaff[i].Position.Description,listStaff[i].ContactNo,
                    listStaff[i].BirthDate.ToString("yyyy-MM-dd"),listStaff[i].Sex,listStaff[i].Address,listStaff[i].IsEmployed);
            }
            dgvStaffs.Columns["colId"].Width = 100;
            dgvStaffs.Columns["colFullName"].Width = 160;
            dgvStaffs.Columns[2].Width = 100;
            dgvStaffs.Columns[3].Width = 110;
            dgvStaffs.Columns["colSex"].Width = 80;
            dgvStaffs.Columns["colView"].Width = 80;
            //if no rows
            if (dgvStaffs.Rows.Count > 0)
            {
                lblNoResult.Visible = false;
            }
            else
            {
                lblNoResult.Visible = true;
            }
            lblCountShow.Text = "Showing " + dgvStaffs.Rows.Count + " of " + staff.Count() + " Employees.";

        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            isAdd = true;
            chkDeactivate.Hide();
            lblTab2Head.Text = "Add New Employee";
            pnlAcc.Visible = true;
            ClearInputs();
            tabControl1.SelectedIndex = 1;
            txtId.Text = staff.GenerateID();
            cboPosition.Items.Clear();
            cboPosition.Items.AddRange(new Position().Retrieve().ToArray());
            cboPosition.SelectedIndex = 0;
            cboSex.SelectedIndex = 0;
            dtpBDate.MaxDate = DateTime.Now.AddYears(-18);

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            BackToMenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Image Files|*.png;*jpg;*.jpeg;";
                openFileDialog1.ShowDialog();
                txtFilePath.Text = openFileDialog1.FileName;
                picProfile.Image = Image.FromFile(txtFilePath.Text);
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Occured: " + ex.Message,"Error");
                txtFilePath.Text = "";
                openFileDialog1.FileName = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Staff staff = new Staff();
                staff.StaffId = txtId.Text;
                staff.FirstName = txtFName.Text;
                staff.LastName = txtLName.Text;
                staff.MiddleName = txtMName.Text;
                staff.Position.PositionId = new Position().Retrieve(cboPosition.Text);
                staff.ContactNo = txtContactNo.Text;
                staff.BirthDate = dtpBDate.Value;
                staff.Sex = cboSex.Text;
                staff.Address = txtAddress.Text;
                staff.UserName = txtUserName.Text;
                staff.Password = txtPassword.Text;
                staff.DateHired = dtpDateHired.Value;
                staff.IsEmployed = !chkDeactivate.Checked;

                if (staff.Validate() == "")
                {
                    bool result = false;
                    
                    if (isAdd)
                    {
                        if (txtConfirm.Text != txtPassword.Text)
                        {
                            Message.ShowError("Password does not match", "Please fix the following error: ");
                        }
                        else
                        {
                            result = staff.InsertRecord();
                            txtConfirm.Clear();
                        }
                    }
                    else
                    {
                        result = staff.UpdateRecord();
                    }
                    if (result == true)
                    {
                        if (openFileDialog1.FileName != "")
                        {
                            if (File.Exists("../../img/" + staff.StaffId + ".png"))
                            {
                                File.Delete("../../img/" + staff.StaffId + ".png");
                            }
                            File.Copy(openFileDialog1.FileName, "../../img/" + staff.StaffId + ".png");
                        }
                        tabControl1.SelectedIndex = 3;
                        Load_StaffInfo(txtId.Text);
                        ClearInputs();
                        txtId.Text = staff.GenerateID();

                    }
                    
                }
                else
                {
                    Message.ShowError(staff.Validate(), "Please fix the following error: ");
                }
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
            }
        }

        private void btnAddPosition_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            Load_dgvPositions();
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            BackToMenu();
        }
        private void BackToMenu()
        {
            tabControl1.SelectedIndex = 0;
            Load_dgvStaffs(new Staff().Retrieve());
        }
        private void Load_dgvPositions()
        {
            dgvPositions.Rows.Clear();
            List<string> listPos = new Position().Retrieve();
            for (int i = 0; i < listPos.Count; i++)
            {
                dgvPositions.Rows.Add(listPos[i]);
            }
            dgvPositions.Columns[1].Width = 120;
        }

        private void btnAddPos_Click(object sender, EventArgs e)
        {
            Validation validation = new Validation();
            Position position = new Position();
            position.Description = txtPosDesc.Text;
            if (!validation.Is_Blank(position.Description))
            {
                position.InsertRecord();
                Load_dgvPositions();
                txtPosDesc.Clear();
            }
            else
            {
                Message.ShowError("Please fill up the description!","Error");
            }
        }
        private void ClearInputs()
        {
            picProfile.Image = Hardware_MS.Properties.Resources.user_prof;
            txtFName.Clear();
            txtLName.Clear();
            txtMName.Clear();
            txtContactNo.Clear();
            txtAddress.Clear();
            txtUserName.Clear();
            txtPassword.Clear();
            txtFilePath.Clear();
            staff = new Staff();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Load_dgvStaffs(new Staff().Search(txtSearch.Text));
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                Load_dgvStaffs(new Staff().Retrieve());
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            BackToMenu();
        }

        private void dgvStaffs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8 && e.RowIndex > -1)
            {
                tabControl1.SelectedIndex = 3;
                Load_StaffInfo(dgvStaffs.Rows[dgvStaffs.CurrentCell.RowIndex].Cells["colId"].Value.ToString());
                //picProfile.Visible = true;
                //btnBrowse.Visible = true;
            }
        }
        private void Load_StaffInfo(string staffId)
        {
            try
            {

                Staff staff = new Staff().Retrieve(staffId);
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
                chkDeactivate.Checked = !staff.IsEmployed;
                cboPosition.Text = staff.Position.Description;
                txtStatus.Text = staff.IsEmployed ? "Active" : "Deactivated";
                
                //if (staff.Position.Description == "Administrator" && new Staff().Retrieve(LoginInfo.StaffId).Position.Description == "Administrator")
                //{
                //    btn4Update.Hide();
                //}
                //else
                //{
                //    btn4Update.Show();
                //}
                if (File.Exists("../../img/" + staff.StaffId + ".png"))
                {
                    picStaffImage.Image = Image.FromFile("../../img/" + staff.StaffId + ".png");
                    //FileStream fs = new FileStream("../../img/" + staff.StaffId + ".png",FileMode.Open);
                    //var file = new FileStream("../../img/" + staff.StaffId + ".png",FileMode.Open);
                    //Byte[] buffer = new Byte[255];
                    //picStaffImage.Image = file.BeginRead(buffer, 0, buffer.Length,Ass);
                }
                else
                {
                    if (staff.Sex == "Male")
                    {
                        picStaffImage.Image = Hardware_MS.Properties.Resources.user_prof;
                    }
                    else
                    {
                        picStaffImage.Image = Hardware_MS.Properties.Resources.user__2_;
                    }
                }
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error has Occured: " + ex.Message,"Error");
            }
        }

        private void btn4Update_Click(object sender, EventArgs e)
        {
            try
            {
                chkDeactivate.Show();
                cboPosition.Items.Clear();
                cboPosition.Items.AddRange(new Position().Retrieve().ToArray());
                isAdd = false;
                lblTab2Head.Text = "Update Employee Info";
                pnlAcc.Visible = false;
                tabControl1.SelectedIndex = 1;
                Staff staff = new Staff().Retrieve(txt4Id.Text);
                txtId.Text = staff.StaffId;
                txtFName.Text = staff.FirstName;
                txtLName.Text = staff.LastName;
                txtMName.Text = staff.MiddleName;
                cboSex.Text = staff.Sex;
                dtpBDate.Value = staff.BirthDate;
                txtContactNo.Text = staff.ContactNo;
                cboPosition.Text = staff.Position.Description;
                txtAddress.Text = staff.Address;
                txtUserName.Text = staff.UserName;
                txtPassword.Text = staff.Password;
                dtpDateHired.Value = staff.DateHired;
                if (staff.Position.Description == "Administrator")
                {
                    cboPosition.Enabled = false;
                    cboPosition.Items.Clear();
                    cboPosition.Items.Add("Administrator");
                    cboPosition.SelectedIndex = 0;
                    chkDeactivate.Hide();
                }
                else
                {
                    cboPosition.Enabled = true;
                    chkDeactivate.Show();
                }
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
                //picProfile.Visible = false;
                //btnBrowse.Visible = false;
                
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message,"Error");
            }
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

        private void dgvPositions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex > -1)
            {
                if (dgvPositions.Rows[e.RowIndex].Cells[0].Value.ToString() == "Administrator" || dgvPositions.Rows[e.RowIndex].Cells[0].Value.ToString() == "Super Admin")
                {
                    btnSavePrivileges.Visible = false;
                }
                else
                {
                    btnSavePrivileges.Visible = true;
                }
                pnlPrivilege.Visible = true;
                selectedPositionId = new Position().Retrieve(dgvPositions.Rows[e.RowIndex].Cells[0].Value.ToString());
                List<string> privileges = new PositionPrivilege().GetPrivileges(selectedPositionId);

                
                for (int i = 0; i < chkPrivileges.Length; i++)
                {
                    chkPrivileges[i].Checked = false;
                }
                for (int i = 0; i < privileges.Count; i++)
                {
                    //if (privileges[i] == "Dashboard")
                    //{
                    //    chkDashboard.Checked = true;
                    //}
                    //if (privileges[i] == "Transaction")
                    //{
                    //    chkTransaction.Checked = true;
                    //}
                    //if (privileges[i] == "Supplier")
                    //{
                    //    chkSupplier.Checked = true;
                    //}
                    //if (privileges[i] == "Inventory")
                    //{
                    //    chkInventory.Checked = true;
                    //}
                    //if (privileges[i] == "Staff")
                    //{
                    //    chkStaff.Checked = true;
                    //}
                    for (int chk = 0; chk < chkPrivileges.Length; chk++)
                    {
                        if (chkPrivileges[chk].Text == privileges[i])
                        {
                            chkPrivileges[chk].Checked = true;
                        }
                    }
                }
            }
        }

        private void btnSavePrivileges_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkPrivileges.Length; i++)
            {
                PositionPrivilege privilege = new PositionPrivilege();
                privilege.Privilege.PrivilegeId = new Privilege().Retrieve(chkPrivileges[i].Text);
                privilege.Position.PositionId = new Position().Retrieve(dgvPositions.Rows[dgvPositions.CurrentCell.RowIndex].Cells[0].Value.ToString());
                privilege.IsGranted = chkPrivileges[i].Checked;
                privilege.SavePrivileges();
                //pnlPrivilege.Visible = false;
            }
            Message.ShowSuccess("Privileges Saved Successfully", "Message");
        }

        private void cboChooseFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChooseFilter.SelectedIndex == 0)
            {
                pnlPosition.Hide();
                pnlSex.Hide();
                pnlStatus.Hide();
                Load_dgvStaffs(new Staff().Retrieve());
            }
            else if (cboChooseFilter.SelectedIndex == 1)
            {
                pnlPosition.Show();
                pnlSex.Hide();
                pnlStatus.Hide();
                //Load_dgvStaffs(new Staff().RetrieveByPosition(new Position().Retrieve(cboFilterPosition.Text)));
                cboFilterPosition.SelectedIndex = 0;
            }
            else if (cboChooseFilter.SelectedIndex == 2)
            {
                cboFilterSex.SelectedIndex = 0;
                pnlSex.Show();
                pnlPosition.Hide();
                pnlStatus.Hide();
            }
            else if (cboChooseFilter.SelectedIndex == 3)
            {
                cboFilterStatus.SelectedIndex = 0;
                pnlStatus.Show();
                pnlPosition.Hide();
                pnlSex.Hide();
            }
        }

        private void cboFilterPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFilterPosition.Text == "All")
            {
                Load_dgvStaffs(new Staff().Retrieve());
            }
            else
            {
                Load_dgvStaffs(new Staff().RetrieveByPosition(new Position().Retrieve(cboFilterPosition.Text)));
            }
        }
        private void Load_cboFilterPosition(List<string> positions)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                cboFilterPosition.Items.Add(positions[i]);
            }
        }

        private void cboFilterSex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFilterSex.Text == "All")
            {
                Load_dgvStaffs(new Staff().Retrieve());
            }
            else if (cboFilterSex.Text == "Male")
            {
                Load_dgvStaffs(new Staff().RetrieveBySex(0));
            }
            else if (cboFilterSex.Text == "Female")
            {
                Load_dgvStaffs(new Staff().RetrieveBySex(1));
            }

            //else
            //{
            //    Load_dgvStaffs(new Staff().RetrieveBySex(new Staff().ToSexInt(cboSex.Text)));
            //}
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void cboFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFilterStatus.Text == "All")
            {
                Load_dgvStaffs(new Staff().Retrieve());
            }
            else if (cboFilterStatus.Text == "Active")
            {
                Load_dgvStaffs(new Staff().RetrieveByStatus(true));
            }
            else if (cboFilterStatus.Text == "Deactivated")
            {
                Load_dgvStaffs(new Staff().RetrieveByStatus(false));
            }
        }

        private void txtContactNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions functions = new Functions();
            functions.AllowNumbersOnly(sender,e);
        }

    }
}
