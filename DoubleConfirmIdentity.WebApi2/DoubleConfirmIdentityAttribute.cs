using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DoubleConfirmIdentity.WebApi2
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class DoubleConfirmIdentityAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext == null)
                throw new ArgumentNullException("actionContext");

            if (IsAuthorized(actionContext))
                return;
            HandleUnauthorizedRequest(actionContext);
        }

        protected virtual bool IsAuthorized(HttpActionContext actionContext)
        {
            ClaimsPrincipal principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            return principal != null && principal.HasClaim(x => x.Type == DoubleConfirmIdentityConstants.ClaimType);
        }

        protected virtual void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = actionContext.Request
            };
        }
    }
}