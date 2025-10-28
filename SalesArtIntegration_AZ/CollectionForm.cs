using SalesArtIntegration_AZ.Helper;
using SalesArtIntegration_AZ.Manager.Api;
using SalesArtIntegration_AZ.Manager.Config;
using SalesArtIntegration_AZ.Manager.Service;
using SalesArtIntegration_AZ.Models.Collection;
using SalesArtIntegration_AZ.Models.Enums;
using SalesArtIntegration_AZ.Models.Request;
using SalesArtIntegration_AZ.Models.Response;
using static SalesArtIntegration_AZ.Models.Request.InvoiceSyncRequest;

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
            comboboxInvoiceType.DataSource = Enum.GetValues(typeof(Enums.TransactionType))
                .Cast<Enums.TransactionType>()
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

        private async void bttnGetCollection_Click(object sender, EventArgs e)
        {
            // "Seçiniz" kontrolü
            if (string.IsNullOrEmpty(documentType) || documentType == "SEÇİNİZ...")
            {
                documentType = comboboxInvoiceType.SelectedValue?.ToString() ?? string.Empty;
            }

            string beginDate = dateTimeStartDate.Value.ToString("yyyy-MM-dd");
            string endDate = dateTimeFinishDate.Value.ToString("yyyy-MM-dd");

            var invoiceRequest = new CollectionRequest
            {
                startDate = beginDate + " 00:00:00.000 ",
                endDate = endDate + " 23:59:59.999 ",
                transactionTypes = new[] { documentType }
            };

            collectionResponse = await ApiManager.PostAsync<CollectionRequest, CollectionModelResponse>(Configuration.GetUrl() + "management/collections-for-erp", invoiceRequest);

            if (collectionResponse?.data == null)
            {
                MessageBox.Show("Tahsilat/Ödeme bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            dataGridInvoiceList.Columns["Number"].HeaderText = "Numara";
            dataGridInvoiceList.Columns["Date"].HeaderText = "Tarih";
            dataGridInvoiceList.Columns["DocumentNo"].HeaderText = "Belge No";
            dataGridInvoiceList.Columns["CustomerCode"].HeaderText = "Müşteri Kodu";
            dataGridInvoiceList.Columns["CustomerName"].HeaderText = "Müşteri Adı";
            dataGridInvoiceList.Columns["Amount"].HeaderText = "Tutar";
            dataGridInvoiceList.Columns["Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["DocumentNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["CustomerCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["CustomerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

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

            if (comboboxInvoiceType.SelectedValue != null && comboboxInvoiceType.SelectedValue is Enums.TransactionType transactionType)
            {
                documentType = transactionType.ToString();
            }
            else
            {
                documentType = string.Empty;
            }
        }

        private async void bttnSendWaybill_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(documentType))
            {
                MessageBox.Show("Lütfen bir fatura tipi seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ServiceFactory ile istemciyi al
            using var client = ServiceFactory.GetServiceClient();

            foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
            {
                if (Convert.ToBoolean(row.Cells["chk"].Value))
                {
                    string? number = row.Cells["Number"].Value?.ToString();
                    if (string.IsNullOrEmpty(number))
                    {
                        MessageBox.Show("Fatura numarası boş olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    var selectedInvoice = collectionResponse?.data?.FirstOrDefault(inv => inv.documentNo == number);
                    if (selectedInvoice == null)
                    {
                        MessageBox.Show($"Fatura numarası {number} bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    bool success = false;
                    string errorMessage = "";
                    string remoteInvoiceNumber = "";

                    try
                    {
                        switch (documentType)
                        {
                            case nameof(Enums.TransactionType.CASH_PAYMENT):

                                var invoiceResponse = await client.InsertNewIncomingPaymentAsync(selectedInvoice.date, "KASSA TAHSILAT", selectedInvoice.documentNo
                                    , selectedInvoice.customerCode, selectedInvoice.paymentCode, selectedInvoice.paymentName, selectedInvoice.salesmanCode, selectedInvoice.customerCode, selectedInvoice.amount, selectedInvoice.description);

                                remoteInvoiceNumber = selectedInvoice.documentNo;

                                success = true;
                                MessageBox.Show("Aktarım Başarılı", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                break;

                            default:
                                errorMessage = $"Desteklenmeyen fatura tipi: {documentType}";
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                    }

                    #region Faturalar Başarılı/Başarısız İşaretle
                    InvoiceSyncRequest syncRequest = new InvoiceSyncRequest
                    {
                        integratedInvoices = new[]
                        {
                            new IntegratedInvoice
                            {
                                successfullyIntegrated = success,
                                invoiceNumber = selectedInvoice.documentNo,
                                remoteInvoiceNumber = remoteInvoiceNumber,
                                errorMessage = errorMessage
                            }
                        }
                    };

                    var syncResponse = await ApiManager.PutAsync<InvoiceSyncRequest, InvoiceSyncResponse>(
                        syncRequest, Configuration.GetUrl() + "management/sync-invoice-statuses");


                    #endregion
                }
            }
        }

        private void CollectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
