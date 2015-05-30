using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;
using AllTheSame.Service.Interfaces;

namespace AllTheSame.Service.Implementation
{
    /// <summary>
    ///     Provides methods for interacting with DataSync Entities
    /// </summary>
    public class DataSyncService : EntityService<DataSync, IDataSyncRepository>, IDataSyncService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DataSyncService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The repository.</param>
        public DataSyncService(IUnitOfWork unitOfWork, IDataSyncRepository repository) : base(unitOfWork, repository)
        {
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