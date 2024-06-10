namespace WebApimyServices.Dto
{
    public class ConfirmResetPasswordDto
    {
        public string NewPassword { get; set; } = null!;

        [Compare("NewPassword", ErrorMessage = "ConfirmNewPassword Must Be Same NewPassword")]
        public string ConfirmNewPassword { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
