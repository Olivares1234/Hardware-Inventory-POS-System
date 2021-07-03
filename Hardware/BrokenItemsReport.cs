using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware_MS
{
    class BrokenItemsReport
    {
        public Item Item { get; set; }
        public string ItemId{get; set;}
        public int BrokenItemId { get; set; }
        
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

    }
     //public SaleDetailReport(SaleDetail saleDetail)
     //   {
     //       this.SaleDetail = saleDetail;
     //       ItemId = SaleDetail.Item.ItemId;
     //       Description = SaleDetail.Item.GetDescription();
     //       Unit = SaleDetail.Item.UnitCategory.Unit;
     //       Price = SaleDetail.Item.GetPriceWithVatString();
     //       Quantity = SaleDetail.Quantity;
     //       DiscountPercentage = SaleDetail.DiscountPercentage;
     //       SubTotal = SaleDetail.ComputeSubTotal().ToString("N2");
     //   }  
      

}
