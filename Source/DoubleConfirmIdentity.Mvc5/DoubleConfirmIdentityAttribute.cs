using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace DoubleConfirmIdentity.Mvc5
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class DoubleConfirmIdentityAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            ClaimsPrincipal principal = httpContext.User as ClaimsPrincipal;
            return principal != null && principal.HasClaim(x => x.Type == DoubleConfirmIdentityConstants.ClaimType);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string userId = filterContext.HttpContext.User.Identity.GetUserId();
            filterContext.Result = new DoubleConfirmIdentityChallengeResult(userId);
        }
    }
}