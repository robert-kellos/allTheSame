using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     FamilyMemberRepository
    /// </summary>
    public class FamilyMemberRepository : SyncRepository<FamilyMember>, IFamilyMemberRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FamilyMemberRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public FamilyMemberRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}