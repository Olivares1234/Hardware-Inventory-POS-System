namespace Hardware_MS
{
    partial class FrmBrokenItem
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBrokenItem));
            this.label2 = new System.Windows.Forms.Label();
            this.dgvBroken = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnPrintBroken = new System.Windows.Forms.Button();
            this.lblCountShow = new System.Windows.Forms.Label();
            this.lblNoResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBroken)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.label2.Location = new System.Drawing.Point(26, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 29);
            this.label2.TabIndex = 24;
            this.label2.Text = "Broken Item List";
            // 
            // dgvBroken
            // 
            this.dgvBroken.AllowUserToAddRows = false;
            this.dgvBroken.AllowUserToDeleteRows = false;
            this.dgvBroken.AllowUserToResizeColumns = false;
            this.dgvBroken.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvBroken.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBroken.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBroken.BackgroundColor = System.Drawing.Color.White;
            this.dgvBroken.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBroken.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvBroken.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBroken.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBroken.ColumnHeadersHeight = 40;
            this.dgvBroken.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBroken.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colItem,
            this.colQty,
            this.colDate,
            this.colPrice,
            this.colDesc});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBroken.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvBroken.EnableHeadersVisualStyles = false;
            this.dgvBroken.Location = new System.Drawing.Point(25, 122);
            this.dgvBroken.MultiSelect = false;
            this.dgvBroken.Name = "dgvBroken";
            this.dgvBroken.ReadOnly = true;
            this.dgvBroken.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBroken.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvBroken.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.dgvBroken.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvBroken.RowTemplate.Height = 40;
            this.dgvBroken.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBroken.Size = new System.Drawing.Size(902, 341);
            this.dgvBroken.TabIndex = 23;
            // 
            // colId
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colId.DefaultCellStyle = dataGridViewCellStyle3;
            this.colId.FillWeight = 101.5228F;
            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // colItem
            // 
            this.colItem.FillWeight = 99.49239F;
            this.colItem.HeaderText = "Description";
            this.colItem.Name = "colItem";
            this.colItem.ReadOnly = true;
            // 
            // colQty
            // 
            this.colQty.FillWeight = 99.49239F;
            this.colQty.HeaderText = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;
            // 
            // colDate
            // 
            this.colDate.HeaderText = "Date Recorded";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            // 
            // colPrice
            // 
            this.colPrice.FillWeight = 99.49239F;
            this.colPrice.HeaderText = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            // 
            // colDesc
            // 
            this.colDesc.HeaderText = "Description";
            this.colDesc.Name = "colDesc";
            this.colDesc.ReadOnly = true;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnAddItem.Image = global::Hardware_MS.Properties.Resources.notepad;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddItem.Location = new System.Drawing.Point(25, 33);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(166, 50);
            this.btnAddItem.TabIndex = 22;
            this.btnAddItem.Text = "Add Broken Item";
            this.btnAddItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnPrintBroken
            // 
            this.btnPrintBroken.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintBroken.Image = global::Hardware_MS.Properties.Resources.printer;
            this.btnPrintBroken.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintBroken.Location = new System.Drawing.Point(754, 33);
            this.btnPrintBroken.Name = "btnPrintBroken";
            this.btnPrintBroken.Size = new System.Drawing.Size(111, 42);
            this.btnPrintBroken.TabIndex = 25;
            this.btnPrintBroken.Text = "Print";
            this.btnPrintBroken.UseVisualStyleBackColor = true;
            this.btnPrintBroken.Click += new System.EventHandler(this.btnPrintBroken_Click);
            // 
            // lblCountShow
            // 
            this.lblCountShow.AutoSize = true;
            this.lblCountShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblCountShow.Location = new System.Drawing.Point(51, 466);
            this.lblCountShow.Name = "lblCountShow";
            this.lblCountShow.Size = new System.Drawing.Size(51, 20);
            this.lblCountShow.TabIndex = 26;
            this.lblCountShow.Text = "label1";
            // 
            // lblNoResult
            // 
            this.lblNoResult.AutoSize = true;
            this.lblNoResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoResult.ForeColor = System.Drawing.Color.Silver;
            this.lblNoResult.Location = new System.Drawing.Point(356, 303);
            this.lblNoResult.Name = "lblNoResult";
            this.lblNoResult.Size = new System.Drawing.Size(230, 25);
            this.lblNoResult.TabIndex = 27;
            this.lblNoResult.Text = "No BrokenItems found.";
            this.lblNoResult.Visible = false;
            // 
            // FrmBrokenItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(956, 495);
            this.Controls.Add(this.lblNoResult);
            this.Controls.Add(this.lblCountShow);
            this.Controls.Add(this.btnPrintBroken);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvBroken);
            this.Controls.Add(this.btnAddItem);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBrokenItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Southern Sky Hardware and Construction Supply";
            this.Load += new System.EventHandler(this.FrmBrokenItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBroken)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvBroken;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesc;
        private System.Windows.Forms.Button btnPrintBroken;
        private System.Windows.Forms.Label lblCountShow;
        private System.Windows.Forms.Label lblNoResult;
    }
}