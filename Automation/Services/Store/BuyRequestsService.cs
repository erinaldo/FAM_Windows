using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baran.Ferroalloy.Automation.Models;

namespace Baran.Ferroalloy.Automation
{
    public class BuyRequestsService : Generic<tabBuyRequests>, IBuyRequests
    {
        private dbAfrzEntities _db = new dbAfrzEntities();
        public BuyRequestsService(DbContext db) : base(db)
        {
        }

        public IEnumerable<tabBuyRequests> FilterBuyRequests(object department, DateTime from, DateTime to)
        {
            var buyRequests = _db.tabBuyRequests.ToList().Where(t =>
                t.tabDepartments.nvcName.Equals(department) ||
                (t.datDate >= from && t.datDate <= to)).ToList();
            return buyRequests;
        }

        public IEnumerable<dgvBuyRequestsViewModel> FillDgvBuyRequests(List<tabBuyRequests> buyRequests)
        {
            List<dgvBuyRequestsViewModel> list = new List<dgvBuyRequestsViewModel>();
            foreach (var item in buyRequests)
            {
                var buyRequestItems = _db.tabBuyRequestItems.FirstOrDefault(t => t.intRequestNumber == item.intNumber);
                var employees = _db.tabEmployees.FirstOrDefault(t => t.nvcCoID == item.nvcRequesterCoID);
                if (buyRequestItems != null)
                {
                    var partCode = buyRequestItems.nvcPartCode.ToCharArray();
                    if (employees != null)
                        list.Add(new dgvBuyRequestsViewModel()
                        {
                            intID = item.intID,
                            PartCode = buyRequestItems.nvcPartCode,
                            intNumber = item.intNumber,
                            RequesterCoID = item.nvcRequesterCoID,
                            Date = item.datDate.ToString("D"),
                            DepartmentName = item.tabDepartments.nvcName,
                            RequesterName = employees.nvcFirstname + " " + employees.nvcLastname,
                            PartName = partCode.GetPartName()
                        });
                }
            }

            return list;
        }

        public IEnumerable<tabBuyRequests> FilterBuyRequests(string coId, object department, DateTime from, DateTime to)
        {
            var buyRequests = _db.tabBuyRequests.ToList().Where(t =>
                t.nvcRequesterCoID == coId &&
                (t.tabDepartments.nvcName.Equals(department) ||
                (t.datDate >= from && t.datDate <= to))).ToList();
            return buyRequests;
        }

        public IEnumerable<BuyRequestsViewModel> FillBuyRequests(List<tabBuyRequests> buyRequests)
        {
            List<BuyRequestsViewModel> list = new List<BuyRequestsViewModel>();
            foreach (var item in buyRequests)
            {
                var buyRequestItems = _db.tabBuyRequestItems.FirstOrDefault(t => t.intRequestNumber == item.intNumber);
                var supervisor = _db.tabEmployees.FirstOrDefault(t => t.nvcCoID == item.nvcSupervisorCoID);
                var plantmanager = _db.tabEmployees.FirstOrDefault(t => t.nvcCoID == item.nvcPlantmanagerCoID);
                var ceo = _db.tabEmployees.FirstOrDefault(t => t.nvcCoID == item.nvcCeoCoID);
                if (buyRequestItems != null)
                {
                    var partCode = buyRequestItems.nvcPartCode.ToCharArray();
                    list.Add(new BuyRequestsViewModel()
                    {
                        intID = item.intID,
                        intNumber = item.intNumber,
                        Date = item.datDate.ToString("D"),
                        DepartmentName = item.tabDepartments.nvcName,
                        Supervisor = supervisor != null ? supervisor.nvcFirstname + " " + supervisor.nvcLastname : "",
                        Plantmanager = plantmanager!=null ? plantmanager.nvcFirstname + " " + plantmanager.nvcLastname : "",
                        Ceo = ceo!=null ? ceo.nvcFirstname + " " + ceo.nvcLastname : ""
                    });
                }
            }

            return list;
        }
    }
}
