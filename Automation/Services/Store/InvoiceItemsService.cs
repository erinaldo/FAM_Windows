using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baran.Ferroalloy.Automation.Models;

namespace Baran.Ferroalloy.Automation
{
    public class InvoiceItemsService : Generic<tabInvoiceItems>, IInvoiceItems
    {
        private UnitOfWork _db = new UnitOfWork();
        public InvoiceItemsService(DbContext db) : base(db)
        {
        }

        public IEnumerable<dgvInvoiceItemsViewModel> Filter(int invoiceId, string filter = "")
        {
            var invoiceItems = _db.InvoiceItems.GetByWhere(t => t.intInvoiceNumber == invoiceId);
            List<dgvInvoiceItemsViewModel> list = new List<dgvInvoiceItemsViewModel>();
            foreach (var item in invoiceItems)
            {
                var partCode = item.nvcPartCode.ToCharArray();
                list.Add(new dgvInvoiceItemsViewModel()
                {
                    intID = item.intID,
                    ID=item.intID,
                    RequestNumber = (int)item.intRequestNumber,
                    PartCode = item.nvcPartCode,
                    PartName = partCode.GetPartName(),
                    Amount = item.floAmount + " " + partCode.GetMeasurementUnit(),
                    Price = (int)item.intPrice
                });
            }
            if (filter == "")
            {
                return list;
            }

            return list.Where(t => t.PartName.Contains(filter)).ToList();
        }
    }
}
