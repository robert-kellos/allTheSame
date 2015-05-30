using AllTheSame.Entity.Model;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;

namespace AllTheSame.Service.Interfaces
{
    /// <summary>
    /// </summary>
    public interface ICommunityWorker_AlertService :
        IEntityService<CommunityWorker_Alert, ICommunityWorker_AlertRepository>
    {
    }
}