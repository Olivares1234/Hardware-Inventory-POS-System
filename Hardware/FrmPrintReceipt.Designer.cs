namespace Hardware_MS
{
    partial class FrmPrintReceipt
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintReceipt));
            this.SaleDetailBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rvReceipt = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SaleDetailBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // SaleDetailBindingSource
            // 
            this.SaleDetailBindingSource.DataSource = typeof(Hardware_MS.SaleDetail);
            // 
            // rvReceipt
            // 
            this.rvReceipt.Dock = System.Windows.Forms.DockStyle.Top;
            reportDataSource1.Name = "dsSaleDetails";
            reportDataSource1.Value = this.SaleDetailBindingSource;
            this.rvReceipt.LocalReport.DataSources.Add(reportDataSource1);
            this.rvReceipt.LocalReport.ReportEmbeddedResource = "Hardware_MS.Receipt.rdlc";
            this.rvReceipt.Location = new System.Drawing.Point(0, 0);
            this.rvReceipt.Name = "rvReceipt";
            this.rvReceipt.Size = new System.Drawing.Size(609, 603);
            this.rvReceipt.TabIndex = 0;
            this.rvReceipt.Load += new System.EventHandler(this.rvReceipt_Load);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::Hardware_MS.Properties.Resources.printer;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.Location = new System.Drawing.Point(489, 622);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(81, 39);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FrmPrintReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(609, 685);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.rvReceipt);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmPrintReceipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Southern Sky Hardware and Construction Supply";
            this.Load += new System.EventHandler(this.FrmPrintReceipt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SaleDetailBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvReceipt;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.BindingSource SaleDetailBindingSource;
    }
}