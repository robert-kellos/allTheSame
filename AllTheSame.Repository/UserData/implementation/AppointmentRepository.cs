using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     AppointmentRepository
    /// </summary>
    public class AppointmentRepository : SyncRepository<Appointment>, IAppointmentRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AppointmentRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AppointmentRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}