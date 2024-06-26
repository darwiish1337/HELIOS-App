namespace WebApimyServices.Middelwares
{
    public static class EndpointValidator
    {
        public static bool IsExcludedEndpoint(string path, List<string> excludedEndpoints)
        {
            return excludedEndpoints.Any(endpoint => path.Contains(endpoint, StringComparison.OrdinalIgnoreCase));
        }
    }
}
