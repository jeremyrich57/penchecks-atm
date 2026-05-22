namespace PenChecksAPI.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public List<Account> Accounts { get; set; } = new();
    }
}
