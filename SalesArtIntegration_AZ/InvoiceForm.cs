using OneCService;
using SalesArtIntegration_AZ.Helper;
using SalesArtIntegration_AZ.Manager.Api;
using SalesArtIntegration_AZ.Manager.Config;
using SalesArtIntegration_AZ.Manager.Service;
using SalesArtIntegration_AZ.Models.Enums;
using SalesArtIntegration_AZ.Models.Invoice;
using SalesArtIntegration_AZ.Models.Request;
using SalesArtIntegration_AZ.Models.Response;
using System.Data;
using static SalesArtIntegration_AZ.Models.Request.InvoiceSyncRequest;

namespace SalesArtIntegration_AZ
{
    public partial class InvoiceForm : Form
    {
        string documentType = "";
        InvoiceModelResponse invoiceResponse = new InvoiceModelResponse();

        public InvoiceForm()
        {
            InitializeComponent();
            LoadComboBox();
        }
        private void comboboxInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboboxInvoiceType.SelectedValue != null && comboboxInvoiceType.SelectedValue is Enums.InvoiceType)
            {
                documentType = comboboxInvoiceType.SelectedValue?.ToString() ?? string.Empty;
            }
        }

        private void collectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollectionForm collectionForm = new CollectionForm();
            collectionForm.Show();
            this.Hide();
        }

        private async void bttnGetInvoice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(documentType) || documentType == "SEÇİNİZ...")
            {
                documentType = comboboxInvoiceType.SelectedValue?.ToString() ?? string.Empty;
            }

            string beginDate = dateTimeStartDate.Value.AddDays(-1).ToString("yyyy-MM-dd");
            string endDate = dateTimeFinishDate.Value.AddDays(1).ToString("yyyy-MM-dd");

            var invoiceRequest = new InvoiceRequest
            {
                startDate = beginDate,
                endDate = endDate,
                invoiceTypes = new[] { documentType }
            };

            invoiceResponse = await ApiManager.PostAsync<InvoiceRequest, InvoiceModelResponse>(Configuration.GetUrl() + "management/invoices-for-erp", invoiceRequest);

            if (invoiceResponse?.data == null)
            {
                MessageBox.Show("Fatura bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<DisplayInvoiceInfo> displayInfoList = invoiceResponse.data.Select(header => new DisplayInvoiceInfo
            {
                Number = header.number,
                Date = header.date.ToShortDateString(),
                //DocumentNumber = header.documentNumber,
                CustomerCode = header.customerCode,
                CustomerName = header.customerName,
                DiscountTotal = header.discountTotal.ToString(),
                VatTotal = header.vatTotal.ToString(),
                GrossTotal = header.grossTotal.ToString(),
                SalesDepartmentName = header.salesDepartmentName,
                SalesGroupName = header.salesGroupName
            }).ToList();

            dataGridInvoiceList.DataSource = displayInfoList;
            dataGridInvoiceList.AutoGenerateColumns = false;

            dataGridInvoiceList.Columns["Number"].HeaderText = "Fatura Numarası";
            dataGridInvoiceList.Columns["Date"].HeaderText = "Tarih";
            //dataGridInvoiceList.Columns["DocumentNumber"].HeaderText = "Belge Numarası";
            dataGridInvoiceList.Columns["CustomerCode"].HeaderText = "Müşteri Kodu";
            dataGridInvoiceList.Columns["CustomerName"].HeaderText = "Müşteri Adı";
            dataGridInvoiceList.Columns["SalesDepartmentName"].HeaderText = "Satış Bölümü";
            dataGridInvoiceList.Columns["SalesGroupName"].HeaderText = "Satış Şefliği";
            dataGridInvoiceList.Columns["DiscountTotal"].HeaderText = "İndirim Toplamı";
            dataGridInvoiceList.Columns["VatTotal"].HeaderText = "KDV Toplamı";
            dataGridInvoiceList.Columns["GrossTotal"].HeaderText = "Genel Toplam";


            dataGridInvoiceList.Columns["Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dataGridInvoiceList.Columns["DocumentNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["CustomerCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["CustomerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["DiscountTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["VatTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["GrossTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["SalesDepartmentName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["SalesGroupName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            chckAll.BringToFront();

            foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
            {
                var invoiceNumber = row.Cells["Number"].Value?.ToString();
                if (!string.IsNullOrEmpty(invoiceNumber))
                {
                    //helper.LogFile("Fatura Numarası: ", invoiceNumber : invoiceNumber, "-", "-", "-");
                }
            }

            if (invoiceResponse != null && invoiceResponse.data != null)
            {
                string invoiceNumbers = string.Empty;
                if (invoiceResponse.data.Any())
                {
                    invoiceNumbers = string.Join(", ", invoiceResponse.data.Select(header => header.number));
                    //Helpers.LogFile("Fatura Numarası: ", invoiceNumber: invoiceNumbers, "-", "-", "-");
                    Helpers.LogFile(Helpers.LogLevel.INFO, "Fatura", "Faturalar API'dan başarıyla listelendi.", $"Numaralar: {invoiceNumbers}");

                }
                else
                {
                    invoiceNumbers = "Faturalar listeleniyor";
                }
            }
            else
            {
                //Helpers.LogFile("Faturalar listelenmedi! - Fatura response data null veya boş olamaz.", invoiceNumber: "N/A", "-", "-", "-");
                Helpers.LogFile(Helpers.LogLevel.WARNING, "Fatura", "Faturalar listelenmedi. API yanıt verisi (response data) boş veya null.", "Ek Bilgi: Veri yok");
            }
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

        private async void bttnSendInvoice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(documentType))
            {
                MessageBox.Show("Lütfen bir fatura tipi seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var client = ServiceFactory.GetServiceClient();
            string distributorCode = await Helpers.GetDistributorCodeAsync();

            foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
            {
                if (Convert.ToBoolean(row.Cells["chk"].Value))
                {
                    string? number = row.Cells["Number"].Value?.ToString();
                    if (string.IsNullOrEmpty(number))
                    {
                        MessageBox.Show("Fatura numarası boş olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Helpers.LogFile("Faturalar Aktarılmadı! ", invoiceNumber: "N/A", "-", "-", "-"); // Fix: Use "N/A" for null or empty invoiceNumber
                        Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", "Fatura numarası boş olduğu için aktarım yapılamadı.", "Numara: N/A");
                        continue;
                    }

                    var selectedInvoice = invoiceResponse?.data?.FirstOrDefault(inv => inv.number == number);

                    if (selectedInvoice == null)
                    {
                        MessageBox.Show($"Fatura numarası {number} bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    bool success = false;
                    string errorMessage = "";
                    string remoteInvoiceNumber = "";
                    bttnSendInvoice.Enabled = false;
                    bttnGetInvoice.Enabled = true;
                    string formattedDate = selectedInvoice.documentDate.ToString("yyyyMMdd");

                    try
                    {
                        bttnGetInvoice.Enabled = false;
                        var tableLines = new List<InvoiceItemTableLine>();

                        var tableReceiptLines = new List<ReceiptItemTableLine>();

                        InsertNewInvoiceResponse invoiceResponse = null; 
                        InsertNewRefundOfInvoiceResponse invoiceResponseRefund = null; 
                        switch (documentType)
                        {
                            case nameof(Enums.InvoiceType.SELLING):
                                if (selectedInvoice.details == null || !selectedInvoice.details.Any())
                                {
                                    MessageBox.Show($"Fatura {number} için detay bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                 tableLines = new List<InvoiceItemTableLine>();
                                foreach (var detail in selectedInvoice.details)
                                {
                                    tableLines.Add(new InvoiceItemTableLine
                                    {
                                        ItemCode = detail.code,
                                        Quantity = (decimal)detail.quantity,
                                        Unit = detail.unitCode,
                                        Price = Math.Round(Convert.ToDecimal((detail.grossTotal - detail.discountTotal) / detail.quantity), 2) //Math.Round(Convert.ToDecimal(detail.price), 2)

                                    });
                                }

                                invoiceResponse = await client.InsertNewInvoiceAsync(selectedInvoice.distCode = distributorCode, formattedDate,
                                    selectedInvoice.number, selectedInvoice.customerCode, 1, selectedInvoice.warehouseCode, tableLines.ToArray(),
                                    selectedInvoice.customerCode+"C"+selectedInvoice.salesDepartmentName+
                                    "_"
                                    +selectedInvoice.customerName+
                                    "_"+
                                    selectedInvoice.number+
                                    "_"+
                                    selectedInvoice.salesGroupName);

                                remoteInvoiceNumber = selectedInvoice.number;

                                if (invoiceResponse.@return.ErrorTable != null && invoiceResponse.@return.ErrorTable.Any())
                                {
                                    success = false;
                                    errorMessage = string.Join(Environment.NewLine, invoiceResponse.@return.ErrorTable.Select(e => e.ErrorMessage));
                                    MessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //Helpers.LogFile("Faturalar Aktarılmadı! ", $"Fatura {number}", errorMessage, "-", "-");
                                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Fatura No: {number}");
                                    bttnSendInvoice.Enabled = true;
                                    bttnGetInvoice.Enabled = true;
                                }
                                else
                                {
                                    success = true;
                                    MessageBox.Show("Aktarım Başarılı" + $"Fatura {number}", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //Helpers.LogFile("Aktarım Başarılı. Fatura No: ", $"Fatura {number}", "-", "-", "-");
                                    Helpers.LogFile(Helpers.LogLevel.INFO, "Fatura", "Fatura aktarımı **başarılı**.", $"Fatura No: {number}");
                                    bttnSendInvoice.Enabled = true;
                                    bttnGetInvoice.Enabled = true;
                                }
                                break;
                            case nameof(Enums.InvoiceType.BUYING):
                                if (selectedInvoice.details == null || !selectedInvoice.details.Any())
                                {
                                    MessageBox.Show($"Fatura {number} için detay bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                tableReceiptLines = new List<ReceiptItemTableLine>();
                                foreach (var detail in selectedInvoice.details)
                                {
                                    tableReceiptLines.Add(new ReceiptItemTableLine
                                    {
                                        ItemCode = detail.code,
                                        Quantity = (decimal)detail.quantity,
                                        Unit = detail.unitCode,
                                        Price = Math.Round(Convert.ToDecimal((detail.grossTotal - detail.discountTotal) / detail.quantity), 2) //Math.Round(Convert.ToDecimal(detail.price), 2)

                                    });
                                }

                               var ınsertNewRefundOfReceiptResponse = await client.InsertNewReceiptAsync(selectedInvoice.distCode = distributorCode, formattedDate,
                                   selectedInvoice.number, selectedInvoice.customerCode, 1, selectedInvoice.warehouseCode, tableReceiptLines.ToArray());

                                remoteInvoiceNumber = selectedInvoice.number;

                                if (ınsertNewRefundOfReceiptResponse.@return.ErrorTable != null && ınsertNewRefundOfReceiptResponse.@return.ErrorTable.Any())
                                {
                                    success = false;
                                    errorMessage = string.Join(Environment.NewLine, ınsertNewRefundOfReceiptResponse.@return.ErrorTable.Select(e => e.ErrorMessage));
                                    MessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //Helpers.LogFile("Faturalar Aktarılmadı! ", $"Fatura {number}", errorMessage, "-", "-");
                                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Fatura No: {number}");
                                    bttnSendInvoice.Enabled = true;
                                    bttnGetInvoice.Enabled = true;
                                }
                                else
                                {
                                    success = true;
                                    MessageBox.Show("Aktarım Başarılı" + $"Fatura {number}", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //Helpers.LogFile("Aktarım Başarılı. Fatura No: ", $"Fatura {number}", "-", "-", "-");
                                    Helpers.LogFile(Helpers.LogLevel.INFO, "Fatura", "Fatura aktarımı **başarılı**.", $"Fatura No: {number}");
                                    bttnSendInvoice.Enabled = true;
                                    bttnGetInvoice.Enabled = true;
                                }
                                break;
                            case nameof(Enums.InvoiceType.SELLING_RETURN):
                            case nameof(Enums.InvoiceType.DAMAGED_SELLING_RETURN):
                                if (selectedInvoice.details == null || !selectedInvoice.details.Any())
                                {
                                    MessageBox.Show($"Fatura {number} için detay bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                tableLines = new List<InvoiceItemTableLine>();
                                foreach (var detail in selectedInvoice.details)
                                {
                                    tableLines.Add(new InvoiceItemTableLine
                                    {
                                        ItemCode = detail.code,
                                        Quantity = (decimal)detail.quantity,
                                        Unit = detail.unitCode,
                                        Price = Math.Round(Convert.ToDecimal((detail.grossTotal - detail.discountTotal) / detail.quantity), 2) //Math.Round(Convert.ToDecimal(detail.price), 2)

                                    });
                                }

                                invoiceResponseRefund = await client.InsertNewRefundOfInvoiceAsync(selectedInvoice.distCode = distributorCode, formattedDate,
                                   selectedInvoice.number, selectedInvoice.customerCode, 1, selectedInvoice.warehouseCode, tableLines.ToArray());

                                remoteInvoiceNumber = selectedInvoice.number;

                                if (invoiceResponseRefund.@return.ErrorTable != null && invoiceResponseRefund.@return.ErrorTable.Any())
                                {
                                    success = false;
                                    errorMessage = string.Join(Environment.NewLine, invoiceResponseRefund.@return.ErrorTable.Select(e => e.ErrorMessage));
                                    MessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //Helpers.LogFile("Faturalar Aktarılmadı! ", $"Fatura {number}", errorMessage, "-", "-");
                                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Fatura No: {number}");
                                    bttnSendInvoice.Enabled = true;
                                    bttnGetInvoice.Enabled = true;
                                }
                                else
                                {
                                    success = true;
                                    MessageBox.Show("Aktarım Başarılı" + $"Fatura {number}", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //Helpers.LogFile("Aktarım Başarılı. Fatura No: ", $"Fatura {number}", "-", "-", "-");
                                    Helpers.LogFile(Helpers.LogLevel.INFO, "Fatura", "Fatura aktarımı **başarılı**.", $"Fatura No: {number}");
                                    bttnSendInvoice.Enabled = true;
                                    bttnGetInvoice.Enabled = true;
                                }
                                break;
                            case nameof(Enums.InvoiceType.BUYING_RETURN):
                            case nameof(Enums.InvoiceType.DAMAGED_BUYING_RETURN):
                                if (selectedInvoice.details == null || !selectedInvoice.details.Any())
                                {
                                    MessageBox.Show($"Fatura {number} için detay bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                tableReceiptLines = new List<ReceiptItemTableLine>();
                                foreach (var detail in selectedInvoice.details)
                                {
                                    tableReceiptLines.Add(new ReceiptItemTableLine
                                    {
                                        ItemCode = detail.code,
                                        Quantity = (decimal)detail.quantity,
                                        Unit = detail.unitCode,
                                        Price = Math.Round(Convert.ToDecimal((detail.grossTotal - detail.discountTotal) / detail.quantity), 2) //Math.Round(Convert.ToDecimal(detail.price), 2)

                                    });
                                }

                               var invoiceResponseRefundResponse = await client.InsertNewRefundOfReceiptAsync(selectedInvoice.distCode = distributorCode, formattedDate,
                                   selectedInvoice.number, selectedInvoice.customerCode, 1, selectedInvoice.warehouseCode, tableReceiptLines.ToArray());

                                remoteInvoiceNumber = selectedInvoice.number;

                                if (invoiceResponseRefundResponse.@return.ErrorTable != null && invoiceResponseRefundResponse.@return.ErrorTable.Any())
                                {
                                    success = false;
                                    errorMessage = string.Join(Environment.NewLine, invoiceResponseRefundResponse.@return.ErrorTable.Select(e => e.ErrorMessage));
                                    MessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //Helpers.LogFile("Faturalar Aktarılmadı! ", $"Fatura {number}", errorMessage, "-", "-");
                                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Fatura No: {number}");
                                    bttnSendInvoice.Enabled = true;
                                    bttnGetInvoice.Enabled = true;
                                }
                                else
                                {
                                    success = true;
                                    MessageBox.Show("Aktarım Başarılı" + $"Fatura {number}", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //Helpers.LogFile("Aktarım Başarılı. Fatura No: ", $"Fatura {number}", "-", "-", "-");
                                    Helpers.LogFile(Helpers.LogLevel.INFO, "Fatura", "Fatura aktarımı **başarılı**.", $"Fatura No: {number}");
                                    bttnSendInvoice.Enabled = true;
                                    bttnGetInvoice.Enabled = true;
                                }
                                break;
                            default:
                                errorMessage = $"Desteklenmeyen fatura tipi: {documentType}";
                                bttnSendInvoice.Enabled = true;
                                bttnGetInvoice.Enabled = true;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                        MessageBox.Show(ex.Message.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Helpers.LogFile("Bağlantı hatası", ex.Message.ToString(), "-", "-", "-");
                        Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", $"**Bağlantı/İşlem Hatası** oluştu: {ex.Message}", "Detay: Try-Catch Bloğu");
                    }

                    #region Faturalar Başarılı/Başarısız İşaretle
                    InvoiceSyncRequest syncRequest = new InvoiceSyncRequest
                    {
                        integratedInvoices = new[]
                        {
                            new IntegratedInvoice
                            {
                                successfullyIntegrated = success,
                                invoiceNumber = selectedInvoice.number,
                                remoteInvoiceNumber = remoteInvoiceNumber,
                                errorMessage = errorMessage
                            }
                        }
                    };

                    var syncResponse = await ApiManager.PutAsync<InvoiceSyncRequest, InvoiceSyncResponse>(
                        syncRequest, Configuration.GetUrl() + "management/sync-invoice-statuses");
                    bttnSendInvoice.Enabled = true;
                    bttnGetInvoice.Enabled = true;
                    #endregion
                }
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
        private void bttnLogs_Click(object sender, EventArgs e)
        {
            invoiceListLogs invoiceListLogs = new invoiceListLogs();
            invoiceListLogs.Show();
        }

        private void anaMenuyeDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();
            this.Hide();
        }

        private void InvoiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
