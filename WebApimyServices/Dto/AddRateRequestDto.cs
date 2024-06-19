namespace WebApimyServices.Dto
{
    public class AddRateRequestDto
    {
        [Required]
        public string CustomerId { get; set; } 

        [Required]
        public string FactorId { get; set; } 

        [Required]
        public decimal RatingValue { get; set; }
    }

    public class FactorRatingDto
    {
        public IEnumerable<Rate> Ratings { get; set; }
        public int RatingCount { get; set; }
    }
}
