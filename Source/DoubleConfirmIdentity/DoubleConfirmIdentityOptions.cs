using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace DoubleConfirmIdentity
{
    public class DoubleConfirmIdentityOptions : AuthenticationOptions
    {
        public DoubleConfirmIdentityOptions()
            : base(DoubleConfirmIdentityConstants.AuthenticationType)
        {
            ReturnUrlParameter = "returnUrl";
            AuthenticationMode = AuthenticationMode.Passive;
        }

        public PathString RedirectPath { get; set; }

        public string ReturnUrlParameter { get; set; }
    }
}