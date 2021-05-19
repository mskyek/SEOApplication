using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SEOApplication.Application.Proxies.External;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using SEOApplication.Infrastructure.Proxies.External;
using Moq.Protected;
using System.Threading;
using SEOApplication.Domain.Enums;

namespace SEOApplication.Tests.Infrastructure.SEO.Proxies.External
{
    [TestClass]
    public class SEOControllerProxyTests
    {
        private Mock<ISEOControllerProxy> _seoControllerProxy;

        [TestInitialize]
        public void Initialize()
        {
            _seoControllerProxy = new Mock<ISEOControllerProxy>();
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Test_Unit_GetSEOResult_Match_Google()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("<div class=\"ZINbbc xpd O9g5cc uUPGi\"=\"www.infotrack.com\" /><div class=\"ZINbbc xpd O9g5cc uUPGi\"=\"www.infotrack.com\" />"),
                }));

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://www.google.com/search?num=100&q=efiling+integration")
            };

            var sut = new SEOControllerProxy(httpClient);
            var task = sut.GetSearchResults("efiling integration", "100", SearchEngineType.Google, "www.infotrack.com");

            Assert.IsNotNull(task);
            Assert.IsNotNull(task.Result);
            Assert.AreEqual(2, task.Result.Result.Count);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Test_Unit_GetSEOResult_Match_Bing()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("<li class=\"b_algo\"=\"www.infotrack.com\" /><li class=\"b_algo\"=\"www.infotrack.com\" />"),
                }));

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://www.google.com/search?num=100&q=efiling+integration")
            };

            var sut = new SEOControllerProxy(httpClient);
            var task = sut.GetSearchResults("efiling integration", "100", SearchEngineType.Bing, "www.infotrack.com");

            Assert.IsNotNull(task);
            Assert.IsNotNull(task.Result);
            Assert.AreEqual(2, task.Result.Result.Count);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Test_Unit_GetSEOResult_NoMatch()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("<div class=\"ZINbbc xpd O9g5cc uUPGi\"=\"www.infotracks.com\" /><div class=\"ZINbbc xpd O9g5cc uUPGi\"=\"www.infotracks.com\" />"),
                }));

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://www.google.com/search?num=100&q=efiling+integration")
            };

            var sut = new SEOControllerProxy(httpClient);
            var task = sut.GetSearchResults("efiling integration", "100", SearchEngineType.Google, "www.infotrack.com");

            Assert.IsNotNull(task);
            Assert.IsNotNull(task.Result);
            Assert.AreEqual(0, task.Result.Result.Count);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Test_Unit_GetSEOResult_MissingWWW()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("<div class=\"ZINbbc xpd O9g5cc uUPGi\"=\"www.infotrack.com\" /><div class=\"ZINbbc xpd O9g5cc uUPGi\"=\"www.infotrack.com\" />"),
                }));

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://www.google.com/search?num=100&q=efiling+integration")
            };

            var sut = new SEOControllerProxy(httpClient);
            var task = sut.GetSearchResults("efiling integration", "100", SearchEngineType.Google, "infotrack.com");

            Assert.IsNotNull(task);
            Assert.IsNotNull(task.Result);
            Assert.AreEqual(2, task.Result.Result.Count);
        }
    }
}
