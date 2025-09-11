using System.ComponentModel.DataAnnotations;

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
        public void LogFile(string logCaption, string invoiceNumber, string remoteInvoiceNumber, string isSuccess, string message)
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
    }
}
