namespace WebApimyServices.Authorization
{
    public class GlobalVerbRoleRequirement : IAuthorizationRequirement
    {
        public bool IsAllowed(string role, string verb)
        {
            // Allow all verbs if user is "Admin"
            if (string.Equals(RoleConstants.Admin, role, StringComparison.OrdinalIgnoreCase))
                return true;

            // Allow the "POST" verb if user is "Customer"
            if (string.Equals(RoleConstants.Customer, role, StringComparison.OrdinalIgnoreCase) &&
                string.Equals("POST", verb, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            // Allow the "PUT" verb if user is "Customer"
            if (string.Equals(RoleConstants.Customer, role, StringComparison.OrdinalIgnoreCase) &&
                string.Equals("PUT", verb, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            // Allow the "DELETE" verb if user is "Customer"
            if (string.Equals(RoleConstants.Customer, role, StringComparison.OrdinalIgnoreCase) &&
                string.Equals("DELETE", verb, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
