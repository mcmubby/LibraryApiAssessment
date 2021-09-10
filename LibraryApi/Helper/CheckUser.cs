using System.Linq;
using Microsoft.AspNetCore.Http;

namespace LibraryApi.Helper
{
    public class CheckUser
    {
        public static int GetAuthenticatedUser(HttpContext httpContext)
        {
            var isUser = httpContext.Request.Headers.Any(c => c.Key.ToUpper() == "USERNAME" && c.Value == "user");
            var isAdmin = httpContext.Request.Headers.Any(c => c.Key.ToUpper() == "ADMIN" && c.Value == "1");

            if(isAdmin){ return 1; }
            else if(isUser) { return 2; }
            else { return 0; }
        }
    }

}