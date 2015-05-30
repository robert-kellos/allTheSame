using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     VisitRepository
    /// </summary>
    public class VisitRepository : GenericRepository<Visit>, IVisitRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VisitRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public VisitRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}