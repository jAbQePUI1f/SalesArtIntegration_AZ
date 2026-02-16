using System.Text;

namespace SalesArtIntegration_AZ.Manager.RawSoap
{
    public class SoapService
    {
        private readonly string _url = "http://10.100.0.55/KURDAMIR/ws/WebService/";
        private readonly string _username = "WEB_USER";
        private readonly string _password = "WB_123";
        public async Task<string> SendIncomingPaymentRawAsync(
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
            var url = "http://10.100.0.55/KURDAMIR/ws/WebService/";
            string username = "WEB_USER";
            string password = "WB_123";
            string amountString = amount.ToString(System.Globalization.CultureInfo.InvariantCulture);

            // XML encoding (önemli!)
            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <Envelope xmlns=""http://schemas.xmlsoap.org/soap/envelope/"">
              <Body>
                <InsertNewIncomingPayment xmlns=""http://127.0.0.1"">
                  <Date>{date:yyyy-MM-dd}</Date>
                  <Type>{System.Security.SecurityElement.Escape(type)}</Type>
                  <DocNumber>{System.Security.SecurityElement.Escape(number)}</DocNumber>
                  <PartnerCode>{System.Security.SecurityElement.Escape(customerCode)}</PartnerCode>
                  <Bank_Acc_Code>{System.Security.SecurityElement.Escape(bankCode)}</Bank_Acc_Code>
                  <Bank_Acc_Name>{System.Security.SecurityElement.Escape(bankName)}</Bank_Acc_Name>
                  <Bank_Cash_Name>{System.Security.SecurityElement.Escape(bankBranchName)}</Bank_Cash_Name>
                  <Vat_Acc_Code>{System.Security.SecurityElement.Escape(taxCode)}</Vat_Acc_Code>
                  <Amount>{amountString}</Amount>
                  <Description>{System.Security.SecurityElement.Escape(description)}</Description>
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

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            File.WriteAllText(Path.Combine(desktop, "lastResponse.xml"), responseXml);

            return responseXml;
        }
    }
}
