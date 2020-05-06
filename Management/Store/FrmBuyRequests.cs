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
using Baran.Ferroalloy.Automation.Security;
using Baran.Ferroalloy.Automation.SqlDataBase;

namespace Baran.Ferroalloy.Management.Store
{
    public partial class FrmBuyRequests : Form
    {
        public Connection cnConnection;
        public User usUser;
        public string coId;
        public FrmBuyRequests()
        {
            InitializeComponent();
        }

        private void FrmBuyRequests_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
                var departments = db.department.GetAll();
                foreach (var item in departments)
                {
                    cbDepartment.Items.Add(item.nvcName);
                }
            }
        }

        private void Filter()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var buyRequests = db.BuyRequests.FilterBuyRequests(coId, cbDepartment.SelectedItem, dtpFromDate.Value, dtpToDate.Value);
                dgvBuyRequests.DataSource = db.BuyRequests.FillBuyRequests(buyRequests.ToList());
            }
        }

        private void ChbDateCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDateCheck.Checked)
            {
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
            }
            else
            {
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }
        }

        private void BtmSearch_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void BtmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtmDelete_Click(object sender, EventArgs e)
        {
            if (dgvBuyRequests.CurrentRow != null)
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    if (RtlMessageBox.Show($"آیا از حذف مطمئن هستید؟", "توجه", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        var selectItems = dgvBuyRequests.Rows.Cast<DataGridViewRow>().Where(t => Convert.ToBoolean(t.Cells["bitSelect"].Value) == true).ToList();

                        foreach (var item in selectItems)
                        {
                            var id = int.Parse(item.Cells["intID"].Value.ToString());
                            var buyRequests = db.BuyRequests.GetEntity(t => t.intID == id);
                            var buyRequestItems = db.BuyRequestItems.GetByWhere(t => t.intRequestNumber == buyRequests.intNumber);
                            if (buyRequestItems == null || !buyRequestItems.Any())
                            {
                                db.BuyRequests.Delete(buyRequests);
                            }
                            else
                            {
                                RtlMessageBox.Show("ابتدا باید آیتم های درخواست را حذف کنید", "توجه",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        db.Save();
                        Filter();
                    }
                }

            }
            else
            {
                RtlMessageBox.Show("لطفا سطرهای مورد نظر خود را انتخاب کنید", "توجه", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void BtmInsert_Click(object sender, EventArgs e)
        {
            FrmBuyRequestItems frmBuyRequestItems = new FrmBuyRequestItems();
            frmBuyRequestItems.requestCoId = coId;
            if (dgvBuyRequests.CurrentRow != null)
            {
                frmBuyRequestItems.requestId = Convert.ToInt32(dgvBuyRequests.CurrentRow.Cells["intNumber"].Value.ToString());
            }
            else
            {
                frmBuyRequestItems.requestId = 0;
            }
            frmBuyRequestItems.Show();
        }
    }
}
