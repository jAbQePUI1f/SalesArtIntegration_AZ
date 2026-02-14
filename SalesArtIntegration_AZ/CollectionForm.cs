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
        CollectionModelResponse collectionResponse = new CollectionModelResponse();
        public CollectionForm()
        {
            InitializeComponent();
            LoadComboBox();
            string taxAccountCode = Configuration.getTaxAccountCode();
            string noTaxAccountCode = Configuration.getNoTaxAccountCode();
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
            string accountTaxCode;
            string accountDescription;

            if (string.IsNullOrEmpty(documentType))
            {
                MessageBox.Show("Lütfen bir tahsilat tipi seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    "", 
                                    "", 
                                    selectedInvoice.salesmanFirstName + " " + selectedInvoice.salesmanLastName,
                                    "18", 
                                    selectedInvoice.amount, 
                                    selectedInvoice.documentNo);

                                remoteInvoiceNumber = selectedInvoice.documentNo;

                                if (invoiceResponse.Status)
                                {
                                    success = true;
                                    MessageBox.Show("Aktarım Başarılı", invoiceResponse.Message + " :" + selectedInvoice.documentNo.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Helpers.LogFile(Helpers.LogLevel.INFO, "Tahsilat", "Tahsilat aktarımı **başarılı**.", $"Tahsilat No: {number}");
                                }
                                else
                                {
                                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Tahsilat", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Tahsilat No: {number}");
                                    MessageBox.Show(invoiceResponse.Message.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;

                                case nameof(Enums.TransactionType.BANK_TRANSFER_COLLECTION):
                                // BankCode'a göre accountTaxCode belirleme
                                accountTaxCode = selectedInvoice.detail.bankCode switch
                                {
                                    "805454" => "223.01",
                                    "210027" => "224.03.01",
                                    _ => "0"
                                };

                                accountDescription = selectedInvoice.description switch
                                {
                                    "805454" => "BANK",
                                    "210027" => "EDV",
                                    _ => "BANKA HAVALE"
                                };

                                var result = await ServiceFactory.SendIncomingPaymentRawAsync(
                                    selectedInvoice.date,
                                    "BANKA_TAHSILAT",
                                    selectedInvoice.documentNo,
                                    selectedInvoice.customerCode,
                                    selectedInvoice.detail.bankCode,
                                    selectedInvoice.detail.bankName,
                                    selectedInvoice.detail.bankBranchName,
                                    accountTaxCode,
                                    selectedInvoice.amount,
                                    selectedInvoice.description
                                    );

                                if (result.Status)
                                {
                                    success = true;
                                    MessageBox.Show("Aktarım Başarılı", result.Message+" :" + selectedInvoice.documentNo.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Helpers.LogFile(Helpers.LogLevel.INFO, "Tahsilat", "Tahsilat aktarımı **başarılı**.", $"Tahsilat No: {number}");
                                }
                                else
                                {
                                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Tahsilat", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Tahsilat No: {number}");
                                    MessageBox.Show(result.Message.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                #region old BANK_TRANSFER_COLLECTION code
                                //invoiceResponse = await client.InsertNewIncomingPaymentAsync(selectedInvoice.date, 
                                //    "BANKA_TAHSILAT", 
                                //    selectedInvoice.documentNo,
                                //    selectedInvoice.customerCode, 
                                //    selectedInvoice.detail.bankCode, 
                                //    selectedInvoice.detail.bankName,
                                //    "",
                                //    "18", 
                                //    selectedInvoice.amount, 
                                //    selectedInvoice.description);
                                //decimal veri nokta ile ayrıştırılacak virgül kullanılmayacak. Bank_Acc_Code,Bank_Acc_Name,Bank_Cash_Name
                                #endregion
                                remoteInvoiceNumber = selectedInvoice.documentNo;                               
                             
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
                        MessageBox.Show(ex.Message.ToString(),"Hata", MessageBoxButtons.OK,MessageBoxIcon.Error);

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
                                  errorMessage = errorMessage == "" ? "Status OK": errorMessage
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

    }
}
