namespace WebApimyServices.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public virtual ICollection<Problems> Problems { get; set; }  = new List<Problems>();
    }
}
