using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;

namespace Hardware_MS
{
    public partial class FrmPrintReceipt : Form
    {
        List<SaleDetail> listSaleDetail = new List<SaleDetail>();
        List<SaleDetailReport> listSaleDetailReport = new List<SaleDetailReport>();

        Sale sale = new Sale();
        string saleId = "";
        string orNo = "";
        string customerName = "";
        string address = "";
        decimal discount = 0;
        decimal amountTendered = 0;
        string vatable = "";
        string vatTax = "";
        string nonVatable = "";
        public FrmPrintReceipt()
        {
            InitializeComponent();       
        }
        public FrmPrintReceipt(string saleId,string orNo, string customerName, string address, decimal discount, decimal amountTendered,string vatable,string vatTax,string nonVatable)
            : this()
        {
            this.saleId = saleId;
            this.orNo = orNo;
            this.customerName = customerName;
            this.address = address;
            this.discount = discount;
            this.amountTendered = amountTendered;
            this.vatable = vatable;
            this.vatTax = vatTax;
            this.nonVatable = nonVatable;
        }

        private void FrmPrintReceipt_Load(object sender, EventArgs e)
        {
            LoadData();
            ReportParameter[] reportParam = new ReportParameter[]
            {
                new ReportParameter("rpOrNo",orNo),
                new ReportParameter("rpDateTime",sale.DateTime.ToString("yyyy-MM-dd HH:mm:ss")),
                new ReportParameter("rpTotalPrice",sale.TotalPrice.ToString("N2")),
                new ReportParameter("rpCustomerName",customerName),
                new ReportParameter("rpAddress",address),
                new ReportParameter("rpDiscount",discount.ToString()),
                new ReportParameter("rpTendered",amountTendered.ToString("N2")),
                new ReportParameter("rpChange",(amountTendered - sale.TotalPrice).ToString("N2")),
                new ReportParameter("rpVatTax",vatTax),
                new ReportParameter("rpVatable",vatable),
                new ReportParameter("rpVatTaxPercent","VatTax (" + (new Global().vatTaxPercentage * 100).ToString() + "%):"),
                new ReportParameter("rpNonVatable",nonVatable),
                new ReportParameter("rpDeliveryFee",new Delivery().GetDeliveryFee(sale.SaleId).ToString("N2"))
            };

            //List<Test> listTest = new List<Test>();
            //listTest.Add(new Test("jasper", "Diongco"));
            //listTest.Add(new Test("test", "test"));
            ReportDataSource ds = new ReportDataSource("dsSale", listSaleDetailReport);
            rvReceipt.LocalReport.SetParameters(reportParam);
            rvReceipt.LocalReport.DataSources.Add(ds);

            this.rvReceipt.RefreshReport();
            //for (int i = 0; i < listSaleDetailReport.Count; i++)
            //{
            //    MessageBox.Show(listSaleDetailReport[i].Description);
            //}
            
        }
        private void LoadData()
        {
            sale = sale.Retrieve(saleId);
            listSaleDetail = new SaleDetail().Retrieve(saleId);

            for (int i = 0; i < listSaleDetail.Count; i++)
            {
                listSaleDetailReport.Add(new SaleDetailReport(listSaleDetail[i]));
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //rvReceipt.ShowPrintButton.GetType();
            rvReceipt.PrintDialog();
            this.Hide();
            this.Close();
            
        }

        private void rvReceipt_Load(object sender, EventArgs e)
        {

        }
    }
}
