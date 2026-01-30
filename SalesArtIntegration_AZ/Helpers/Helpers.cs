using SalesArtIntegration_AZ.Manager.Api;
using SalesArtIntegration_AZ.Manager.Config;
using SalesArtIntegration_AZ.Models.Response;
using System.ComponentModel.DataAnnotations;

namespace SalesArtIntegration_AZ.Helper
{
    public class Helpers
    {
       
        public enum LogLevel
        {
            INFO,    // Bilgilendirme
            WARNING, // Uyarı
            ERROR,   // Hata
            DEBUG    // Geliştirme/Detay
        }
        public static string? GetDisplayName<T>(T value) where T : Enum
        {
            var fieldInfo = typeof(T).GetField(value.ToString());
            if (fieldInfo == null) return value.ToString();

            var attributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
            return attributes?.Length > 0 ? attributes[0].Name : value.ToString();
        }
        /// <summary>
        /// Log mesajını belirtilen seviye ve kaynak ile dosyaya yazar.
        /// </summary>
        /// <param name="level">Log seviyesi (INFO, ERROR, vb.)</param>
        /// <param name="source">Logun kaynağı (Fatura, Müşteri, Ürün, Genel)</param>
        /// <param name="message">Log mesajı</param>
        /// <param name="additionalInfo">Ek bilgi (Örn: Fatura No, Ürün Kodu vb.)</param>
        public static void LogFile(LogLevel level, string source, string message, string additionalInfo = "")
        {
            
            const string logFilePath = "logfile.txt";

            try
            {
                using (StreamWriter log = new StreamWriter(logFilePath, true)) // 'true' ile append modunu açtık.
                {
                    log.WriteLine("------------------------");
                    log.WriteLine($"Zaman: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                    log.WriteLine($"Seviye: {level}");
                    log.WriteLine($"Kaynak: {source}");
                    log.WriteLine($"Mesaj: {message}");
                    if (!string.IsNullOrWhiteSpace(additionalInfo))
                    {
                        log.WriteLine($"Ek Bilgi: {additionalInfo}");
                    }
                    log.WriteLine("------------------------");
                }
            }
            catch (Exception ex)
            {
            
                System.Diagnostics.Debug.WriteLine($"Loglama Hatası: {ex.Message}");
            }
        }
        public static void LogFileDataIntegration(string logCaption, string message)
        {
            StreamWriter log;
            if (!File.Exists("logfileDataIntegration.txt"))
            {
                log = new StreamWriter("logfileDataIntegration.txt");
            }
            else
            {
                log = File.AppendText("logfileDataIntegration.txt");
            }
            log.WriteLine("------------------------");
            log.WriteLine("Hata Mesajı: " + message);
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
