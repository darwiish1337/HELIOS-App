namespace WebApimyServices.Models
{
    public class Governorate
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public ICollection<City> City { get; set; } = new List<City>();
    }
}
