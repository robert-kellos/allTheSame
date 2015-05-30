using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     Repository for working with DataSync records
    /// </summary>
    public class DataSyncRepository : GenericRepository<DataSync>, IDataSyncRepository
    {
        /// <summary>
        ///     Creates new DataSyncRepository using supplied DbContext
        /// </summary>
        /// <param name="context"></param>
        public DataSyncRepository(DbContext context) : base(context)
        {
        }
    }
}