using System.ComponentModel.DataAnnotations;

namespace SalesArtIntegration_AZ.Models.Enums
{
    public  class Enums
    {
        public enum InvoiceType
        {
            [Display(Name = "Satış Faturası")]
            SELLING,
            [Display(Name = "Satın Alma Faturası")]
            BUYING,
            [Display(Name = "Satış Iade Faturası")]
            SELLING_RETURN,
            [Display(Name = "Satın Alma Iade Faturası")]
            BUYING_RETURN,
            [Display(Name = "Hasarlı Satış Iade")]
            DAMAGED_SELLING_RETURN,
            [Display(Name = "Hasarlı Satın Alma Iade")]
            DAMAGED_BUYING_RETURN,
            [Display(Name = "Satılan Hizmet Faturası")]
            SELLING_SERVICE,
            [Display(Name = "Alınan Hizmet Faturası")]
            BUYING_SERVICE,

        }
        public enum TransactionType
        {
            [Display(Name = "Çek Ödeme")]
            CHECK_PAYMENT,
            [Display(Name = "Senet Ödeme")] 
            BOND_PAYMENT,
            [Display(Name = "Kredi Kartı Ödeme")]
            CREDIT_CARD_PAYMENT,
            [Display(Name = "Havale Ödeme")]
            BANK_TRANSFER_PAYMENT,
            [Display(Name = "Nakit Ödeme")]
            CASH_PAYMENT,
            [Display(Name = "Nakit Tahsilat")]
            CASH_COLLECTION,
            [Display(Name = "Senet Tahsilat")]
            BOND_COLLECTION,
            [Display(Name = "Havale Tahsilat")]
            BANK_TRANSFER_COLLECTION,
            [Display(Name = "Kredi Kartı Tahsilat")]
            CREDIT_CARD_COLLECTION,
            [Display(Name = "Çek Tahsilat")]
            CHECK_COLLECTION
        }
    }
}
