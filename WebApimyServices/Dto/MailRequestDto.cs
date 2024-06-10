namespace WebApimyServices.Dto
{
    public class MailRequestDto
    {
        public string Email { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
    }
}
