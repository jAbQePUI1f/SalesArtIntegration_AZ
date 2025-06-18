using OneCService;
using System.ServiceModel;

namespace SalesArtIntegration_AZ.Manager.Service
{
    public class ServiceFactory
    {
        private static readonly string ServiceUrl = "https://1cdist.sgofc.com/BARDA_TEST_1/ws/webservice?wsdl";
        private static readonly string Username = "web_user";
        private static readonly string Password = "wb_123";

        public static WebServicePortTypeClient GetServiceClient(int timeout = 100000) // Varsayılan timeout: 100 saniye
        {
            // BasicHttpBinding yapılandırması
            var binding = new BasicHttpBinding
            {
                Security = new BasicHttpSecurity
                {
                    Mode = BasicHttpSecurityMode.TransportWithMessageCredential, // HTTPS ve kullanıcı adı/şifre
                    Message = new BasicHttpMessageSecurity
                    {
                        ClientCredentialType = BasicHttpMessageCredentialType.UserName
                    }
                },
                MaxReceivedMessageSize = 2147483647, // Büyük mesajlar için
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

            // Endpoint adresi
            var endpoint = new EndpointAddress(ServiceUrl);

            // WebserviceClient oluştur
            var client = new WebServicePortTypeClient(binding, endpoint);

            // Kimlik doğrulama
            client.ClientCredentials.UserName.UserName = Username;
            client.ClientCredentials.UserName.Password = Password;

            return client;
        }
    }
}
