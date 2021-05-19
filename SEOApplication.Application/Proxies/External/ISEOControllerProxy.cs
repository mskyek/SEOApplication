using SEOApplication.Application.SEO;
using SEOApplication.Domain.Enums;
using System.Threading.Tasks;

namespace SEOApplication.Application.Proxies.External
{
    public interface ISEOControllerProxy
    {
        Task<SEOResponse> GetSearchResults(string searchTerm, string resultNumber, SearchEngineType searchEngine, string searchURL);
    }
 }
