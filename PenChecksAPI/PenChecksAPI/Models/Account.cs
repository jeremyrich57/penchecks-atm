namespace PenChecksAPI.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public decimal Balance { get; set; }
        public Guid CustomerId { get; set; }
    }
}
