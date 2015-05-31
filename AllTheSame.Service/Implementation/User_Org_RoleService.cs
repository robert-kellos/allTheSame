using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;
using AllTheSame.Service.Interfaces;

namespace AllTheSame.Service.Implementation
{
    /// <summary>
    /// User_Org_RoleService
    /// Uncomment _unitOfWork, _repository and Dispose area below
    /// when building custom methods for this service
    /// </summary>
    public class User_Org_RoleService : EntityService<User_Org_Role, IUser_Org_RoleRepository>, IUser_Org_RoleService
    {
        /// <summary>
        /// The _repository
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The repository.</param>
        public User_Org_RoleService(IUnitOfWork unitOfWork, IUser_Org_RoleRepository repository)
            : base(unitOfWork, repository)
        {
            //_unitOfWork = unitOfWork;
            //_repository = repository;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    _unitOfWork.Dispose();
            //}

            base.Dispose(disposing);
        }
    }
}