namespace WebApimyServices.Dto
{
    public class ProfileDto
    {
        public string Id { get; set; } = null!;

        [RegularExpression(@"^[\p{L}\s]{2,30}$",
                         ErrorMessage = "Last Name Must be 2 To 30 characters and can contain Arabic and English letters Only!.")]
        public string? FirstName { get; set; }

        [RegularExpression(@"^[\p{L}\s]{2,30}$",
                         ErrorMessage = "Last Name Must be 2 To 30 characters and can contain Arabic and English letters Only!.")]
        public string? LastName { get; set; } 
        public string? UserType { get; set; }
        public int JobId { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtenions),
            MaxFileSize(FileSettings.MaxFileSizeInMB)]
        public IFormFile? ProfilePicture { get; set; } 
    }
}
