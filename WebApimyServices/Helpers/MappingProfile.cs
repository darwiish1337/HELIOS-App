namespace WebApimyServices.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, RegistertionFactorDto>().ReverseMap();

            CreateMap<ApplicationUser, RegistertionCustomerDto>().ReverseMap();

            CreateMap<Problems, ProblemsDto>().ReverseMap();

            CreateMap<ApplicationUser, ProfileDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
