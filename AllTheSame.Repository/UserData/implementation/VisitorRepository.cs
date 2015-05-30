using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     VisitorRepository
    /// </summary>
    public class VisitorRepository : GenericRepository<Visitor>, IVisitorRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VisitorRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public VisitorRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}