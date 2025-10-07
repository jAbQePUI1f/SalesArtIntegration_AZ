namespace SalesArtIntegration_AZ.Models.Request
{
    public class InvoiceSyncRequest
    {
        public IntegratedInvoice[] integratedInvoices { get; set; }
        public class IntegratedInvoice
        {
            public bool successfullyIntegrated { get; set; }
            public string invoiceNumber { get; set; }
            public string remoteInvoiceNumber { get; set; }
            public string errorMessage { get; set; }
        }
    }
}
