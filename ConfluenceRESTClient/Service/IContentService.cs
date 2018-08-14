using ConfluenceRestClient.Model;
using ConfluenceRESTClient.Service;

namespace ConfluenceRestClient.Service
{
    public interface IContentService
    {

        ContentList GetContentBySpaceKey(string spaceKey);

        ContentList GetAllContent();

        void CreateContent(Content content);
        
        //TODO GetContent by spaceKey query parameter
    }
}
