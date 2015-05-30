using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     IndustryRepository
    /// </summary>
    public class IndustryRepository : GenericRepository<Industry>, IIndustryRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IndustryRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public IndustryRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}