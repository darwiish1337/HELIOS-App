namespace WebApimyServices.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string ImagePath { get; set; }
        public virtual ICollection<Problems> Problems { get; set; }  = new List<Problems>();
    }
}
