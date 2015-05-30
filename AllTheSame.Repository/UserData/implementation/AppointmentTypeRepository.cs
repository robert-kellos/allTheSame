using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     AppointmentTypeRepository
    /// </summary>
    public class AppointmentTypeRepository : GenericRepository<AppointmentType>, IAppointmentTypeRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AppointmentTypeRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public AppointmentTypeRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}