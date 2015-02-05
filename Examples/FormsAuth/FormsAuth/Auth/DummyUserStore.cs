using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace DoubleConfirmIdentity.Examples.FormsAuth.Auth
{
    public class DummyUserStore<TUser> : IUserStore<TUser>, IUserPasswordStore<TUser>, IUserLockoutStore<TUser, string>, IUserTwoFactorStore<TUser, string>
        where TUser : class, IUser<string>
    {
        private readonly Dictionary<string, TUser> _storeById = new Dictionary<string, TUser>();
        private readonly Dictionary<string, TUser> _storeByName = new Dictionary<string, TUser>();
        private readonly Dictionary<string, string> _passwordsById = new Dictionary<string, string>();

        public Task CreateAsync(TUser user)
        {
            _storeById.Add(user.Id, user);
            _storeByName.Add(user.UserName, user);
            return Task.FromResult(0);
        }

        public Task DeleteAsync(TUser user)
        {
            _storeById.Remove(user.Id);
            _storeByName.Remove(user.UserName);
            _passwordsById.Remove(user.Id);
            return Task.FromResult(0);
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            TUser user = _storeById.ContainsKey(userId) ? _storeById[userId] : null;
            return Task.FromResult(user);
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            TUser user = _storeByName.ContainsKey(userName) ? _storeByName[userName] : null;
            return Task.FromResult(user);
        }

        public Task UpdateAsync(TUser user)
        {
            _storeById[user.Id] = user;
            _storeByName[user.UserName] = user;
            return Task.FromResult(0);
        }

        public void Dispose()
        {
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            return Task.FromResult(_passwordsById.ContainsKey(user.Id) ? _passwordsById[user.Id] : null);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(_passwordsById.ContainsKey(user.Id));
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            if (_passwordsById.ContainsKey(user.Id))
            {
                _passwordsById[user.Id] = passwordHash;
            }
            else
            {
                _passwordsById.Add(user.Id, passwordHash);
            }
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return Task.FromResult(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            return Task.FromResult<DateTimeOffset>(default(DateTimeOffset));
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(0);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return Task.FromResult(false);
        }

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            return Task.FromResult(0);
        }
    }

    public class ApplicationDummyUserStore : DummyUserStore<ApplicationUser>
    {
        public static ApplicationDummyUserStore Create()
        {
            var passwordHasher = new PasswordHasher();
            string passwordHash = passwordHasher.HashPassword("12345678");
            var user1 = new ApplicationUser { Id = "1", UserName = "test" };

            var store = new ApplicationDummyUserStore();
            store.CreateAsync(user1).Wait();
            store.SetPasswordHashAsync(user1, passwordHash).Wait();
            return store;
        }
    }
}