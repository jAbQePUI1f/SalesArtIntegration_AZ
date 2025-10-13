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
            Application.Exit();
        }
        private async void bttnTransferToProducts_Click(object sender, EventArgs e)
        {
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
                        Console.WriteLine($"Uzaktaki servise aktarılacak yeni ürün sayısı: {newItemsToCreate.Count}");
                        Helpers.LogFileDataIntegration($"Aktarım Başlatılamadı. Hata:  { newItemsToCreate.Count}","-");

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
                                    Console.WriteLine($"Ürün '{itemName}' başarıyla kaydedildi.");
                                    Helpers.LogFileDataIntegration($"Ürün Aktarıldı. Bilgi:  {itemName}", "-");
                                }
                                else
                                {
                                    Console.WriteLine($"Ürün '{itemName}' kayıt edilemedi!! . " + resultValue.Message.ToString());
                                    Helpers.LogFileDataIntegration($"Ürünler Aktarılamadı. Bilgi:  {itemName}", "-");
                                }

                            }
                            catch (Exception ex)
                            {
                                // Hata yönetimi
                                Console.WriteLine($"Ürün '{newItem.Name}' kaydedilirken bir hata oluştu: {ex.Message}");
                                Helpers.LogFileDataIntegration($"Ürün Aktarılamadı. Bilgi:  {ex.Message}", "-");
                            }
                        }

                        Console.WriteLine("Tüm yeni ürünlerin transfer işlemi tamamlandı.");
                        Helpers.LogFileDataIntegration($"Ürünler Aktarıldı. Bilgi: ", "Tüm Kayıtlar Yenilendi");
                    }
                    else
                    {
                        Console.WriteLine("Uzaktaki servise aktarılacak yeni ürün bulunamadı.");
                        Helpers.LogFileDataIntegration($"Ürün Aktarılmadı. Bilgi: ", "Uzaktaki Servise aktarılacak Yeni ürün bulunamadı.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün transferinde bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helpers.LogFileDataIntegration($"Ürün Aktarılamadı. Bilgi: ", "Tüm Kayıtlar Yenilenemedi");
            }
        }
        private async void bttnTransferToCustomer_Click_1(object sender, EventArgs e)
        {
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
                        string customerTIN = customerInfo.code;
                        Helpers.LogFileDataIntegration($"Müşteri Bilgi: ", customerTIN);
                        if (!string.IsNullOrWhiteSpace(customerTIN) && !existingPartnersTINs.Contains(customerTIN))
                        {

                            newCustomersToCreate.Add(customerInfo);
                            Helpers.LogFileDataIntegration($"Müşteri Bilgi: ", customerInfo.code);
                        }
                    }

                    if (newCustomersToCreate.Count > 0)
                    {
                        Console.WriteLine($"Uzaktaki servise aktarılacak yeni müşteri sayısı: {newCustomersToCreate.Count}");
                        Helpers.LogFileDataIntegration($"Müşteri Bilgi:  { newCustomersToCreate.Count}","--");

                        int totalCount = newCustomersToCreate.Count;
                        int processedCount = 0;

                        foreach (var newCustomer in newCustomersToCreate)
                        {
                            try
                            {
                                string partnerCode = newCustomer.code.ToString();
                                string partnerTIN = newCustomer.code;
                                string partnerName = newCustomer.name;
                                bool isJuridicalPerson = false;
                                Helpers.LogFileDataIntegration($"Müşteri Bilgi: ", partnerCode + " " + partnerTIN + " " + partnerName);

                                var resultValue = await _client.InsertNewPartnerAsync(partnerCode, partnerTIN, partnerName, isJuridicalPerson);

                                if (resultValue.Result)
                                {
                                    processedCount++;
                                    Console.WriteLine($"Müşteri '{partnerName}' ({partnerTIN}) başarıyla kaydedildi. ({processedCount}/{totalCount})");
                                    Helpers.LogFileDataIntegration($"Müşteri '{partnerName}' ({partnerTIN}) başarıyla kaydedildi. ({processedCount}/{totalCount})","-");
                                }
                                else
                                {
                                    Console.WriteLine($"Müşteri '{partnerName}' ({partnerTIN}) kayıt edilemedi!!.  " + resultValue.Message.ToString());
                                    Helpers.LogFileDataIntegration($"Müşteri '{partnerName}' ({partnerTIN}) kayıt edilemedi!!.  " , resultValue.Message.ToString());
                                }

                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine($"Müşteri '{newCustomer.name}' kaydedilirken bir hata oluştu: {ex.Message}");
                                Helpers.LogFileDataIntegration($"Müşteri '{newCustomer.name}' kaydedilirken bir hata oluştu: {ex.Message}","--");
                            }
                        }

                        Console.WriteLine("Tüm yeni müşterilerin transfer işlemi tamamlandı.");
                    }
                    else
                    {
                        Console.WriteLine("Uzaktaki servise aktarılacak yeni müşteri bulunamadı.");
                        Helpers.LogFileDataIntegration("Uzaktaki Servise aktarılacak yeni müşteri bulunamadı.", "--");
                    }

                    Console.WriteLine($"Toplam Yerel Müşteri Sayısı: {customerResponse.data.customers.Count}");
                    Helpers.LogFileDataIntegration($"Toplam Yerel Müşteri Sayısı: {customerResponse.data.customers.Count}","--");
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi: Kullanıcıya uygun bir mesaj göster
                MessageBox.Show($"Müşteri transferinde bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helpers.LogFileDataIntegration($"Müşteri transferinde bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void bttnLogs_Click(object sender, EventArgs e)
        {
            DataIntegrationLogs dataIntegrationLogs = new DataIntegrationLogs();
            dataIntegrationLogs.Show();
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
