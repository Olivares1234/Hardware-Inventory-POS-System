namespace Hardware_MS
{
    partial class FrmPrintBrokenItem
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintBrokenItem));
            this.BrokenItemsReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rvBrokenItems = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.BrokenItemsReportBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BrokenItemsReportBindingSource
            // 
            this.BrokenItemsReportBindingSource.DataSource = typeof(Hardware_MS.BrokenItemsReport);
            // 
            // rvBrokenItems
            // 
            reportDataSource2.Name = "dsBrokenItems";
            reportDataSource2.Value = this.BrokenItemsReportBindingSource;
            this.rvBrokenItems.LocalReport.DataSources.Add(reportDataSource2);
            this.rvBrokenItems.LocalReport.ReportEmbeddedResource = "Hardware_MS.BrokenItemsReport.rdlc";
            this.rvBrokenItems.Location = new System.Drawing.Point(0, 0);
            this.rvBrokenItems.Name = "rvBrokenItems";
            this.rvBrokenItems.Size = new System.Drawing.Size(684, 415);
            this.rvBrokenItems.TabIndex = 0;
            // 
            // FrmPrintBrokenItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 411);
            this.Controls.Add(this.rvBrokenItems);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPrintBrokenItem";
            this.Text = "Southern Sky Hardware and Construction Supply";
            this.Load += new System.EventHandler(this.BrokenItemReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BrokenItemsReportBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvBrokenItems;
        private System.Windows.Forms.BindingSource BrokenItemsReportBindingSource;
    }
}