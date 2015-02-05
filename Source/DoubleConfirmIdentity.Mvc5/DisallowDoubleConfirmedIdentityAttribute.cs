using System.Security.Claims;
using System.Web.Mvc;

namespace DoubleConfirmIdentity.Mvc5
{
    /// <summary>
    /// Disallow users confirming their identity twice, if already done.
    /// </summary>
    public class DisallowDoubleConfirmedIdentityAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ClaimsPrincipal principal = filterContext.HttpContext.User as ClaimsPrincipal;
            if (principal != null && principal.Identity.IsAuthenticated && principal.HasClaim(c => c.Type == DoubleConfirmIdentityConstants.ClaimType))
            {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}