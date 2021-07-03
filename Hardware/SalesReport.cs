using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware_MS
{
    class SalesReport
    {
        public string SaleId { get; set; }
        public string OrNo { get; set; }
        public string DateTime { get; set; }
        public decimal VATable { get; set; }
        public decimal VAT_Tax { get; set; }
        public string StaffName { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
