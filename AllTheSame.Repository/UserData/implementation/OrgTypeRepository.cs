using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     OrgTypeRepository
    /// </summary>
    public class OrgTypeRepository : GenericRepository<OrgType>, IOrgTypeRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OrgTypeRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public OrgTypeRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}