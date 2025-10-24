using System.Configuration;

namespace SalesArtIntegration_AZ.Manager.Config
{
    public static  class Configuration
    {
        public static string GetUrl()
        {
            return ConfigurationManager.AppSettings["URL"];
        }
        public static string getCollectionIsAutoApply()
        {
            return ConfigurationManager.AppSettings["collectionIsAutoApply"];
        }
        public static string getC1ServiceUrl()
        {
            return ConfigurationManager.AppSettings["C1ServiceUrl"];
        }
        public static string getC1ServiceUsername()
        {
            return ConfigurationManager.AppSettings["C1ServiceUsername"];
        }
        public static string getC1ServiceUserpassword()
        {
            return ConfigurationManager.AppSettings["C1ServicePassword"];
        }
        public static bool getIsInvoice()
        {
            return Convert.ToBoolean(ConfigurationManager.AppSettings["isInvoice"]);
        }
        public static bool getIsWaybill()
        {
            return Convert.ToBoolean(ConfigurationManager.AppSettings["isWaybill"]);
        }
        public static bool getIsCollection()
        {
            return Convert.ToBoolean(ConfigurationManager.AppSettings["isCollection"]);
        }
    }
}
