using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baran.Ferroalloy.Automation
{
    public class dgvBuyRequestsViewModel
    {
        public int intID { get; set; }
        public int intNumber { get; set; }
        public string RequesterCoID { get; set; }
        public string Date { get; set; }
        public string DepartmentName { get; set; }
        public string RequesterName { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
    }
}
