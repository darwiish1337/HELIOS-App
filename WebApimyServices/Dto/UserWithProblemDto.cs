namespace WebApimyServices.Dto
{
    public class UserDto
    {
        public string Id { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 
        public string? DisplayName { get; set; }
        public string? Job { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserType { get; set; } 
        public int CityId { get; set; }
        public List<ProblemDto>? Problems { get; set; }
    }

    public class FactorDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? DisplayName { get; set; }
        public string? Job { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserType { get; set; }
        public int CityId { get; set; }
        public ICollection<RateDto> FactorGivenRates { get; set; }
        public decimal RatingValue { get; set; }
        public int RatingCount { get; set; }
    }

    public class RateDto
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string FactorId { get; set; }
        public decimal RatingValue { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ApplicationUser Customer { get; set; }
        public ApplicationUser Factor { get; set; }
    }

    public class ProblemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public bool Status { get; set; }
        public string ProblemImg { get; set; } 
        public DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }
    }

    public class CategoryForProblemAndUserDto
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string ImagePath { get; set; }
        public List<ProblemForCategoryDto> Problems { get; set; }
    }

    public class ProblemForCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string ProblemImg { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public virtual UserForGetProblemDto User { get; set; }
    }

    public class ProblemSharedDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
        public bool Status { get; set; }
        public string ProblemImg { get; set; } 
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; } 
        public virtual UserForGetProblemDto User { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryForProblemDto Category { get; set; }

    }

    public class UserForGetProblemDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string ProfilePicture { get; set; }
    }

    public class CategoryForProblemDto
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
    }
}
