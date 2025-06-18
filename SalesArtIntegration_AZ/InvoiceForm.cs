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
                documentType = comboboxInvoiceType.SelectedValue.ToString();
            }

        }

        private void waybillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WaybillForm waybillForm = new WaybillForm();
            waybillForm.Show();
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

        private async void bttnGetInvoice_Click(object sender, EventArgs e)
        {

            // "Seçiniz" kontrolü
            if (string.IsNullOrEmpty(documentType) || documentType == "SEÇİNİZ...")
            {
                documentType = comboboxInvoiceType.SelectedValue.ToString();
            }

            string beginDate = dateTimeStartDate.Value.ToString("yyyy-MM-dd");
            string endDate = dateTimeFinishDate.Value.ToString("yyyy-MM-dd");

            var invoiceRequest = new InvoiceRequest
            {
                startDate = beginDate, //"2023-12-01",
                endDate = endDate,//"2024-01-01",
                invoiceTypes = new[] { documentType }
            };

            invoiceResponse = await ApiManager.PostAsync<InvoiceRequest, InvoiceModelResponse>(Configuration.GetUrl() + "management/invoices-for-erp", invoiceRequest);

            List<DisplayInvoiceInfo> displayInfoList = invoiceResponse.data.Select(header => new DisplayInvoiceInfo
            {
                Number = header.number,
                Date = header.date.ToShortDateString(),
                DocumentNumber = header.documentNumber,
                CustomerCode = header.customerCode,
                CustomerName = header.customerName,
                DiscountTotal = header.discountTotal.ToString(),
                VatTotal = header.vatTotal.ToString(),
                GrossTotal = header.grossTotal.ToString()
            }).ToList();
            dataGridInvoiceList.Visible = true;

            dataGridInvoiceList.DataSource = displayInfoList;
            chckAll.Visible = true;
            chckAll.BringToFront();
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
            // ServiceFactory ile istemciyi al
            using var client = ServiceFactory.GetServiceClient();


            foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
            {
                if (Convert.ToBoolean(row.Cells["chk"].Value))
                {
                    string number = row.Cells["Number"].Value?.ToString();
                    if (string.IsNullOrEmpty(number))
                    {
                        MessageBox.Show("Fatura numarası boş olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    try
                    {

                        // Fatura tipine göre işlem
                        switch (documentType)
                        {
                            case nameof(Enums.InvoiceType.SELLING):
                                // Detaylar için kontrol
                                if (selectedInvoice.details == null || !selectedInvoice.details.Any())
                                {
                                    MessageBox.Show($"Fatura {number} için detay bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                // TableLine listesi oluştur
                                var tableLines = new List<InvoiceItemTableLine>();
                                foreach (var detail in selectedInvoice.details)
                                {
                                    tableLines.Add(new InvoiceItemTableLine
                                    {
                                        ItemCode = detail.code,
                                        Quantity = detail.quantity,
                                        Unit = detail.unitCode, // Birim, gerektiğinde özelleştirin
                                        Price = Math.Round(Convert.ToDecimal(detail.price), 2) // 2 ondalık basamak
                                    });
                                }
                                var invoiceResponse = await client.InsertNewInvoiceAsync(selectedInvoice.distributorBranchCode, selectedInvoice.documentDate.ToString("yyyy-MM-dd"),
                                    selectedInvoice.number, selectedInvoice.customerName, 1, selectedInvoice.warehouseCode, tableLines.ToArray());

                                success = invoiceResponse != null; // Daha spesifik kontrol eklenebilir
                                remoteInvoiceNumber = selectedInvoice.number;

                                if (success)
                                {
                                    MessageBox.Show($"Fatura {remoteInvoiceNumber} başarıyla gönderildi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    errorMessage = "Bilinmeyen hata";
                                    MessageBox.Show($"Fatura {remoteInvoiceNumber} gönderilemedi: {errorMessage}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;

                            case nameof(Enums.InvoiceType.BUYING):
                                // Detaylar için kontrol
                                if (selectedInvoice.details == null || !selectedInvoice.details.Any())
                                {
                                    MessageBox.Show($"Fatura {number} için detay bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                // TableLine listesi oluştur
                                var tableReceiptLines = new List<ReceiptItemTableLine>();
                                foreach (var detail in selectedInvoice.details)
                                {
                                    tableReceiptLines.Add(new ReceiptItemTableLine
                                    {
                                        ItemCode = detail.code,
                                        Quantity = detail.quantity,
                                        Unit = detail.unitCode, // Birim, gerektiğinde özelleştirin
                                        Price = Math.Round(Convert.ToDecimal(detail.price), 2) // 2 ondalık basamak
                                    });
                                }

                                var invoiceBuyingResponse = await client.InsertNewReceiptAsync(selectedInvoice.distributorBranchCode, selectedInvoice.documentDate.ToString("yyyy-MM-dd"),
                                    selectedInvoice.number, selectedInvoice.customerName, 1, selectedInvoice.warehouseCode, tableReceiptLines.ToArray());

                                success = invoiceBuyingResponse != null; // Daha spesifik kontrol eklenebilir
                                remoteInvoiceNumber = selectedInvoice.number;

                                if (success)
                                {
                                    MessageBox.Show($"Fatura {remoteInvoiceNumber} başarıyla gönderildi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    errorMessage = "Bilinmeyen hata";
                                    MessageBox.Show($"Fatura {remoteInvoiceNumber} gönderilemedi: {errorMessage}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;

                            case nameof(Enums.InvoiceType.SELLING_RETURN):
                                // Detaylar için kontrol
                                if (selectedInvoice.details == null || !selectedInvoice.details.Any())
                                {
                                    MessageBox.Show($"Fatura {number} için detay bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                // TableLine listesi oluştur
                                var tableLinesRe = new List<InvoiceItemTableLine>();
                                foreach (var detail in selectedInvoice.details)
                                {
                                    tableLinesRe.Add(new InvoiceItemTableLine
                                    {
                                        ItemCode = detail.code,
                                        Quantity = detail.quantity,
                                        Unit = detail.unitCode, // Birim, gerektiğinde özelleştirin
                                        Price = Math.Round(Convert.ToDecimal(detail.price), 2) // 2 ondalık basamak
                                    });
                                }
                                var invoiceRefundResponse = await client.InsertNewRefundOfInvoiceAsync(selectedInvoice.distributorBranchCode, selectedInvoice.documentDate.ToString("yyyy-MM-dd"),
                                    selectedInvoice.number, selectedInvoice.customerName, 1, selectedInvoice.warehouseCode, tableLinesRe.ToArray());

                                success = invoiceRefundResponse != null; // Daha spesifik kontrol eklenebilir
                                remoteInvoiceNumber = selectedInvoice.number;

                                if (success)
                                {
                                    MessageBox.Show($"Fatura {remoteInvoiceNumber} başarıyla gönderildi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    errorMessage = "Bilinmeyen hata";
                                    MessageBox.Show($"Fatura {remoteInvoiceNumber} gönderilemedi: {errorMessage}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;

                            case nameof(Enums.InvoiceType.BUYING_RETURN):
                                // Detaylar için kontrol
                                if (selectedInvoice.details == null || !selectedInvoice.details.Any())
                                {
                                    MessageBox.Show($"Fatura {number} için detay bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                // TableLine listesi oluştur
                                var tableRefundReceiptLines = new List<ReceiptItemTableLine>();
                                foreach (var detail in selectedInvoice.details)
                                {
                                    tableRefundReceiptLines.Add(new ReceiptItemTableLine
                                    {
                                        ItemCode = detail.code,
                                        Quantity = detail.quantity,
                                        Unit = detail.unitCode, // Birim, gerektiğinde özelleştirin
                                        Price = Math.Round(Convert.ToDecimal(detail.price), 2) // 2 ondalık basamak
                                    });
                                }
                                var invoiceBuyingRefundResponse = await client.InsertNewRefundOfReceiptAsync(selectedInvoice.distributorBranchCode, selectedInvoice.documentDate.ToString("yyyy-MM-dd"),
                                    selectedInvoice.number, selectedInvoice.customerName, 1, selectedInvoice.warehouseCode, tableRefundReceiptLines.ToArray());

                                success = invoiceBuyingRefundResponse != null; // Daha spesifik kontrol eklenebilir
                                remoteInvoiceNumber = selectedInvoice.number;

                                if (success)
                                {
                                    MessageBox.Show($"Fatura {remoteInvoiceNumber} başarıyla gönderildi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    errorMessage = "Bilinmeyen hata";
                                    MessageBox.Show($"Fatura {remoteInvoiceNumber} gönderilemedi: {errorMessage}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
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
                                invoiceNumber = selectedInvoice.number,
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

        private void InvoiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // Form kapatıldığında uygulamayı kapat
        }
    }
}
