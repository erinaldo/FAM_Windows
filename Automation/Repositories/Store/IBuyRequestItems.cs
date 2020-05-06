using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baran.Ferroalloy.Automation.Models;

namespace Baran.Ferroalloy.Automation
{
    public interface IBuyRequestItems:IGeneric<tabBuyRequestItems>
    {
        IEnumerable<dgvBuyRequestItemsViewModel> Filter(int requestId, string filter = "");
    }
}
