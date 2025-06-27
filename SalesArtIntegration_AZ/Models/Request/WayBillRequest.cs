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
