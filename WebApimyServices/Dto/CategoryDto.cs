namespace WebApimyServices.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProblemDto> Problems { get; set; }
    }
}
