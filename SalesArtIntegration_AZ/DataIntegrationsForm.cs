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

                // Uzak serviste mevcut olan TIN'leri HashSet'e al (performans için)
                var existingPartnersTINs = new HashSet<string>(
                    partnersList
                        .Where(p => !string.IsNullOrWhiteSpace(p.TIN))
                        .Select(p => p.TIN),
                    StringComparer.OrdinalIgnoreCase
                );
                bttnSendCustomer.Enabled = true;
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
                                name = customer.shortName,
                                fullName = customer.name,
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
                    dataGridInvoiceList.Columns["fullName"].HeaderText = "Müşteri Voen Adı";
                    dataGridInvoiceList.Columns["vkn"].HeaderText = "Voen No";

                    dataGridInvoiceList.Columns["code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridInvoiceList.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridInvoiceList.Columns["fullName"].AutoS izeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
                    pageSize = 2000,
                    data = new ProductListRequest.Data
                    {
                        productName = ""
                    }
                };

                // POST isteği ile ürün listesi çekme
                var productDataTask = ApiManager.PostAsync<ProductListRequest, ProductResponseJsonModel>(
                    Configuration.GetUrl() + "management/products?includeActiveUnits=true&lang=tr", requestBody);


                //var productDataTask = ApiManager.GetAsync<ProductResponseJsonModel>(Configuration.GetUrl() + "management/products?lang=tr");

                var soapItemsListTask = _client.GetItemListAsync();

                //soapItemsListTask
                await Task.WhenAll(productDataTask, soapItemsListTask);
                var productData = productDataTask.Result;

                var soapItemsList = soapItemsListTask.Result.@return;

                var existingItemCodes = new HashSet<string>(
                    soapItemsList
                        .Where(item => !string.IsNullOrWhiteSpace(item.ItemCodeCode))
                        .Select(item => item.ItemCodeCode),
                    StringComparer.OrdinalIgnoreCase
                );

                //Helpers.LogFile(Helpers.LogLevel.DEBUG, "Ürün", $"Uzak serviste bulunan ürün sayısı: {existingItemCodes.Count}");

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

                // GridView'den seçili satırların kodlarını al
                var selectedProductCodes = new List<string>();
                foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
                {
                    if (row.Cells["chk"].Value != null && Convert.ToBoolean(row.Cells["chk"].Value))
                    {
                        string productName = row.Cells["name"].Value?.ToString();
                        if (!string.IsNullOrWhiteSpace(productName))
                        {
                            selectedProductCodes.Add(productName);
                        }
                    }
                }

                if (selectedProductCodes.Count == 0)
                {
                    MessageBox.Show("Lütfen göndermek istediğiniz ürünleri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show($"{selectedProductCodes.Count} adet ürün uzak servise gönderilecek. Devam etmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult != DialogResult.Yes)
                {
                    return;
                }

                dataGridInvoiceList.Enabled = false;

                int processedCount = 0;
                int successCount = 0;
                int errorCount = 0;

                // Her seçili ürün için ayrı ayrı detaylarını çek ve işle
                foreach (var productName in selectedProductCodes)
                {
                    try
                    {
                        // Her ürün için sadece o ürüne ait detayları çek
                        var requestBody = new ProductListRequest
                        {
                            pageNumber = 0,
                            pageSize = 2000, // Tek ürün için yeterli
                            data = new ProductListRequest.Data
                            {
                                productName = productName // Sadece seçili ürünün kodunu ara
                            }
                        };

                        var productData = await ApiManager.PostAsync<ProductListRequest, ProductResponseJsonModel>(
                            Configuration.GetUrl() + "management/products?includeActiveUnits=true&lang=tr", requestBody);

                        if (productData?.data?.products == null || productData.data.products.Count == 0)
                        {
                            errorCount++;
                            Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
                                $"Ürün kodu '{productName}' için detay bilgileri alınamadı.");
                            processedCount++;
                            continue;
                        }

                        // Sonuçta sadece bir ürün olmalı, ilk ürünü al
                        var product = productData.data.products.FirstOrDefault();
                        if (product == null)
                        {
                            errorCount++;
                            Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
                                $"Ürün kodu '{productName}' için geçerli ürün bilgisi bulunamadı.");
                            processedCount++;
                            continue;
                        }

                        string itemCode = product.Code;
                        string itemName = product.Name;
                        string fullDescription = product.Name;
                        bool isService = false;

                        OneCService.KoeficientTableLine[] koeficientTableLines;
                        string mainUnit = "";
                        // ActiveUnits varsa tüm birimleri KoeficientTableLine olarak ekle
                        if (product.ActiveUnits != null && product.ActiveUnits.Count > 0)
                        {
                            var koeficientLines = new List<OneCService.KoeficientTableLine>();

                            foreach (var unit in product.ActiveUnits)
                            {
                                if (unit.MainUnit) { mainUnit = BirimYoneticisi.BirimGetir(unit.Code); }

                                var koeficientLine = new OneCService.KoeficientTableLine
                                {
                                    Name = unit.Name,
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
                                    Finance = true,
                                    Quantity = true,
                                    Sale = true,
                                    Report = true,
                                    CONVFACT2 = 1,
                                    SHELFLIFE = 0,
                                    DISTPOINT = 0,
                                    UNITTYPE = 0
                                };

                                koeficientLines.Add(koeficientLine);
                            }

                            koeficientTableLines = koeficientLines.ToArray();
                        }
                        else
                        {
                            // ActiveUnits yoksa tek bir birim oluştur
                            string mainUnit1 = BirimYoneticisi.BirimGetir(product.UnitName);
                            koeficientTableLines = new OneCService.KoeficientTableLine[]
                            {
                        new OneCService.KoeficientTableLine
                        {
                            Name = mainUnit1,
                            code = product.UnitName ?? "",
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
                        }
                            };
                        }

                        // Ürünü kaydet
                        var resultValue = await _client.InsertNewItemAsync(
                            itemCode,
                            itemName,
                            fullDescription,
                            isService,
                            mainUnit,
                            koeficientTableLines,
                            18
                        );

                        processedCount++;
                        if (resultValue.@return.Result)
                        {
                            successCount++;
                            Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
                                $"Ürün '{itemName}' başarıyla kaydedildi. ({processedCount}/{selectedProductCodes.Count})",
                                $"Kod: {itemCode}, Birim Sayısı: {koeficientTableLines.Length}");
                        }
                        else
                        {
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
                            $"Ürün kodu '{productName}' işlenirken hata oluştu: {ex.Message}");
                    }
                }

                // Sonuç mesajı
                string resultMessage = $"Transfer işlemi tamamlandı.\n\n" +
                                      $"Toplam İşlenen Ürün: {processedCount}\n" +
                                      $"Başarılı: {successCount}\n" +
                                      $"Hatalı: {errorCount}";

                MessageBox.Show(resultMessage, "Tamamlandı", MessageBoxButtons.OK,
                    errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün gönderiminde bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün", $"Genel gönderim hatası: {ex.Message}");
            }
            finally
            {
                dataGridInvoiceList.Enabled = true;
            }
        }
        //private async void bttnSendProducts_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün", "Seçili ürünlerin transfer işlemi başlatıldı.");

        //        // GridView'den seçili satırları al
        //        var selectedProductCodes = new List<string>();

        //        foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
        //        {
        //            if (row.Cells["chk"].Value != null && Convert.ToBoolean(row.Cells["chk"].Value))
        //            {
        //                string productCode = row.Cells["code"].Value?.ToString();
        //                if (!string.IsNullOrWhiteSpace(productCode))
        //                {
        //                    selectedProductCodes.Add(productCode);
        //                }
        //            }
        //        }

        //        bttnSendProducts.Enabled = true;

        //        if (selectedProductCodes.Count == 0)
        //        {
        //            MessageBox.Show("Lütfen göndermek istediğiniz ürünleri seçin.", "Uyarı",
        //                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        // Onay mesajı
        //        var confirmResult = MessageBox.Show(
        //            $"{selectedProductCodes.Count} adet ürün uzak servise gönderilecek. Devam etmek istiyor musunuz?",
        //            "Onay",
        //            MessageBoxButtons.YesNo,
        //            MessageBoxIcon.Question);

        //        if (confirmResult != DialogResult.Yes)
        //        {
        //            return;
        //        }

        //        dataGridInvoiceList.Enabled = false;

        //        Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
        //            $"Uzak servise aktarılacak ürün sayısı: {selectedProductCodes.Count}");

        //        // Seçili ürünlerin detaylarını API'den çek (ActiveUnits dahil)
        //        var requestBody = new ProductListRequest
        //        {
        //            pageNumber = 0,
        //            pageSize = 2000,
        //            data = new ProductListRequest.Data
        //            {
        //                productName = ""
        //            }
        //        };

        //        var productData = await ApiManager.PostAsync<ProductListRequest, ProductResponseJsonModel>(
        //            Configuration.GetUrl() + "management/products?includeActiveUnits=true&lang=tr", requestBody);

        //        if (productData?.data?.products == null)
        //        {
        //            MessageBox.Show("Ürün detayları alınamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        // Seçili ürünleri filtrele
        //        var selectedProductsWithDetails = productData.data.products
        //            .Where(p => selectedProductCodes.Contains(p.Code, StringComparer.OrdinalIgnoreCase))
        //            .ToList();

        //        int totalCount = selectedProductsWithDetails.Count;
        //        int processedCount = 0;
        //        int successCount = 0;
        //        int errorCount = 0;

        //        Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
        //            $"Toplam işlenecek ürün sayısı: {totalCount}");

        //        foreach (var product in selectedProductsWithDetails)
        //        {
        //            try
        //            {
        //                string itemCode = product.Code;
        //                string itemName = product.Name;
        //                string fullDescription = product.Name;
        //                bool isService = false;

        //                // Ana birim (Unit) - İlk ActiveUnit veya UnitName
        //                string mainUnit = string.Empty;
        //                OneCService.KoeficientTable koeficientTable = new OneCService.KoeficientTable();

        //                // ActiveUnits kontrolü - sadece ilk birimi al
        //                if (product.ActiveUnits != null && product.ActiveUnits.Count > 0)
        //                {
        //                    // İlk aktif birimi ana birim olarak al
        //                    var firstUnit = product.ActiveUnits.First();
        //                    mainUnit = BirimYoneticisi.BirimGetir(firstUnit.Code);

        //                    // Sadece ilk birimi KoeficientTableLine olarak oluştur (tek satır)
        //                    koeficientTable.KoeficientTableLine = new OneCService.KoeficientTableLine
        //                    {
        //                        Name = firstUnit.Name,
        //                        code = firstUnit.Code,
        //                        conversionFactor = (decimal)firstUnit.ConversionFactor,
        //                        area = (decimal)firstUnit.Area,
        //                        grossVolume = (decimal)firstUnit.GrossVolume,
        //                        grossWeight = (decimal)firstUnit.GrossWeight,
        //                        height = (decimal)firstUnit.Height,
        //                        length = (decimal)firstUnit.Length,
        //                        volume = (decimal)firstUnit.Volume,
        //                        weight = (decimal)firstUnit.Weight,
        //                        width = (decimal)firstUnit.Width,
        //                        Finance = true,
        //                        Quantity = true,
        //                        Sale = true,
        //                        Report = true,
        //                        CONVFACT2 = 1,
        //                        SHELFLIFE = 0,
        //                        DISTPOINT = 0,
        //                        UNITTYPE = 0
        //                    };

        //                    Helpers.LogFile(Helpers.LogLevel.DEBUG, "Ürün",
        //                        $"Ürün '{itemName}' için ilk birim hazırlandı.",
        //                        $"Ana birim: {mainUnit}, Birim kodu: {firstUnit.Code}");
        //                }
        //                else
        //                {
        //                    // ActiveUnits yoksa fallback
        //                    mainUnit = BirimYoneticisi.BirimGetir(product.UnitName);
        //                    koeficientTable.KoeficientTableLine = null;

        //                    Helpers.LogFile(Helpers.LogLevel.WARNING, "Ürün",
        //                        $"Ürün '{itemName}' için ActiveUnits bulunamadı, sadece ana birim gönderiliyor.",
        //                        $"Ana birim: {mainUnit}");
        //                }

        //                // Tek bir insert ile tüm bilgileri gönder
        //                var resultValue = await _client.InsertNewItemAsync(
        //                    itemCode,
        //                    itemName,
        //                    fullDescription,
        //                    isService,
        //                    mainUnit,
        //                    koeficientTable,
        //                    18
        //                );

        //                processedCount++;

        //                if (resultValue.@return.Result)
        //                {
        //                    successCount++;
        //                    Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
        //                        $"Ürün '{itemName}' başarıyla kaydedildi. ({processedCount}/{totalCount})",
        //                        $"Kod: {itemCode}, Ana Birim: {mainUnit}");
        //                }
        //                else
        //                {
        //                    errorCount++;
        //                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
        //                        $"Ürün '{itemName}' kayıt edilemedi: {resultValue.@return.Message}",
        //                        $"Kod: {itemCode}");
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                processedCount++;
        //                errorCount++;
        //                Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
        //                    $"Kayıt sırasında hata: {ex.Message}",
        //                    $"Adı: {product.Name}, Kod: {product.Code}");
        //            }
        //        }

        //        // Sonuç mesajı
        //        string resultMessage = $"Transfer işlemi tamamlandı.\n\n" +
        //                               $"Toplam Ürün: {totalCount}\n" +
        //                               $"Başarılı: {successCount}\n" +
        //                               $"Hatalı: {errorCount}";

        //        MessageBox.Show(resultMessage, "Tamamlandı",
        //            MessageBoxButtons.OK,
        //            errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

        //        Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
        //            $"Seçili ürünlerin transfer işlemi tamamlandı. Başarılı: {successCount}, Hatalı: {errorCount}");

        //        // Başarılı transferleri GridView'den kaldır (opsiyonel)
        //        if (successCount > 0)
        //        {
        //            var confirmRemove = MessageBox.Show(
        //                "Başarıyla gönderilen ürünler listeden kaldırılsın mı?",
        //                "Liste Güncelleme",
        //                MessageBoxButtons.YesNo,
        //                MessageBoxIcon.Question);

        //            if (confirmRemove == DialogResult.Yes)
        //            {
        //                // Listeyi yeniden yükle
        //                bttnGetProducts_Click(sender, e);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ürün gönderiminde bir hata oluştu: {ex.Message}",
        //            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
        //            $"Genel gönderim hatası: {ex.Message}", "Detay: bttnSendProducts_Click");
        //    }
        //    finally
        //    {
        //        // Buton ve GridView'i tekrar aktif et
        //        dataGridInvoiceList.Enabled = true;
        //    }
        //}


        //private async void bttnSendProducts_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün", "Seçili ürünlerin transfer işlemi başlatıldı.");

        //        // GridView'den seçili satırları al
        //        var selectedProducts = new List<ProductInfo>();

        //        foreach (DataGridViewRow row in dataGridInvoiceList.Rows)
        //        {
        //            if (row.Cells["chk"].Value != null && Convert.ToBoolean(row.Cells["chk"].Value))
        //            {
        //                selectedProducts.Add(new ProductInfo
        //                {
        //                    code = row.Cells["code"].Value?.ToString(),
        //                    name = row.Cells["name"].Value?.ToString(),
        //                    unit = row.Cells["unit"].Value?.ToString()
        //                });
        //            }
        //        }
        //        bttnSendProducts.Enabled = true;
        //        if (selectedProducts.Count == 0)
        //        {
        //            MessageBox.Show("Lütfen göndermek istediğiniz ürünleri seçin.", "Uyarı",
        //                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        // Onay mesajı
        //        var confirmResult = MessageBox.Show(
        //            $"{selectedProducts.Count} adet ürün uzak servise gönderilecek. Devam etmek istiyor musunuz?",
        //            "Onay",
        //            MessageBoxButtons.YesNo,
        //            MessageBoxIcon.Question);

        //        if (confirmResult != DialogResult.Yes)
        //        {
        //            return;
        //        }

        //        dataGridInvoiceList.Enabled = false;

        //        Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
        //            $"Uzak servise aktarılacak ürün sayısı: {selectedProducts.Count}");

        //        int totalCount = selectedProducts.Count;
        //        int processedCount = 0;
        //        int successCount = 0;
        //        int errorCount = 0;

        //        foreach (var product in selectedProducts)
        //        {
        //            try
        //            {

        //                string itemCode = product.code;
        //                string itemName = product.name;
        //                bool isService = false;
        //                string unit = BirimYoneticisi.BirimGetir(product.unit);


        //                var resultValue = await _client.InsertNewItemAsync(itemCode, itemName, isService, unit, 18);

        //                if (resultValue.@return.Result)
        //                {
        //                    processedCount++;
        //                    successCount++;
        //                    Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
        //                        $"Ürün '{itemName}' başarıyla kaydedildi. ({processedCount}/{totalCount})",
        //                        $"Kod: {itemCode}");
        //                }
        //                else
        //                {
        //                    processedCount++;
        //                    errorCount++;
        //                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
        //                        $"Ürün '{itemName}' kayıt edilemedi: {resultValue.@return.Message}",
        //                        $"Kod: {itemCode}");
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                processedCount++;
        //                errorCount++;
        //                Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
        //                    $"Kayıt sırasında hata: {ex.Message}",
        //                    $"Adı: {product.name}, Kod: {product.code}");
        //            }
        //        }

        //        // Sonuç mesajı
        //        string resultMessage = $"Transfer işlemi tamamlandı.\n\n" +
        //                               $"Toplam: {totalCount}\n" +
        //                               $"Başarılı: {successCount}\n" +
        //                               $"Hatalı: {errorCount}";

        //        MessageBox.Show(resultMessage, "Tamamlandı",
        //            MessageBoxButtons.OK,
        //            errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

        //        Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün",
        //            $"Seçili ürünlerin transfer işlemi tamamlandı. Başarılı: {successCount}, Hatalı: {errorCount}");

        //        // Başarılı transferleri GridView'den kaldır (opsiyonel)
        //        if (successCount > 0)
        //        {
        //            var confirmRemove = MessageBox.Show(
        //                "Başarıyla gönderilen ürünler listeden kaldırılsın mı?",
        //                "Liste Güncelleme",
        //                MessageBoxButtons.YesNo,
        //                MessageBoxIcon.Question);

        //            if (confirmRemove == DialogResult.Yes)
        //            {
        //                // Listeyi yeniden yükle
        //                bttnGetProducts_Click(sender, e);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ürün gönderiminde bir hata oluştu: {ex.Message}",
        //            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün",
        //            $"Genel gönderim hatası: {ex.Message}", "Detay: bttnSendProducts_Click");
        //    }
        //    finally
        //    {
        //        // Buton ve GridView'i tekrar aktif et
        //        dataGridInvoiceList.Enabled = true;

        //    }
        //}

        private void DataIntegrationsForm_Load(object sender, EventArgs e)
        {
            bttnSendCustomer.Enabled = false;
            bttnSendProducts.Enabled = false;
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

        private void bttnLogs_Click(object sender, EventArgs e)
        {
            DataIntegrationLogs dataIntegrationLogs = new DataIntegrationLogs();
            dataIntegrationLogs.Show();
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
    }
}
