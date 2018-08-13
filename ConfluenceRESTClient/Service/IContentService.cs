using ConfluenceRestClient.Model;
using ConfluenceRESTClient.Service;

namespace ConfluenceRestClient.Service
{
    public interface IContentService
    {

        Content GetContentById(int id);

        ContentList GetAllContent();

        void CreateContent(Content content);
        
        //TODO GetContent by spaceKey query parameter
    }
}
