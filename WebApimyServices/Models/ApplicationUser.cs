namespace WebApimyServices.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string DisplayName { get; set; }
        public string UserType { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime? LastFirstnameUpdateDate { get; set; }
        public DateTime? LastLastnameUpdateDate { get; set; }
        public DateTime? LastUserTypeUpdateDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Problems> Problems { get; set; } = new List<Problems>();
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<Rate> ReceivedRates { get; set; }
        public ICollection<Rate> CustomerRates { get; set; }
        public int? JobId { get; set; }
        public virtual Job? Job { get; set; }
        public decimal AverageRating { get; set; }
        public int CityId { get; set; }
        public virtual City? City { get; set; } 
    }
}
