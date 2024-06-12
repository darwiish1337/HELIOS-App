namespace WebApimyServices.Dto
{
    public class ProfileDto
    {
        public string Id { get; set; } = null!;
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? UserType { get; set; }
        public string? Job { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtenions),
            MaxFileSize(FileSettings.MaxFileSizeInMB)]
        public IFormFile? ProfilePicture { get; set; } 
    }
}
