using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;
using AllTheSame.Service.Interfaces;

namespace AllTheSame.Service.Implementation
{
    /// <summary>
    /// VendorAdminService
    /// Uncomment _unitOfWork, _repository and Dispose area below
    /// when building custom methods for this service
    /// </summary>
    public class VendorAdminService : SyncEntityService<VendorAdmin, IVendorAdminRepository>, IVendorAdminService
    {
        /// <summary>
        /// The _repository
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The repository.</param>
        public VendorAdminService(IUnitOfWork unitOfWork, IVendorAdminRepository repository)
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