namespace WebApimyServices.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string? DisplayName { get; set; }
        public string UserType { get; set; } 
        public string? Job { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Problems> Problems { get; set; } = new List<Problems>();
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; } = new List<RefreshToken>();
        public int CityId { get; set; }
        public virtual City? City { get; set; } 
    }
}
