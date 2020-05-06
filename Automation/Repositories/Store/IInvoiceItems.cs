using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baran.Ferroalloy.Automation.Models;

namespace Baran.Ferroalloy.Automation
{
    public interface IInvoiceItems:IGeneric<tabInvoiceItems>
    {
        IEnumerable<dgvInvoiceItemsViewModel> Filter(int invoiceId, string filter = "");
    }
}
