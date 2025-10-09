namespace SalesArtIntegration_AZ.Models.Response
{
    public class CustomerResponseJsonModel : PagedResponseJson
    {
        public DataContainer data { get; set; }

        public class DataContainer
        {
            public List<Customer> customers { get; set; }
        }
        public class Customer
        {
            public int id { get; set; }
            public bool enabled { get; set; }
            public string code { get; set; }
            public object ediCode { get; set; } // null olabilir
            public string email { get; set; }
            public string name { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string distributorName { get; set; }
            public string customerChannelName { get; set; }
            public int customerBranchCount { get; set; }
            public int userCount { get; set; }
            public bool taxFree { get; set; }
            public double balance { get; set; }
            public object syncStatus { get; set; } // null olabilir
            public object syncMessage { get; set; } // null olabilir
            public string taxNumber { get; set; }
            public string identificationNumber { get; set; }
            public bool ebillCustomer { get; set; }
        }
    }
}
