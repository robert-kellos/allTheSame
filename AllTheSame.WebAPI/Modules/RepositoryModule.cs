using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace AllTheSame.WebAPI.Modules
{
    /// <summary>
    ///     RepositoryModule
    /// </summary>
    public class RepositoryModule : Module
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
            builder.RegisterAssemblyTypes(Assembly.Load("AllTheSame.Repository"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                //.SingleInstance();
                //.AutoActivate();
                .InstancePerLifetimeScope();
        }
    }
}