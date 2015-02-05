using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace DoubleConfirmIdentity.Mvc5
{
    public class DoubleConfirmIdentityChallengeResult : HttpUnauthorizedResult
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        public DoubleConfirmIdentityChallengeResult()
            : this(null)
        {
        }

        public DoubleConfirmIdentityChallengeResult(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties();
            if (UserId != null)
            {
                properties.Dictionary[XsrfKey] = UserId;
            }
            IAuthenticationManager authManager = context.HttpContext.GetOwinContext().Authentication;
            authManager.Challenge(properties, DoubleConfirmIdentityConstants.AuthenticationType);
        }

    }
}