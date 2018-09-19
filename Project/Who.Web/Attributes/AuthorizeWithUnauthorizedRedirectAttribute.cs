using System.Web.Mvc;
using System.Web.Routing;

namespace Who.Web
{
    public class AuthorizeWithUnauthorizedRedirectAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Returns HTTP 401 by default - see HttpUnauthorizedResult.cs.
            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary
            {
                { "action", "SignIn" },
                { "controller", "Account" }
            });
        }
    }
}