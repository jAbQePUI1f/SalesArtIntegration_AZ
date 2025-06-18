using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Models.Request
{
    public  class CollectionRequest
    {

        public CollectionRequest() { }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string[] transactionTypes { get; set; }
    }
}
