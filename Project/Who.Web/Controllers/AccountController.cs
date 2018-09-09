using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web;
using System.Web.Mvc;
using Who.BL.IServices;

namespace Who.Web.Controllers
{
    public class AccountController : BaseController
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public void SignIn()
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(
       new AuthenticationProperties { RedirectUri = "http://localhost:53306/Account/LogInCallback" },
       OpenIdConnectAuthenticationDefaults.AuthenticationType);


            }
        }

        private string GetAzureObjectIdentifier()
        {
            if (Request.IsAuthenticated)
            {
                var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

                return userClaims.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            }
            return "";
        }

        private string GetUserName()
        {
            if (Request.IsAuthenticated)
            {
                var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

                return userClaims?.FindFirst("name")?.Value;
            }
            return "";
        }

        /// <summary>
        /// Send an OpenID Connect sign-out request.
        /// </summary>
        public void SignOut()
        {
            string callbackUrl = Url.Action("SignOutCallback", "Account", routeValues: null, protocol: Request.Url.Scheme);

            HttpContext.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);

        }

        public ActionResult SignOutCallback()
        {
                return RedirectToAction("Index", "Home");
        }

        public ActionResult LogInCallback()
        {
            if (Request.IsAuthenticated)
            {
                var userId = _userService.GetUser(GetAzureObjectIdentifier());
                if (-1 == userId)
                {
                    userId = _userService.Register(GetUserName(), GetAzureObjectIdentifier());
                }
                AddUserIdToSessionStorage(userId);
                // Redirect to home page if the user is authenticated.
                return RedirectToAction("Index", "Game");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}