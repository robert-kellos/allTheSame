using AllTheSame.Entity.Model;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;

namespace AllTheSame.Service.Interfaces
{
    /// <summary>
    /// </summary>
    public interface IUser_Org_PermissionService : IEntityService<User_Org_Permission, IUser_Org_PermissionRepository>
    {
    }
}