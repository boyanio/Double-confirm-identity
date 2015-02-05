using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoubleConfirmIdentity
{
    public class DoubleConfirmIdentityHandler : AuthenticationHandler<DoubleConfirmIdentityOptions>
    {
        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode != 401 || !Options.RedirectPath.HasValue || !Request.User.Identity.IsAuthenticated
                || ((ClaimsPrincipal)Request.User).HasClaim(x => x.Type == DoubleConfirmIdentityConstants.ClaimType))
            {
                return Task.FromResult(0);
            }

            // Check for an issued challenge
            AuthenticationResponseChallenge challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);
            if (challenge != null)
            {
                string currentUri = Request.PathBase + Request.Path + Request.QueryString;
                string loginUri = Request.Scheme + Uri.SchemeDelimiter + Request.Host + Request.PathBase + Options.RedirectPath +
                                  new QueryString(Options.ReturnUrlParameter, currentUri);

                Response.Redirect(loginUri);
            }
            return Task.FromResult<object>(null);
        }

        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            return Task.FromResult<AuthenticationTicket>(null);
        }
    }
}