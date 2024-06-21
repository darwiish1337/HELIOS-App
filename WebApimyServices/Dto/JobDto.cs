namespace WebApimyServices.Dto
{
    public class JobDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public FactorForJobDto User { get; set; }
    }

    public class JobOnlyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }

    public class FactorForJobDto
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string DisplayName { get; set; }
        public string ProfilePicture { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }

    public class JobForFactorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }

    public class UserJobInfoDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
