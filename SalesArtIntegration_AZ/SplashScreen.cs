using SalesArtIntegration_AZ.Manager.Config;


namespace SalesArtIntegration_AZ

{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            bool isInvoice = Configuration.getIsInvoice();
            bool isWaybill = Configuration.getIsWaybill();
            bool isCollection = Configuration.getIsCollection();
        }

        private void bttnInvoice_Click(object sender, EventArgs e)
        {
            InvoiceForm invoiceForm = new InvoiceForm();
            invoiceForm.Show();
            this.Hide();
        }

        private void bttnWaybill_Click(object sender, EventArgs e)
        {
            WaybillForm waybillForm = new WaybillForm();
            waybillForm.Show();
            this.Hide();
        }

        private void bttnCollection_Click(object sender, EventArgs e)
        {
            CollectionForm collectionForm = new CollectionForm();
            collectionForm.Show();
            this.Hide();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            if (Configuration.getIsCollection())
            {
                bttnCollection.Enabled = true;
            }
            if (Configuration.getIsWaybill())
            {
                bttnWaybill.Enabled = false;
            }
            if (Configuration.getIsInvoice())
            {
                bttnInvoice.Enabled = true;
            }
        }

        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DataIntegrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataIntegrationForm dataIntegrationForm = new DataIntegrationForm();
            dataIntegrationForm.Show();
            this.Hide();
        }
    }
}
