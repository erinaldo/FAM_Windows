using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baran.Ferroalloy.Automation.Models;

namespace Baran.Ferroalloy.Automation
{
    public interface IBuyRequests:IGeneric<tabBuyRequests>
    {
        IEnumerable<tabBuyRequests> FilterBuyRequests(object department, DateTime from, DateTime to);
        IEnumerable<dgvBuyRequestsViewModel> FillDgvBuyRequests(List<tabBuyRequests> buyRequests);

        IEnumerable<tabBuyRequests> FilterBuyRequests(string coId,object department, DateTime from, DateTime to);
        IEnumerable<BuyRequestsViewModel> FillBuyRequests(List<tabBuyRequests> buyRequests);
    }
}
