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

        private async void bttnGetInvoice_Click(object sender, EventArgs e)
        {



            if (string.IsNullOrEmpty(documentType) || documentType == "SEÇİNİZ...")
            {
                documentType = comboboxInvoiceType.SelectedValue?.ToString() ?? string.Empty;
            }

            string beginDate = dateTimeStartDate.Value.ToString("yyyy-MM-dd");
            string endDate = dateTimeFinishDate.Value.ToString("yyyy-MM-dd");

            var invoiceRequest = new InvoiceRequest
            {
                startDate = beginDate,
                endDate = endDate,
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

            dataGridInvoiceList.DataSource = displayInfoList;
            dataGridInvoiceList.AutoGenerateColumns = false;

            dataGridInvoiceList.Columns["Number"].HeaderText = "Fatura Numarası";
            dataGridInvoiceList.Columns["Date"].HeaderText = "Tarih";
            dataGridInvoiceList.Columns["DocumentNumber"].HeaderText = "Belge Numarası";
            dataGridInvoiceList.Columns["CustomerCode"].HeaderText = "Müşteri Kodu";
            dataGridInvoiceList.Columns["CustomerName"].HeaderText = "Müşteri Adı";
            dataGridInvoiceList.Columns["DiscountTotal"].HeaderText = "İndirim Toplamı";
            dataGridInvoiceList.Columns["VatTotal"].HeaderText = "KDV Toplamı";
            dataGridInvoiceList.Columns["GrossTotal"].HeaderText = "Genel Toplam";

            dataGridInvoiceList.Columns["Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["DocumentNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["CustomerCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["CustomerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["DiscountTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["VatTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridInvoiceList.Columns["GrossTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

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
                    Helpers.LogFile("Fatura Numarası: ", invoiceNumber: invoiceNumbers, "-", "-", "-");
                }
                else
                {
                    invoiceNumbers = "Faturalar listeleniyor";
                }
            }
            else
            {
                Helpers.LogFile("Faturalar listelenmedi! - Fatura response data null veya boş olamaz.", invoiceNumber: "N/A", "-", "-", "-");
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
                        Helpers.LogFile("Faturalar Aktarılmadı! ", invoiceNumber: "N/A", "-", "-", "-"); // Fix: Use "N/A" for null or empty invoiceNumber
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
                        switch (documentType)
                        {
                            case nameof(Enums.InvoiceType.SELLING):
                                if (selectedInvoice.details == null || !selectedInvoice.details.Any())
                                {
                                    MessageBox.Show($"Fatura {number} için detay bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                var tableLines = new List<InvoiceItemTableLine>();
                                foreach (var detail in selectedInvoice.details)
                                {
                                    tableLines.Add(new InvoiceItemTableLine
                                    {
                                        ItemCode = detail.code,
                                        Quantity = detail.quantity,
                                        Unit = detail.unitCode,
                                        Price = Math.Round(Convert.ToDecimal(detail.price), 2)
                                    });
                                }

                                var invoiceResponse = await client.InsertNewInvoiceAsync(selectedInvoice.distCode = distributorCode, formattedDate,
                                    selectedInvoice.number, selectedInvoice.customerCode, 1, selectedInvoice.warehouseCode, tableLines.ToArray());

                                remoteInvoiceNumber = selectedInvoice.number;

                                if (invoiceResponse.@return.ErrorTable != null && invoiceResponse.@return.ErrorTable.Any())
                                {
                                    success = false;
                                    errorMessage = string.Join(Environment.NewLine, invoiceResponse.@return.ErrorTable.Select(e => e.ErrorMessage));
                                    MessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Helpers.LogFile("Faturalar Aktarılmadı! ", $"Fatura {number}", errorMessage, "-", "-");
                                    bttnSendInvoice.Enabled = true;
                                    bttnGetInvoice.Enabled = true;
                                }
                                else
                                {
                                    success = true;
                                    MessageBox.Show("Aktarım Başarılı" + $"Fatura {number}", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Helpers.LogFile("Aktarım Başarılı. Fatura No: ", $"Fatura {number}", "-", "-", "-");
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
                        Helpers.LogFile("Bağlantı hatası", ex.Message.ToString(), "-", "-", "-");
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
            System.Windows.Forms.Application.Exit();
            Application.Exit();
        }

        private void InvoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
            Application.Exit();
        }
    }
}
