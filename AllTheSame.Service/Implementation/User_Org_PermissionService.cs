﻿using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;
using AllTheSame.Service.Interfaces;

namespace AllTheSame.Service.Implementation
{
    /// <summary>
    /// User_Org_PermissionService
    /// Uncomment _unitOfWork, _repository and Dispose area below
    /// when building custom methods for this service
    /// </summary>
    public class User_Org_PermissionService : EntityService<User_Org_Permission, IUser_Org_PermissionRepository>, IUser_Org_PermissionService
    {
        /// <summary>
        /// The _repository
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The repository.</param>
        public User_Org_PermissionService(IUnitOfWork unitOfWork, IUser_Org_PermissionRepository repository)
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