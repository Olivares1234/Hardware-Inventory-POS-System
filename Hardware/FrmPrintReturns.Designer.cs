namespace Hardware_MS
{
    partial class FrmPrintReturns
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintReturns));
            this.ReturnReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rvReturns = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SaleDetailReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ReturnReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleDetailReportBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReturnReportBindingSource
            // 
            this.ReturnReportBindingSource.DataSource = typeof(Hardware_MS.ReturnReport);
            // 
            // rvReturns
            // 
            this.rvReturns.Dock = System.Windows.Forms.DockStyle.Top;
            reportDataSource1.Name = "dsReturns";
            reportDataSource1.Value = this.ReturnReportBindingSource;
            this.rvReturns.LocalReport.DataSources.Add(reportDataSource1);
            this.rvReturns.LocalReport.ReportEmbeddedResource = "Hardware_MS.ReturnReport.rdlc";
            this.rvReturns.Location = new System.Drawing.Point(0, 0);
            this.rvReturns.Name = "rvReturns";
            this.rvReturns.Size = new System.Drawing.Size(986, 515);
            this.rvReturns.TabIndex = 0;
            // 
            // SaleDetailReportBindingSource
            // 
            this.SaleDetailReportBindingSource.DataSource = typeof(Hardware_MS.SaleDetailReport);
            // 
            // FrmPrintReturns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(986, 586);
            this.Controls.Add(this.rvReturns);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmPrintReturns";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Southern Sky Hardware and Construction Supply";
            this.Load += new System.EventHandler(this.FrmPrintReturns_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReturnReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleDetailReportBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvReturns;
        private System.Windows.Forms.BindingSource ReturnReportBindingSource;
        private System.Windows.Forms.BindingSource SaleDetailReportBindingSource;
    }
}