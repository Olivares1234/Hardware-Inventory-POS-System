using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hardware_MS
{
    class SaleDetail
    {
        public int SaleDetailId { get; set; }
        public Sale Sales { get; set; }
        public Item Item { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public double DiscountPercentage { get; set; }
        public bool IsVatable { get; set; }

        public SaleDetail()
        {
            Sales = new Sale();
            Item = new Item();
        }

        public List<SaleDetail> Retrieve(string saleId)
        {
            Global global = new Global();
            List<SaleDetail> listSaleDetails = new List<SaleDetail>();
            MySqlConnection conn = new MySqlConnection(global.connectionString);
            string sql = "SELECT * FROM sale_details WHERE sale_id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", saleId);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    SaleDetail saleDetail = new SaleDetail();
                    saleDetail.SaleDetailId = dr.GetInt32("sale_detail_id");
                    saleDetail.Item = new Item().Retrieve(dr.GetString("item_id"));
                    saleDetail.Price = dr.GetDecimal("price");
                    saleDetail.Quantity = dr.GetDouble("quantity");
                    saleDetail.DiscountPercentage = dr.GetDouble("discount_percentage");
                    saleDetail.IsVatable = dr.GetBoolean("is_vatable");
                    listSaleDetails.Add(saleDetail);
                }
            }
            conn.Close();
            return listSaleDetails;
        }
        public decimal ComputeSubTotal()
        {
            Global global = new Global();
            decimal price = (Price * Convert.ToDecimal(Quantity)) * (Convert.ToDecimal(100 - DiscountPercentage) * 0.01M);
            return price;
        }
        public decimal ComputeSubTotalWithVat()
        {
            Global global = new Global();
            decimal total = ((Convert.ToDecimal(Price) * (Convert.ToDecimal(100 - DiscountPercentage) * 0.01M)) * Convert.ToDecimal(Quantity));
            //return total + total * .12M;//total + vat
            if (IsVatable)
            {
                //total = total + total * .12M;//total + vat
                total = total + total * global.vatTaxPercentage;//total + vat
            }
            return total;
        }
        public decimal ComputeDiscount()
        {
            Global global = new Global();
            decimal discount = (Price * Convert.ToDecimal(Quantity)) - ComputeSubTotalWithVat();

            if (IsVatable)
            {
                discount = (Price * Convert.ToDecimal(Quantity)) + ((Price * Convert.ToDecimal(Quantity)) * global.vatTaxPercentage) - ComputeSubTotalWithVat();
            }

            return discount;
        }
        public decimal ComputeVatable()
        {
            decimal vatable = 0;
            if (IsVatable)
            {
                vatable = Price * Convert.ToDecimal(Quantity);
            }
            return vatable;
        }
        public decimal ComputeNonVatable()
        {
            decimal nonVatable = 0;
            if (!IsVatable)
            {
                nonVatable = Price * Convert.ToDecimal(Quantity);
            }
            return nonVatable;
        }
        public decimal ComputeVATTax()
        {
            Global global = new Global();
            return ComputeVatable() * global.vatTaxPercentage;
        }
        public string GetPriceWithVatString()
        {
            Global global = new Global();
            string price = Price.ToString("N2");

            if (IsVatable)
            {
                //price = (Price + (Price * global.vatTaxPercentage)).ToString("N2") + "V";      
                price = price + "V";
            }
            return price;

        }
    }
}
