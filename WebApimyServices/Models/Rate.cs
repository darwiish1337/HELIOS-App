namespace WebApimyServices.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public decimal RatingValue { get; set; }
        public DateTime RatedAt { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
