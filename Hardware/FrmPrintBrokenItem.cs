using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Hardware_MS
{
    public partial class FrmPrintBrokenItem : Form
    {
        List<BrokenItem> listBrokenItems = new BrokenItem().Retrieve();
        List<BrokenItemsReport> listBrokenItemsReport = new List<BrokenItemsReport>();

        public FrmPrintBrokenItem()
        {
            InitializeComponent();
        }

        private void BrokenItemReport_Load(object sender, EventArgs e)
        {
            LoadBroken();
            ReportParameter[] reportParam = new ReportParameter[]
            {
                
                new ReportParameter("rpDateTime", DateTime.Now.ToString("MMM dd yyyy hh:mm tt"))
            };
          
            BrokenItemsReportBindingSource.DataSource = listBrokenItemsReport;
           rvBrokenItems.LocalReport.SetParameters(reportParam);
            this.rvBrokenItems.RefreshReport();
           

            
        }
        private void LoadBroken()
        {
            for (int i = 0; i < listBrokenItems.Count; i++)
            {
                BrokenItemsReport brokenReport = new BrokenItemsReport();
                brokenReport.BrokenItemId = listBrokenItems[i].BrokenItemId;
                brokenReport.ItemId = listBrokenItems[i].Item.GetDescription();
                brokenReport.Quantity = listBrokenItems[i].Quantity;
                brokenReport.Date = listBrokenItems[i].Date.ToString("yyyy-MM-dd HH:mm:ss");
                brokenReport.Price = listBrokenItems[i].Price;
                brokenReport.Description = listBrokenItems[i].Description;
                listBrokenItemsReport.Add(brokenReport);

            }
        }
    }
}
