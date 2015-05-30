using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Authentication;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.implementation;
using AllTheSame.Service.Implementation;
using AllTheSame.Service.Interfaces;
using Microsoft.AspNet.Identity;

namespace AllTheSame.WebAPI.Models
{
    /// <summary>
    ///     AllTheSameUserStore
    /// </summary>
    public class AllTheSameUserStore : IUserPasswordStore<User, int>, IUserEmailStore<User, int>,
        IUserClaimStore<User, int>
    {
        /// <summary>
        ///     The _user service
        /// </summary>
        private readonly UserService _userService;

        /// <summary>
        ///     The _disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AllTheSameUserStore" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public AllTheSameUserStore(DbContext dbContext)
        {
            Context = dbContext;
            _userService = new UserService(new UnitOfWork(Context), new UserRepository(Context));
        }

        /// <summary>
        ///     Context for the store
        /// </summary>
        /// <value>
        ///     The context.
        /// </value>
        public DbContext Context { get; private set; }

        /// <summary>
        ///     If true will call dispose on the DbContext during Dispose
        /// </summary>
        /// <value>
        ///     <c>true</c> if [dispose context]; otherwise, <c>false</c>.
        /// </value>
        public bool DisposeContext { get; set; }

        /// <summary>
        ///     Gets the claims asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task<IList<Claim>> GetClaimsAsync(User user)
        {
            IAuthService service = new AuthService(new UnitOfWork(Context), new AuthRepository(Context));
            var sessionId = service.CreateSession(user.Id, DateTime.UtcNow.AddMinutes(20));
            //TODO: Configure Session Timeout
            IList<Claim> list = new List<Claim> { new Claim(ClaimTypes.PrimarySid, sessionId.ToString()) };

            return Task.FromResult(list);
        }

        /// <summary>
        ///     Adds the claim asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="claim">The claim.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task AddClaimAsync(User user, Claim claim)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Removes the claim asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="claim">The claim.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task RemoveClaimAsync(User user, Claim claim)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region IUserStore Implementation

        /// <summary>
        ///     Creates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task CreateAsync(User user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _userService.Add(user);
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Updates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task UpdateAsync(User user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _userService.Repository.Edit(user);
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Deletes the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task DeleteAsync(User user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _userService.Repository.Delete(user);
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<User> FindByIdAsync(int userId)
        {
            ThrowIfDisposed();

            return Task.FromResult(_userService.GetById(userId));
        }

        /// <summary>
        ///     Finds the by name asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public Task<User> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            var users = _userService.Repository.FindBy(u => u.Username.ToUpper() == userName.ToUpper(),
                p => p.Person);
            return Task.FromResult(users.FirstOrDefault());
        }

        #endregion

        #region IUserPasswordStore Implementation

        /// <summary>
        ///     Sets the password hash asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Gets the password hash asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task<string> GetPasswordHashAsync(User user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>
        ///     Determines whether [has password asynchronous] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        #endregion

        #region Helpers

        /// <summary>
        ///     If disposing, calls dispose on the Context.  Always nulls out the Context
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_userService != null)
            {
                _userService.Dispose();
            }
            if (DisposeContext && disposing && Context != null)
            {
                Context.Dispose();
            }
            _disposed = true;
            //Context = null; don't need to set to null, breask GC.SuppressFinalize
        }

        /// <summary>
        ///     Throws if disposed.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException"></exception>
        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        #endregion

        #region IUserEmailStore Implementation

        /// <summary>
        ///     Sets the email asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public Task SetEmailAsync(User user, string email)
        {
            user.Person.Email = email;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Gets the email asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task<string> GetEmailAsync(User user)
        {
            return Task.FromResult(user.Person.Email);
        }

        /// <summary>
        ///     Gets the email confirmed asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        ///     Sets the email confirmed asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="confirmed">if set to <c>true</c> [confirmed].</param>
        /// <returns></returns>
        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Finds the by email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public Task<User> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            var users = _userService.Repository.FindBy(u => u.Person.Email.ToUpper() == email.ToUpper());
            return Task.FromResult(users.SingleOrDefault());
        }

        #endregion
    }
}