using Microsoft.AspNetCore.Authorization;

namespace CovidSledilnik.Helpers
{
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            Policy = "BasicAuthentication";
        }
    }
}
