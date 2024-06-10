namespace WebApimyServices.Dto
{
    public class UserDto
        {
            public string Id { get; set; } = null!;
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string PhoneNumber { get; set; } = null!;
            public string? DisplayName { get; set; }
            public string? Job { get; set; }
            public string? ProfilePicture { get; set; }
            public DateTime CreatedDate { get; set; }
            public string UserType { get; set; } = null!;
            public int CityId { get; set; }
            public List<ProblemDto>? Problems { get; set; }
    }

    public class ProblemDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
            public string Type { get; set; } = null!;
            public bool Status { get; set; }
            public string ProblemImg { get; set; } = null!;
            public DateTime CreatedDate { get; set; }
            public int CategoryId { get; set; }
    }

    public class ProblemWithUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!;
        public bool Status { get; set; }
        public string ProblemImg { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; } = null!;
    }
}
