namespace WebApimyServices.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string ToBase64(this string originalString)
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(originalString);
            return Convert.ToBase64String(bytesToEncode);
        }
        public static string FromBase64(this string base64String)
        {
            byte[] bytesToDecode = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(bytesToDecode);
        }
    }
}
