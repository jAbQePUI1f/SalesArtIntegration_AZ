using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesArtIntegration_AZ
{
    public partial class WaybillForm : Form
    {
        public WaybillForm()
        {
            InitializeComponent();
        }

        private void bttnSendWaybill_Click(object sender, EventArgs e)
        {

        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceForm invoiceForm = new InvoiceForm();
            invoiceForm.Show();
            this.Hide();
        }

        private void collectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollectionForm collectionForm = new CollectionForm();
            collectionForm.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
