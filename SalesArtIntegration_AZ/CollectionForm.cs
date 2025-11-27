using SalesArtIntegration_AZ.Helper;
using SalesArtIntegration_AZ.Manager.Api;
using SalesArtIntegration_AZ.Manager.Config;
using SalesArtIntegration_AZ.Manager.Service;
using SalesArtIntegration_AZ.Models.Collection;
using SalesArtIntegration_AZ.Models.Enums;
using SalesArtIntegration_AZ.Models.Request;
using SalesArtIntegration_AZ.Models.Response;
using static SalesArtIntegration_AZ.Models.Request.CollectionSyncRequest;
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
                startDate = beginDate,
                endDate = endDate + "T23:59:00.000Z",
                transactionTypes = new[] { documentType }
            };
            try
            {
                collectionResponse = await ApiManager.PostAsync<CollectionRequest, CollectionModelResponse>(Configuration.GetUrl() + "management/collections-for-erp", invoiceRequest);

                if (collectionResponse?.data == null)
                {
                    MessageBox.Show("Tahsilat/Ödeme bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<DisplayCollectionInfo> displayInfoList = collectionResponse.data.Select(header => new DisplayCollectionInfo
                {
                    Number = header.documentNo,
                    Date = header.date.ToShortDateString(),
                    DocumentNo = header.ficheNo,
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
            catch (Exception ex)
            {

                throw;
            }

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

        private async void bttnSendCollection_Click(object sender, EventArgs e)
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
                        MessageBox.Show("Tahsilat numarası boş olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Helpers.LogFile(Helpers.LogLevel.ERROR, "Tahsilat", "Tahsilat numarası boş olduğu için aktarım yapılamadı.", "Numara: N/A");
                        continue;
                    }

                    var selectedInvoice = collectionResponse?.data?.FirstOrDefault(inv => inv.documentNo == number);

                    //if (selectedInvoice == null)
                    //{
                    //    MessageBox.Show($"Fatura numarası {number} bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    continue;
                    //}

                    bool success = false;
                    string errorMessage = "";
                    string remoteInvoiceNumber = "";

                    try
                    {
                        switch (documentType)
                        {
                            case nameof(Enums.TransactionType.CASH_COLLECTION):

                                var invoiceResponse = await client.InsertNewIncomingPaymentAsync(selectedInvoice.date, "KASSA TAHSILAT", selectedInvoice.documentNo
                                    , selectedInvoice.customerCode, "", "", "", "18", selectedInvoice.amount, "");

                                remoteInvoiceNumber = selectedInvoice.documentNo;

                                success = true;
                                MessageBox.Show("Aktarım Başarılı", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Helpers.LogFile(Helpers.LogLevel.INFO, "Tahsilat", "Tahsilat aktarımı **başarılı**.", $"Tahsilat No: {number}");
                                break;
                            case nameof(Enums.TransactionType.BANK_TRANSFER_COLLECTION):

                                invoiceResponse = await client.InsertNewIncomingPaymentAsync(selectedInvoice.date, "BANKA_TAHSILAT", selectedInvoice.documentNo
                                   , selectedInvoice.customerCode, "", "", "", "18", selectedInvoice.amount, "");

                                remoteInvoiceNumber = selectedInvoice.documentNo;

                                success = true;
                                MessageBox.Show("Aktarım Başarılı", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Helpers.LogFile(Helpers.LogLevel.INFO, "Tahsilat", "Tahsilat aktarımı **başarılı**.", $"Tahsilat No: {number}");
                                break;
                            default:
                                errorMessage = $"Desteklenmeyen fatura tipi: {documentType}";
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                        Helpers.LogFile(Helpers.LogLevel.ERROR, "Tahsilat", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Tahsilat No: {number}");

                    }

                    #region Faturalar Başarılı/Başarısız İşaretle
                    CollectionSyncRequest syncRequest = new CollectionSyncRequest
                    {
                        integratedCollections = new[]
                        {
                            new  IntegratedCollection
                            {
                                  successfullyIntegrated = success,
                                 ficheNo = selectedInvoice.documentNo,
                                 remoteCollectionNumber = remoteInvoiceNumber,
                                  errorMessage = errorMessage
                            }
                        }
                    };

                    var syncResponse = await ApiManager.PostAsync<CollectionSyncRequest, InvoiceSyncResponse>(
                         Configuration.GetUrl() + "management/sync-collection-statuses", syncRequest);


                    #endregion
                }
            }
        }

        private void CollectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void backToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
