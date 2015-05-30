using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;
using AllTheSame.Service.Interfaces;

namespace AllTheSame.Service.Implementation
{
    /// <summary>
    ///     UserService
    /// </summary>
    public class UserService : EntityService<User, IUserRepository>, IUserService
    {
        /// <summary>
        ///     The _user repository
        /// </summary>
        private readonly IUserRepository _repository;

        /// <summary>
        ///     The _unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The user repository.</param>
        public UserService(IUnitOfWork unitOfWork, IUserRepository repository)
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
        public User GetById(long id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Dispose locals
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

                //if (_userRepository != null)
                //{
                //    _userRepository.Dispose();
                //}
            }
            base.Dispose(disposing);
        }
    }
}