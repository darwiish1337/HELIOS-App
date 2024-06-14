namespace WebApimyServices.Helpers
{
    public class UpdateResult
    {
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
        public Dictionary<string, int> DaysUntilNextUpdate { get; set; }
        public ApplicationUser User { get; set; }
    }
}
