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
using System.Collections;
using System.Text.RegularExpressions;
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

        // using System.Text.RegularExpressions; // dosyanızın başına ekleyin

        private async void bttnSendInvoice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(documentType))
            {
                MessageBox.Show("Lütfen bir fatura tipi seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var successfulInvoices = new List<string>();
            // Üçüncü eleman: hatalı ürün kodları listesi
            var failedInvoices = new List<(string InvoiceNo, string ErrorMessage, List<string> FailedProducts)>();

            using var client = ServiceFactory.GetServiceClient();
            string distributorCode = await Helpers.GetDistributorCodeAsync();

            bttnSendInvoice.Enabled = false;
            bttnGetInvoice.Enabled = false;

            foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
            {
                if (!Convert.ToBoolean(row.Cells["chk"].Value))
                    continue;

                string? number = row.Cells["Number"].Value?.ToString();
                if (string.IsNullOrEmpty(number))
                {
                    failedInvoices.Add(("N/A", "Fatura numarası boş olamaz!", new List<string>()));
                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", "Fatura numarası boş olduğu için aktarım yapılamadı.", "Numara: N/A");
                    continue;
                }

                var selectedInvoice = invoiceResponse?.data?.FirstOrDefault(inv => inv.number == number);
                if (selectedInvoice == null)
                {
                    failedInvoices.Add((number, "Fatura bulunamadı!", new List<string>()));
                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", $"Fatura numarası {number} bulunamadı!", $"Fatura No: {number}");
                    continue;
                }

                bool success = false;
                string errorMessage = "";
                string remoteInvoiceNumber = "";
                string formattedDate = selectedInvoice.documentDate.ToString("yyyyMMdd");

                try
                {
                    // Hatalı ürünleri bu listede toplayacağız
                    var failedProducts = new List<string>();

                    // Temel detay doğrulaması: kod, miktar, birim, fiyat
                    if (selectedInvoice.details == null || !selectedInvoice.details.Any())
                    {
                        failedInvoices.Add((number, "Fatura için detay bulunamadı!", new List<string>()));
                        continue;
                    }

                    for (int i = 0; i < selectedInvoice.details.Count; i++)
                    {
                        var detail = selectedInvoice.details[i];
                        var lineIndex = i + 1;
                        if (string.IsNullOrWhiteSpace(detail.code))
                            failedProducts.Add($"Satır {lineIndex}: Kod boş");
                        if (detail.quantity <= 0)
                            failedProducts.Add($"{detail.code ?? "Kod Yok"}: Miktar {detail.quantity}");
                        if (string.IsNullOrWhiteSpace(detail.unitCode))
                            failedProducts.Add($"{detail.code ?? "Kod Yok"}: Birim boş");
                        // Hesaplanan fiyatı kontrol et
                        decimal price = 0;
                        try
                        {
                            price = Math.Round(Convert.ToDecimal((detail.grossTotal - detail.discountTotal) / detail.quantity), 6);
                            if (price <= 0)
                                failedProducts.Add($"{detail.code ?? "Kod Yok"}: Hesaplanan fiyat {price}");
                        }
                        catch
                        {
                            failedProducts.Add($"{detail.code ?? "Kod Yok"}: Fiyat hesaplama hatası");
                        }
                    }

                    // Eğer doğrulama hatası varsa, SOAP çağrısı yapmadan kaydet ve devam et
                    if (failedProducts.Any())
                    {
                        failedInvoices.Add((number, "Detay doğrulama hatası", failedProducts));
                        Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", $"Detay doğrulama hataları bulundu: {string.Join(" | ", failedProducts)}", $"Fatura No: {number}");
                        continue;
                    }

                    var tableLines = new List<InvoiceItemTableLine>();
                    var tableReceiptLines = new List<ReceiptItemTableLine>();

                    InsertNewInvoiceResponse invoiceResponse = null;
                    InsertNewRefundOfInvoiceResponse invoiceResponseRefund = null;

                    switch (documentType)
                    {
                        case nameof(Enums.InvoiceType.SELLING):
                            foreach (var detail in selectedInvoice.details)
                            {
                                tableLines.Add(new InvoiceItemTableLine
                                {
                                    ItemCode = detail.code,
                                    Quantity = (decimal)detail.quantity,
                                    Unit = detail.unitCode,
                                    Price = Math.Round(Convert.ToDecimal((detail.grossTotal - detail.discountTotal) / detail.quantity), 6)
                                });
                            }
                            int vat = 0;
                            invoiceResponse = await client.InsertNewInvoiceAsync(
                                selectedInvoice.distCode = distributorCode,
                                formattedDate,
                                selectedInvoice.number,
                                selectedInvoice.customerCode,
                                vat,
                                selectedInvoice.warehouseCode,
                                tableLines.ToArray(),
                                selectedInvoice.customerCode + "C" + selectedInvoice.salesDepartmentName +
                                "_" + selectedInvoice.customerName +
                                "_" + selectedInvoice.number +
                                "_" + selectedInvoice.salesGroupName);
                            remoteInvoiceNumber = selectedInvoice.number;

                            if (invoiceResponse.@return.ErrorTable != null && invoiceResponse.@return.ErrorTable.Any())
                            {
                                success = false;
                                errorMessage = string.Join(" | ", invoiceResponse.@return.ErrorTable.Select(e => e.ErrorMessage));

                                // Hata mesajından fatura detaylarındaki kodları tespit etmeye çalış
                                var productCodesFromDetails = selectedInvoice.details.Select(d => d.code).Where(c => !string.IsNullOrEmpty(c)).ToList();
                                var matchedCodes = productCodesFromDetails.Where(c => errorMessage.Contains(c)).Distinct().ToList();
                                if (matchedCodes.Any())
                                    failedProducts.AddRange(matchedCodes);

                                failedInvoices.Add((number, errorMessage, failedProducts));
                                Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Fatura No: {number}");
                            }
                            else
                            {
                                success = true;
                                successfulInvoices.Add(number);
                                Helpers.LogFile(Helpers.LogLevel.INFO, "Fatura", "Fatura aktarımı **başarılı**.", $"Fatura No: {number}");
                            }
                            break;

                        case nameof(Enums.InvoiceType.BUYING):
                            foreach (var detail in selectedInvoice.details)
                            {
                                tableReceiptLines.Add(new ReceiptItemTableLine
                                {
                                    ItemCode = detail.code,
                                    Quantity = (decimal)detail.quantity,
                                    Unit = detail.unitCode,
                                    Price = Math.Round(Convert.ToDecimal((detail.grossTotal - detail.discountTotal) / detail.quantity), 6)
                                });
                            }
                            var insertNewRefundOfReceiptResponse = await client.InsertNewReceiptAsync(
                                selectedInvoice.distCode = distributorCode,
                                formattedDate,
                                selectedInvoice.number,
                                selectedInvoice.customerCode,
                                0,
                                selectedInvoice.warehouseCode,
                                tableReceiptLines.ToArray());
                            remoteInvoiceNumber = selectedInvoice.number;

                            if (insertNewRefundOfReceiptResponse.@return.ErrorTable != null && insertNewRefundOfReceiptResponse.@return.ErrorTable.Any())
                            {
                                success = false;
                                errorMessage = string.Join(" | ", insertNewRefundOfReceiptResponse.@return.ErrorTable.Select(e => e.ErrorMessage));
                                var productCodesFromDetails = selectedInvoice.details.Select(d => d.code).Where(c => !string.IsNullOrEmpty(c)).ToList();
                                var matchedCodes = productCodesFromDetails.Where(c => errorMessage.Contains(c)).Distinct().ToList();
                                if (matchedCodes.Any())
                                    failedProducts.AddRange(matchedCodes);

                                failedInvoices.Add((number, errorMessage, failedProducts));
                                Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Fatura No: {number}");
                            }
                            else
                            {
                                success = true;
                                successfulInvoices.Add(number);
                                Helpers.LogFile(Helpers.LogLevel.INFO, "Fatura", "Fatura aktarımı **başarılı**.", $"Fatura No: {number}");
                            }
                            break;

                        case nameof(Enums.InvoiceType.SELLING_RETURN):
                        case nameof(Enums.InvoiceType.DAMAGED_SELLING_RETURN):
                            foreach (var detail in selectedInvoice.details)
                            {
                                tableLines.Add(new InvoiceItemTableLine
                                {
                                    ItemCode = detail.code,
                                    Quantity = (decimal)detail.quantity,
                                    Unit = detail.unitCode,
                                    Price = Math.Round(Convert.ToDecimal((detail.grossTotal - detail.discountTotal) / detail.quantity), 6)
                                });
                            }
                            invoiceResponseRefund = await client.InsertNewRefundOfInvoiceAsync(
                                selectedInvoice.distCode = distributorCode,
                                formattedDate,
                                selectedInvoice.number,
                                selectedInvoice.customerCode,
                                0,
                                selectedInvoice.warehouseCode,
                                tableLines.ToArray());
                            remoteInvoiceNumber = selectedInvoice.number;

                            if (invoiceResponseRefund.@return.ErrorTable != null && invoiceResponseRefund.@return.ErrorTable.Any())
                            {
                                success = false;
                                errorMessage = string.Join(" | ", invoiceResponseRefund.@return.ErrorTable.Select(e => e.ErrorMessage));
                                var productCodesFromDetails = selectedInvoice.details.Select(d => d.code).Where(c => !string.IsNullOrEmpty(c)).ToList();
                                var matchedCodes = productCodesFromDetails.Where(c => errorMessage.Contains(c)).Distinct().ToList();
                                if (matchedCodes.Any())
                                    failedProducts.AddRange(matchedCodes);

                                failedInvoices.Add((number, errorMessage, failedProducts));
                                Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Fatura No: {number}");
                            }
                            else
                            {
                                success = true;
                                successfulInvoices.Add(number);
                                Helpers.LogFile(Helpers.LogLevel.INFO, "Fatura", "Fatura aktarımı **başarılı**.", $"Fatura No: {number}");
                            }
                            break;

                        case nameof(Enums.InvoiceType.BUYING_RETURN):
                        case nameof(Enums.InvoiceType.DAMAGED_BUYING_RETURN):
                            foreach (var detail in selectedInvoice.details)
                            {
                                tableReceiptLines.Add(new ReceiptItemTableLine
                                {
                                    ItemCode = detail.code,
                                    Quantity = (decimal)detail.quantity,
                                    Unit = detail.unitCode,
                                    Price = Math.Round(Convert.ToDecimal((detail.grossTotal - detail.discountTotal) / detail.quantity), 6)
                                });
                            }
                            var invoiceResponseRefundResponse = await client.InsertNewRefundOfReceiptAsync(
                                selectedInvoice.distCode = distributorCode,
                                formattedDate,
                                selectedInvoice.number,
                                selectedInvoice.customerCode,
                                0,
                                selectedInvoice.warehouseCode,
                                tableReceiptLines.ToArray());
                            remoteInvoiceNumber = selectedInvoice.number;

                            if (invoiceResponseRefundResponse.@return.ErrorTable != null && invoiceResponseRefundResponse.@return.ErrorTable.Any())
                            {
                                success = false;
                                errorMessage = string.Join(" | ", invoiceResponseRefundResponse.@return.ErrorTable.Select(e => e.ErrorMessage));
                                var productCodesFromDetails = selectedInvoice.details.Select(d => d.code).Where(c => !string.IsNullOrEmpty(c)).ToList();
                                var matchedCodes = productCodesFromDetails.Where(c => errorMessage.Contains(c)).Distinct().ToList();
                                if (matchedCodes.Any())
                                    failedProducts.AddRange(matchedCodes);

                                failedInvoices.Add((number, errorMessage, failedProducts));
                                Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", $"Aktarım sırasında **SOAP Hatası** oluştu: {errorMessage}", $"Fatura No: {number}");
                            }
                            else
                            {
                                success = true;
                                successfulInvoices.Add(number);
                                Helpers.LogFile(Helpers.LogLevel.INFO, "Fatura", "Fatura aktarımı **başarılı**.", $"Fatura No: {number}");
                            }
                            break;

                        default:
                            errorMessage = $"Desteklenmeyen fatura tipi: {documentType}";
                            failedInvoices.Add((number, errorMessage, new List<string>()));
                            break;
                    }
                }
                catch (Exception ex)
                {
                    var failedProducts = new List<string>();
                    errorMessage = ex.Message;
                    // Eğer exception mesajı ürün kodu içeriyorsa eklemeye çalış
                    if (selectedInvoice?.details != null)
                    {
                        var productCodesFromDetails = selectedInvoice.details.Select(d => d.code).Where(c => !string.IsNullOrEmpty(c)).ToList();
                        var matchedCodes = productCodesFromDetails.Where(c => errorMessage.Contains(c)).Distinct().ToList();
                        if (matchedCodes.Any())
                            failedProducts.AddRange(matchedCodes);
                    }

                    failedInvoices.Add((number, errorMessage, failedProducts));
                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Fatura", $"**Bağlantı/İşlem Hatası** oluştu: {ex.Message}", $"Fatura No: {number}");
                }

                // Sync durumunu gönder (orijinal alanları bozulmadan bırakıldı)
                InvoiceSyncRequest syncRequest = new InvoiceSyncRequest
                {
                    integratedInvoices = new[]
                    {
                new IntegratedInvoice
                {
                    successfullyIntegrated = success,
                    invoiceNumber = selectedInvoice?.number,
                    remoteInvoiceNumber = remoteInvoiceNumber,
                    errorMessage = errorMessage
                }
            }
                };
                var syncResponse = await ApiManager.PutAsync<InvoiceSyncRequest, InvoiceSyncResponse>(
                    syncRequest, Configuration.GetUrl() + "management/sync-invoice-statuses");
            }

            bttnSendInvoice.Enabled = true;
            bttnGetInvoice.Enabled = true;

            // Güncellenmiş ShowInvoiceSummary çağrısı
            ShowInvoiceSummary(successfulInvoices, failedInvoices);
        }

        private void ShowInvoiceSummary(List<string> successful, List<(string InvoiceNo, string ErrorMessage, List<string> FailedProducts)> failed)
        {
            var summary = new System.Text.StringBuilder();
            summary.AppendLine("╔════════════════════════════════════════╗");
            summary.AppendLine("      ║          FATURA AKTARIM RAPORU       ║");
            summary.AppendLine("╚════════════════════════════════════════╝\n");

            if (successful.Count > 0)
            {
                summary.AppendLine($"✓ BAŞARILI AKTARIMLAR: {successful.Count} Kayıt");
                summary.AppendLine("─────────────────────────────────────────");
                for (int i = 0; i < successful.Count; i++)
                {
                    summary.Append(successful[i]);
                    if (i < successful.Count - 1)
                    {
                        summary.Append(", ");
                        if ((i + 1) % 5 == 0)
                            summary.AppendLine();
                    }
                }
                summary.AppendLine("\n");
            }

            if (failed.Count > 0)
            {
                summary.AppendLine($"✗ BAŞARISIZ AKTARIMLAR: {failed.Count} Kayıt");
                summary.AppendLine("─────────────────────────────────────────");
                foreach (var (invoiceNo, error, failedProducts) in failed)
                {
                    summary.AppendLine($"• {invoiceNo}");
                    summary.AppendLine($"  Hata: {error}");
                    if (failedProducts != null && failedProducts.Any())
                    {
                        summary.AppendLine($"  Hatalı Ürünler ({failedProducts.Count}):");
                        // Her hatalı ürünü bir satırda göster
                        foreach (var p in failedProducts.Distinct())
                        {
                            summary.AppendLine($"    - {p}");
                        }
                    }
                    summary.AppendLine();
                }
            }

            summary.AppendLine("═════════════════════════════════════════");
            summary.AppendLine($"Toplam İşlem: {successful.Count + failed.Count}");
            summary.AppendLine($"Başarılı: {successful.Count} | Başarısız: {failed.Count}");
            if (successful.Count + failed.Count > 0)
            {
                double successRate = (double)successful.Count / (successful.Count + failed.Count) * 100;
                summary.AppendLine($"Başarı Oranı: %{successRate:F1}");
            }

            MessageBoxIcon icon = failed.Count == 0 ? MessageBoxIcon.Information :
                                  successful.Count == 0 ? MessageBoxIcon.Error :
                                  MessageBoxIcon.Warning;
            string title = failed.Count == 0 ? "Aktarım Başarılı" :
                           successful.Count == 0 ? "Aktarım Başarısız" :
                           "Aktarım Tamamlandı";
            MessageBox.Show(summary.ToString(), title, MessageBoxButtons.OK, icon);
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

        private void veriAktarımıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataIntegrationsForm dataIntegrationsForm = new DataIntegrationsForm();
            dataIntegrationsForm.Show();
        }
    }
}
