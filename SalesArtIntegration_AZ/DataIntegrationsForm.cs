using OneCService;
using SalesArtIntegration_AZ.Helper;
using SalesArtIntegration_AZ.Manager.Api;
using SalesArtIntegration_AZ.Manager.Config;
using SalesArtIntegration_AZ.Manager.Service;
using SalesArtIntegration_AZ.Models.Request;
using SalesArtIntegration_AZ.Models.Response;
using System.Data;

namespace SalesArtIntegration_AZ
{
    public partial class DataIntegrationsForm : Form
    {
        private List<CustomerInfo> allCustomers;
        private readonly WebServicePortTypeClient _client;
        public DataIntegrationsForm()
        {
            InitializeComponent();
            _client = ServiceFactory.GetServiceClient();
        }

        private async void bttnGetCustomers_Click(object sender, EventArgs e)
        {
            try
            {
                Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri", "Müşteri listesi çekme işlemi başlatıldı.");

                // Her iki servisten paralel veri çekimi
                var distributorsTask = ApiManager.GetAsync<DistributorsResponseModel>(Configuration.GetUrl() + "management/distributors");
                var partnersListTask = _client.GetPartnersListAsync("", ""); // Uzak SOAP Servisindeki Müşteri Listesi

                // Yerel servis için istek hazırlama
                var requestBody = new CustomerListRequest
                {
                    pageNumber = 0,
                    pageSize = 9999,
                    data = new CustomerListRequest.Data
                    {
                        searchStr = "",
                        sortBalanceByAsc = true
                    }
                };

                // Tüm isteklerin tamamlanmasını bekle
                await Task.WhenAll(distributorsTask, partnersListTask);

                var distributors = distributorsTask.Result;
                var partnersList = partnersListTask.Result.@return;

                requestBody.distributorIds = new List<int> { distributors.data.Id };

                // Yerel müşteri listesini çek
                var customerResponseTask = ApiManager.PostAsync<CustomerListRequest, CustomerResponseJsonModel>(
                    Configuration.GetUrl() + "management/customers-list?lang=tr", requestBody);
                var customerResponse = await customerResponseTask;

                // Uzak serviste mevcut olan TIN'leri HashSet'e al (performans için)
                var existingPartnersTINs = new HashSet<string>(
                    partnersList
                        .Where(p => !string.IsNullOrWhiteSpace(p.TIN))
                        .Select(p => p.TIN),
                    StringComparer.OrdinalIgnoreCase
                );

                Helpers.LogFile(Helpers.LogLevel.DEBUG, "Müşteri", $"Uzak serviste bulunan müşteri sayısı: {existingPartnersTINs.Count}");

                if (customerResponse?.data?.customers != null)
                {
                    // Sadece uzak serviste OLMAYAN müşterileri filtrele
                    var newCustomersToDisplay = new List<CustomerInfo>();

                    foreach (var customer in customerResponse.data.customers)
                    {
                        string customerTIN = customer.taxNumber == "" ? customer.identificationNumber : customer.taxNumber;

                        // TIN boş değilse VE uzak serviste yoksa listeye ekle
                        if (!string.IsNullOrWhiteSpace(customerTIN) && !existingPartnersTINs.Contains(customerTIN))
                        {
                            newCustomersToDisplay.Add(new CustomerInfo
                            {
                                code = customer.code,
                                name = customer.name,
                                vkn = customerTIN
                            });

                            Helpers.LogFileDataIntegration($"Uzak serviste bulunmayan müşteri: ", customer.code);
                        }
                    }

                    // GridView'e bağla
                    dataGridInvoiceList.DataSource = null;
                    dataGridInvoiceList.Columns.Clear();

                    dataGridInvoiceList.DataSource = newCustomersToDisplay;
                    dataGridInvoiceList.AutoGenerateColumns = true;


                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    chk.HeaderText = "";
                    chk.MinimumWidth = 6;
                    chk.Name = "chk";
                    chk.Width = 80;
                    dataGridInvoiceList.Columns.Insert(0, chk); // İlk sıraya ekle

                    dataGridInvoiceList.Columns["code"].HeaderText = "Müşteri Numarası";
                    dataGridInvoiceList.Columns["name"].HeaderText = "Müşteri Adı";
                    dataGridInvoiceList.Columns["vkn"].HeaderText = "VKN / TCKN";

                    dataGridInvoiceList.Columns["code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridInvoiceList.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridInvoiceList.Columns["vkn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    chckAll.BringToFront();

                    Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri",
                        $"Toplam yerel müşteri: {customerResponse.data.customers.Count}, " +
                        $"Uzak serviste olmayan: {newCustomersToDisplay.Count}");
                }
                else
                {
                    Helpers.LogFile(Helpers.LogLevel.WARNING, "Müşteri", "Yerel servisten müşteri verisi alınamadı.");
                    MessageBox.Show("Müşteri verisi alınamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müşteri listesi çekilirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helpers.LogFile(Helpers.LogLevel.ERROR, "Müşteri", $"Liste çekme hatası: {ex.Message}", "Detay: bttnGetCustomers_Click");
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

        private async void bttnSendCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri", "Seçili müşterilerin transfer işlemi başlatıldı.");

                // GridView'den seçili satırları al
                var selectedCustomers = new List<CustomerInfo>();

                foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
                {
                    if (row.Cells["chk"].Value != null && Convert.ToBoolean(row.Cells["chk"].Value))
                    {
                        selectedCustomers.Add(new CustomerInfo
                        {
                            code = row.Cells["code"].Value?.ToString(),
                            name = row.Cells["name"].Value?.ToString(),
                            vkn = row.Cells["vkn"].Value?.ToString()
                        });
                    }
                }

                if (selectedCustomers.Count == 0)
                {
                    MessageBox.Show("Lütfen göndermek istediğiniz müşterileri seçin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Onay mesajı
                var confirmResult = MessageBox.Show(
                    $"{selectedCustomers.Count} adet müşteri uzak servise gönderilecek. Devam etmek istiyor musunuz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                {
                    return;
                }

                // Buton ve GridView'i devre dışı bırak
                bttnSendCustomer.Enabled = false;
                dataGridInvoiceList.Enabled = false;

                Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri",
                    $"Uzak servise aktarılacak müşteri sayısı: {selectedCustomers.Count}");

                int totalCount = selectedCustomers.Count;
                int processedCount = 0;
                int successCount = 0;
                int errorCount = 0;

                foreach (var customer in selectedCustomers)
                {
                    try
                    {
                        string partnerCode = customer.code;
                        string partnerTIN = customer.vkn;
                        string partnerName = customer.name;

                        // TIN uzunluğuna göre gerçek/tüzel kişi kontrolü (11 haneli TCKN, 10 haneli VKN)
                        bool isJuridicalPerson = partnerTIN?.Length == 10;

                        var resultValue = await _client.InsertNewPartnerAsync(
                            partnerCode,
                            partnerTIN,
                            partnerName,
                            isJuridicalPerson);

                        if (resultValue.Result)
                        {
                            processedCount++;
                            successCount++;
                            Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri",
                                $"Müşteri '{partnerName}' başarıyla kaydedildi. ({processedCount}/{totalCount})",
                                $"TIN: {partnerTIN}");
                        }
                        else
                        {
                            processedCount++;
                            errorCount++;
                            Helpers.LogFile(Helpers.LogLevel.ERROR, "Müşteri",
                                $"Müşteri '{partnerName}' kayıt edilemedi: {resultValue.Message}",
                                $"TIN: {partnerTIN}");
                        }
                    }
                    catch (Exception ex)
                    {
                        processedCount++;
                        errorCount++;
                        Helpers.LogFile(Helpers.LogLevel.ERROR, "Müşteri",
                            $"Kayıt sırasında hata: {ex.Message}",
                            $"Adı: {customer.name}, TIN: {customer.vkn}");
                    }
                }

                // Sonuç mesajı
                string resultMessage = $"Transfer işlemi tamamlandı.\n\n" +
                                       $"Toplam: {totalCount}\n" +
                                       $"Başarılı: {successCount}\n" +
                                       $"Hatalı: {errorCount}";

                MessageBox.Show(resultMessage, "Tamamlandı",
                    MessageBoxButtons.OK,
                    errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri",
                    $"Seçili müşterilerin transfer işlemi tamamlandı. Başarılı: {successCount}, Hatalı: {errorCount}");

                // Başarılı transferleri GridView'den kaldır (opsiyonel)
                if (successCount > 0)
                {
                    var confirmRemove = MessageBox.Show(
                        "Başarıyla gönderilen müşteriler listeden kaldırılsın mı?",
                        "Liste Güncelleme",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                        chckAll.Checked = false;

                    if (confirmRemove == DialogResult.Yes)
                    {
                        // Listeyi yeniden yükle
                        bttnGetCustomers_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müşteri gönderiminde bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helpers.LogFile(Helpers.LogLevel.ERROR, "Müşteri",
                    $"Genel gönderim hatası: {ex.Message}", "Detay: bttnSendCustomer_Click");
            }
            finally
            {
                // Buton ve GridView'i tekrar aktif et
                bttnSendCustomer.Enabled = true;
                dataGridInvoiceList.Enabled = true;
            }
        }

        #region helper 
        private class CustomerInfo
        {
            public string vkn { get; set; }
            public string code { get; set; }
            public string name { get; set; }
        }
        public class ProductInfo
        {
            public string code { get; set; }
            public string name { get; set; }
            public string unit { get; set; }
        }

        private async void bttnGetProducts_Click(object sender, EventArgs e)
        {
            try
            {
                Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün", "Ürün listesi çekme işlemi başlatıldı.");


                var productDataTask = ApiManager.GetAsync<ProductResponseJsonModel>(Configuration.GetUrl() + "management/products?lang=tr");
                var soapItemsListTask = _client.GetItemListAsync();

                await Task.WhenAll(productDataTask, soapItemsListTask);

                var productData = productDataTask.Result;
                var soapItemsList = soapItemsListTask.Result.@return;

                var existingItemCodes = new HashSet<string>(
                    soapItemsList
                        .Where(item => !string.IsNullOrWhiteSpace(item.ItemCodeCode))
                        .Select(item => item.ItemCodeCode),
                    StringComparer.OrdinalIgnoreCase
                );

                Helpers.LogFile(Helpers.LogLevel.DEBUG, "Ürün", $"Uzak serviste bulunan ürün sayısı: {existingItemCodes.Count}");

                if (productData?.data?.products != null)
                {

                    var newProductsToDisplay = new List<ProductInfo>();

                    foreach (var product in productData.data.products)
                    {
                        string productCode = product.Code;


                        if (!string.IsNullOrWhiteSpace(productCode) && !existingItemCodes.Contains(productCode))
                        {
                            newProductsToDisplay.Add(new ProductInfo
                            {
                                code = product.Code,
                                name = product.Name,
                                unit = product.UnitName

                            });

                            Helpers.LogFileDataIntegration($"Uzak serviste bulunmayan ürün: ", product.Code);
                        }
                    }

                    dataGridInvoiceList.DataSource = null;
                    dataGridInvoiceList.Columns.Clear();

                    dataGridInvoiceList.DataSource = newProductsToDisplay;
                    dataGridInvoiceList.AutoGenerateColumns = true;


                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    chk.HeaderText = "";
                    chk.MinimumWidth = 6;
                    chk.Name = "chk";
                    chk.Width = 80;
                    dataGridInvoiceList.Columns.Insert(0, chk); // İlk sıraya ekle

                    dataGridInvoiceList.Columns["code"].HeaderText = "Ürün Kodu";
                    dataGridInvoiceList.Columns["name"].HeaderText = "Ürün Adı";
                    dataGridInvoiceList.Columns["unit"].HeaderText = "Birim";

                    dataGridInvoiceList.Columns["code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridInvoiceList.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridInvoiceList.Columns["unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    chckAll.BringToFront();

                    Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
                        $"Toplam yerel ürün: {productData.data.products.Count}, " +
                        $"Uzak serviste olmayan: {newProductsToDisplay.Count}");
                }
                else
                {
                    Helpers.LogFile(Helpers.LogLevel.WARNING, "Ürün", "Yerel servisten ürün verisi alınamadı.");
                    MessageBox.Show("Ürün verisi alınamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün listesi çekilirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün", $"Liste çekme hatası: {ex.Message}", "Detay: bttnGetProducts_Click");
            }
        }

        private async void bttnSendProducts_Click(object sender, EventArgs e)
        {
            try
            {
                Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün", "Seçili ürünlerin transfer işlemi başlatıldı.");

                // GridView'den seçili satırları al
                var selectedProducts = new List<ProductInfo>();

                foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
                {
                    if (row.Cells["chk"].Value != null && Convert.ToBoolean(row.Cells["chk"].Value))
                    {
                        selectedProducts.Add(new ProductInfo
                        {
                            code = row.Cells["code"].Value?.ToString(),
                            name = row.Cells["name"].Value?.ToString(),
                            unit = row.Cells["unit"].Value?.ToString()
                        });
                    }
                }

                if (selectedProducts.Count == 0)
                {
                    MessageBox.Show("Lütfen göndermek istediğiniz ürünleri seçin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Onay mesajı
                var confirmResult = MessageBox.Show(
                    $"{selectedProducts.Count} adet ürün uzak servise gönderilecek. Devam etmek istiyor musunuz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                {
                    return;
                }

                bttnSendProducts.Enabled = false;
                dataGridInvoiceList.Enabled = false;

                Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
                    $"Uzak servise aktarılacak ürün sayısı: {selectedProducts.Count}");

                int totalCount = selectedProducts.Count;
                int processedCount = 0;
                int successCount = 0;
                int errorCount = 0;

                foreach (var product in selectedProducts)
                {
                    try
                    {
                        string itemCode = product.code;
                        string itemName = product.name;
                        bool isService = false;
                        string unit = product.unit;


                        var resultValue = await _client.InsertNewItemAsync(itemCode, itemName, isService, unit, 18);

                        if (resultValue.@return.Result)
                        {
                            processedCount++;
                            successCount++;
                            Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
                                $"Ürün '{itemName}' başarıyla kaydedildi. ({processedCount}/{totalCount})",
                                $"Kod: {itemCode}");
                        }
                        else
                        {
                            processedCount++;
                            errorCount++;
                            Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
                                $"Ürün '{itemName}' kayıt edilemedi: {resultValue.@return.Message}",
                                $"Kod: {itemCode}");
                        }
                    }
                    catch (Exception ex)
                    {
                        processedCount++;
                        errorCount++;
                        Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
                            $"Kayıt sırasında hata: {ex.Message}",
                            $"Adı: {product.name}, Kod: {product.code}");
                    }
                }

                // Sonuç mesajı
                string resultMessage = $"Transfer işlemi tamamlandı.\n\n" +
                                       $"Toplam: {totalCount}\n" +
                                       $"Başarılı: {successCount}\n" +
                                       $"Hatalı: {errorCount}";

                MessageBox.Show(resultMessage, "Tamamlandı",
                    MessageBoxButtons.OK,
                    errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
                    $"Seçili ürünlerin transfer işlemi tamamlandı. Başarılı: {successCount}, Hatalı: {errorCount}");

                // Başarılı transferleri GridView'den kaldır (opsiyonel)
                if (successCount > 0)
                {
                    var confirmRemove = MessageBox.Show(
                        "Başarıyla gönderilen ürünler listeden kaldırılsın mı?",
                        "Liste Güncelleme",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirmRemove == DialogResult.Yes)
                    {
                        // Listeyi yeniden yükle
                        bttnGetProducts_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün gönderiminde bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
                    $"Genel gönderim hatası: {ex.Message}", "Detay: bttnSendProducts_Click");
            }
            finally
            {
                // Buton ve GridView'i tekrar aktif et
                bttnSendProducts.Enabled = true;
                dataGridInvoiceList.Enabled = true;
            }
        }

        private void DataIntegrationsForm_Load(object sender, EventArgs e)
        {

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

        private void anaMenüyeDönToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion

        //private void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    string searchText = txtSearch.Text.Trim().ToLower();

        //    if (string.IsNullOrEmpty(searchText))
        //    {
        //        // Boşsa tüm veriyi göster
        //        dataGridInvoiceList.DataSource = allCustomers;
        //    }
        //    else
        //    {
        //        // Tüm kolonlarda arama yap
        //        var filtered = allCustomers.Where(x =>
        //            (x.vkn != null && x.vkn.ToLower().Contains(searchText)) ||
        //            (x.code != null && x.code.ToLower().Contains(searchText)) ||
        //             (x.name != null && x.name.ToLower().Contains(searchText))
        //        ).ToList();

        //        dataGridInvoiceList.DataSource = filtered;
        //    }
        //}
    }
}
