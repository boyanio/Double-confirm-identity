using Owin;

namespace DoubleConfirmIdentity
{
    public static class OwinExtensions
    {
        public static void UseDoubleConfirmIdentity(this IAppBuilder app, DoubleConfirmIdentityOptions options)
        {
            app.Use(typeof(DoubleConfirmIdentityMiddleware), options);
        }
    }
}