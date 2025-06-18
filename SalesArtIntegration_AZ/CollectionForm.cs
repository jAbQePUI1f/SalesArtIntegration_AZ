using SalesArtIntegration_AZ.Helper;
using SalesArtIntegration_AZ.Manager.Api;
using SalesArtIntegration_AZ.Manager.Config;
using SalesArtIntegration_AZ.Models.Collection;
using SalesArtIntegration_AZ.Models.Enums;
using SalesArtIntegration_AZ.Models.Invoice;
using SalesArtIntegration_AZ.Models.Request;
using SalesArtIntegration_AZ.Models.Response;
using System.Xml.Linq;

namespace SalesArtIntegration_AZ
{
    public partial class CollectionForm : Form
    {
        string documentType = "";
        CollectionModelResponse collectionResponse = new CollectionModelResponse();
        public CollectionForm()
        {
            InitializeComponent();
            LoadComboBox();
        }
        private void LoadComboBox()
        {
            // ComboBox'a InvoiceType enum değerlerini ekle
            comboboxInvoiceType.DataSource = Enum.GetValues(typeof(Enums.InvoiceType))
                .Cast<Enums.InvoiceType>()
                .Select(value => new
                {
                    Value = value,
                    Display = Helpers.GetDisplayName(value)
                })
                .ToList();

            // DisplayMember ve ValueMember ayarları
            comboboxInvoiceType.DisplayMember = "Display";
            comboboxInvoiceType.ValueMember = "Value";
        }
        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceForm invoiceForm = new InvoiceForm();
            invoiceForm.Show();
            this.Hide();
        }
        private void collectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WaybillForm waybillForm = new WaybillForm();
            waybillForm.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void bttnGetWaybill_Click(object sender, EventArgs e)
        {
            // "Seçiniz" kontrolü
            if (string.IsNullOrEmpty(documentType) || documentType == "SEÇİNİZ...")
            {
                documentType = comboboxInvoiceType.SelectedValue.ToString();
            }

            string beginDate = dateTimeStartDate.Value.ToString("yyyy-MM-dd");
            string endDate = dateTimeFinishDate.Value.ToString("yyyy-MM-dd");

            var invoiceRequest = new CollectionRequest
            {
                startDate = beginDate, //"2023-12-01",
                endDate = endDate,//"2024-01-01",
                transactionTypes = new[] { documentType }
            };

            collectionResponse = await ApiManager.PostAsync<CollectionRequest, CollectionModelResponse>(Configuration.GetUrl() + "management/collections-for-erp", invoiceRequest);

            List<DisplayCollectionInfo> displayInfoList = collectionResponse.data.Select(header => new DisplayCollectionInfo
            {
                Number = header.invoiceNo,
                Date = header.date.ToShortDateString(),
                DocumentNo = header.documentNo,
                CustomerCode = header.customerCode,
                CustomerName = header.customerName,
                Amount = header.amount.ToString(),

            }).ToList();
            dataGridInvoiceList.Visible = true;

            dataGridInvoiceList.DataSource = displayInfoList;
            chckAll.Visible = true;
            chckAll.BringToFront();
        }

        private void chckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chckAll.Checked)
            {
                foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
                {
                    row.Cells["chk"].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
                {
                    row.Cells["chk"].Value = false;
                }
            }
        }

        private void comboboxInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboboxInvoiceType.SelectedValue != null && comboboxInvoiceType.SelectedValue is Enums.TransactionType)
            {
                documentType = comboboxInvoiceType.SelectedValue.ToString();
            }
        }
    }
}
