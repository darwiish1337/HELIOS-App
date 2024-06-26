namespace WebApimyServices.Models
{
    public class Problems
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string? ProblemImg { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; } 
        public virtual ApplicationUser? User { get; set; } 
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
