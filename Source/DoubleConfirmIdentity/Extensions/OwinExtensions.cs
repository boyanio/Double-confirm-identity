using DoubleConfirmIdentity;
using Owin;

namespace Unipension.SelfService.Web.Auth.DoubleConfirmIdentity
{
    public static class OwinExtensions
    {
        public static void UseDoubleConfirmIdentity(this IAppBuilder app, DoubleConfirmIdentityOptions options)
        {
            app.Use(typeof(DoubleConfirmIdentityMiddleware), options);
        }
    }
}