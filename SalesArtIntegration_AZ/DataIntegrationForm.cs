using SalesArtIntegration_AZ.Helper;
using SalesArtIntegration_AZ.Manager.Api;
using SalesArtIntegration_AZ.Manager.Config;
using SalesArtIntegration_AZ.Manager.Service;
using SalesArtIntegration_AZ.Models.Request;
using SalesArtIntegration_AZ.Models.Response;
using System.ServiceModel;

namespace SalesArtIntegration_AZ
{
    public partial class DataIntegrationForm : Form
    {
        public DataIntegrationForm()
        {
            InitializeComponent();
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
            var productData = await ApiManager.GetAsync<ProductResponseJsonModel>(Configuration.GetUrl() + "management/products?lang=tr");
            if (productData != null)
            {
                if (productData.data?.products != null)
                {

                    foreach (var item in productData.data.products)
                    {

                    }
                }
            }
        }
        private async void bttnTransferToCustomer_Click_1(object sender, EventArgs e)
        {
            var distributors = await ApiManager.GetAsync<DistributorsResponseModel>(Configuration.GetUrl() + "management/distributors");

            var requestBody = new CustomerListRequest
            {
                pageNumber = 0,
                pageSize = 20,
                distributorIds = new List<int> { distributors.data.Id },
                data = new CustomerListRequest.Data
                {
                    searchStr = "",
                    sortBalanceByAsc = true
                }
            };
            var customerResponse = await ApiManager.PostAsync<CustomerListRequest, CustomerResponseJsonModel>(Configuration.GetUrl() + "management/customers-list?lang=tr", requestBody);

            if (customerResponse != null)
            {


                if (customerResponse.data?.customers != null)
                {

                    foreach (var customerInfo in customerResponse.data?.customers)
                    {
                    }

                    Console.WriteLine($"Number of Customers: {customerResponse.data.customers.Count}");
                    // You can now access customer data like this:
                    // Console.WriteLine($"First customer's name: {customerResponse.data.customers[0].name}");
                }
            }
        }
    }
}
