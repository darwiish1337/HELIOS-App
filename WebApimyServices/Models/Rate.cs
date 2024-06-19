namespace WebApimyServices.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public decimal RatingValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string FactorId { get; set; }
        public ApplicationUser Factor { get; set; } // the Factor who received the rate

        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
    }
}
