namespace WebApimyServices.Dto
{
    public class ProblemWithUserWithCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string ProblemImg { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; } 
        public string UserId { get; set; }
        public UserDtoForSearch User { get; set; } 
    }

    public class UserDtoForSearch
    {
        public string Id { get; set; }
        public string Email { get; set; } 
        public string DisplayName { get; set; }
        public string ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }

    }
}
