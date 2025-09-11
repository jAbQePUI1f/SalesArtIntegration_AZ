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
