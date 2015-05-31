using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using Autofac;
using Module = Autofac.Module;

namespace AllTheSame.WebAPI.Modules
{
    /// <summary>
    ///     EFModule
    /// </summary>
    public class EfModule : Module
    {
        /// <summary>
        ///     Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">
        ///     The builder through which components can be
        ///     registered.
        /// </param>
        /// <remarks>
        ///     Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new RepositoryModule());

            builder.RegisterType(typeof (AllTheSameDbContext)).As(typeof (DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof (UnitOfWork)).As(typeof (IUnitOfWork)).InstancePerRequest();
        }
    }
}