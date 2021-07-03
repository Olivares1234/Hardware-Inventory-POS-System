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
    public partial class FrmReturn : Form
    {
        List<ItemReturn> listReturn = new List<ItemReturn>();
        string returnId = "";
        decimal totalReturnAmt = 0;
        string orNo = "";
        public FrmReturn()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            orNo = txtOrNo.Text;
            Sale sale = new Sale();
            try
            {
                if (!(new Validation().Is_Blank(txtOrNo.Text)))
                {
                    if (sale.CheckIfExisting(txtOrNo.Text))
                    {
                        if (!(new Return().CheckIfAlreadyReturned(txtOrNo.Text)))
                        {
                            sale = new Sale().RetrieveByORNo(txtOrNo.Text);
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
                        else
                        {
                            Message.ShowError("the sale with OR No REF of : " + txtOrNo.Text + " is already returned once. You cannot return it again.", "Already returned");
                            Clear();
                        }
                    }
                    else
                    {
                        Message.ShowError("There is no existing sale with OR No REF of : " + txtOrNo.Text, "Not found.");
                        Clear();
                    }
                }
                else
                {
                    Message.ShowError("Please enter the OR No. REF", "Cannot be blank.");
                }
            }
            catch (Exception ex)
            {
                Message.ShowError("An error has occured :" + ex.Message,"Error");
            }
        }
        private void Load_dgvSaleDetails(List<SaleDetail> listDetail)
        {
            dgvSaleDetails.Rows.Clear();
            for (int i = 0; i < listDetail.Count; i++)
            {
                dgvSaleDetails.Rows.Add(listDetail[i].Item.ItemId, listDetail[i].Item.GetDescription(), listDetail[i].Item.UnitCategory.Unit, listDetail[i].Quantity, listDetail[i].DiscountPercentage, listDetail[i].GetPriceWithVatString(), listDetail[i].ComputeSubTotal().ToString("N2"));
            }
        }

        private void FrmItemReturn_Load(object sender, EventArgs e)
        {
            returnId = new Return().GenerateID();
            lblReturnId.Text = "#" + returnId;
        }

        private void txtOrNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions functions = new Functions();
            functions.AllowNumbersOnly(sender,e);
        }
        private void Clear()
        {
            txtP4SaleNo.Text = "--";
            txtP4OrNo.Text = "--";
            txtP4DateTime.Text = "--";
            txtP4Staff.Text = "--";
            txtP4Vatable.Text = "--";
            txtP4VatTax.Text = "--";
            txtP4TotalAmt.Text = "--";
            txtP4NoOfItems.Text = "--";
            txtP4TotalQty.Text = "--";
            txtP4CustomerType.Text = "--";
            pnlP4Customer.Visible = false;
            listReturn.Clear();
            dgvSaleDetails.Rows.Clear();
        }

        private void dgvSaleDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7 && e.RowIndex > -1)
            {
                if (CheckIfExisting(dgvSaleDetails.Rows[e.RowIndex].Cells[0].Value.ToString()))
                {
                    Message.ShowError("The item is already in the return cart", "Error");
                }
                else
                {
                    Item item = new Item().Retrieve(dgvSaleDetails.Rows[e.RowIndex].Cells[0].Value.ToString());
                    //ItemReturn itemReturn = new ItemReturn();
                    //itemReturn.Item = item;
                    //listReturn.Add(itemReturn);
                    //Load_dgvReturnItem();
                    lblDescription.Text = item.GetDescription();
                    pnlInfo.Visible = true;
                    txtPrice.Text = dgvSaleDetails.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtItmUnit.Text = item.UnitCategory.Unit;
                    txtItemId.Text = item.ItemId;
                    txtQtySold.Text = dgvSaleDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cboCondition.SelectedIndex = 0;
                    if (txtPrice.Text[txtPrice.Text.Length - 1] == 'V')
                    {
                        txtPrice.Text = txtPrice.Text.Substring(0, txtPrice.Text.Length - 1);
                    }
                }
            }
        }
        private void Load_dgvReturnItem()
        {
            dgvReturnItem.Rows.Clear();
            for (int i = 0; i < listReturn.Count; i++)
            {
                dgvReturnItem.Rows.Add(listReturn[i].Item.ItemId, listReturn[i].Item.GetDescription(),listReturn[i].Item.UnitCategory.Unit,listReturn[i].Quantity,
                    listReturn[i].ReturnAmount, (Convert.ToDecimal(listReturn[i].Quantity) * listReturn[i].ReturnAmount).ToString("N2"), listReturn[i].ReasonForReturn, listReturn[i].ItemCondition);
            }
            dgvReturnItem.Columns[0].Width = 80;
            dgvReturnItem.Columns[1].Width = 200;
            dgvReturnItem.Columns[2].Width = 50;
            dgvReturnItem.Columns[3].Width = 50;
            dgvReturnItem.Columns[4].Width = 100;
            dgvReturnItem.Columns[5].Width = 100;
            dgvReturnItem.Columns[6].Width = 200;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlInfo.Visible = false;
            txtReason.Clear();
            nudQty.Value = 0;
            txtReturnAmt.Clear();
        }

        private void txtReturnAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions functions = new Functions();
            functions.AllowNumbersOnly(sender,e);
        }
        private bool CheckIfExisting(string itemId)
        {
            bool exist = false;
            for (int i = 0; i < listReturn.Count; i++)
            {
                if (listReturn[i].Item.ItemId == itemId)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Validation validation = new Validation();
            Item item = new Item().Retrieve(txtItemId.Text);
            //validation
            if (item.UnitCategory.WholeInSale && nudQty.Value % 1 != 0)
            {
                Message.ShowError("The unit of selected item is whole in sale. The valid quantity are: 1,2,3 ...", "Error");
            }
            else if (validation.Is_Blank(txtReturnAmt.Text))
            {
                Message.ShowError("The return amount cannot be blank.", "Error");
            }
            else if (Convert.ToDecimal(txtPrice.Text) < Convert.ToDecimal(txtReturnAmt.Text))
            {
                Message.ShowError("The return amount cannot be greater than the price.", "Error");
            }
            else if (Convert.ToDouble(txtQtySold.Text) < Convert.ToDouble(nudQty.Value) || Convert.ToDouble(nudQty.Value) <= 0)
            {
                Message.ShowError("The quantity is not valid.", "Error");
            }
            else if (validation.Is_Blank(txtReason.Text))
            {
                Message.ShowError("Reason for return cannot be blank.", "Error");
            }
            else
            {
                //valid
                ItemReturn itemReturn = new ItemReturn();
                itemReturn.Item = item;
                itemReturn.Quantity = Convert.ToDouble(nudQty.Value);
                itemReturn.ItemPrice = Convert.ToDecimal(txtPrice.Text);
                itemReturn.ReasonForReturn = txtReason.Text;
                itemReturn.ItemCondition = cboCondition.Text;
                itemReturn.ReturnId = returnId;
                itemReturn.ReturnAmount = Convert.ToDecimal(txtReturnAmt.Text);
                listReturn.Add(itemReturn);
                Load_dgvReturnItem();
                pnlInfo.Hide();
                totalReturnAmt += (itemReturn.ReturnAmount * Convert.ToDecimal(itemReturn.Quantity));
                txtTotalReturnAmount.Text = totalReturnAmt.ToString("N2");
            }
        }

        private void dgvReturnItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 8)
            {
                if (MessageBox.Show("Are you sure you want to remove?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    totalReturnAmt -= Convert.ToDecimal(dgvReturnItem.Rows[e.RowIndex].Cells[5].Value.ToString());
                    txtTotalReturnAmount.Text = totalReturnAmt.ToString("N2");
                    RemoveFromListReturn(dgvReturnItem.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Load_dgvReturnItem();
                    
                }
            }
        }
        private void RemoveFromListReturn(string itemId)
        {
            for (int i = 0; i < listReturn.Count; i++)
            {
                if (listReturn[i].Item.ItemId == itemId)
                {
                    listReturn.RemoveAt(i);
                    break;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (listReturn.Count > 0)
            {
                try
                {
                    Return saleReturn = new Return();
                    saleReturn.ReturnId = returnId;
                    saleReturn.ListItemReturn = listReturn;
                    saleReturn.TotalAmountReturned = totalReturnAmt;
                    saleReturn.Staff.StaffId = LoginInfo.StaffId;
                    saleReturn.OrRefNumber = orNo;
                    saleReturn.InsertRecord();
                    this.Close();
                }
                catch (Exception ex)
                {
                    Message.ShowError("An Error has occured: " + ex.Message, "Error");
                }
            }
            else
            {
                Message.ShowError("The item return list blank.", "Error");
            }
        }
    }
}
