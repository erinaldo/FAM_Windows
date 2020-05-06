using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baran.Ferroalloy.Automation
{
    public class dgvInvoiceItemsViewModel
    {
        public int intID { get; set; }
        public int ID { get; set; }
        public int RequestNumber { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public string Amount { get; set; }
        public int Price { get; set; }
    }
}
