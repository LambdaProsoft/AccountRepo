namespace Application.Request
{
    public class AccountRequest
    {
        public int User { get; set; }
        public string? Alias { get; set; }
        public int Currency { get; set; }
        public int? State { get; set; }
        public int AccountType { get; set; }
    }
}
