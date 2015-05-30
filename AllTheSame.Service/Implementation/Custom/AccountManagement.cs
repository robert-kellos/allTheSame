using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Service.Interfaces.Custom;
using AllTheSame.Common.Logging;

namespace AllTheSame.Service.Implementation.Custom
{
    /// <summary>
    /// AccountManagement
    /// </summary>
    public class AccountManagement : IAccountManagement
    {
        /// <summary>
        /// Gets my account.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Entity.Model.Custom.AccountData GetMyAccount(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets my account.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="accountData">The account data.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool SetMyAccount(string userName, Entity.Model.Custom.AccountData accountData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool ResetPassword(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool UpdatePassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Requests the user identifier.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string RequestUserId(params string[] data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>
        /// The number of objects in an Added, Modified, or Deleted state
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Commit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (obj != null)
                //    obj.Dispose();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

            Audit.Log.Debug("AccountManagement Dispose :: CurrentDbContext destroyed");
        }
    }
}
