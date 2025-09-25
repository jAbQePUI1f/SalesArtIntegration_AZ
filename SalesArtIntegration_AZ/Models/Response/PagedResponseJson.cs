using SalesArtIntegration_AZ.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Models.Response
{
    public class PagedResponseJson : BaseResponseJson
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalElements { get; set; }
    }
}
