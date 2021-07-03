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
    public partial class FrmPrintSales : Form
    {
        List<Sale> listSales = new Sale().Retrieve();
        List<SalesReport> listSalesReport = new List<SalesReport>();
        public FrmPrintSales()
        {
            InitializeComponent();
        }
        public FrmPrintSales(DateTime date) : this()
        {
            listSales = new Sale().Retrieve(date);
        }
        public FrmPrintSales(string staffId)
            : this()
        {
            listSales = new Sale().RetrieveByStaff(staffId);
        }
        public FrmPrintSales(DateTime from, DateTime to)
            : this()
        {
            listSales = new Sale().Retrieve(from, to);
            
        }

        private void FrmPrintSales_Load(object sender, EventArgs e)
        {
            Parse();
            ReportParameter[] reportParam = new ReportParameter[]
            {
                new ReportParameter("rpTotalSales",ComputeTotalAmount()),
                new ReportParameter("rpDateTime", DateTime.Now.ToString("MMM dd yyyy hh:mm tt")),
                new ReportParameter("rpVATable",ComputeVATable()),
                new ReportParameter("rpVAT_Tax",ComputeVAT_Tax())
            };
            SalesReportBindingSource.DataSource = listSalesReport;

            rvSales.LocalReport.SetParameters(reportParam);
            this.rvSales.RefreshReport();
        }
        private void Parse()
        {
            for (int i = 0; i < listSales.Count; i++)
            {
                SalesReport saleReport = new SalesReport();
                saleReport.SaleId = listSales[i].SaleId;
                saleReport.OrNo = listSales[i].OrNo;
                saleReport.DateTime = listSales[i].DateTime.ToString("MMM dd yyyy hh:mm tt");
                saleReport.StaffName = listSales[i].Staff.GetFullName();
                saleReport.VATable = listSales[i].VATable;
                saleReport.VAT_Tax = listSales[i].VAT_tax;
                saleReport.TotalPrice = listSales[i].TotalPrice;
                listSalesReport.Add(saleReport);
            }
        }
        private string ComputeTotalAmount()
        {
            decimal total = 0;
            for (int i = 0; i < listSales.Count; i++)
            {
                total += listSales[i].TotalPrice;
            }
            return total.ToString("N2");
        }
        private string ComputeVATable()
        {
            decimal total = 0;
            for (int i = 0; i < listSales.Count; i++)
            {
                total += listSales[i].VATable;
            }
            return total.ToString("N2");
        }
        private string ComputeVAT_Tax()
        {
            decimal total = 0;
            for (int i = 0; i < listSales.Count; i++)
            {
                total += listSales[i].VAT_tax;
            }
            return total.ToString("N2");
        }
    }
}
