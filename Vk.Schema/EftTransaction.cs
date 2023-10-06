namespace Vk.Schema;


public class EftTransactionRequest
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
}

public class EftTransactionResponse
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string ReferenceNumber { get; set; }  
    public DateTime TransactionDate { get; set; }
    public string TransactionCode { get; set; }
}