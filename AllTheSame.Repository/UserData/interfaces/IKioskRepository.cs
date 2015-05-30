using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IKioskRepository
    /// </summary>
    public interface IKioskRepository : IGenericRepository<Kiosk>, ISyncRepository<Kiosk>
    {
    }
}