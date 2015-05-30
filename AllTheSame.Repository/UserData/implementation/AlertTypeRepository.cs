using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     AlertTypeRepository
    /// </summary>
    public class AlertTypeRepository : GenericRepository<AlertType>, IAlertTypeRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AlertTypeRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public AlertTypeRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}