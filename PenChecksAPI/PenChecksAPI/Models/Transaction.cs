namespace PenChecksAPI.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public required TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public Guid AccountId { get; set; }
        public Guid? ToAccountId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
