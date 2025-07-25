﻿using SalesArtIntegration_AZ.Models.Base;

namespace SalesArtIntegration_AZ.Models.Response
{
    public class InvoiceModelResponse :BaseResponseJson
    {

        public List<InvoiceHeader> data { get; set; }

        public class InvoiceDetail
        {
            public long invoiceDetailId { get; set; }
            public string code { get; set; }
            public int quantity { get; set; }
            public double price { get; set; }
            public double total { get; set; }
            public double discountTotal { get; set; }
            public string unitCode { get; set; }
            public bool vatIncluded { get; set; }
            public double grossTotal { get; set; }
            public double vatRate { get; set; }
            public double vatAmount { get; set; }
            public double netTotal { get; set; }
            public string barcode { get; set; }
            public int invoiceDetailLineOrder { get; set; }
            public string lineType { get; set; }
            public List<object> campaignRewards { get; set; }
        }
        public class InvoiceHeader
        {
            public long invoiceId { get; set; }
            public string invoiceType { get; set; }
            public string number { get; set; }
            public DateTime date { get; set; }
            public string documentNumber { get; set; }
            public string warehouseCode { get; set; }
            public string customerCode { get; set; }
            public string customerName { get; set; }
            public string customerBranchCode { get; set; }
            public string customerBranchName { get; set; }
            public DateTime documentDate { get; set; }
            public object deliveryDate { get; set; }
            public DateTime updateDate { get; set; }
            public double discountTotal { get; set; }
            public double preVatIncludedTotal { get; set; }
            public double vatTotal { get; set; }
            public double grossTotal { get; set; }
            public double netTotal { get; set; }
            public string paymentCode { get; set; }
            public string note { get; set; }
            public string salesmanCode { get; set; }
            public string distributorBranchCode { get; set; }
            public object vehiclePlate { get; set; }
            public object dispatcherFirstName { get; set; }
            public object dispatcherLastName { get; set; }
            public object dispatcherIdentityNumber { get; set; }
            public int? orderId { get; set; }
            public List<InvoiceDetail> details { get; set; }
            public bool ebillCustomer { get; set; }
            public string distCode { get; set; }
        }
    }
}
