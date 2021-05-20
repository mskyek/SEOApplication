using SEOApplication.Application.Proxies.External;
using SEOApplication.Application.SEO;
using SEOApplication.Domain;
using SEOApplication.Domain.Enums;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SEOApplication.Controllers
{
    public class HomeController : Controller
    {
        private ISEOControllerProxy _seoControllerProxy;
        private Serilog.ILogger _logger => Serilog.Log.ForContext<HomeController>();

        public HomeController(ISEOControllerProxy seoControllerProxy)
        {
            this._seoControllerProxy = seoControllerProxy;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetSearchResults(string searchTerm, string resultNumber, SearchEngineType searchEngine, string searchURL)
        {
            using (ServerResponse _response = new ServerResponse())
            {
                try
                {
                    var searchResults = new SEOResponse();
                    searchResults = await _seoControllerProxy.GetSearchResults(searchTerm, resultNumber, searchEngine, searchURL);
                    _response.Result = true;
                    _response.Data = searchResults;
                }
                catch (Exception ex)
                {
                    _response.ErrorMessages = new Domain.Entities.Error { Message = ex.Message };
                    _logger.Error("Something went wrong getting search results for the following values: search terms - {searchTerm}, search url - {searchURL}, exception - {ex}", searchTerm, searchURL, ex);
                }
                return Json(_response, JsonRequestBehavior.AllowGet);
            }
        }
    }
}