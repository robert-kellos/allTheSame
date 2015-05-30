using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     RequirementRepository
    /// </summary>
    public class RequirementRepository : GenericRepository<Requirement>, IRequirementRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RequirementRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public RequirementRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}