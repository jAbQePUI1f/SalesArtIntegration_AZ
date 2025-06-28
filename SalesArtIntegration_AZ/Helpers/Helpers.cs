using SalesArtIntegration_AZ.Models.Enums;
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
    }
}
