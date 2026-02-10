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
            bttnSendProducts.Enabled = false;
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

                //Uzak serviste mevcut olan TIN'leri HashSet'e al(performans için)
                var existingPartnersTINs = new HashSet<string>(
                    partnersList
                        .Where(p => !string.IsNullOrWhiteSpace(p.TIN))
                        .Select(p => p.TIN),
                    StringComparer.OrdinalIgnoreCase
                );
                bttnSendCustomer.Enabled = true;
                Helpers.LogFile(Helpers.LogLevel.DEBUG, "Müşteri", $"1C'te bulunan müşteri sayısı: {existingPartnersTINs.Count}");

                if (customerResponse?.data?.customers != null)
                {
                    // Sadece uzak serviste OLMAYAN müşterileri filtrele
                    var newCustomersToDisplay = new List<CustomerInfo>();

                    foreach (var customer in customerResponse.data.customers)
                    {
                        string customerTIN = customer.taxNumber == "" ? customer.identificationNumber : customer.taxNumber;

                        // TIN boş değilse VE uzak serviste yoksa listeye ekle
                        if (!string.IsNullOrWhiteSpace(customerTIN) )
                        {
                            newCustomersToDisplay.Add(new CustomerInfo
                            {
                                code = customer.code,
                                name = customer.shortName,
                                fullName = customer.name,
                                vkn = customerTIN
                            });

                            Helpers.LogFileDataIntegration($"1C'te kayıtlı olmayan müşteri: ", customer.code);
                        }
                    }

                    // GridView'e bağla
                    dataGridDataList.DataSource = null;
                    dataGridDataList.Columns.Clear();

                    dataGridDataList.DataSource = newCustomersToDisplay;
                    dataGridDataList.AutoGenerateColumns = true;


                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    chk.HeaderText = "";
                    chk.MinimumWidth = 6;
                    chk.Name = "chk";
                    chk.Width = 80;
                    dataGridDataList.Columns.Insert(0, chk); // İlk sıraya ekle

                    dataGridDataList.Columns["code"].HeaderText = "Müşteri Numarası";
                    dataGridDataList.Columns["name"].HeaderText = "Müşteri Adı";
                    dataGridDataList.Columns["fullName"].HeaderText = "Müşteri Voen Adı";
                    dataGridDataList.Columns["vkn"].HeaderText = "Voen No";

                    dataGridDataList.Columns["code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridDataList.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridDataList.Columns["fullName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridDataList.Columns["vkn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                    chckAll.BringToFront();

                    Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri",
                        $"SalesArt'taki Toplam Müşteri sayısı : {customerResponse.data.customers.Count}, " +
                        $"1C'te olmayan Müşteri sayısı : {newCustomersToDisplay.Count}");
                }
                else
                {
                    Helpers.LogFile(Helpers.LogLevel.WARNING, "Müşteri", "SalesArt'tan Müşteri verisi alınamadı.");
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
                foreach (DataGridViewRow row in dataGridDataList.Rows)
                {
                    row.Cells["chk"].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridDataList.Rows)
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

                foreach (DataGridViewRow row in dataGridDataList.Rows)
                {
                    if (row.Cells["chk"].Value != null && Convert.ToBoolean(row.Cells["chk"].Value))
                    {
                        selectedCustomers.Add(new CustomerInfo
                        {
                            code = row.Cells["code"].Value?.ToString() ?? string.Empty,
                            name = row.Cells["name"].Value?.ToString() ?? string.Empty,
                            vkn = row.Cells["vkn"].Value?.ToString() ?? string.Empty,
                            fullName = row.Cells["fullName"].Value?.ToString() ?? string.Empty,

                        });
                    }
                }

                if (selectedCustomers.Count == 0)
                {
                    MessageBox.Show("Lütfen göndermek istediğiniz müşterileri seçin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Onay mesajı
                var confirmResult = MessageBox.Show(
                    $"{selectedCustomers.Count} adet Müşteri 1C'e gönderilecek. Devam etmek istiyor musunuz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                {
                    return;
                }

                // Buton ve GridView'i devre dışı bırak
                bttnSendCustomer.Enabled = false;
                dataGridDataList.Enabled = false;

                Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri",
                    $"1C'e aktarımı yapılacak Müşteri sayısı: {selectedCustomers.Count}");

                int totalCount = selectedCustomers.Count;
                int processedCount = 0;
                int successCount = 0;
                int errorCount = 0;

                foreach (var customer in selectedCustomers)
                {
                    bttnGetCustomers.Enabled = false;
                    bttnGetProducts.Enabled = false;
                    try
                    {
                        string partnerCode = customer.code;
                        string partnerTIN = customer.vkn;
                        string partnerName = customer.name;
                        string fullName = customer.fullName;

                        // TIN uzunluğuna göre gerçek/tüzel kişi kontrolü (11 haneli TCKN, 10 haneli VKN)
                        bool isJuridicalPerson = partnerTIN?.Length == 10;

                        var resultValue = await _client.InsertNewPartnerAsync(
                            partnerCode,
                            partnerTIN,
                            partnerName,
                            fullName,
                            isJuridicalPerson);

                        if (resultValue.@return.Result)
                        {
                            processedCount++;
                            successCount++;
                            Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri",
                                $"Müşteri '{partnerName}' başarıyla kaydedildi. ({processedCount}/{totalCount})",
                                $"TIN: {partnerTIN}");
                            dataGridDataList.Refresh();
                            chckAll.Checked = false;
                        }
                        else
                        {
                            processedCount++;
                            errorCount++;
                            Helpers.LogFile(Helpers.LogLevel.ERROR, "Müşteri",
                                $"Müşteri '{partnerName}' kayıt edilemedi: {resultValue.@return.Message}",
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

                MessageBox.Show(resultMessage, "Tamamlandı", MessageBoxButtons.OK, errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                dataGridDataList.Refresh();
                chckAll.Checked = false;

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
                    bttnGetCustomers.Enabled = true;
                    bttnGetProducts.Enabled = true;
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
                dataGridDataList.Enabled = true;
                bttnSendProducts.Enabled = false;
            }
        }

        #region helper 
        private class CustomerInfo
        {
            public string vkn { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string fullName { get; set; }
        }
        public class ProductInfo
        {
            public string code { get; set; }
            public string name { get; set; }
            public string unit { get; set; }
        }

        private async void bttnGetProducts_Click(object sender, EventArgs e)
        {
            bttnSendCustomer.Enabled = false;
            try
            {
                Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün", "Ürün listesi çekme işlemi başlatıldı.");

                // Yerel servis için POST request body hazırlama
                var requestBody = new ProductListRequest
                {
                    pageNumber = 0,
                    pageSize = 20000,
                    data = new ProductListRequest.Data
                    {
                        productName = ""
                    }
                };

                // POST isteği ile ürün listesi çekme
                var productDataTask = ApiManager.PostAsync<ProductListRequest, ProductResponseJsonModel>(
                    Configuration.GetUrl() + "management/products?includeActiveUnits=true&lang=tr", requestBody);

                var soapItemsListTask = _client.GetItemListAsync();
                await Task.WhenAll(productDataTask, soapItemsListTask);
                var productData = productDataTask.Result;

                var soapItemsList = soapItemsListTask.Result.@return;

                var existingItemCodes = new HashSet<string>(
                    soapItemsList
                        .Where(item => !string.IsNullOrWhiteSpace(item.ItemName))
                        .Select(item => item.ItemName),
                    StringComparer.OrdinalIgnoreCase
                );

                if (productData?.data?.products != null)
                {
                    var newProductsToDisplay = new List<ProductInfo>();
                    foreach (var product in productData.data.products)
                    {
                        string productCode = product.Name;
                        if (!string.IsNullOrWhiteSpace(productCode) && !existingItemCodes.Contains(productCode))
                        {
                            newProductsToDisplay.Add(new ProductInfo
                            {
                                code = product.Code,
                                name = product.Name,
                                unit = product.UnitName
                            });
                            Helpers.LogFileDataIntegration($"Uzak serviste bulunmayan ürün: " + productCode, product.Code);
                        }
                    }

                    dataGridDataList.DataSource = null;
                    dataGridDataList.Columns.Clear();
                    // Ürün adına göre alfabetik sıralama
                    dataGridDataList.DataSource = newProductsToDisplay.OrderBy(p => p.name).ToList();
                    dataGridDataList.AutoGenerateColumns = true;

                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    chk.HeaderText = "";
                    chk.MinimumWidth = 6;
                    chk.Name = "chk";
                    chk.Width = 80;
                    dataGridDataList.Columns.Insert(0, chk); // İlk sıraya ekle

                    dataGridDataList.Columns["code"].HeaderText = "Ürün Kodu";
                    dataGridDataList.Columns["name"].HeaderText = "Ürün Adı";
                    dataGridDataList.Columns["unit"].HeaderText = "Birim";

                    dataGridDataList.Columns["code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridDataList.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridDataList.Columns["unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    chckAll.BringToFront();
                    bttnSendProducts.Enabled = true;
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

                // Seçili ürünleri al
                var selectedProducts = GetSelectedProducts();

                if (selectedProducts.Count == 0)
                {
                    MessageBox.Show("Lütfen göndermek istediğiniz ürünleri seçin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Onay al
                var confirmResult = MessageBox.Show(
                    $"{selectedProducts.Count} adet ürün uzak servise gönderilecek. Devam etmek istiyor musunuz?",
                    "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                    return;

                // UI'ı devre dışı bırak
                SetUIEnabled(false);

                // Transfer işlemini gerçekleştir
                var transferResult = await ProcessProductTransfer(selectedProducts);

                // Sonuç mesajını göster
                ShowTransferResult(transferResult);

                // Başarılı ürünleri listeden kaldırma seçeneği
                await HandleSuccessfulTransfers(transferResult, sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün gönderiminde bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün", $"Genel gönderim hatası: {ex.Message}");
            }
            finally
            {
                SetUIEnabled(true);
                bttnSendProducts.Enabled = false;
            }
        }

        private List<ProductInfo> GetSelectedProducts()
        {
            var selectedProducts = new List<ProductInfo>();

            foreach (DataGridViewRow row in dataGridDataList.Rows)
            {
                if (row.Cells["chk"].Value != null && Convert.ToBoolean(row.Cells["chk"].Value))
                {
                    var product = new ProductInfo
                    {
                        code = row.Cells["code"].Value?.ToString(),
                        name = row.Cells["name"].Value?.ToString(),
                        unit = row.Cells["unit"].Value?.ToString()
                    };

                    if (!string.IsNullOrWhiteSpace(product.name))
                    {
                        selectedProducts.Add(product);
                    }
                }
            }

            return selectedProducts;
        }

        private async Task<TransferResult> ProcessProductTransfer(List<ProductInfo> selectedProducts)
        {
            var result = new TransferResult();

            foreach (var selectedProduct in selectedProducts)
            {
                try
                {
                    // Ürün detaylarını çek
                    var productDetails = await FetchProductDetails(selectedProduct.name);

                    if (productDetails == null)
                    {
                        result.AddError(selectedProduct.name, "Ürün detayları alınamadı");
                        continue;
                    }

                    // Birim dönüşüm tablosu hazırla
                    var koeficientTableLines = BuildKoeficientTable(productDetails, out string mainUnit);

                    // Ürünü kaydet
                    var saveResult = await SaveProductToRemote(productDetails, mainUnit, koeficientTableLines);

                    if (saveResult.Success)
                    {
                        result.AddSuccess(productDetails.Name, productDetails.Code, koeficientTableLines.Length);
                    }
                    else
                    {
                        result.AddError(productDetails.Name, saveResult.Message);
                    }
                }
                catch (Exception ex)
                {
                    result.AddError(selectedProduct.name, ex.Message);
                }
            }

            return result;
        }

        private async Task<Product> FetchProductDetails(string productName)
        {
            var requestBody = new ProductListRequest
            {
                pageNumber = 0,
                pageSize = 1,
                data = new ProductListRequest.Data
                {
                    productName = productName
                }
            };

            var productData = await ApiManager.PostAsync<ProductListRequest, ProductResponseJsonModel>(
                Configuration.GetUrl() + "management/products?includeActiveUnits=true&lang=tr", requestBody);

            if (productData?.data?.products == null || productData.data.products.Count == 0)
            {
                Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
                    $"Ürün '{productName}' için detay bilgileri alınamadı.");
                return null;
            }

            return productData.data.products.FirstOrDefault();
        }

        private OneCService.KoeficientTableLine[] BuildKoeficientTable(Product product, out string mainUnit)
        {
            mainUnit = "";
            var koeficientLines = new List<OneCService.KoeficientTableLine>();

            if (product.ActiveUnits != null && product.ActiveUnits.Count > 0)
            {
                foreach (var unit in product.ActiveUnits)
                {
                    if (unit.MainUnit)
                    {
                        mainUnit = unit.Code;
                    }

                    koeficientLines.Add(new OneCService.KoeficientTableLine
                    {
                        Name = unit.Code,
                        code = unit.Code,
                        conversionFactor = (decimal)unit.ConversionFactor,
                        area = (decimal)unit.Area,
                        grossVolume = (decimal)unit.GrossVolume,
                        grossWeight = (decimal)unit.GrossWeight,
                        height = (decimal)unit.Height,
                        length = (decimal)unit.Length,
                        volume = (decimal)unit.Volume,
                        weight = (decimal)unit.Weight,
                        width = (decimal)unit.Width,
                        Finance = unit.MainUnit,
                        Quantity = unit.MainUnit,
                        Sale = unit.MainUnit,
                        Report = unit.MainUnit,
                        CONVFACT2 = 1,
                        SHELFLIFE = 0,
                        DISTPOINT = 0,
                        UNITTYPE = 0
                    });
                }
            }
            else
            {
                mainUnit = product.UnitName;
                koeficientLines.Add(new OneCService.KoeficientTableLine
                {
                    Name = product.UnitName,
                    code = product.UnitName,
                    conversionFactor = 1,
                    area = 0,
                    grossVolume = 0,
                    grossWeight = 0,
                    height = 0,
                    length = 0,
                    volume = 0,
                    weight = 0,
                    width = 0,
                    Finance = false,
                    Quantity = false,
                    Sale = true,
                    Report = false,
                    CONVFACT2 = 1,
                    SHELFLIFE = 0,
                    DISTPOINT = 0,
                    UNITTYPE = 0
                });
            }

            return koeficientLines.ToArray();
        }

        private async Task<SaveResult> SaveProductToRemote(Product product, string mainUnit,
            OneCService.KoeficientTableLine[] koeficientTableLines)
        {
            var resultValue = await _client.InsertNewItemAsync(
                product.Code,
                product.Name,
                product.Name,
                false,
                mainUnit,
                koeficientTableLines,
                1
            );

            return new SaveResult
            {
                Success = resultValue.@return.Result,
                Message = resultValue.@return.Message
            };
        }

        private void ShowTransferResult(TransferResult result)
        {
            string resultMessage = $"Transfer işlemi tamamlandı.\n\n" +
                                  $"Toplam İşlenen: {result.TotalProcessed}\n" +
                                  $"Başarılı: {result.SuccessCount}\n" +
                                  $"Hatalı: {result.ErrorCount}";

            MessageBox.Show(resultMessage, "Tamamlandı", MessageBoxButtons.OK,
                result.ErrorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
                $"Transfer tamamlandı. Başarılı: {result.SuccessCount}, Hatalı: {result.ErrorCount}");
        }

        private async Task HandleSuccessfulTransfers(TransferResult result, object sender, EventArgs e)
        {
            if (result.SuccessCount > 0)
            {
                // Başarılı ürünleri _allProducts listesinden kaldır
                var successfulProductNames = result.SuccessfulProducts.Select(p => p.Name).ToHashSet();
                _allProducts.RemoveAll(p => successfulProductNames.Contains(p.name));

                var confirmRemove = MessageBox.Show(
                    "Başarıyla gönderilen ürünler listeden kaldırıldı. Listeyi yenilemek ister misiniz?",
                    "Liste Güncelleme",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                bttnGetCustomers.Enabled = true;
                bttnGetProducts.Enabled = true;
                chckAll.Checked = false;

                if (confirmRemove == DialogResult.Yes)
                {
                    bttnGetProducts_Click(sender, e);
                }
                else
                {
                    // Sadece mevcut görünümü güncelle
                    RefreshProductGrid(_allProducts.OrderBy(p => p.name).ToList());
                }
            }
        }

        private void SetUIEnabled(bool enabled)
        {
            bttnSendCustomer.Enabled = enabled;
            dataGridDataList.Enabled = enabled;

            if (!enabled)
            {
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        // Yardımcı sınıflar
        private class TransferResult
        {
            public int SuccessCount { get; private set; }
            public int ErrorCount { get; private set; }
            public int TotalProcessed => SuccessCount + ErrorCount;
            public List<SuccessfulProduct> SuccessfulProducts { get; } = new List<SuccessfulProduct>();

            public void AddSuccess(string name, string code, int unitCount)
            {
                SuccessCount++;
                SuccessfulProducts.Add(new SuccessfulProduct { Name = name, Code = code });
                Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
                    $"Ürün '{name}' başarıyla kaydedildi. ({TotalProcessed})",
                    $"Kod: {code}, Birim Sayısı: {unitCount}");
            }

            public void AddError(string name, string errorMessage)
            {
                ErrorCount++;
                Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
                    $"Ürün '{name}' işlenirken hata oluştu: {errorMessage}");
            }
        }

        private class SuccessfulProduct
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        private class SaveResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        private void DataIntegrationsForm_Load(object sender, EventArgs e)
        {
            bttnSendCustomer.Enabled = false;
            bttnSendProducts.Enabled = false;
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

        private void bttnLogs_Click(object sender, EventArgs e)
        {
            DataIntegrationLogs dataIntegrationLogs = new DataIntegrationLogs();
            dataIntegrationLogs.Show();
        }
        #endregion

        public class KoeficientTableLine
        {
            public string Name { get; set; }
            public string code { get; set; }
            public decimal conversionFactor { get; set; }
            public decimal area { get; set; }
            public decimal grossVolume { get; set; }
            public decimal grossWeight { get; set; }
            public decimal height { get; set; }
            public decimal length { get; set; }
            public decimal volume { get; set; }
            public decimal weight { get; set; }
            public decimal width { get; set; }
            public bool Finance { get; set; }
            public bool Quantity { get; set; }
            public bool Sale { get; set; }
            public bool Report { get; set; }
            public decimal CONVFACT2 { get; set; }
            public decimal SHELFLIFE { get; set; }
            public decimal DISTPOINT { get; set; }
            public decimal UNITTYPE { get; set; }
        }

        private void invoiceToolTip_Click(object sender, EventArgs e)
        {
            InvoiceForm invoiceForm = new InvoiceForm();
            invoiceForm.Show();
            this.Hide();
        }
    }
}
