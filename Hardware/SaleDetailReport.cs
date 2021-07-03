using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware_MS
{
    class SaleDetailReport
    {
        public SaleDetail SaleDetail { get; set; }
        public string ItemId { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Price { get; set; }
        public double Quantity { get; set; }
        public double DiscountPercentage { get; set; }
        public string SubTotal { get; set; }


        public SaleDetailReport(SaleDetail saleDetail)
        {
            this.SaleDetail = saleDetail;
            ItemId = SaleDetail.Item.ItemId;
            Description = SaleDetail.Item.GetDescription();
            Unit = SaleDetail.Item.UnitCategory.Unit;
            Price = SaleDetail.Item.GetPriceWithVatString();
            Quantity = SaleDetail.Quantity;
            DiscountPercentage = SaleDetail.DiscountPercentage;
            SubTotal = SaleDetail.ComputeSubTotal().ToString("N2");
        }       

    }
}
