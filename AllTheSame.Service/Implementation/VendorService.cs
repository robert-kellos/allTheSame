using System.Collections.Generic;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;
using AllTheSame.Service.Interfaces;

namespace AllTheSame.Service.Implementation
{
    /// <summary>
    ///     VendorService
    /// </summary>
    public class VendorService : SyncEntityService<Vendor, IVendorRepository>, IVendorService
    {
        /// <summary>
        ///     The _person repository
        /// </summary>
        private readonly IVendorRepository _repository;

        /// <summary>
        ///     The _unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="vendorRepository">The vendor repository.</param>
        public VendorService(IUnitOfWork unitOfWork, IVendorRepository vendorRepository)
            : base(unitOfWork, vendorRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = vendorRepository;
        }

        /// <summary>
        ///     Gets the vendor worker community.
        /// </summary>
        /// <param name="orgId">The org identifier.</param>
        /// <returns></returns>
        public IEnumerable<Vendor> GetVendorWorkerCommunity(int orgId)
        {
            return _repository.GetVendorWorkerCommunity(orgId);
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Vendor GetById(long id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Dispose
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