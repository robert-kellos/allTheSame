using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AllTheSame.Common.Core;
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model;
using AllTheSame.Service;
using AllTheSame.WebAPI.Modules;
using Autofac;
using Autofac.Integration.Mvc;

namespace AllTheSame.WebAPI
{
    /// <summary>
    ///     WebApiApplication
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        ///     Application_Start
        /// </summary>
        protected void Application_Start()
        {
            try
            {
                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);

                //Autofac Configuration
                var builder = new ContainerBuilder();

                builder.RegisterType(typeof (WebApiApplication)).PropertiesAutowired();

                builder.RegisterModule(new RepositoryModule());
                builder.RegisterModule(new ServiceModule());
                builder.RegisterModule(new EfModule());

                builder.Register(c => new AllTheSameDbContext());
                builder.Register(c => new ServiceProxy());
                builder.Register(c => new Repository<User>(new AllTheSameDbContext()));
                builder.Register(c => new Repository<Person>(new AllTheSameDbContext()));
                builder.Register(c => new Repository<Vendor>(new AllTheSameDbContext()));

                var container = builder.Build();

                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }
        }

        //capture any unhandled exceptions
        /// <summary>
        ///     Executes custom initialization code after all event handler modules have been added.
        /// </summary>
        public override void Init()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            base.Init();
        }

        /// <summary>
        ///     Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs" /> instance containing the event data.</param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e?.ExceptionObject != null)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.UnhandledExceptionMessage, e.ExceptionObject as Exception);
            }
            else
            {
                Audit.Log.Error(AppConstants.ErrorMessages.UnhandledExceptionMessage);
            }
        }
    }
}