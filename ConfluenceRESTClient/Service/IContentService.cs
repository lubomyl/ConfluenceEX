using ConfluenceRestClient.Model;
using ConfluenceRESTClient.Service;
using System.Threading.Tasks;

namespace ConfluenceRestClient.Service
{
    public interface IContentService
    {

        ContentList GetContentBySpaceKey(string spaceKey);

        Task<ContentList> GetContentBySpaceKeyAsync(string spaceKey);

        ContentList GetAllContent();

        void CreateContent(Content content);
    }
}
