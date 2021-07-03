using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware_MS
{
    class ReturnReport
    {
        public string ReturnId { get; set; }
        public string OrNo { get; set; }
        public string DateTime { get; set; }
        public string StaffName { get; set; }
        public int ItemCount { get; set; }
        public double TotalQuantity { get; set; }
        public decimal TotalAmtReturn { get; set; }
        public string Unit { get; set; }
    }
}
