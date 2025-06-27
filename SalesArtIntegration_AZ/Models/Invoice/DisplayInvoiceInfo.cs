namespace SalesArtIntegration_AZ.Models.Invoice
{
    public class DisplayInvoiceInfo
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string DocumentNumber { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DiscountTotal { get; set; }
        public string VatTotal { get; set; }
        public string GrossTotal { get; set; }
    }
}
