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
    public partial class FrmInvoiceItems : Form
    {
        public int invoiceId;
        public string partCode;
        public int buyRequestId;

        public FrmInvoiceItems()
        {
            InitializeComponent();
        }

        private void FrmInvoiceItems_Load(object sender, EventArgs e)
        {
            dgvInvoiceItems.AutoGenerateColumns = false;
            Filter();
            using (UnitOfWork db = new UnitOfWork())
            {
                var invoiceNumberOfVendor = db.InvoiceItems.GetEntity(t => t.intInvoiceNumber == invoiceId).tabInvoices
                    .intInvoiceNumberOfVendor;
                this.Text = "ورود آیتم های فاکتور شماره " + invoiceNumberOfVendor;
            }
        }

        private void Filter()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvInvoiceItems.DataSource = db.InvoiceItems.Filter(invoiceId, txtSearch.Text);
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

        private void BtmSelectBuyRequest_Click(object sender, EventArgs e)
        {
            FrmSelectBuyRequest frmSelectBuyRequest = new FrmSelectBuyRequest();
            frmSelectBuyRequest.ShowDialog();
            var code = frmSelectBuyRequest.partCode.ToCharArray();
            buyRequestId = frmSelectBuyRequest.buyRequestId;
            txtBuyRequest.Text =
                "شماره درخواست: " + " (" + frmSelectBuyRequest.buyRequestId + ") - " + "نام قطعه: " + " (" +
                code.GetPartName() + ")";

        }

        private void BtmInsert_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    tabInvoiceItems tabInvoiceItems = new tabInvoiceItems()
                    {
                        bitSelect = false,
                        floAmount = Convert.ToDouble(txtAmount.Text),
                        intInvoiceNumber = invoiceId,
                        intPrice = (int)numPrice.Value,
                        intRequestNumber = buyRequestId,
                        nvcPartCode = partCode
                    };
                    db.InvoiceItems.Insert(tabInvoiceItems);
                    db.Save();
                    Filter();
                    txtBuyRequest.Text = "";
                    txtPartName.Text = "";
                    txtAmount.Text = "";
                    numPrice.Value = 0;
                }
            }
            else
            {
                RtlMessageBox.Show("لطفا تمام فیلدها را پر کنید", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool Validate()
        {
            if (txtBuyRequest.Text != "" && txtPartName.Text != "" && txtAmount.Text != "" && numPrice.Value > 0)
            {
                return true;
            }
            return false;
        }

        private void BtmDelete_Click(object sender, EventArgs e)
        {
            if (dgvInvoiceItems.CurrentRow != null)
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    int id = Convert.ToInt32(dgvInvoiceItems.CurrentRow.Cells["ID"].Value.ToString());
                    var invoiceItems = db.InvoiceItems.GetEntity(t => t.intID == id);
                    db.InvoiceItems.Delete(invoiceItems);
                    db.Save();
                    Filter();
                    txtSearch.Text = "";
                }
            }
        }
    }
}

