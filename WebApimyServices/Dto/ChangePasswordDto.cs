namespace WebApimyServices.Dto
{
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;

        [Compare("NewPassword", ErrorMessage = "ConfirmNewPassword Must Be Same With NewPassword")]
        public string ConfirmNewPassword { get; set; } = null!;
    }
}
