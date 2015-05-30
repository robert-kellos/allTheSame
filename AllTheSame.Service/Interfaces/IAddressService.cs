using AllTheSame.Entity.Model;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;

namespace AllTheSame.Service.Interfaces
{
    /// <summary>
    ///     IAddressService
    /// </summary>
    public interface IAddressService : IEntityService<Address, IAddressRepository>
    {
    }
}