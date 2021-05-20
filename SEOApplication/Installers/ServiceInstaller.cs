using Castle.MicroKernel.Registration;
using SEOApplication.Application.Proxies.External;
using SEOApplication.Infrastructure.Proxies.External;
using System.Net.Http;

namespace SEOApplication.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component
                .For<ISEOControllerProxy>()
                .ImplementedBy<SEOControllerProxy>()
                .LifestyleSingleton());

            container.Register(
                Component
                .For<HttpClient>()
                .ImplementedBy<HttpClient>()
                .LifestyleSingleton());
        }
    }
}