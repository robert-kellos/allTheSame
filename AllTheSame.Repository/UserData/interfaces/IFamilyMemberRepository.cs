using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IFamilyMemberRepository
    /// </summary>
    public interface IFamilyMemberRepository : IGenericRepository<FamilyMember>, ISyncRepository<FamilyMember>
    {
    }
}