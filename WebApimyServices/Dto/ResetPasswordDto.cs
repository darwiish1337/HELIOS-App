namespace WebApimyServices.Dto
{
    public class ResetPasswordDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;
    }
}
