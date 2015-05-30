using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace AllTheSame.WebAPI.Modules
{
    /// <summary>
    ///     ServiceModule
    /// </summary>
    public class ServiceModule : Module
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
            builder.RegisterAssemblyTypes(Assembly.Load("AllTheSame.Service"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                //.SingleInstance();
                //.AutoActivate();
                .InstancePerLifetimeScope();
        }
    }
}