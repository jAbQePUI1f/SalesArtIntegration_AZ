using OneCService;
using System.ServiceModel;

namespace SalesArtIntegration_AZ.Manager.Service
{
    public static class ServiceFactory
    {
        private static readonly string ServiceUrl = "https://1cdist.sgofc.com/BARDA_TEST_1/ws/webservice";    //ConfigurationManager.AppSettings["ServiceUrl"] ?? throw new ConfigurationErrorsException("ServiceUrl app.config'de tanımlı değil.");
        private static readonly string Username = "web_user"; //ConfigurationManager.AppSettings["ServiceUsername"] ?? throw new ConfigurationErrorsException("ServiceUsername app.config'de tanımlı değil.");
        private static readonly string Password = "wb_123";//ConfigurationManager.AppSettings["ServicePassword"] ?? throw new ConfigurationErrorsException("ServicePassword app.config'de tanımlı değil.");

        public static WebServicePortTypeClient GetServiceClient(int timeout = 100000) // Varsayılan timeout: 100 saniye
        {
            try
            {
                // BasicHttpBinding yapılandırması
                var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport)
                {
                    MaxReceivedMessageSize = 2147483647,
                    MaxBufferSize = 2147483647
                };

                // Timeout ayarları
                if (timeout > 0)
                {
                    binding.SendTimeout = TimeSpan.FromMilliseconds(timeout);
                    binding.ReceiveTimeout = TimeSpan.FromMilliseconds(timeout);
                    binding.OpenTimeout = TimeSpan.FromMilliseconds(timeout);
                    binding.CloseTimeout = TimeSpan.FromMilliseconds(timeout);
                }

                // Güvenlik ayarları (Basic Authentication)
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

                // Endpoint adresi
                var endpoint = new EndpointAddress(ServiceUrl);

                // WebServicePortTypeClient oluştur
                var client = new WebServicePortTypeClient(binding, endpoint);

                // Kimlik doğrulama bilgilerini ayarla
                client.ClientCredentials.UserName.UserName = Username;
                client.ClientCredentials.UserName.Password = Password;

                // İstemciyi aç (isteğe bağlı, ancak hata önlemek için)
                client.Open();

                return client;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("ServiceFactory istemci oluşturma sırasında hata oluştu.", ex);
            }
        }
    }
}