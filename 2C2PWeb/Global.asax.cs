using _2C2P.Common;
using _2C2P.DAL.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

namespace _2C2PWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<ILogger, Logger>();
            container.RegisterType<IDalClient, DalClient>();
        }
    }
}
