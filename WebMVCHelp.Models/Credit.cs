namespace WebMVCHelp.Models
{
    public class Credit
    {
        public Credit()
        {
            CreditId = 0;
            Description = string.Empty;
        }
        public Credit(string Description)
        {
            CreditId = 0;
            this.Description = Description;
        }
        public Credit(int CreditId, string Description)
        {
            this.CreditId = CreditId;
            this.Description = Description;
        }
        public int CreditId { get; set; }
        public string Description { get; set; }
    }
}
