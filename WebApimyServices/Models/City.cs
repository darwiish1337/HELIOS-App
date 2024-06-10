namespace WebApimyServices.Models
{
    public class City
    {
        public int Id { get; set; }
        public string CityNameAR { get; set; } 
        public string CityNameEN { get; set; } 
        public int GovernorateId { get; set; }
        public Governorate? Governorate { get; set; } 
        public ICollection<ApplicationUser> User { get; set; } = new List<ApplicationUser>();
    }
}
