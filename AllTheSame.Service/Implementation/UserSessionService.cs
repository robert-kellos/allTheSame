using System;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;
using AllTheSame.Service.Interfaces;

namespace AllTheSame.Service.Implementation
{
    /// <summary>
    ///     UserSessionService
    /// </summary>
    public class UserSessionService : EntityService<UserSession, IUserSessionRepository>, IUserSessionService
    {
        /// <summary>
        ///     The _user session repository
        /// </summary>
        private readonly IUserSessionRepository _repository;

        /// <summary>
        ///     The _unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserSessionService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The user session repository.</param>
        public UserSessionService(IUnitOfWork unitOfWork, IUserSessionRepository repository)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public UserSession GetById(long id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        ///     Gets the session.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public UserSession GetSession(long userId)
        {
            return _repository.GetSession(userId);
        }

        /// <summary>
        ///     Deletes the session.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public long DeleteSession(long userId)
        {
            var exists = GetSession(userId);
            if (exists == null) return -1;

            var deleted = _repository.Delete(exists);
            if (deleted == null) return -1;

            return deleted.Id;
        }

        /// <summary>
        /// Dispoase locals
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                }
                //if (_repository != null)
                //{
                //    _repository.Dispose();
                //}
            }
            base.Dispose(disposing);
        }
    }
}