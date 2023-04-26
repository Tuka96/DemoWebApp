using System.Collections.Generic;
using System;

namespace WebApp1.Data.ViewModel
{
    public class BatchVM
    {
        public string BusinessUnitName { get; set; }
        public AccessVM AccessList { get; set; }
        public List<AttributeVM> Attributes { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class BatchResponse { 
        public string BatchGuid { get; set; }
    }
}
