using OneCService;
using SalesArtIntegration_AZ.Helper;
using SalesArtIntegration_AZ.Manager.Api;
using SalesArtIntegration_AZ.Manager.Config;
using SalesArtIntegration_AZ.Manager.Service;
using SalesArtIntegration_AZ.Models.Request;
using SalesArtIntegration_AZ.Models.Response;
using static SalesArtIntegration_AZ.Models.Response.CustomerResponseJsonModel;
using static SalesArtIntegration_AZ.Models.Response.ProductResponseJsonModel;

namespace SalesArtIntegration_AZ
{
    public partial class DataIntegrationForm : Form
    {
        private readonly WebServicePortTypeClient _client;
        public DataIntegrationForm()
        {
            InitializeComponent();
            // Form başlatıldığında bir kez oluşturulur.
            _client = ServiceFactory.GetServiceClient();

        }
        private void DataIntegrationForm_Load(object sender, EventArgs e)
        {

        }
        private void anaMenüyeDönToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();
            this.Hide();
        }
        private void çıkışToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private async void bttnTransferToProducts_Click(object sender, EventArgs e)
        {
            Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün", "Ürün transfer işlemi başlatıldı.");

            try
            {
                var productDataTask = ApiManager.GetAsync<ProductResponseJsonModel>(Configuration.GetUrl() + "management/products?lang=tr");

                var soapItemsListTask = _client.GetItemListAsync();

                await Task.WhenAll(productDataTask, soapItemsListTask);

                var productData = productDataTask.Result;

                var soapItemsList = soapItemsListTask.Result; // SOAP'tan gelen ürün listesi

                var existingItemCodes = new HashSet<string>(
                    soapItemsList.@return
                        .Where(item => !string.IsNullOrWhiteSpace(item.ItemCodeCode))
                        .Select(item => item.ItemCodeCode),
                    StringComparer.OrdinalIgnoreCase
                );


                if (productData?.data?.products != null)
                {
                    var newItemsToCreate = new List<Products>();

                    foreach (var localProduct in productData.data.products)
                    {

                        string localProductCode = localProduct.Code;
                        Helpers.LogFileDataIntegration("Ürün Kod: ", localProductCode);

                        if (!string.IsNullOrWhiteSpace(localProductCode) && !existingItemCodes.Contains(localProductCode))
                        {

                            newItemsToCreate.Add(localProduct);
                            Helpers.LogFileDataIntegration("Yeni Ürün Kod: ", localProductCode);
                        }
                    }

                    if (newItemsToCreate.Count > 0)
                    {
                        //Console.WriteLine($"Uzaktaki servise aktarılacak yeni ürün sayısı: {newItemsToCreate.Count}");
                        Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün", $"Uzaktaki servise aktarılacak yeni ürün sayısı: {newItemsToCreate.Count}");

                        foreach (var newItem in newItemsToCreate)
                        {
                            try
                            {
                                string itemCode = newItem.Code.ToString();
                                string itemName = newItem.Name;
                                bool isService = false;
                                string unit = newItem.UnitName;

                                var resultValue = await _client.InsertNewItemAsync(itemCode, itemName, isService, unit);

                                if (resultValue.Result)
                                {
                                    Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün", $"Ürün '{itemName}' başarıyla kaydedildi.", $"Kod: {itemCode}");
                                    //Console.WriteLine($"Ürün '{itemName}' başarıyla kaydedildi.");
                                }
                                else
                                {
                                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün", $"Ürün '{itemName}' kayıt edilemedi: {resultValue.Message}", $"Kod: {itemCode}");
                                    //Console.WriteLine($"Ürün '{itemName}' kayıt edilemedi!! . " + resultValue.Message.ToString());
                                }

                            }
                            catch (Exception ex)
                            {
                                // Hata yönetimi
                                //Console.WriteLine($"Ürün '{newItem.Name}' kaydedilirken bir hata oluştu: {ex.Message}");
                                Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün", $"Ürün '{newItem.Name}' kaydedilirken bir hata oluştu: {ex.Message}", $"Kod: {newItem.Code}");
                            }
                        }

                        //Console.WriteLine("Tüm yeni ürünlerin transfer işlemi tamamlandı.");
                        Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün", "Tüm yeni ürünlerin transfer işlemi tamamlandı.");

                    }
                    else
                    {
                        Helpers.LogFile(Helpers.LogLevel.INFO, "Ürün", "Uzaktaki servise aktarılacak yeni ürün bulunamadı.");
                        //Console.WriteLine("Uzaktaki servise aktarılacak yeni ürün bulunamadı.");
                    }
                }
            }
            catch (Exception ex)
            {
                Helpers.LogFile(Helpers.LogLevel.ERROR, "Ürün", $"Genel transfer hatası: {ex.Message}", "Detay: Ana Catch Bloğu");
                MessageBox.Show($"Ürün transferinde bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helpers.LogFileDataIntegration($"Ürün Aktarılamadı. Bilgi: ", "Tüm Kayıtlar Yenilenemedi");
            }
        }
        private async void bttnTransferToCustomer_Click_1(object sender, EventArgs e)
        {
            Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri", "Müşteri transfer işlemi başlatıldı.");
            try
            {
                var distributorsTask = ApiManager.GetAsync<DistributorsResponseModel>(Configuration.GetUrl() + "management/distributors");


                var partnersListTask = _client.GetPartnersListAsync("", ""); // Uzak SOAP Servisindeki Müşteri Listesi

                // Diğer verilerin çekimi (bu kısım zaten vardı):
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

                await Task.WhenAll(distributorsTask, partnersListTask);

                var distributors = distributorsTask.Result;

                var partnersList = partnersListTask.Result.@return;

                requestBody.distributorIds = new List<int> { distributors.data.Id };

                var customerResponseTask = ApiManager.PostAsync<CustomerListRequest, CustomerResponseJsonModel>(Configuration.GetUrl() + "management/customers-list?lang=tr", requestBody);

                var customerResponse = await customerResponseTask;

                var existingPartnersTINs = new HashSet<string>(
                    partnersList
                        .Where(p => !string.IsNullOrWhiteSpace(p.TIN))
                        .Select(p => p.TIN),
                    StringComparer.OrdinalIgnoreCase
                );

                if (customerResponse?.data?.customers != null)
                {
                    var newCustomersToCreate = new List<Customer>();

                    foreach (var customerInfo in customerResponse.data.customers)
                    {
                        string customerTIN = customerInfo.taxNumber == null ? customerInfo.identificationNumber : customerInfo.taxNumber;

                        if (!string.IsNullOrWhiteSpace(customerTIN) && !existingPartnersTINs.Contains(customerTIN))
                        {

                            newCustomersToCreate.Add(customerInfo);
                            Helpers.LogFileDataIntegration($"Müşteri Bilgi: ", customerInfo.code);
                        }
                    }

                    if (newCustomersToCreate.Count > 0)
                    {
                        //Console.WriteLine($"Uzaktaki servise aktarılacak yeni müşteri sayısı: {newCustomersToCreate.Count}");
                        Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri", $"Uzaktaki servise aktarılacak yeni müşteri sayısı: {newCustomersToCreate.Count}");
                        int totalCount = newCustomersToCreate.Count;
                        int processedCount = 0;

                        foreach (var newCustomer in newCustomersToCreate)
                        {
                            try
                            {
                                string partnerCode = newCustomer.code.ToString();
                                string partnerTIN = newCustomer.taxNumber == "" ? newCustomer.identificationNumber : newCustomer.taxNumber;
                                string partnerName = newCustomer.name;
                                bool isJuridicalPerson = newCustomer.taxNumber == "" ? true : false;

                                var resultValue = await _client.InsertNewPartnerAsync(partnerCode, partnerTIN, partnerName, isJuridicalPerson);

                                if (resultValue.Result)
                                {
                                    processedCount++;
                                    Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri", $"Müşteri '{partnerName}' başarıyla kaydedildi. ({processedCount}/{totalCount})", $"TIN: {partnerTIN}");
                                    //Console.WriteLine($"Müşteri '{partnerName}' ({partnerTIN}) başarıyla kaydedildi. ({processedCount}/{totalCount})");
                                }
                                else
                                {
                                    Helpers.LogFile(Helpers.LogLevel.ERROR, "Müşteri", $"Müşteri '{partnerName}' kayıt edilemedi: {resultValue.Message}", $"TIN: {partnerTIN}");
                                    //Console.WriteLine($"Müşteri '{partnerName}' ({partnerTIN}) kayıt edilemedi!!.  " + resultValue.Message.ToString());
                                }

                            }
                            catch (Exception ex)
                            {
                                Helpers.LogFile(Helpers.LogLevel.ERROR, "Müşteri", $"Kayıt sırasında hata: {ex.Message}", $"Adı: {newCustomer.name}, TIN: {newCustomer.taxNumber}");

                                //Console.WriteLine($"Müşteri '{newCustomer.name}' kaydedilirken bir hata oluştu: {ex.Message}");

                            }
                        }
                        Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri", "Tüm yeni müşterilerin transfer işlemi tamamlandı.");
                        //Console.WriteLine("Tüm yeni müşterilerin transfer işlemi tamamlandı.");
                    }
                    else
                    {
                        //Console.WriteLine("Uzaktaki servise aktarılacak yeni müşteri bulunamadı.");
                        Helpers.LogFile(Helpers.LogLevel.INFO, "Müşteri", "Uzaktaki servise aktarılacak yeni müşteri bulunamadı.");
                    }

                    //Console.WriteLine($"Toplam Yerel Müşteri Sayısı: {customerResponse.data.customers.Count}");
                    Helpers.LogFile(Helpers.LogLevel.DEBUG, "Müşteri", $"Toplam yerel müşteri sayısı: {customerResponse.data.customers.Count}");
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi: Kullanıcıya uygun bir mesaj göster
                MessageBox.Show($"Müşteri transferinde bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helpers.LogFile(Helpers.LogLevel.ERROR, "Müşteri", $"Genel transfer hatası: {ex.Message}", "Detay: Ana Catch Bloğu");
            }
        }

        private void bttnLogs_Click(object sender, EventArgs e)
        {
            invoiceListLogs invoiceListLogs = new invoiceListLogs();
            invoiceListLogs.Show();
        }

        private void DataIntegrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
    public class PartnerInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string TIN { get; set; } // Anahtar alanımız
                                        // ... diğer alanlar
    }
}
