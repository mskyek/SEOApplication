using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SEOApplication.App_Start.WindsorActivator), "PreStart")]
[assembly: ApplicationShutdownMethodAttribute(typeof(SEOApplication.App_Start.WindsorActivator), "Shutdown")]

namespace SEOApplication.App_Start
{
    public class WindsorActivator
    {
        static ContainerBootstrapper bootstrapper;

        public static void PreStart()
        {
            bootstrapper = ContainerBootstrapper.Bootstrap();
        }

        public static void Shutdown()
        {
            if (bootstrapper != null)
                bootstrapper.Dispose();
        }
    }
}