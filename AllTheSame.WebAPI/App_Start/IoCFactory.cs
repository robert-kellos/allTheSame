using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Activators.ProvidedInstance;
using Autofac.Core.Activators.Reflection;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;

namespace Accushield.WebAPI.App_Start
{
    /// <summary>
    /// Factory
    /// </summary>
    public static class IoCFactory
    {
        /// <summary>
        /// Creates the singleton registration.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="activator">The activator.</param>
        /// <returns></returns>
        public static IComponentRegistration CreateSingletonRegistration(IEnumerable<Autofac.Core.Service> services, IInstanceActivator activator)
        {
            return CreateRegistration(services, activator, new RootScopeLifetime(), InstanceSharing.Shared);
        }

        /// <summary>
        /// Creates the singleton registration.
        /// </summary>
        /// <param name="implementation">The implementation.</param>
        /// <returns></returns>
        public static IComponentRegistration CreateSingletonRegistration(Type implementation)
        {
            return CreateSingletonRegistration(
                new Autofac.Core.Service[] { new TypedService(implementation) },
                CreateReflectionActivator(implementation));
        }

        /// <summary>
        /// Creates the registration.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="activator">The activator.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="sharing">The sharing.</param>
        /// <returns></returns>
        public static IComponentRegistration CreateRegistration(IEnumerable<Autofac.Core.Service> services, IInstanceActivator activator, IComponentLifetime lifetime, InstanceSharing sharing)
        {
            return new ComponentRegistration(
                Guid.NewGuid(),
                activator,
                lifetime,
                sharing,
                InstanceOwnership.OwnedByLifetimeScope,
                services,
                NoMetadata);
        }

        /// <summary>
        /// Creates the singleton object registration.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IComponentRegistration CreateSingletonObjectRegistration(object instance)
        {
            return RegistrationBuilder
                .ForDelegate((c, p) => instance)
                .SingleInstance()
                .CreateRegistration();
        }

        /// <summary>
        /// Creates the singleton object registration.
        /// </summary>
        /// <returns></returns>
        public static IComponentRegistration CreateSingletonObjectRegistration()
        {
            return CreateSingletonRegistration(
                new Autofac.Core.Service[] { new TypedService(typeof(object)) },
                CreateReflectionActivator(typeof(object)));
        }

        /// <summary>
        /// Creates the reflection activator.
        /// </summary>
        /// <param name="implementation">The implementation.</param>
        /// <returns></returns>
        public static ReflectionActivator CreateReflectionActivator(Type implementation)
        {
            return CreateReflectionActivator(
                implementation,
                NoParameters);
        }

        /// <summary>
        /// Creates the reflection activator.
        /// </summary>
        /// <param name="implementation">The implementation.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static ReflectionActivator CreateReflectionActivator(Type implementation, IEnumerable<Parameter> parameters)
        {
            return CreateReflectionActivator(
                implementation,
                parameters,
                NoProperties);
        }

        /// <summary>
        /// Creates the reflection activator.
        /// </summary>
        /// <param name="implementation">The implementation.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
        public static ReflectionActivator CreateReflectionActivator(Type implementation, IEnumerable<Parameter> parameters, IEnumerable<Parameter> properties)
        {
            return new ReflectionActivator(
                implementation,
                new DefaultConstructorFinder(),
                new MostParametersConstructorSelector(),
                parameters,
                properties);
        }

        /// <summary>
        /// Creates the provided instance activator.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ProvidedInstanceActivator CreateProvidedInstanceActivator(object instance)
        {
            return new ProvidedInstanceActivator(instance);
        }

        /// <summary>
        /// The empty container
        /// </summary>
        public static readonly IContainer EmptyContainer = new Autofac.ContainerBuilder().Build();
        /// <summary>
        /// The empty context
        /// </summary>
        public static readonly IComponentContext EmptyContext = new Autofac.ContainerBuilder().Build();
        /// <summary>
        /// The no parameters
        /// </summary>
        public static readonly IEnumerable<Parameter> NoParameters = Enumerable.Empty<Parameter>();
        /// <summary>
        /// The no properties
        /// </summary>
        public static readonly IEnumerable<Parameter> NoProperties = Enumerable.Empty<Parameter>();
        /// <summary>
        /// The no metadata
        /// </summary>
        public static readonly IDictionary<string, object> NoMetadata = new Dictionary<string, object>();
    }
}