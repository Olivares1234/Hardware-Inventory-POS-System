using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;

namespace Hardware_MS
{
    public partial class FrmPrintReturns : Form
    {
        List<Return> listReturn = new Return().Retrieve();
        List<ReturnReport> listReturnReport = new List<ReturnReport>();
        public FrmPrintReturns()
        {
            InitializeComponent();
            //listReturn = new Return().Retrieve();
        }
        public FrmPrintReturns(DateTime date) : this()
        {
            listReturn = new Return().Retrieve(date);
        }
        public FrmPrintReturns(string staffId) :this()
        {
            listReturn = new Return().RetrieveByStaff(staffId);
        }
        public FrmPrintReturns(DateTime from, DateTime to)
            : this()
        {
            listReturn = new Return().Retrieve(from, to);
        }

        private void FrmPrintReturns_Load(object sender, EventArgs e)
        {
                  
            //MessageBox.Show(listReturn.Count.ToString());
            Parse();
            ReportDataSource ds = new ReportDataSource("dsReturns", listReturnReport);
            ReportParameter[] reportParam = new ReportParameter[]
            {
                new ReportParameter("rpTotalAmount",ComputeTotalAmount()),
                new ReportParameter("rpDateTime", DateTime.Now.ToString("MMM dd yyyy hh:mm tt"))
            };
            rvReturns.LocalReport.DataSources.Add(ds);
            ReturnReportBindingSource.DataSource = listReturnReport;
            rvReturns.LocalReport.SetParameters(reportParam);
            //MessageBox.Show(listReturnReport[0].StaffName);
            this.rvReturns.RefreshReport();
            this.rvReturns.RefreshReport();
        }
        private void Parse()
        {
            for (int i = 0; i < listReturn.Count; i++)
            {
                ReturnReport returnReport = new ReturnReport();
                returnReport.ReturnId = listReturn[i].ReturnId;
                returnReport.OrNo = listReturn[i].OrRefNumber;
                returnReport.DateTime = listReturn[i].DateTime.ToString("MMM dd yyyy hh:mm tt");
                returnReport.StaffName = listReturn[i].Staff.GetFullName();
                returnReport.ItemCount = listReturn[i].ListItemReturn.Count;
                returnReport.TotalQuantity = listReturn[i].ComputeTotalQty();
                returnReport.TotalAmtReturn = listReturn[i].TotalAmountReturned;
                listReturnReport.Add(returnReport);
            }
        }
        private string ComputeTotalAmount()
        {
            decimal total = 0;
            for (int i = 0; i < listReturn.Count; i++)
            {
                total += listReturn[i].TotalAmountReturned;
            }
            return total.ToString("N2");
        }
    }
}
