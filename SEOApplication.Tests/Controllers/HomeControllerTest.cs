using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SEOApplication.Application.Proxies.External;
using SEOApplication.Controllers;

namespace SEOApplication.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<ISEOControllerProxy> _seoControllerProxy;

        [TestInitialize]
        public void Initialize()
        {
            _seoControllerProxy = new Mock<ISEOControllerProxy>();
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_seoControllerProxy.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
