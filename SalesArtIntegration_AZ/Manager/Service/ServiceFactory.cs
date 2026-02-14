using OneCService;
using SalesArtIntegration_AZ.Manager.Config;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml.Linq;

namespace SalesArtIntegration_AZ.Manager.Service
{
    public static class ServiceFactory
    {
        private static readonly string ServiceUrl = Configuration.getC1ServiceUrl(); //"http://10.100.0.152/BARDA_TEST/ws/WebService/"; //"https://1cdist.sgofc.com/BARDA_TEST_1/ws/webservice";    //ConfigurationManager.AppSettings["ServiceUrl"] ?? throw new ConfigurationErrorsException("ServiceUrl app.config'de tanımlı değil.");
        private static readonly string Username = Configuration.getC1ServiceUsername();//"web_user"; //ConfigurationManager.AppSettings["ServiceUsername"] ?? throw new ConfigurationErrorsException("ServiceUsername app.config'de tanımlı değil.");
        private static readonly string Password = Configuration.getC1ServiceUserpassword();// "wb_123";//ConfigurationManager.AppSettings["ServicePassword"] ?? throw new ConfigurationErrorsException("ServicePassword app.config'de tanımlı değil.");

        public static WebServicePortTypeClient GetServiceClient(int timeout = 100000) // Varsayılan timeout: 100 saniye
        {
            try
            {
                // BasicHttpBinding yapılandırması
                var binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly)
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
        public static async Task<SoapResult> SendIncomingPaymentRawAsync(
               DateTime date,
               string type,
               string number,
               string customerCode,
               string bankCode,
               string bankName,
               string bankBranchName,
               string taxCode,
               decimal amount,
               string description)
        {
            var url = ServiceUrl; //"http://10.100.0.55/KURDAMIR/ws/WebService/";

            string username = Username; //"WEB_USER";
            string password = Password; //"WB_123";

            string amountString = amount.ToString(System.Globalization.CultureInfo.InvariantCulture);

            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
        <Envelope xmlns=""http://schemas.xmlsoap.org/soap/envelope/"">
          <Body>
            <InsertNewIncomingPayment xmlns=""http://127.0.0.1"">
              <Date>{date:yyyy-MM-dd}</Date>
              <Type>""{type}""</Type>
              <DocNumber>{number}</DocNumber>
              <PartnerCode>{customerCode}</PartnerCode>
              <Bank_Acc_Code>""{bankCode}""</Bank_Acc_Code>
              <Bank_Acc_Name>""{bankName}""</Bank_Acc_Name>
              <Bank_Cash_Name>{bankBranchName}</Bank_Cash_Name>
              <Vat_Acc_Code>{taxCode}</Vat_Acc_Code>
              <Amount>{amountString}</Amount>
              <Description>""{description}""</Description>
            </InsertNewIncomingPayment>
          </Body>
        </Envelope>";

            var handler = new HttpClientHandler
            {
                PreAuthenticate = true,
                Credentials = new System.Net.NetworkCredential(username, password)
            };

            using var httpClient = new HttpClient(handler);

            var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");

            content.Headers.Clear();
            content.Headers.Add("Content-Type", "text/xml; charset=utf-8");

            var response = await httpClient.PostAsync(url, content);
            var responseXml = await response.Content.ReadAsStringAsync();


            return ParseSoapResponse(responseXml);
        }
        private static SoapResult ParseSoapResponse(string responseXml)
        {
            var result = new SoapResult();
            var doc = XDocument.Parse(responseXml);

            var errorNode = doc.Descendants()
                               .FirstOrDefault(x => x.Name.LocalName == "ErrorMessage");

            if (errorNode != null && !string.IsNullOrWhiteSpace(errorNode.Value))
            {
                result.Status = false;
                result.Message = errorNode.Value.Trim();
                return result;
            }

            var statusNode = doc.Descendants()
                                .FirstOrDefault(x => x.Name.LocalName == "Status");

            if (statusNode != null)
            {
                result.Status = true;
                result.Message = statusNode.Value.Trim();
                return result;
            }

            result.Status = false;
            result.Message = "Beklenmeyen servis cevabı.";
            return result;
        }
        public class SoapResult
        {
            public bool Status { get; set; }
            public string Message { get; set; }
        }

    }
}