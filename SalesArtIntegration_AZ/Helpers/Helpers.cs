using SalesArtIntegration_AZ.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Helper
{
    public class Helpers
    {
        // Enum'un Display Name'ini almak için yardımcı metot
        public static string GetDisplayName(Enums.InvoiceType value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
            return attributes?.Length > 0 ? attributes[0].Name : value.ToString();
        }
    }
}
