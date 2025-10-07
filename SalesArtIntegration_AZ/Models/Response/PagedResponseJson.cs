using SalesArtIntegration_AZ.Models.Base;

namespace SalesArtIntegration_AZ.Models.Response
{
    public class PagedResponseJson : BaseResponseJson
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalElements { get; set; }
    }
}
