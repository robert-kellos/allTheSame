using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Common.Interfaces.Generic;
using AllTheSame.Entity.Model.Custom;
using AllTheSame.Repository.Common;

namespace AllTheSame.Service.Interfaces.Custom
{
    /// <summary>
    /// IAccountManagement - Composite interface for account management
    /// </summary>
    public interface IAccountManagement : IUnitOfWork
    {
        /// <summary>
        /// Gets my account.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        AccountData GetMyAccount(string userName);
        /// <summary>
        /// Sets my account.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="accountData">The account data.</param>
        /// <returns></returns>
        bool SetMyAccount(string userName, AccountData accountData);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        bool ResetPassword(string userName);
        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        bool UpdatePassword(string userName, string password);

        /// <summary>
        /// Requests the user identifier.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        string RequestUserId(params string[] data);
    }
}
