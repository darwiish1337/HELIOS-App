namespace WebApimyServices.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public List<ProblemDto> Problems { get; set; }
    }
}
