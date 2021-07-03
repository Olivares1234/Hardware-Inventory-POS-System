namespace Hardware_MS
{
    partial class FrmInventory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVatable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPriceV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBtnInfo = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblNoResult = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboFilterCategory = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNewSupplyCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCountShow = new System.Windows.Forms.Label();
            this.btnAddAttribute = new System.Windows.Forms.Button();
            this.btnViewSupplies = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnNewSupply = new System.Windows.Forms.Button();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnAddUnit = new System.Windows.Forms.Button();
            this.btnBrokenItem = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboStockLevel = new System.Windows.Forms.ComboBox();
            this.lblOutOfStock = new System.Windows.Forms.Label();
            this.lblLowStock = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeColumns = false;
            this.dgvItems.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvItems.ColumnHeadersHeight = 40;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colDescription,
            this.colPrice,
            this.colSRP,
            this.colVatable,
            this.colPriceV,
            this.colQty,
            this.colUnit,
            this.colCategory,
            this.colBtnInfo});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvItems.EnableHeadersVisualStyles = false;
            this.dgvItems.Location = new System.Drawing.Point(17, 171);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvItems.RowHeadersVisible = false;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.Black;
            this.dgvItems.RowsDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvItems.RowTemplate.Height = 40;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(943, 354);
            this.dgvItems.TabIndex = 6;
            this.dgvItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellClick);
            // 
            // colId
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colId.DefaultCellStyle = dataGridViewCellStyle13;
            this.colId.FillWeight = 101.5228F;
            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.FillWeight = 99.49239F;
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // colPrice
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPrice.DefaultCellStyle = dataGridViewCellStyle14;
            this.colPrice.FillWeight = 99.49239F;
            this.colPrice.HeaderText = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            // 
            // colSRP
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSRP.DefaultCellStyle = dataGridViewCellStyle15;
            this.colSRP.HeaderText = "SRP";
            this.colSRP.Name = "colSRP";
            this.colSRP.ReadOnly = true;
            // 
            // colVatable
            // 
            this.colVatable.HeaderText = "Vatable";
            this.colVatable.Name = "colVatable";
            this.colVatable.ReadOnly = true;
            // 
            // colPriceV
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPriceV.DefaultCellStyle = dataGridViewCellStyle16;
            this.colPriceV.HeaderText = "PriceV";
            this.colPriceV.Name = "colPriceV";
            this.colPriceV.ReadOnly = true;
            // 
            // colQty
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colQty.DefaultCellStyle = dataGridViewCellStyle17;
            this.colQty.FillWeight = 99.49239F;
            this.colQty.HeaderText = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;
            // 
            // colUnit
            // 
            this.colUnit.HeaderText = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;
            this.colUnit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCategory
            // 
            this.colCategory.HeaderText = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            // 
            // colBtnInfo
            // 
            this.colBtnInfo.HeaderText = "Action";
            this.colBtnInfo.Name = "colBtnInfo";
            this.colBtnInfo.ReadOnly = true;
            this.colBtnInfo.Text = "View";
            this.colBtnInfo.UseColumnTextForButtonValue = true;
            // 
            // lblNoResult
            // 
            this.lblNoResult.AutoSize = true;
            this.lblNoResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoResult.ForeColor = System.Drawing.Color.Silver;
            this.lblNoResult.Location = new System.Drawing.Point(401, 332);
            this.lblNoResult.Name = "lblNoResult";
            this.lblNoResult.Size = new System.Drawing.Size(151, 25);
            this.lblNoResult.TabIndex = 10;
            this.lblNoResult.Text = "No item found.";
            this.lblNoResult.Visible = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(127, 126);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(222, 26);
            this.txtSearch.TabIndex = 17;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // cboFilterCategory
            // 
            this.cboFilterCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterCategory.FormattingEnabled = true;
            this.cboFilterCategory.Items.AddRange(new object[] {
            "All"});
            this.cboFilterCategory.Location = new System.Drawing.Point(809, 126);
            this.cboFilterCategory.Name = "cboFilterCategory";
            this.cboFilterCategory.Size = new System.Drawing.Size(151, 28);
            this.cboFilterCategory.TabIndex = 18;
            this.cboFilterCategory.SelectedIndexChanged += new System.EventHandler(this.cboFilterCategory_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(671, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Filter by Category";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 29);
            this.label2.TabIndex = 20;
            this.label2.Text = "Item List";
            // 
            // lblNewSupplyCount
            // 
            this.lblNewSupplyCount.AutoSize = true;
            this.lblNewSupplyCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblNewSupplyCount.ForeColor = System.Drawing.Color.White;
            this.lblNewSupplyCount.Location = new System.Drawing.Point(788, 2);
            this.lblNewSupplyCount.Name = "lblNewSupplyCount";
            this.lblNewSupplyCount.Size = new System.Drawing.Size(18, 20);
            this.lblNewSupplyCount.TabIndex = 22;
            this.lblNewSupplyCount.Text = "0";
            this.lblNewSupplyCount.Click += new System.EventHandler(this.lblNewSupplyCount_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 23;
            this.label3.Text = "Search Item: ";
            // 
            // lblCountShow
            // 
            this.lblCountShow.AutoSize = true;
            this.lblCountShow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.lblCountShow.Location = new System.Drawing.Point(8, 551);
            this.lblCountShow.Name = "lblCountShow";
            this.lblCountShow.Size = new System.Drawing.Size(51, 20);
            this.lblCountShow.TabIndex = 24;
            this.lblCountShow.Text = "label4";
            // 
            // btnAddAttribute
            // 
            this.btnAddAttribute.Image = global::Hardware_MS.Properties.Resources.to_do;
            this.btnAddAttribute.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddAttribute.Location = new System.Drawing.Point(237, 12);
            this.btnAddAttribute.Name = "btnAddAttribute";
            this.btnAddAttribute.Size = new System.Drawing.Size(137, 50);
            this.btnAddAttribute.TabIndex = 25;
            this.btnAddAttribute.Text = "Add Attribute";
            this.btnAddAttribute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddAttribute.UseVisualStyleBackColor = true;
            this.btnAddAttribute.Click += new System.EventHandler(this.btnAddAttribute_Click);
            // 
            // btnViewSupplies
            // 
            this.btnViewSupplies.Image = global::Hardware_MS.Properties.Resources.check_mark;
            this.btnViewSupplies.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnViewSupplies.Location = new System.Drawing.Point(666, 12);
            this.btnViewSupplies.Name = "btnViewSupplies";
            this.btnViewSupplies.Size = new System.Drawing.Size(141, 50);
            this.btnViewSupplies.TabIndex = 21;
            this.btnViewSupplies.Text = "View Supplies";
            this.btnViewSupplies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewSupplies.UseVisualStyleBackColor = true;
            this.btnViewSupplies.Click += new System.EventHandler(this.btnViewSupplies_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::Hardware_MS.Properties.Resources.search;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.Location = new System.Drawing.Point(355, 124);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(86, 29);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnNewSupply
            // 
            this.btnNewSupply.Image = global::Hardware_MS.Properties.Resources.trolley;
            this.btnNewSupply.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNewSupply.Location = new System.Drawing.Point(523, 12);
            this.btnNewSupply.Name = "btnNewSupply";
            this.btnNewSupply.Size = new System.Drawing.Size(137, 50);
            this.btnNewSupply.TabIndex = 15;
            this.btnNewSupply.Text = "New Supply";
            this.btnNewSupply.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewSupply.UseVisualStyleBackColor = true;
            this.btnNewSupply.Click += new System.EventHandler(this.btnNewSupply_Click);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Image = global::Hardware_MS.Properties.Resources.networking;
            this.btnAddCategory.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddCategory.Location = new System.Drawing.Point(380, 12);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(137, 50);
            this.btnAddCategory.TabIndex = 14;
            this.btnAddCategory.Text = "Add Category";
            this.btnAddCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Image = global::Hardware_MS.Properties.Resources.notepad;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddItem.Location = new System.Drawing.Point(12, 12);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(108, 50);
            this.btnAddItem.TabIndex = 13;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnAddUnit
            // 
            this.btnAddUnit.Image = global::Hardware_MS.Properties.Resources.ruler;
            this.btnAddUnit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddUnit.Location = new System.Drawing.Point(126, 12);
            this.btnAddUnit.Name = "btnAddUnit";
            this.btnAddUnit.Size = new System.Drawing.Size(105, 50);
            this.btnAddUnit.TabIndex = 12;
            this.btnAddUnit.Text = "Add Unit";
            this.btnAddUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddUnit.UseVisualStyleBackColor = true;
            this.btnAddUnit.Click += new System.EventHandler(this.btnAddUnit_Click);
            // 
            // btnBrokenItem
            // 
            this.btnBrokenItem.Image = global::Hardware_MS.Properties.Resources.error;
            this.btnBrokenItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBrokenItem.Location = new System.Drawing.Point(813, 12);
            this.btnBrokenItem.Name = "btnBrokenItem";
            this.btnBrokenItem.Size = new System.Drawing.Size(137, 50);
            this.btnBrokenItem.TabIndex = 26;
            this.btnBrokenItem.Text = "Broken Item";
            this.btnBrokenItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrokenItem.UseVisualStyleBackColor = true;
            this.btnBrokenItem.Click += new System.EventHandler(this.btnBrokenItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(669, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 20);
            this.label4.TabIndex = 27;
            this.label4.Text = "Filter Stock Level:";
            // 
            // cboStockLevel
            // 
            this.cboStockLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStockLevel.FormattingEnabled = true;
            this.cboStockLevel.Items.AddRange(new object[] {
            "All",
            "In stock",
            "Low stock",
            "Out of stock"});
            this.cboStockLevel.Location = new System.Drawing.Point(809, 89);
            this.cboStockLevel.Name = "cboStockLevel";
            this.cboStockLevel.Size = new System.Drawing.Size(151, 28);
            this.cboStockLevel.TabIndex = 28;
            this.cboStockLevel.SelectedIndexChanged += new System.EventHandler(this.cboStockLevel_SelectedIndexChanged);
            // 
            // lblOutOfStock
            // 
            this.lblOutOfStock.AutoSize = true;
            this.lblOutOfStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblOutOfStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutOfStock.ForeColor = System.Drawing.Color.White;
            this.lblOutOfStock.Location = new System.Drawing.Point(945, 78);
            this.lblOutOfStock.Name = "lblOutOfStock";
            this.lblOutOfStock.Size = new System.Drawing.Size(15, 16);
            this.lblOutOfStock.TabIndex = 29;
            this.lblOutOfStock.Text = "1";
            // 
            // lblLowStock
            // 
            this.lblLowStock.AutoSize = true;
            this.lblLowStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.lblLowStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowStock.ForeColor = System.Drawing.Color.White;
            this.lblLowStock.Location = new System.Drawing.Point(927, 78);
            this.lblLowStock.Name = "lblLowStock";
            this.lblLowStock.Size = new System.Drawing.Size(15, 16);
            this.lblLowStock.TabIndex = 30;
            this.lblLowStock.Text = "1";
            this.lblLowStock.Click += new System.EventHandler(this.lblLowStock_Click);
            // 
            // FrmInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1004, 608);
            this.Controls.Add(this.lblLowStock);
            this.Controls.Add(this.lblOutOfStock);
            this.Controls.Add(this.cboStockLevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBrokenItem);
            this.Controls.Add(this.btnAddAttribute);
            this.Controls.Add(this.lblCountShow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblNewSupplyCount);
            this.Controls.Add(this.btnViewSupplies);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNoResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboFilterCategory);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnNewSupply);
            this.Controls.Add(this.btnAddCategory);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.btnAddUnit);
            this.Controls.Add(this.dgvItems);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmInventory";
            this.Text = "FrmInventory";
            this.Load += new System.EventHandler(this.FrmInventory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Label lblNoResult;
        private System.Windows.Forms.Button btnAddUnit;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Button btnNewSupply;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cboFilterCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnViewSupplies;
        private System.Windows.Forms.Label lblNewSupplyCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCountShow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVatable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPriceV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewButtonColumn colBtnInfo;
        private System.Windows.Forms.Button btnAddAttribute;
        private System.Windows.Forms.Button btnBrokenItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboStockLevel;
        private System.Windows.Forms.Label lblOutOfStock;
        private System.Windows.Forms.Label lblLowStock;

    }
}