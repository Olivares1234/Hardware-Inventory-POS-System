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
    public partial class FrmViewItem : Form
    {
        string itemId = "";
        //string updateColumn = "";
        public FrmViewItem()
        {
            InitializeComponent();
        }
        public FrmViewItem(string itemId) : this()
        {
            this.itemId = itemId;
        }

        private void FrmViewItem_Load(object sender, EventArgs e)
        {
            Load_ItemDetails();
            Load_dgvDetails();
        }
        private void Load_ItemDetails()
        {
            Item item = new Item();
            item = item.Retrieve(itemId);
            lblItemId.Text = item.ItemId;
            lblItemName.Text = item.ItemName;
            lblPrice.Text = item.Price.ToString("N2");
            lblQuantity.Text = item.Quantity.ToString();
            lblCategory.Text = item.Category.Description;
            lblUnit.Text = item.UnitCategory.Unit;
            lblSRP.Text = item.SRP.ToString("N2");
            lblVatable.Text = item.IsVatable ? "Yes" : "No";
            lblPriceV.Text = item.GetPriceWithVatString();
        }
        private void Load_dgvDetails()
        {
            List<ItemDetail> listItemDetails = new ItemDetail().Retrieve(itemId);
            dgvDetails.Rows.Clear();
            for (int i = 0; i < listItemDetails.Count; i++)
            {
                dgvDetails.Rows.Add(listItemDetails[i].DetailId,listItemDetails[i].ItemAttribute.Description,listItemDetails[i].DetailDescription);
            }
            dgvDetails.Columns["colId"].Width = 50;
        }

        //private void btnAddDetail_Click(object sender, EventArgs e)
        //{
        //    Validation validation = new Validation();
        //    if (validation.Is_Blank(txtDetailName.Text) || validation.Is_Blank(txtDetailDesc.Text))
        //    {
        //        Message.ShowError("Inputs cannot be blank.", "Please fix");
        //    }
        //    else
        //    {
        //        ItemDetail itemDetail = new ItemDetail();
        //        itemDetail.ItemId = itemId;
        //        itemDetail.DetailName = txtDetailName.Text;
        //        itemDetail.DetailDescription = txtDetailDesc.Text;
        //        itemDetail.InsertRecord();
        //        Message.ShowSuccess("New Detail added successfully.","Message");
        //        Load_dgvDetails();
        //    }
        //}

        private void dgvDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex > -1)
            {
                FrmAddEditDetail frmAddEditDetail = new FrmAddEditDetail(Convert.ToInt32(dgvDetails.Rows[e.RowIndex].Cells["colId"].Value.ToString()));
                frmAddEditDetail.ShowDialog();
                Load_dgvDetails();
            }
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            FrmAddEditDetail frmAddEditDetail = new FrmAddEditDetail(itemId);
            frmAddEditDetail.ShowDialog();
            Load_dgvDetails();
        }

        private void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            FrmUpdateItem frmUpdateItem = new FrmUpdateItem(itemId);
            frmUpdateItem.ShowDialog();
            Load_ItemDetails();
        }

        //private void btnUpdatePrice_Click(object sender, EventArgs e)
        //{
        //    pnlEditValue.Visible = true;
        //    lblEdit.Text = "New Price:";
        //    updateColumn = "price";
        //}

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    pnlEditValue.Visible = false;
        //}

        //private void btnEditName_Click(object sender, EventArgs e)
        //{
        //    pnlEditValue.Visible = true;
        //    lblEdit.Text = "New Name:";
        //    updateColumn = "item_name";
        //}

    }
}
