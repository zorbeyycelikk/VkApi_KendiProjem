namespace Vk.Schema;

public class AccountTransactionRequest
{
    public int AccountId { get; set; }
    public string ReferenceNumber { get; set; }   
    public decimal DebitAmount { get; set; }
    public decimal CreditAmount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionCode { get; set; }
}

public class AccountTransactionResponse
{
    public int AccountId { get; set; }
    public string ReferenceNumber { get; set; }   
    public decimal DebitAmount { get; set; }
    public decimal CreditAmount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionCode { get; set; }
    public string AccountName { get; set; } // Account'tan gelecek
    public int AccountNumber { get; set; } // Account'tan gelecek
    public int CustomerNumber { get; set; } // Account.Customer'dan gelecek
    public string CustomerName { get; set; } // Account.Customer'dan gelecek
}