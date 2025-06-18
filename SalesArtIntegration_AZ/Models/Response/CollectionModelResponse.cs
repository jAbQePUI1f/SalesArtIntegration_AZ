using SalesArtIntegration_AZ.Models.Base;

namespace SalesArtIntegration_AZ.Models.Response
{
    public  class CollectionModelResponse : BaseResponseJson
    {
        public List<CollectionHeader> data { get; set; }
        public class CollectionHeader
        {
            public long customerFinancialTransactionId { get; set; }
            public string transactionType { get; set; }
            public string customerCode { get; set; }
            public string customerName { get; set; }
            public string paymentCode { get; set; }
            public string paymentName { get; set; }
            public string salesmanCode { get; set; }
            public string salesmanFirstName { get; set; }
            public string salesmanLastName { get; set; }
            public string documentNo { get; set; }
            public string ficheNo { get; set; }
            public string invoiceNo { get; set; }
            public decimal amount { get; set; }
            public bool affectRisk { get; set; }
            public string description { get; set; }
            public DateTime date { get; set; }
            public DateTime createDate { get; set; }
            public string status { get; set; }
            public DateTime dueDate { get; set; }
            public string warehouseName { get; set; }
            public CollectionDetail detail { get; set; }
        }
        public class CollectionDetail
        {
            public string checkNo { get; set; }
            public string bankCode { get; set; }
            public string bankName { get; set; }
            public string creditCardNo { get; set; }
            public string bankBranchCode { get; set; }
            public string bankBranchName { get; set; }
            public string iban { get; set; }
            public string bankNo { get; set; }
            public string checkLocation { get; set; }
            public string debitOwner { get; set; }
            public string taxNo { get; set; }
            public string taxOffice { get; set; }
        }
    }
}
