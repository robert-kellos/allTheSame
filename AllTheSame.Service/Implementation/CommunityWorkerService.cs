using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;
using AllTheSame.Service.Interfaces;

namespace AllTheSame.Service.Implementation
{
    /// <summary>
    /// CommunityWorkerService
    /// Uncomment _unitOfWork, _repository and Dipose area below
    /// when building custom methods for this service
    /// </summary>
    public class CommunityWorkerService : SyncEntityService<CommunityWorker, ICommunityWorkerRepository>, ICommunityWorkerService
    {
        /// <summary>
        /// The _repository
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The repository.</param>
        public CommunityWorkerService(IUnitOfWork unitOfWork, ICommunityWorkerRepository repository)
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