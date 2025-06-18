using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Models.Enums
{
    public  class Enums
    {
        public enum InvoiceType
        {
            [Display(Name = "Satış Faturası")]
            SELLING,
            [Display(Name = "Alış Faturası")]
            BUYING,
            [Display(Name = "Satış Iade Faturası")]
            SELLING_RETURN,
            [Display(Name = "Alış Iade Faturası")]
            BUYING_RETURN,
            [Display(Name = "Hasarlı Satış Iade")]
            DAMAGED_SELLING_RETURN,
            [Display(Name = "Hasarlı Alış Iade")]
            DAMAGED_BUYING_RETURN,
            [Display(Name = "Hizmet Satış")]
            SELLING_SERVICE,
            [Display(Name = "Hizmet Alış")]
            BUYING_SERVICE,

        }
        public enum TransactionType
        {
            [Display(Name = "Çek Ödemesi")]
            CHECK_PAYMENT,
            [Display(Name = "Nakit")]
            CASH_COLLECTION,
            [Display(Name = "Kredi Kartı")]
            CREDIT_CARD_PAYMENT,
            [Display(Name = "BANK_TRANSFER_COLLECTION")]
            BANK_TRANSFER_COLLECTION,
            [Display(Name = "BOND_PAYMENT")]
            BOND_PAYMENT,
            [Display(Name = "BOND_COLLECTION")]
            BOND_COLLECTION,
            [Display(Name = "CREDIT_CARD_COLLECTION")]
            CREDIT_CARD_COLLECTION,
            [Display(Name = "CHECK_COLLECTION")]
            CHECK_COLLECTION,
            [Display(Name = "BANK_TRANSFER_PAYMENT")]
            BANK_TRANSFER_PAYMENT,
            [Display(Name = "CASH_PAYMENT")]
            CASH_PAYMENT
        }
    }
}
