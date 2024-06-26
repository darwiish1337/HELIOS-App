namespace WebApimyServices.Attributes
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string _role;
        private readonly string _unauthorizedMessage;
        private readonly string _forbiddenMessage;

        public AuthorizeRoleAttribute(string role, string unauthorizedMessage = "User is not authenticated", string forbiddenMessage = "User does not have the required role")
        {
            _role = role;
            _unauthorizedMessage = unauthorizedMessage;
            _forbiddenMessage = forbiddenMessage;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new JsonResult(new { message = _unauthorizedMessage })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            if (!user.IsInRole(_role))
            {
                context.Result = new JsonResult(new { message = _forbiddenMessage })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }

            await Task.CompletedTask;
        }
    }
}
