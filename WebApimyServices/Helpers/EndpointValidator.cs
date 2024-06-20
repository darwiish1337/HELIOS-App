namespace WebApimyServices.Helpers
{
    public static class EndpointValidator
    {
        public static bool IsExcludedEndpoint(string path, List<string> excludedEndpoints)
        {
            return excludedEndpoints.Any(endpoint => path.StartsWith(endpoint, StringComparison.OrdinalIgnoreCase));
        }
    }
}
