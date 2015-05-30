using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IAppointmentRepository
    /// </summary>
    public interface IAppointmentRepository : IGenericRepository<Appointment>, ISyncRepository<Appointment>
    {
    }
}