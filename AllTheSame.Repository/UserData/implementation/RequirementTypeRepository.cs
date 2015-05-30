using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     RequirementTypeRepository
    /// </summary>
    public class RequirementTypeRepository : GenericRepository<RequirementType>, IRequirementTypeRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RequirementTypeRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public RequirementTypeRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}