using ConfluenceRestClient.Model;
using ConfluenceRESTClient.Service;
using System.Threading.Tasks;

namespace ConfluenceRestClient.Service
{


    public interface IContentService
    { 

        Task<ContentList> GetContentBySpaceKeyAsync(string spaceKey);

        Task<ContentList> GetAllContentAsync();

        void CreateContent(Content content);
    }
}
