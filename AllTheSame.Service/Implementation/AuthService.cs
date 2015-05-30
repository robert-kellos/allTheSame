using System;
using System.Collections.Generic;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Authentication;
using AllTheSame.Repository.Common;
using AllTheSame.Service.Common;
using AllTheSame.Service.Interfaces;

namespace AllTheSame.Service.Implementation
{
    /// <summary>
    ///     AuthService
    /// 
    /// Uncomment _unitOfWork, _repository and Dipose area below 
    /// when building custom methods for this service
    /// 
    /// </summary>
    public class AuthService : EntityService<UserSession, IAuthRepository>, IAuthService
    {
        ///// <summary>
        /////     The _unit of work
        ///// </summary>
        //private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="authRepository">The authentication repository.</param>
        public AuthService(IUnitOfWork unitOfWork, IAuthRepository authRepository)
            : base(unitOfWork, authRepository)
        {
            //_unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Gets the authentication.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public UserSession GetAuthentication(long id)
        {
            return Repository.GetAuthentication(id);
        }

        /// <summary>
        ///     Creates a new
        ///     UserSession
        ///     with default paramaters and returns
        ///     a newly generated AuthToken
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="expiration">Expiration Date of Session in UTC Null for no expiration</param>
        /// <returns></returns>
        public Guid CreateSession(int userId, DateTime? expiration)
        {
            var uSession = new UserSession
            {
                UserId = userId,
                SessionId = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Expiration = expiration,
                IsValid = true
            };

            uSession = Add(uSession);
            Save();
            return uSession.SessionId;
        }

        /// <summary>
        ///     Returns list of calcualted permissiosn for the user at the given orgId
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">User Session is expired</exception>
        public IList<string> GetUserRoles(Guid sessionId, int orgId)
        {
            var user = Repository.GetSingle(u => u.SessionId == sessionId);
            if (user == null)
            {
                throw new Exception(string.Format("User Session Not found. SessionId:{0}", sessionId));
            }
            if (user.Expiration != null && user.Expiration < DateTime.UtcNow)
            {
                throw new Exception("User Session is expired");
            }

            return Repository.GetUserRoles(user.UserId, orgId);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if(obj != null)
                //{
                //    obj.Dispose();
                //}
            }
            base.Dispose();
        }
    }
}