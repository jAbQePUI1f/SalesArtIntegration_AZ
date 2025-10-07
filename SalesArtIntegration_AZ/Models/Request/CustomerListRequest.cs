namespace SalesArtIntegration_AZ.Models.Request
{
    public class CustomerListRequest
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public List<int> distributorIds { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public string searchStr { get; set; }
            public bool sortBalanceByAsc { get; set; }
        }
    }
}
