namespace WebApimyServices.Models
{
    public class Governorate
    {
        public int Id { get; set; }
        public string GoverNameAR { get; set; } 
        public string GoverNameEN { get; set; } 
        public ICollection<City> City { get; set; } = new List<City>();
    }
}
