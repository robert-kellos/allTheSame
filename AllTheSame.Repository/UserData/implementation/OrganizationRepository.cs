using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     OrganizationRepository
    /// </summary>
    public class OrganizationRepository : SyncRepository<Organization>, IOrganizationRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OrganizationRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public OrganizationRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}