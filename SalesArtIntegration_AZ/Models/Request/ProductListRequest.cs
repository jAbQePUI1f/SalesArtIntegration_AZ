using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Models.Request
{
    public class ProductListRequest
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public string productName { get; set; }
        }
    }
}
