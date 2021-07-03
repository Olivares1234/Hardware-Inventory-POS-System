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
    public partial class FrmTransaction : Form
    {
        List<Item> listItem = new List<Item>(); // list of items
        Item item = new Item();
        List<SaleDetail> listSaleDetail = new List<SaleDetail>(); //list of sale details
        public FrmTransaction()
        {
            InitializeComponent();
        }

        private void FrmTransaction_Load(object sender, EventArgs e)
        {
            HideHeader_tabContol1();//hide the header of tabControl1
            Load_dgvTransaction();//set width of columns
            lblNoTransaction.Visible = true;//show label that telling no item selected
            lblNoTransaction.BringToFront(); //bring to front
            lblP1InvoiceNo.Text = new Sale().GenerateID();//generate id of sales
            rdoGuest.Checked = true;
            LoadCustomerLogic();//determine if the customer is guest or regular
            CountReturns();//count the returns today
            Load_cboSelectStaff();
            cboChooseFilter.SelectedIndex = 1;
        }
        private void HideHeader_tabContol1()
        {
            //implementation for hiding tablControl headers
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        /**********************************************      tabPage1 Codes          *********************************************************/
        
        private void LoadCustomerLogic()
        {
            txtCustAddressP1.Clear();
            txtCustomerNameP1.Clear();
            txtSearchCustomerP1.Clear();
            lblIdSelected.Text = "";
            chkForDelivery.Checked = false;
            if (rdoGuest.Checked)
            {
                txtSearchCustomerP1.Enabled = false;
                txtCustomerNameP1.ReadOnly = false;
                txtCustAddressP1.ReadOnly = false;
                chkForDelivery.Hide();
            }
            else if (rdoRegular.Checked)
            {
                txtSearchCustomerP1.Enabled = true;
                txtCustomerNameP1.ReadOnly = true;
                txtCustAddressP1.ReadOnly = true;
                chkForDelivery.Show();
            }
        }
        private void Load_dgvTransaction()
        {
            dgvTransaction.Columns["colItemCode"].Width = 80;
            dgvTransaction.Columns["colDesc"].Width = 140;
            dgvTransaction.Columns["colUnit"].Width = 50;
            dgvTransaction.Columns["colQty"].Width = 50;
            dgvTransaction.Columns["colDiscount"].Width = 50;
            dgvTransaction.Columns["colRemove"].Width = 80;
        }

        private void btnAddP1_Click(object sender, EventArgs e)//add item to cart
        {
            try
            {
                if (txtItemCodeP1.Text.Length == 8)
                {
                    Show_pnlInput(txtItemCodeP1.Text);
                }
                else
                {
                    string[] keyword = txtItemCodeP1.Text.Trim().Split(new char[] { '*' });//0 - item_id  1 - qty 2 - discount
                    item = new Item().Retrieve(keyword[0]);
                    if (string.IsNullOrEmpty(item.ItemId))
                    {
                        Message.ShowError("No Item Found.", "Error");
                    }
                    else
                    {
                        bool duplicate = false;
                        for (int i = 0; i < listItem.Count; i++)
                        {
                            if (listItem[i].ItemId == item.ItemId)
                            {
                                duplicate = true;
                                break;
                            }
                        }
                        if (duplicate)
                        {
                            Message.ShowError("Item is already in the cart.", "Error");
                        }
                        else
                        {
                            if (Convert.ToDecimal(keyword[1]) <= 0)
                            {
                                Message.ShowError("Quantity is not valid.", "Error");
                            }
                            else
                            {
                                if (item.Quantity >= Convert.ToDouble(keyword[1]))
                                {
                                    if (item.UnitCategory.WholeInSale && Convert.ToDouble(keyword[1]) % 1 != 0)
                                    {
                                        Message.ShowError("Please enter a valid quantity.", "Error");
                                    }
                                    else if (keyword.Length == 2)
                                    {
                                        SaleDetail saleDetail = new SaleDetail();
                                        saleDetail.Item = item;
                                        saleDetail.Sales.SaleId = lblP1InvoiceNo.Text;
                                        saleDetail.Price = item.Price;
                                        saleDetail.Quantity = Convert.ToDouble(keyword[1]);
                                        saleDetail.DiscountPercentage = 0;
                                        saleDetail.IsVatable = item.IsVatable;
                                        listSaleDetail.Add(saleDetail);
                                        listItem.Add(item);
                                        //string vatable = item.IsVatable ? "Yes" : "No";
                                        //dgvTransaction.Rows.Add(item.ItemId, item.GetDescription(), item.UnitCategory.Unit, Convert.ToDecimal(keyword[1]), "0", item.Price.ToString("N2"),vatable, ComputeSubTotal(item.Price, Convert.ToDecimal(keyword[1]), 0).ToString("N2"));
                                        //dgvTransaction.Rows.Add(item.ItemId, item.GetDescription(), item.UnitCategory.Unit, Convert.ToDecimal(keyword[1]), "0", item.Price.ToString("N2"), vatable, saleDetail.ComputeSubTotal().ToString("N2"));
                                        dgvTransaction.Rows.Add(item.ItemId, item.GetDescription(), item.UnitCategory.Unit, Convert.ToDecimal(keyword[1]), "0", item.GetPriceWithVatString(), saleDetail.ComputeSubTotalWithVat().ToString("N2"));
                                        txtItemCodeP1.Clear();
                                        Toggle_lblNoTransaction();
                                        
                                    }
                                    else if (keyword.Length == 3)
                                    {
                                        if (Convert.ToDecimal(keyword[2]) <= 100 && Convert.ToDecimal(keyword[2]) >= 0)
                                        {
                                            SaleDetail saleDetail = new SaleDetail();
                                            saleDetail.Item = item;
                                            saleDetail.Sales.SaleId = lblP1InvoiceNo.Text;
                                            saleDetail.Price = item.Price;
                                            saleDetail.Quantity = Convert.ToDouble(keyword[1]);
                                            saleDetail.DiscountPercentage = Convert.ToDouble(keyword[2]);
                                            saleDetail.IsVatable = item.IsVatable;
                                            listSaleDetail.Add(saleDetail);
                                            listItem.Add(item);
                                            //dgvTransaction.Rows.Add(item.ItemId, item.GetDescription(), item.UnitCategory.Unit, Convert.ToDecimal(keyword[1]), Convert.ToDecimal(keyword[2]), item.Price.ToString("N2"), ComputeSubTotal(item.Price, Convert.ToDecimal(keyword[1]), Convert.ToDecimal(keyword[2])).ToString("N2"));
                                            //dgvTransaction.Rows.Add(item.ItemId, item.GetDescription(), item.UnitCategory.Unit, Convert.ToDecimal(keyword[1]), Convert.ToDecimal(keyword[2]), item.Price.ToString("N2"), vatable, saleDetail.ComputeSubTotal().ToString("N2"));
                                            dgvTransaction.Rows.Add(item.ItemId, item.GetDescription(), item.UnitCategory.Unit, Convert.ToDecimal(keyword[1]), Convert.ToDecimal(keyword[2]), item.GetPriceWithVatString(), saleDetail.ComputeSubTotalWithVat().ToString("N2"));
                                            txtItemCodeP1.Clear();
                                            Toggle_lblNoTransaction();
                                        }
                                        else
                                        {
                                            Message.ShowError("Please enter a valid discount percentage.", "Error");
                                        }
                                    }
                                    UpdateInvoiceDetails();
                                }
                                else
                                {
                                    Message.ShowError("Item is out of stock. Only " + item.Quantity + " left.","Error");
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message,"Error");
            }
        }

        private void Show_pnlInput(string id)
        {
            item = new Item().Retrieve(id);
            if (string.IsNullOrEmpty(item.ItemId))
            {
                Message.ShowError("No Item Found.", "Error");
            }
            else
            {
                bool duplicate = false;
                for (int i = 0; i < listItem.Count; i++)
                {
                    if (listItem[i].ItemId == item.ItemId)
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (duplicate)
                {
                    Message.ShowError("Item is already in the cart.", "Error");
                }
                else
                {
                    pnlInput.Location = new Point(273, 147);
                    pnlInput.Visible = true;
                    pnlInput.BringToFront();
                    txtDescPInput.Text = item.GetDescription();
                    txtPricePInput.Text = item.GetPriceWithVatString();
                    txtUnitPInput.Text = item.UnitCategory.Unit;
                    this.ActiveControl = nudQtyPInput;
                    nudQtyPInput.Focus();
                }

            }
        }

        private void dgvTransaction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7 && e.RowIndex > -1)
            {
                listItem.RemoveAt(SearchItem(dgvTransaction.Rows[e.RowIndex].Cells["colItemCode"].Value.ToString()));
                listSaleDetail.RemoveAt(SearchSaleDetail(dgvTransaction.Rows[e.RowIndex].Cells["colItemCode"].Value.ToString()));
                dgvTransaction.Rows.RemoveAt(e.RowIndex);                
                Toggle_lblNoTransaction();
                if (dgvTransaction.Rows.Count <= 0)
                {
                    pnlEdit.Visible = false;
                }
                //MessageBox.Show(listSaleDetail.Count + " " + transaction.Count);
                UpdateInvoiceDetails();
            }
            else if (e.RowIndex > -1)
            {
                item = new Item().Retrieve(dgvTransaction.Rows[e.RowIndex].Cells["colItemCode"].Value.ToString());
                pnlEdit.Visible = true;
                txtEditDiscount.Text = dgvTransaction.Rows[e.RowIndex].Cells["colDiscount"].Value.ToString();
                txtEditQty.Text = dgvTransaction.Rows[e.RowIndex].Cells["colQty"].Value.ToString();
            }
        }

        private int SearchItem(string id)
        {
            int i = 0;
            for (i = 0; i < listItem.Count; i++)
            {
                if (listItem[i].ItemId == id)
                {
                    break;
                }
            }
            return i;
        }
        private int SearchSaleDetail(string id)
        {
            int i = 0;
            for (i = 0; i < listSaleDetail.Count; i++)
            {
                if (listSaleDetail[i].Item.ItemId == id)
                {
                    break;
                }
            }
            return i;
        }

        private void btnAddPInput_Click(object sender, EventArgs e) // Add Item
        {
            try
            {
                //if (txtUnitPInput.Text != "kg" && nudQtyPInput.Value % 1 != 0)
                //{
                //    Message.ShowError("Please enter a valid quantity.", "Error");
                //}
                if (item.UnitCategory.WholeInSale && nudQtyPInput.Value % 1 != 0)
                {
                    Message.ShowError("Please enter a valid quantity.", "Error");
                }
                else if (Convert.ToDouble(nudQtyPInput.Value) > item.Quantity)
                {
                    Message.ShowError("Item is out of stock. Only " + item.Quantity + " left.", "Error");
                }
                else
                {
                    //validate
                    Validation validation = new Validation();
                    if (validation.IsGreaterThanZero(nudQtyPInput.Value) && !validation.Is_Blank(txtDiscountPInput.Text))
                    {
                        SaleDetail saleDetail = new SaleDetail();
                        saleDetail.Item = item;
                        saleDetail.Sales.SaleId = lblP1InvoiceNo.Text;
                        saleDetail.Price = item.Price;
                        saleDetail.Quantity = Convert.ToDouble(nudQtyPInput.Value);
                        saleDetail.DiscountPercentage = Convert.ToDouble(txtDiscountPInput.Text);
                        saleDetail.IsVatable = item.IsVatable;
                        listSaleDetail.Add(saleDetail);

                        listItem.Add(item);
                        //dgvTransaction.Rows.Add(item.ItemId, item.GetDescription(), item.UnitCategory.Unit, nudQtyPInput.Value, txtDiscountPInput.Text, item.Price.ToString("N2"),vatable, ComputeSubTotal(item.Price, nudQtyPInput.Value, Convert.ToDecimal(txtDiscountPInput.Text)).ToString("N2"));
                        //dgvTransaction.Rows.Add(item.ItemId, item.GetDescription(), item.UnitCategory.Unit, nudQtyPInput.Value, txtDiscountPInput.Text, item.Price.ToString("N2"), vatable, saleDetail.ComputeSubTotal().ToString("N2"));
                        //dgvTransaction.Rows.Add(item.ItemId, item.GetDescription(), item.UnitCategory.Unit, Convert.ToDecimal(keyword[1]), "0", item.Price.ToString("N2"), vatable, saleDetail.ComputeSubTotal().ToString("N2"));
                        dgvTransaction.Rows.Add(item.ItemId, item.GetDescription(), item.UnitCategory.Unit, nudQtyPInput.Value, txtDiscountPInput.Text, item.GetPriceWithVatString(), saleDetail.ComputeSubTotalWithVat().ToString("N2"));
                        pnlInput.Visible = false;
                        txtItemCodeP1.Clear();
                        Toggle_lblNoTransaction();
                        txtSearch.Text = "";
                        nudQtyPInput.Value = 1;
                        txtDiscountPInput.Text = "0";
                        UpdateInvoiceDetails();
                        //PickLastItem();
                    }
                    else
                    {
                        Message.ShowError("Inputs Error", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message,"Error");
            }
        }
        //private void PickLastItem()
        //{
        //    pnlEdit.Visible = true;
        //    txtEditDiscount.Text = dgvTransaction.Rows[dgvTransaction.Rows.Count - 1].Cells["colDiscount"].Value.ToString();
        //    txtEditQty.Text = dgvTransaction.Rows[dgvTransaction.Rows.Count - 1].Cells["colQty"].Value.ToString();
        //}
        private void Toggle_lblNoTransaction()
        {
            lblNoTransaction.BringToFront();
            if (dgvTransaction.Rows.Count > 0)
            {
                lblNoTransaction.Visible = false;
            }
            else
            {
                lblNoTransaction.Visible = true;
            }
        }
        private decimal ComputeSubTotal(decimal price, decimal qty, decimal discount)
        {
            return (price * ((100 - discount) * 0.01M)) * qty;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                dgvItemChoose.Visible = false;
                lblNoItem.Visible = false;
            }
            else
            {
                dgvItemChoose.Location = new Point(135, 150);
                dgvItemChoose.Visible = true;
                Load_dgvItemChoose(new Item().Search(txtSearch.Text));
            }           
        }
        private void Load_dgvItemChoose(List<Item> listItem)
        {
            dgvItemChoose.Rows.Clear();
            dgvItemChoose.BringToFront();
            for (int i = 0; i < listItem.Count; i++)
            {
                dgvItemChoose.Rows.Add(listItem[i].ItemId, listItem[i].GetDescription(), listItem[i].Quantity, listItem[i].UnitCategory.Unit, listItem[i].GetPriceWithVatString());
            }
            if (dgvItemChoose.Rows.Count > 0)
            {
                lblNoItem.Visible = false;
            }
            else
            {
                lblNoItem.Visible = true;
            }
            dgvItemChoose.Columns[1].Width = 120;
            dgvItemChoose.Columns[3].Width = 50;
            dgvItemChoose.Columns[5].Width = 70;
            lblNoItem.BringToFront();
        }

        private void dgvItemChoose_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex > -1)
            {
                if (Convert.ToDouble(dgvItemChoose.Rows[e.RowIndex].Cells["colAvailQty"].Value.ToString()) > 0)
                {
                    Show_pnlInput(dgvItemChoose.Rows[e.RowIndex].Cells["colId"].Value.ToString());
                }
                else
                {
                    Message.ShowError("The item is out of stock.","Error");
                }
            }
        }

        private void btnIncQty_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtEditQty.Text) >= item.Quantity)
            {
                Message.ShowError("Item is out of stock. Only " + item.Quantity + " left.", "Error");
            }
            else
            {
                txtEditQty.Text = (Convert.ToDouble(txtEditQty.Text) + 1).ToString();
                dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells["colQty"].Value = txtEditQty.Text;
                listSaleDetail[SearchSaleDetail(dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells[0].Value.ToString())].Quantity = Convert.ToDouble(txtEditQty.Text);
                listSaleDetail[SearchSaleDetail(dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells[0].Value.ToString())].DiscountPercentage = Convert.ToDouble(txtEditDiscount.Text);
                UpdateSubTotal();
                UpdateInvoiceDetails();
            }
        }

        private void btnDecQty_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtEditQty.Text) > 1)
            {
                txtEditQty.Text = (Convert.ToDouble(txtEditQty.Text) - 1).ToString();
                dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells["colQty"].Value = txtEditQty.Text;
                listSaleDetail[SearchSaleDetail(dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells[0].Value.ToString())].Quantity = Convert.ToDouble(txtEditQty.Text);
                listSaleDetail[SearchSaleDetail(dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells[0].Value.ToString())].DiscountPercentage = Convert.ToDouble(txtEditDiscount.Text);
                UpdateSubTotal();
                UpdateInvoiceDetails();
            }
        }

        private void txtEditDiscount_TextChanged(object sender, EventArgs e)
        {
            if (txtEditDiscount.Text == "")
            {
                txtEditDiscount.Text = "0";
            }
            else if (Convert.ToDouble(txtEditDiscount.Text) > 100)
            {
                Message.ShowError("Invalid Discount.","Error");
                txtEditDiscount.Text = "0";
            }
            dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells["colDiscount"].Value = txtEditDiscount.Text;
            listSaleDetail[SearchSaleDetail(dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells[0].Value.ToString())].Quantity = Convert.ToDouble(txtEditQty.Text);
            listSaleDetail[SearchSaleDetail(dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells[0].Value.ToString())].DiscountPercentage = Convert.ToDouble(txtEditDiscount.Text);
            UpdateSubTotal();
            UpdateInvoiceDetails();
        }
        private void UpdateSubTotal()
        {
            try
            {
                //dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells["colSubTotal"].Value = ComputeSubTotal(Convert.ToDecimal(dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells["colPrice"].Value),
                //        Convert.ToDecimal(dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells["colQty"].Value), Convert.ToDecimal(dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells["colDiscount"].Value)).ToString("N2");
                dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells["colSubTotal"].Value = listSaleDetail[SearchSaleDetail(dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells[0].Value.ToString())].ComputeSubTotalWithVat().ToString("N2");
            }
            catch (Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
            }
        }

        private void txtEditDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions functions = new Functions();
            functions.AllowNumbersOnly(sender,e);
        }

        private void txtEditQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions functions = new Functions();
            functions.AllowNumbersOnly(sender, e);
        }

        private void txtEditQty_TextChanged(object sender, EventArgs e)
        {
            if (txtEditQty.Text == "")
            {
                txtEditQty.Text = "1";
            }

            if (item.UnitCategory.WholeInSale && Convert.ToDouble(txtEditQty.Text) % 1 != 0)
            {
                Message.ShowError("Please enter a valid quantity.", "Error");
                txtEditQty.Text = "1";
            }
            else
            {
                dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells["colQty"].Value = txtEditQty.Text;
                listSaleDetail[SearchSaleDetail(dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells[0].Value.ToString())].Quantity = Convert.ToDouble(txtEditQty.Text);
                listSaleDetail[SearchSaleDetail(dgvTransaction.Rows[dgvTransaction.CurrentCell.RowIndex].Cells[0].Value.ToString())].DiscountPercentage = Convert.ToDouble(txtEditDiscount.Text);
                UpdateSubTotal();
                UpdateInvoiceDetails();
            }
        }

        private void txtDiscountPInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions functions = new Functions();
            functions.AllowNumbersOnly(sender,e);
        }

        private void txtItemCodeP1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddP1.PerformClick();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlInput.Visible = false;
        }

        private void txtDiscountPInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                nudQtyPInput.Focus();
            }
        }

        private void nudQtyPInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddPInput.PerformClick();
            }
        }

        private void rdoGuest_CheckedChanged(object sender, EventArgs e)
        {
            LoadCustomerLogic();
        }

        private void rdoRegular_CheckedChanged(object sender, EventArgs e)
        {
            LoadCustomerLogic();
        }

        private void FrmTransaction_KeyUp(object sender, KeyEventArgs e)
        {         
            if (e.KeyCode == Keys.F2)
            {
                btnSettlePayment.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnCancel.PerformClick();
            }
        }

        private void txtItemCodeP1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnBackP2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btnAddCustomerP1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            NewCustomer();
            Load_dgvCustomersP2(new Customer().Retrieve());
        }
        
        private void btnSettlePayment_Click(object sender, EventArgs e)
        {
            //validate
            if (rdoRegular.Checked && lblIdSelected.Text == "")
            {
                Message.ShowError("Type of customer is regular but there is no selected customer","Error");
            }
            else if (txtCustomerNameP1.Text == "" && txtCustAddressP1.Text == "")
            {
                Message.ShowError("Type of customer is guest but there is no name and address cannot be blank!","Error");
            }
            else if (listSaleDetail.Count <= 0)
            {
                Message.ShowError("No Item in cart", "Error");
            }
            else
            {
                pnlTender.Visible = true;
                pnlTender.BringToFront();
                this.ActiveControl = txtORNo;
                txtORNo.Text = lblP1InvoiceNo.Text;
                txtTotalAmountpnlTendered.Text = txtTotalAmountP1.Text;
                txtCashTendered.Text = "0";
                //SaveTransaction();
            }
        }
        private void SaveTransaction()
        {
            try
            {
                Sale saleTransaction = new Sale();
                saleTransaction.SaleId = lblP1InvoiceNo.Text;
                saleTransaction.DateTime = DateTime.Now;
                saleTransaction.Staff.StaffId = LoginInfo.StaffId;
                saleTransaction.TotalPrice = Convert.ToDecimal(txtTotalAmountP1.Text);
                saleTransaction.OrNo = txtORNo.Text;
                saleTransaction.VATable = Convert.ToDecimal(txtVATableP1.Text);
                saleTransaction.VAT_tax = Convert.ToDecimal(txtVatTax.Text);
                saleTransaction.SaleDetails = listSaleDetail;

                bool successful = saleTransaction.InsertRecord();

                if (rdoRegular.Checked)
                {
                    SaleCustomer saleCustomer = new SaleCustomer();
                    saleCustomer.Customer.CustomerId = lblIdSelected.Text;
                    saleCustomer.Sale.SaleId = lblP1InvoiceNo.Text;
                    saleCustomer.InsertRecord();
                }

                if (successful)
                {
                    if (chkForDelivery.Checked)
                    {
                        FrmDeliveryScheduling frmScheduling = new FrmDeliveryScheduling(saleTransaction.SaleId);
                        frmScheduling.ShowDialog();
                        FrmPrintReceipt frmPrintReceipt = new FrmPrintReceipt(saleTransaction.SaleId, txtORNo.Text, txtCustomerNameP1.Text, txtCustAddressP1.Text, Convert.ToDecimal(txtTotalDiscountP1.Text), Convert.ToDecimal(txtCashTendered.Text), txtVATableP1.Text, txtVatTax.Text, txtNonVatable.Text);
                        frmPrintReceipt.ShowDialog();
                    }
                    else
                    {
                        FrmPrintReceipt frmPrintReceipt = new FrmPrintReceipt(saleTransaction.SaleId, txtORNo.Text, txtCustomerNameP1.Text, txtCustAddressP1.Text, Convert.ToDecimal(txtTotalDiscountP1.Text), Convert.ToDecimal(txtCashTendered.Text), txtVATableP1.Text, txtVatTax.Text, txtNonVatable.Text);
                        frmPrintReceipt.ShowDialog();
                    }
                }


                //clear inputs
                NewTransaction();
            }
            catch(Exception ex)
            {
                Message.ShowError("An Error Has Occured: " + ex.Message, "Error");
            }
        }
        private void btnCancelTransaction_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel the transaction?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                NewTransaction();
            }
        }
        private void btnNewTransaction_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Current transaction will be cancelled. Are you sure?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                NewTransaction();
            }
        }
        private void NewTransaction()
        {
            lblP1InvoiceNo.Text = new Sale().GenerateID();
            listItem = new List<Item>(); // list of items
            item = new Item();
            listSaleDetail = new List<SaleDetail>(); //list of sale details
            dgvTransaction.Rows.Clear();
            Toggle_lblNoTransaction();
            pnlEdit.Visible = false;
            rdoGuest.Checked = true;
            UpdateInvoiceDetails();
            txtCustomerNameP1.Clear();
            txtCustAddressP1.Clear();
        }
        private void btnCancelpnlTender_Click(object sender, EventArgs e)
        {
            pnlTender.Visible = false;
        }

        private void btnFinishTransaction_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtTotalAmountpnlTendered.Text) > Convert.ToDecimal(txtCashTendered.Text))
            {
                Message.ShowError("Amount Tendered is less than the total amount.", "Error");
            }
            else if (new Validation().Is_Blank(txtORNo.Text))
            {
                Message.ShowError("OR number cannot be blank.", "Error");
            }
            else if (new Sale().CheckIfExisting(txtORNo.Text))
            {
                Message.ShowError("OR number is already used.", "Error");
            }
            else
            {
                SaveTransaction();
                pnlTender.Visible = false;
            }
        }
        private void txtCashTendered_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (txtCashTendered.Text == "")
                {
                    txtCashTendered.Text = "0";
                    txtChange.Text = "0.00";
                }
                txtChange.Text = (Convert.ToDecimal(txtCashTendered.Text) - Convert.ToDecimal(txtTotalAmountpnlTendered.Text)).ToString("N2");
                if (Convert.ToDecimal(txtChange.Text) < 0)
                {
                    txtChange.Text = "0.00";
                }
            }
            catch (Exception)
            {
                txtCashTendered.Text = "0";
                txtChange.Text = "0.00";
            }
        }
        private void txtCashTendered_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions functions = new Functions();
            functions.AllowNumbersOnly(sender, e);
        }
        private void txtCashTendered_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFinishTransaction.PerformClick();
            }
        }


        /****************************************   tabPage2 Codes    ********************************************************/
        private void NewCustomer()
        {
            txtCustIdP2.Text = new Customer().GenerateID();
            txtFNameP2.Clear();
            txtLNameP2.Clear();
            txtAddressP2.Clear();
            txtContactP2.Clear();
        }
        private void btnSaveP2_Click(object sender, EventArgs e)
        {
            Customer newCustomer = new Customer();
            newCustomer.CustomerId = txtCustIdP2.Text;
            newCustomer.FirstName = txtFNameP2.Text;
            newCustomer.LastName = txtLNameP2.Text;
            newCustomer.Address = txtAddressP2.Text;
            newCustomer.ContactNo = txtContactP2.Text;

            if (newCustomer.Validate() == "")
            {
                newCustomer.InsertRecord();
                NewCustomer();
                Load_dgvCustomersP2(new Customer().Retrieve());
            }
            else
            {
                Message.ShowError(newCustomer.Validate(),"Please fix the following error: ");
            }
        }
        private void Load_dgvCustomersP2(List<Customer> listCustomer)
        {
            dgvCustomersP2.Rows.Clear();
            for (int i = 0; i < listCustomer.Count; i++)
            {
                dgvCustomersP2.Rows.Add(listCustomer[i].CustomerId,listCustomer[i].GetFullName(),listCustomer[i].Address,listCustomer[i].ContactNo,listCustomer[i].RecordDate.ToShortDateString());
            }
            colIdCust.Width = 80;
            colFullNameCust.Width = 120;
            colAddressCust.Width = 120;
            colContactNoCust.Width = 110;
        }
        private void Load_dgvSelectCustomerP1(List<Customer> listCustomer)
        {
            dgvSelectCustomerP1.Rows.Clear();
            for (int i = 0; i < listCustomer.Count; i++)
            {
                dgvSelectCustomerP1.Rows.Add(listCustomer[i].CustomerId, listCustomer[i].GetFullName(), listCustomer[i].Address, listCustomer[i].ContactNo);
            }
        }

        private void txtSearchCustomerP1_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchCustomerP1.Text == "")
            {
                dgvSelectCustomerP1.Visible = false;
            }
            else
            {
                dgvSelectCustomerP1.Visible = true;
                Load_dgvSelectCustomerP1(new Customer().Search(txtSearchCustomerP1.Text));
            }
            dgvSelectCustomerP1.Columns[0].Width = 100;
            dgvSelectCustomerP1.Columns[1].Width = 150;
        }

        private void dgvSelectCustomerP1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex > -1)
            {
                lblIdSelected.Text = dgvSelectCustomerP1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtCustomerNameP1.Text = dgvSelectCustomerP1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtCustAddressP1.Text = dgvSelectCustomerP1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtSearchCustomerP1.Text = "";
            }
        }
        private void UpdateInvoiceDetails()
        {
            decimal totalAmount = 0;
            decimal totalDiscount = 0;
            decimal totalVatable = 0;
            decimal totalVatTax = 0;
            decimal totalNonVatable = 0;
            for (int i = 0; i < listSaleDetail.Count; i++)
            {
                //decimal subTotal = ComputeSubTotal(listSaleDetail[i].Price,Convert.ToDecimal(listSaleDetail[i].Quantity),Convert.ToDecimal(listSaleDetail[i].DiscountPercentage));
                decimal subTotal = listSaleDetail[i].ComputeSubTotalWithVat();
                totalAmount += subTotal;
                //totalDiscount += ((listSaleDetail[i].Price * Convert.ToDecimal(listSaleDetail[i].Quantity)) + (listSaleDetail[i].Price * Convert.ToDecimal(listSaleDetail[i].Quantity) * .12) - subTotal);
                totalDiscount += listSaleDetail[i].ComputeDiscount();
                totalVatable += listSaleDetail[i].ComputeVatable();
                totalVatTax += listSaleDetail[i].ComputeVATTax();
                totalNonVatable += listSaleDetail[i].ComputeNonVatable();
            }
            txtVATableP1.Text = totalVatable.ToString("N2");
            txtTotalAmountP1.Text = totalAmount.ToString("N2");
            txtTotalDiscountP1.Text = totalDiscount.ToString("N2");
            txtVatTax.Text = totalVatTax.ToString("N2");
            txtNonVatable.Text = totalNonVatable.ToString("N2");
        }

        private void txtORNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions function = new Functions();
            function.AllowNumbersOnly(sender,e);
        }

        private void dgvCustomersP2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex > -1)
            {
                FrmEditCustomer frmEditCustomer = new FrmEditCustomer(dgvCustomersP2.Rows[e.RowIndex].Cells[0].Value.ToString());
                frmEditCustomer.ShowDialog();
                Load_dgvCustomersP2(new Customer().Retrieve());
            }
        }

        /**********************************         tabPage3 Codes               **************************************************/

        private void btnBackP3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            Load_dgvSales(new Sale().Retrieve());
            lblDateDetail.Text = "Today, " + DateTime.Now.DayOfWeek;
            dtpFilterSales.Value = DateTime.Now;
            Load_lblSalesToday();
        }

        private void Load_dgvSales(List<Sale> listSale)
        {
            dgvSales.Rows.Clear();
            for (int i = 0; i < listSale.Count; i++)
            {
                dgvSales.Rows.Add(listSale[i].SaleId,listSale[i].OrNo,listSale[i].Staff.GetFullName(),listSale[i].DateTime.ToString(),listSale[i].SaleDetails.Count,listSale[i].VATable.ToString("N2"),listSale[i].VAT_tax.ToString("N2"),listSale[i].TotalPrice.ToString("N2"));
            }
            Load_lblCountShow();
            if (dgvSales.Rows.Count > 0)
            {
                lblNoResult.Visible = false;
            }
            else
            {
                lblNoResult.Visible = true;
            }
        }

        private void dtpFilterSales_ValueChanged(object sender, EventArgs e)
        {
            Load_lblDateDetail();
            //Load_dgvSales(new Sale().Retrieve());
            Load_dgvSales(new Sale().Retrieve(dtpFilterSales.Value));
        }
        private void Load_lblDateDetail()
        {
            if (dtpFilterSales.Value.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                lblDateDetail.Text = "Today, " + DateTime.Now.DayOfWeek;
            }
            else if (dtpFilterSales.Value.ToString("yyyy-MM-dd") == DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"))
            {
                lblDateDetail.Text = "Yesterday, " + dtpFilterSales.Value.DayOfWeek;
            }
            else
            {
                lblDateDetail.Text = dtpFilterSales.Value.DayOfWeek.ToString();
            }
            
        }

        private void Load_lblCountShow()
        {
            Sale sale = new Sale();
            lblCountShow.Text = "Showing " + dgvSales.Rows.Count + " of " + sale.CountSales() + " sales.";
        }
        private void Load_lblSalesToday()
        {
            Sale sale = new Sale();
            lblSalesToday.Text = "Total of Sales Today : " + sale.CountSales(DateTime.Now);
        }
        private void dgvSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8 && e.RowIndex > -1)
            {
                tabControl1.SelectedIndex = 3;
                Sale sale = new Sale().Retrieve(dgvSales.Rows[e.RowIndex].Cells[0].Value.ToString());
                Load_dgvSaleDetails(new SaleDetail().Retrieve(sale.SaleId));
                txtP4SaleNo.Text = sale.SaleId;
                txtP4OrNo.Text = sale.OrNo;
                txtP4DateTime.Text = sale.DateTime.ToString();
                txtP4Staff.Text = sale.Staff.GetFullName();
                txtP4Vatable.Text = sale.VATable.ToString("N2");
                txtP4VatTax.Text = sale.VAT_tax.ToString("N2");
                txtP4TotalAmt.Text = sale.TotalPrice.ToString("N2");
                txtP4NoOfItems.Text = sale.SaleDetails.Count.ToString();
                txtP4TotalQty.Text = sale.ComputeTotalQty().ToString();
                //retrieve customer
                Customer customer = new SaleCustomer().GetCustomer(sale.SaleId);
                if (!string.IsNullOrEmpty(customer.CustomerId))
                {
                    txtP4CustomerType.Text = "Regular Customer";
                    pnlP4Customer.Visible = true;
                    txtP4CustomerId.Text = customer.CustomerId;
                    txtP4CustomerName.Text = customer.GetFullName();
                    txtP4Address.Text = customer.Address;
                    txtP4ContactNo.Text = customer.ContactNo;
                }
                else
                {
                    txtP4CustomerType.Text = "Guest Customer";
                }
            }
        }

        /*********************************      tabPage4 Codes             *********************************************/

        private void btnBackP4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            pnlP4Customer.Visible = false;
            txtP4CustomerName.Clear();
            txtP4Address.Clear();
            txtP4ContactNo.Clear();
        }

        private void Load_dgvSaleDetails(List<SaleDetail> listDetail)
        {
            dgvSaleDetails.Rows.Clear();
            for (int i = 0; i < listDetail.Count; i++)
            {
                dgvSaleDetails.Rows.Add(listDetail[i].Item.ItemId, listDetail[i].Item.GetDescription(), listDetail[i].Item.UnitCategory.Unit, listDetail[i].Quantity, listDetail[i].DiscountPercentage, listDetail[i].GetPriceWithVatString(), listDetail[i].ComputeSubTotal().ToString("N2"));
            }
        }

        private void btnReturnItem_Click(object sender, EventArgs e)
        {
            
            FrmReturn frmItemReturn = new FrmReturn();
            frmItemReturn.ShowDialog();
            CountReturns();
            

        }
        private void CountReturns()
        {
            int count = new Return().Count(DateTime.Now);
            
            if (count > 0)
            {
                lblCountReturs.Show();
                lblCountReturs.Text = count.ToString();
            }
            else
            {
                lblCountReturs.Hide();
            }
        }

        private void btnListReturns_Click(object sender, EventArgs e)
        {
            FrmListReturns frmListReturns = new FrmListReturns();
            frmListReturns.ShowDialog();
            CountReturns();
        }

        private void cboChooseFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChooseFilter.SelectedIndex == 0)
            {
                Load_dgvSales(new Sale().Retrieve());
                pnlFilterStaff.Hide();
                pnlFilterDate.Hide();
                pnlFilterDateRange.Hide();
            }
            else if (cboChooseFilter.SelectedIndex == 1)
            {
                pnlFilterStaff.Hide();
                pnlFilterDate.Show();
                pnlFilterDate.BringToFront();
                pnlFilterDateRange.Hide();
                //dtpFilterSales.MaxDate = DateTime.Now;
                dtpFilterSales.Value = DateTime.Now;
                
                Load_dgvSales(new Sale().Retrieve(dtpFilterSales.Value));
            }
            else if (cboChooseFilter.SelectedIndex == 2)
            {
                pnlFilterStaff.Show();
                pnlFilterStaff.BringToFront();
                pnlFilterDate.Hide();
                pnlFilterDateRange.Hide();
                //Load_dgvSales(new Sale().RetrieveByStaff(new Staff().RetrieveByName(cboSelectStaff.Text)));
                cboSelectStaff.SelectedIndex = 0;
            }
            else if (cboChooseFilter.SelectedIndex == 3)
            {
                pnlFilterStaff.Hide();
                pnlFilterDate.Hide();
                pnlFilterDateRange.Show();
                pnlFilterDateRange.BringToFront();
                dtpFrom.Value = DateTime.Now.AddDays(-1);
                dtpTo.Value = DateTime.Now;
                Load_dgvSales(new Sale().Retrieve(dtpFrom.Value, dtpTo.Value));
            }

        }
        private void Load_cboSelectStaff()
        {
            List<Staff> listStaff = new Staff().Retrieve();
            for (int i = 0; i < listStaff.Count; i++)
            {
                cboSelectStaff.Items.Add(listStaff[i].GetFullName());
            }
            //cboSelectStaff.SelectedIndex = 0;
        }

        private void cboSelectStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSelectStaff.Text == "All")
            {
                Load_dgvSales(new Sale().Retrieve());
            }
            else
            {
                Load_dgvSales(new Sale().RetrieveByStaff(new Staff().RetrieveByName(cboSelectStaff.Text)));
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            Load_dgvSales(new Sale().Retrieve(dtpFrom.Value,dtpTo.Value));
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            Load_dgvSales(new Sale().Retrieve(dtpFrom.Value, dtpTo.Value));
        }

        //print sales
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvSales.Rows.Count > 0)
            {
                FrmPrintSales frmPrintSales = new FrmPrintSales();
                if (cboChooseFilter.SelectedIndex == 1)
                {
                    frmPrintSales = new FrmPrintSales(dtpFilterSales.Value);
                }
                else if (cboChooseFilter.SelectedIndex == 2)
                {
                    frmPrintSales = new FrmPrintSales(new Staff().RetrieveByName(cboSelectStaff.Text));
                    if (cboSelectStaff.Text == "All")
                    {
                        frmPrintSales = new FrmPrintSales();
                    }
                }
                else if (cboChooseFilter.SelectedIndex == 3)
                {
                    frmPrintSales = new FrmPrintSales(dtpFrom.Value, dtpTo.Value);
                }
                frmPrintSales.ShowDialog();
            }
            else
            {
                Message.ShowError("No data.", "Error");
            }
        }

        private void txtContactP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions functions = new Functions();
            functions.AllowNumbersOnly(sender,e);
        }

        private void txtORNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
