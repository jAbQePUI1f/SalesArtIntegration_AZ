using SalesArtIntegration_AZ.Helper;
using SalesArtIntegration_AZ.Manager.Api;
using SalesArtIntegration_AZ.Manager.Config;
using SalesArtIntegration_AZ.Manager.Service;
using SalesArtIntegration_AZ.Models.Collection;
using SalesArtIntegration_AZ.Models.Enums;
using SalesArtIntegration_AZ.Models.Request;
using SalesArtIntegration_AZ.Models.Response;
using static SalesArtIntegration_AZ.Models.Request.CollectionSyncRequest;

namespace SalesArtIntegration_AZ
{
    public partial class CollectionForm : Form
    {
        string documentType = "";
        string taxAccountCode = Configuration.getTaxAccountCode();
        string edvTaxAccountCode = Configuration.getEdvTaxAccountCode();
        string taxBankAccountNo = Configuration.getBankAccountNo();
        string edvBankAccountNo = Configuration.getEdvBankAccountNo();
        string kassaAccountCode = Configuration.getKassaAccountCode();
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

            dateTimeStartDate.Value = DateTime.Now.AddDays(-1); 
            dateTimeFinishDate.Value = DateTime.Now.AddDays(1);    

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

        private async void bttnGetCollection_Click(object sender, EventArgs e)
        {
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

                dataGridInvoiceList.Columns["Number"].HeaderText = "Makbuz No";
                dataGridInvoiceList.Columns["Date"].HeaderText = "Tarih";
                dataGridInvoiceList.Columns["DocumentNo"].HeaderText = "Fiş No";
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
                MessageBox.Show("Lütfen bir tahsilat tipi seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Sonuç takibi için listeler
            var successfulCollections = new List<string>();
            var failedCollections = new List<(string DocumentNo, string ErrorMessage)>();

            // ServiceFactory ile istemciyi al
            using var client = ServiceFactory.GetServiceClient();

            foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
            {
                if (Convert.ToBoolean(row.Cells["chk"].Value))
                {
                    string? number = row.Cells["Number"].Value?.ToString();
                    if (string.IsNullOrEmpty(number))
                    {
                        failedCollections.Add(("N/A", "Tahsilat numarası boş olamaz!"));
                        Helpers.LogFile(Helpers.LogLevel.ERROR, "Tahsilat", "Tahsilat numarası boş olduğu için aktarım yapılamadı.", "Numara: N/A");
                        continue;
                    }

                    var selectedInvoice = collectionResponse?.data?.FirstOrDefault(inv => inv.documentNo == number);
                    bool success = false;
                    string errorMessage = "";
                    string remoteInvoiceNumber = "";

                    try
                    {
                        switch (documentType)
                        {
                            case nameof(Enums.TransactionType.CASH_COLLECTION):
                                var invoiceResponse = await ServiceFactory.SendIncomingPaymentRawAsync(
                                    selectedInvoice.date,
                                    "KASSA TAHSILAT",
                                    selectedInvoice.documentNo,
                                    selectedInvoice.customerCode,
                                    selectedInvoice.detail.bankCode,
                                    selectedInvoice.detail.bankName,
                                    "",
                                    kassaAccountCode,
                                    selectedInvoice.amount,
                                    selectedInvoice.salesmanFirstName + " " + selectedInvoice.salesmanLastName);

                                remoteInvoiceNumber = selectedInvoice.documentNo;

                                if (invoiceResponse.Status)
                                {
                                    success = true;
                                    successfulCollections.Add(selectedInvoice.documentNo);
                                    Helpers.LogFile(Helpers.LogLevel.INFO, "Tahsilat", "Tahsilat aktarımı **başarılı**.", $"Tahsilat No: {number}");
                                }
                                else
                                {
                                    errorMessage = invoiceResponse.Message.ToString();
                                    failedCollections.Add((selectedInvoice.documentNo, errorMessage));
                                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Tahsilat", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Tahsilat No: {number}");
                                }
                                break;

                            case nameof(Enums.TransactionType.BANK_TRANSFER_COLLECTION):
                                // BankCode'a göre accountTaxCode belirleme
                                var (accountTaxCode, description, bankCode) = selectedInvoice.detail.bankCode switch
                                {
                                    "805454" => (taxAccountCode, "BANK", taxBankAccountNo),
                                    "210027" => (edvTaxAccountCode, "EDV", edvBankAccountNo),
                                    _ => ("0", "BANKA HAVALE", edvBankAccountNo)
                                };

                                var result = await ServiceFactory.SendIncomingPaymentRawAsync(
                                    selectedInvoice.date,
                                    "BANKA_TAHSILAT",
                                    selectedInvoice.documentNo,
                                    selectedInvoice.customerCode,
                                    bankCode,
                                    selectedInvoice.detail.bankName,
                                    selectedInvoice.detail.bankBranchName,
                                    accountTaxCode,
                                    selectedInvoice.amount,
                                    description
                                );

                                if (result.Status)
                                {
                                    success = true;
                                    successfulCollections.Add(selectedInvoice.documentNo);
                                    Helpers.LogFile(Helpers.LogLevel.INFO, "Tahsilat", "Tahsilat aktarımı **başarılı**.", $"Tahsilat No: {number}");
                                }
                                else
                                {
                                    errorMessage = result.Message.ToString();
                                    failedCollections.Add((selectedInvoice.documentNo, errorMessage));
                                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Tahsilat", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Tahsilat No: {number}");
                                }

                                remoteInvoiceNumber = selectedInvoice.documentNo;
                                break;

                            default:
                                errorMessage = $"Desteklenmeyen fatura tipi: {documentType}";
                                failedCollections.Add((number, errorMessage));
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                        failedCollections.Add((number, errorMessage));
                        Helpers.LogFile(Helpers.LogLevel.ERROR, "Tahsilat", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Tahsilat No: {number}");
                    }

                    #region Faturalar Başarılı/Başarısız İşaretle
                    CollectionSyncRequest syncRequest = new CollectionSyncRequest
                    {
                        integratedCollections = new[]
                        {
                    new IntegratedCollection
                    {
                        successfullyIntegrated = success,
                        ficheNo = selectedInvoice.documentNo,
                        remoteCollectionNumber = remoteInvoiceNumber,
                        errorMessage = errorMessage == "" ? "Status OK": errorMessage
                    }
                }
                    };

                    var syncResponse = await ApiManager.PostAsync<CollectionSyncRequest, InvoiceSyncResponse>(
                        Configuration.GetUrl() + "management/sync-collection-statuses", syncRequest);
                    #endregion
                }
            }

            // Toplu sonuç bildirimi
            ShowCollectionSummary(successfulCollections, failedCollections);
        }

        private void ShowCollectionSummary(List<string> successful, List<(string DocumentNo, string ErrorMessage)> failed)
        {
            var summary = new System.Text.StringBuilder();

            summary.AppendLine("╔════════════════════════════════════════╗");
            summary.AppendLine("║    TAHSİLAT AKTARIM RAPORU             ║");
            summary.AppendLine("╚════════════════════════════════════════╝\n");

            // Başarılı aktarımlar
            if (successful.Count > 0)
            {
                summary.AppendLine($"✓ BAŞARILI AKTARIMLAR: {successful.Count} Kayıt");
                summary.AppendLine("─────────────────────────────────────────");

                // Numaraları virgülle ayırarak göster (her satırda max 5 numara)
                for (int i = 0; i < successful.Count; i++)
                {
                    summary.Append(successful[i]);
                    if (i < successful.Count - 1)
                    {
                        summary.Append(", ");
                        // Her 5 numarada bir satır atla
                        if ((i + 1) % 5 == 0)
                            summary.AppendLine();
                    }
                }
                summary.AppendLine("\n");
            }

            // Başarısız aktarımlar
            if (failed.Count > 0)
            {
                summary.AppendLine($"✗ BAŞARISIZ AKTARIMLAR: {failed.Count} Kayıt");
                summary.AppendLine("─────────────────────────────────────────");

                // Her hata detaylı olarak gösterilir
                foreach (var (docNo, error) in failed)
                {
                    summary.AppendLine($"• {docNo}");
                    summary.AppendLine($"  Hata: {error}\n");
                }
            }

            // Özet
            summary.AppendLine("═════════════════════════════════════════");
            summary.AppendLine($"Toplam İşlem: {successful.Count + failed.Count}");
            summary.AppendLine($"Başarılı: {successful.Count} | Başarısız: {failed.Count}");

            // Başarı oranı
            if (successful.Count + failed.Count > 0)
            {
                double successRate = (double)successful.Count / (successful.Count + failed.Count) * 100;
                summary.AppendLine($"Başarı Oranı: %{successRate:F1}");
            }

            // Uygun icon seçimi
            MessageBoxIcon icon = failed.Count == 0 ? MessageBoxIcon.Information :
                                  successful.Count == 0 ? MessageBoxIcon.Error :
                                  MessageBoxIcon.Warning;

            string title = failed.Count == 0 ? "Aktarım Başarılı" :
                           successful.Count == 0 ? "Aktarım Başarısız" :
                           "Aktarım Tamamlandı";

            MessageBox.Show(summary.ToString(), title, MessageBoxButtons.OK, icon);
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

        #region --eski tahsilat kodları
        //   private async Task<string> SendIncomingPaymentRawAsync(
        //         DateTime date,
        //         string type,
        //         string number,
        //         string customerCode,
        //         string bankCode,
        //         string bankName,
        //         string bankBranchName,
        //         string taxCode,
        //         decimal amount,
        //         string description)
        //{
        //    var url = "http://10.100.0.55/KURDAMIR/ws/WebService/";

        //    string username = "WEB_USER";
        //    string password = "WB_123";

        //    string amountString = amount.ToString(System.Globalization.CultureInfo.InvariantCulture);

        //            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
        //<Envelope xmlns=""http://schemas.xmlsoap.org/soap/envelope/"">
        //  <Body>
        //    <InsertNewIncomingPayment xmlns=""http://127.0.0.1"">
        //      <Date>{date:yyyy-MM-dd}</Date>
        //      <Type>""{type}""</Type>
        //      <DocNumber>""{number}""</DocNumber>
        //      <PartnerCode>{customerCode}</PartnerCode>
        //      <Bank_Acc_Code>""{bankCode}""</Bank_Acc_Code>
        //      <Bank_Acc_Name>""{bankName}""</Bank_Acc_Name>
        //      <Bank_Cash_Name>{bankBranchName}</Bank_Cash_Name>
        //      <Vat_Acc_Code>{taxCode}</Vat_Acc_Code>
        //      <Amount>{amountString}</Amount>
        //      <Description>""{description}""</Description>
        //    </InsertNewIncomingPayment>
        //  </Body>
        //</Envelope>";

        //    var handler = new HttpClientHandler
        //    {
        //        PreAuthenticate = true,
        //        Credentials = new System.Net.NetworkCredential(username, password)
        //    };

        //    using var httpClient = new HttpClient(handler);

        //    var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");

        //    content.Headers.Clear();
        //    content.Headers.Add("Content-Type", "text/xml; charset=utf-8");

        //    var response = await httpClient.PostAsync(url, content);
        //    var responseXml = await response.Content.ReadAsStringAsync();

        //    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //    File.WriteAllText(Path.Combine(desktop, "lastResponse.xml"), responseXml);

        //    return responseXml;
        //}
        #endregion
    }
}
