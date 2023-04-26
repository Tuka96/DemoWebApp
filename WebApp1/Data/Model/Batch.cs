using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp1.Data.ViewModel;

namespace WebApp1.Data.Model
{
    public class Batch
    {
        [Key]
        public string BatchGuid { get; set; }
        public string BusinessUnitName { get; set; }
        public DateTime ExpiryDate { get; set; }

        [ForeignKey("BatchGuid")]
        public ICollection<Users> _users { get; set; }

        [ForeignKey("BatchGuid")]
        public ICollection<Groups> _groups { get; set; }

        [ForeignKey("BatchGuid")]
        public ICollection<BatchAttribute> _batchattributes { get; set; }

        [ForeignKey("BatchGuid")]
        public ICollection<BatchFiles> BatchFiles { get; set; }
    }
}
