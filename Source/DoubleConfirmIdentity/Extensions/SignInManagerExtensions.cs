using DoubleConfirmIdentity;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Unipension.SelfService.Web.Auth.DoubleConfirmIdentity;

namespace Microsoft.AspNet.Identity.Owin
{
    public static class SignInManagerExtensions
    {
        /// <summary>
        /// Signs in a user by username and password + confirms the identity.
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="signInManager"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="shouldLockout"></param>
        /// <returns></returns>
        public static async Task<SignInStatus> PasswordSignInAndConfirmAsync<TUser, TKey>(this SignInManager<TUser, TKey> signInManager, string userName, string password, bool isPersistent, bool shouldLockout)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>, IConvertible
        {
            SignInStatus status = await signInManager.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
            if (status == SignInStatus.Success)
            {
                // We have to override the existing grant with a new one, as we are adding
                // a new claim
                TUser user = await signInManager.UserManager.FindByNameAsync(userName);
                string userIdString = signInManager.ConvertIdToString(user.Id);
                AuthenticationResponseGrant grant = signInManager.AuthenticationManager.AuthenticationResponseGrant;
                grant.Identity.AddClaim(new Claim(DoubleConfirmIdentityConstants.ClaimName, userIdString));
                signInManager.AuthenticationManager.SignIn(grant.Properties, grant.Identity);
            }
            return status;
        }

        /// <summary>
        /// Confirms the given identity
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="signInManager"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static async Task<SignInStatus> DoubleConfirmIdentityAsync<TUser, TKey>(this SignInManager<TUser, TKey> signInManager, ClaimsIdentity identity)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>, IConvertible
        {
            SignInStatus status;
            if (identity == null || signInManager.UserManager == null)
            {
                status = SignInStatus.Failure;
            }
            else
            {
                TKey userId = identity.GetUserId<TKey>();
                TUser user = await signInManager.UserManager.FindByIdAsync(userId);
                if (user == null)
                {
                    status = SignInStatus.Failure;
                }
                else
                {
                    bool isLockedOut = await signInManager.UserManager.IsLockedOutAsync(userId);
                    if (isLockedOut)
                    {
                        status = SignInStatus.LockedOut;
                    }
                    else
                    {
                        string userIdString = signInManager.ConvertIdToString(userId);
                        ClaimsIdentity newIdentity = new ClaimsIdentity(identity);
                        newIdentity.AddClaim(new Claim(DoubleConfirmIdentityConstants.ClaimName, userIdString));
                        signInManager.AuthenticationManager.SignIn(newIdentity);
                        status = SignInStatus.Success;
                    }
                }
            }
            return status;
        }
    }
}