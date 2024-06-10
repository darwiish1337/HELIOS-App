namespace WebApimyServices.Dto
{
    public class BaseUserDto
    {
        [RegularExpression(@"^[\p{L}\s]{2,30}$",
            ErrorMessage = "First Name Must be 2 To 30 characters and can contain Arabic and English letters Only!.")]
        public string FirstName { get; set; } = null!;

        [RegularExpression(@"^[\p{L}\s]{2,30}$", 
            ErrorMessage = "Last Name Must be 2 To 30 characters and can contain Arabic and English letters Only!.")]
        public string LastName { get; set; } = null!;
        public int GoverID { get; set; }
        public int CityID { get; set; }
        public string UserType { get; set; } = null!;

        [StringLength(maximumLength: 30, MinimumLength = 11)]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(maximumLength: 50, MinimumLength = 10)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string Password { get; set; } = null!;

        [Compare("Password", 
            ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
