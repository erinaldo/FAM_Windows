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

namespace Baran.Ferroalloy.Management.Store
{
    public partial class FrmSelectBuyRequest : Form
    {
        public int buyRequestId;
        public string requesterCoId;
        public string partCode;
        public FrmSelectBuyRequest()
        {
            InitializeComponent();
        }

        private void FrmSelectBuyRequest_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db=new UnitOfWork())
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
            using (UnitOfWork db=new UnitOfWork())
            {
                var buyRequests = db.BuyRequests.FilterBuyRequests(cbDepartment.SelectedItem, dtpFromDate.Value, dtpToDate.Value);
                dgvBuyRequests.DataSource = db.BuyRequests.FillDgvBuyRequests(buyRequests.ToList());
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

        private void BtmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtmSelect_Click(object sender, EventArgs e)
        {
            if (dgvBuyRequests.CurrentRow != null)
            {
                buyRequestId = int.Parse(dgvBuyRequests.CurrentRow.Cells["intNumber"].Value.ToString());
                requesterCoId = dgvBuyRequests.CurrentRow.Cells["RequesterCoID"].Value.ToString();
                partCode = dgvBuyRequests.CurrentRow.Cells["PartCode"].Value.ToString();
                this.Close();
            }
            else
            {
                RtlMessageBox.Show("لطفا سطر مورد نظر را انتخاب کنید", "توجه", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
