using SalesArtIntegration_AZ.Manager.Api;
using SalesArtIntegration_AZ.Models.Response;
using SalesArtIntegration_AZ.Manager.Config;
using System.ComponentModel.DataAnnotations;
using static SalesArtIntegration_AZ.Models.Response.DistributorsResponseModel;

namespace SalesArtIntegration_AZ.Helper
{
    public class Helpers
    {
        // Enum'un Display Name'ini almak için yardımcı metot
        public static string GetDisplayName<T>(T value) where T : Enum
        {
            var fieldInfo = typeof(T).GetField(value.ToString());
            if (fieldInfo == null) return value.ToString();

            var attributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
            return attributes?.Length > 0 ? attributes[0].Name : value.ToString();
        }
        public static void LogFile(string logCaption, string invoiceNumber, string remoteInvoiceNumber, string isSuccess, string message)
        {
            StreamWriter log;
            if (!File.Exists("logfile.txt"))
            {
                log = new StreamWriter("logfile.txt");
            }
            else
            {
                log = File.AppendText("logfile.txt");
            }
            log.WriteLine("------------------------");
            log.WriteLine("Hata Mesajı: " + message);
            log.WriteLine("Faturalar listeleniyor: " + invoiceNumber );
            log.WriteLine("Log Adı: " + logCaption);
            log.WriteLine("Log Zamanı: " + DateTime.Now);

            log.Close();
        }
        public static async Task<string> GetDistributorCodeAsync()
        {
            var distributors = await ApiManager.GetAsync<DistributorsResponseModel>(Configuration.GetUrl() + "management/distributors");

            if (distributors != null && distributors.data != null)
            {
                return distributors.data.Code;
            }

            return ""; // Veya hata durumunu belirten bir değer/istisna döndürebilirsiniz.
        }


        
    }
}
