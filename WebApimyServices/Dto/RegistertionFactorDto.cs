namespace WebApimyServices.Dto
{
    public class RegistertionFactorDto : BaseUserDto
    {
        [Required]
        public int JobId { get; set; }
    }
}
