using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;

namespace DoubleConfirmIdentity
{
    public class DoubleConfirmIdentityMiddleware : AuthenticationMiddleware<DoubleConfirmIdentityOptions>
    {
        public DoubleConfirmIdentityMiddleware(OwinMiddleware next, DoubleConfirmIdentityOptions options)
            : base(next, options) { }

        protected override AuthenticationHandler<DoubleConfirmIdentityOptions> CreateHandler()
        {
            return new DoubleConfirmIdentityHandler();
        }
    }
}