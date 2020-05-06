using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baran.Ferroalloy.Automation.Models;

namespace Baran.Ferroalloy.Automation
{
    public class BuyRequestItemsService:Generic<tabBuyRequestItems>,IBuyRequestItems
    {
        private dbAfrzEntities _db=new dbAfrzEntities();
        public BuyRequestItemsService(DbContext db) : base(db)
        {
        }

        public IEnumerable<dgvBuyRequestItemsViewModel> Filter(int requestId, string filter = "")
        {
            var requestItems = _db.tabBuyRequestItems.Where(t => t.intRequestNumber == requestId).ToList();
            List<dgvBuyRequestItemsViewModel> list = new List<dgvBuyRequestItemsViewModel>();
            foreach (var item in requestItems)
            {
                var partCode = item.nvcPartCode.ToCharArray();
                list.Add(new dgvBuyRequestItemsViewModel()
                {
                    intID = item.intID,
                    RequestNumber = (int)item.intRequestNumber,
                    PartName = partCode.GetPartName(),
                    Amount = item.floAmount + " " + partCode.GetMeasurementUnit(),
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
