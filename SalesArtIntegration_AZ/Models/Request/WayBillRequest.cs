using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Models.Request
{
    public class WayBillRequest
    {
        public WayBillRequest() { }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string[] waybillTypes { get; set; }
    }
}
