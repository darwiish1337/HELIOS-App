namespace WebApimyServices.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public ApplicationUser User { get; set; }
    }
}
