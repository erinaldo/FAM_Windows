using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baran.Ferroalloy.Automation;
using Baran.Ferroalloy.Automation.Models;
using Baran.Ferroalloy.Management.Maintenance;

namespace Baran.Ferroalloy.Management.Store
{
    public partial class FrmBuyRequestItems : Form
    {
        public int requestId;
        public string requestCoId;
        public string partCode;
        public FrmBuyRequestItems()
        {
            InitializeComponent();
        }

        private void FrmBuyRequestItems_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var departments = db.department.GetAll();
                foreach (var item in departments)
                {
                    cbDepartment.Items.Add(item.nvcName);
                }
                Filter();
            }
        }

        private void Filter()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvBuyRequestItems.DataSource = db.BuyRequestItems.Filter(requestId, txtSearch.Text);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            Language.SetFarsiLanguage();
        }

        private void BtmDelete_Click(object sender, EventArgs e)
        {
            if (dgvBuyRequestItems.CurrentRow != null)
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    int id = Convert.ToInt32(dgvBuyRequestItems.CurrentRow.Cells["intID"].Value.ToString());
                    var requestItems = db.BuyRequestItems.GetEntity(t => t.intID == id);
                    db.BuyRequestItems.Delete(requestItems);
                    db.Save();
                    Filter();
                    txtSearch.Text = "";
                }
            }
        }

        private void BtmSelectPartType_Click(object sender, EventArgs e)
        {
            FrmSelectPart frmSelectPart = new FrmSelectPart();
            frmSelectPart.ShowDialog();
            txtPartName.Text = frmSelectPart.partName;
            partCode = frmSelectPart.partCode;

            if (txtPartName.Text != "")
            {
                var code = partCode.ToCharArray();
                lblMeasurementUnit.Text = code.GetMeasurementUnit();
            }
        }

        private void BtmInsert_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    var max = db.BuyRequests.GetAll().Max(t => t.intNumber);
                    var departmentId = db.department.GetEntity(t => t.nvcName == cbDepartment.SelectedItem).intNumber;
                    tabBuyRequests tabBuyRequests = new tabBuyRequests();
                    tabBuyRequests.datDate = DateTime.Now;
                    tabBuyRequests.nvcCeoCoID = null;
                    tabBuyRequests.nvcPlantmanagerCoID = null;
                    tabBuyRequests.nvcSupervisorCoID = null;
                    tabBuyRequests.nvcRequesterCoID = requestCoId;
                    tabBuyRequests.bitSelect = false;
                    tabBuyRequests.intDepartment = departmentId;
                    tabBuyRequests.intRowBudget = 1;

                    tabBuyRequestItems tabBuyRequestItems=new tabBuyRequestItems()
                    {
                        bitSelect = false,
                        floAmount = Convert.ToDouble(txtAmount.Text),
                        nvcPartCode = partCode,
                        
                    };

                    if (requestId == 0)
                    {
                        tabBuyRequests.intNumber = max + 1;
                        tabBuyRequestItems.intRequestNumber = tabBuyRequests.intNumber;
                        
                    }
                    else
                    {
                        tabBuyRequests.intID = tabBuyRequests.intID;
                        tabBuyRequests.intNumber = tabBuyRequests.intNumber;
                        tabBuyRequestItems.intRequestNumber = requestId;
                        tabBuyRequestItems.intID = tabBuyRequestItems.intID;
                    }

                    db.BuyRequests.Insert(tabBuyRequests);
                    db.BuyRequestItems.Insert(tabBuyRequestItems);
                    db.Save();
                    Filter();
                    cbDepartment.SelectedIndex = -1;
                    txtPartName.Text = "";
                    txtAmount.Text = "";
                }
            }
            else
            {
                RtlMessageBox.Show("لطفا تمام فیلدها را پر کنید", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool Validate()
        {
            if (txtPartName.Text != "" && txtAmount.Text != "" && cbDepartment.SelectedIndex >= 0)
            {
                return true;
            }
            return false;
        }
    }
}
