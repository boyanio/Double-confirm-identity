using Microsoft.AspNet.Identity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace DoubleConfirmIdentity.Mvc5
{
    public class DoubleConfirmIdentityAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            ClaimsPrincipal principal = httpContext.User as ClaimsPrincipal;
            return principal != null && principal.Claims.Any(x => x.Type == DoubleConfirmIdentityConstants.ClaimType);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            ClaimsPrincipal principal = filterContext.HttpContext.User as ClaimsPrincipal;
            string userId = principal != null ? principal.Identity.GetUserId() : null;
            filterContext.Result = new DoubleConfirmIdentityChallengeResult(userId);
        }
    }
}