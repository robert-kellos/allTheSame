using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     PolicyRepository
    /// </summary>
    public class PolicyRepository : GenericRepository<Policy>, IPolicyRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PolicyRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public PolicyRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}