namespace WebApimyServices.Dto
{
    public class ProblemsDto
    {
        public int Id { get; set; }

        [RegularExpression(@"^[\u0020-\u007E\u00A0-\u00BE\u0600-\u06FF\u00C0-\u017Fa-zA-Z0-9_]*$", 
            ErrorMessage = "Special characters are not allowed.")]
        public string Name { get; set; } 

        [RegularExpression(@"^[\u0020-\u007E\u00A0-\u00BE\u0600-\u06FF\u00C0-\u017Fa-zA-Z0-9_]*$",
            ErrorMessage = "Special characters are not allowed.")]
        public string Description { get; set; } 
        public bool Status { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtenions),
            MaxFileSize(FileSettings.MaxFileSizeInMB)]
        public IFormFile ProblemImg { get; set; } 
        public string UserId { get; set; } 
        public int CategoryId { get; set; }
    }

    public class ProblemsUpdateDto
    {
        public int Id { get; set; }

        [RegularExpression(@"^[\u0020-\u007E\u00A0-\u00BE\u0600-\u06FF\u00C0-\u017Fa-zA-Z0-9_]*$",
            ErrorMessage = "Special characters are not allowed.")]
        public string Name { get; set; }

        [RegularExpression(@"^[\u0020-\u007E\u00A0-\u00BE\u0600-\u06FF\u00C0-\u017Fa-zA-Z0-9_]*$",
            ErrorMessage = "Special characters are not allowed.")]
        public string Description { get; set; }
        public bool Status { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtenions),
            MaxFileSize(FileSettings.MaxFileSizeInMB)]
        public IFormFile ProblemImg { get; set; }
        public int CategoryId { get; set; }
    }
}
