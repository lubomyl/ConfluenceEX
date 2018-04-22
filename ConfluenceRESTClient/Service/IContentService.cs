using ConfluenceRestClient.Model;

namespace ConfluenceRestClient.Service
{
    public interface IContentService
    {

        Content GetContentById(int id);

        ContentResults GetAllContent();

        void CreateContent(Content content);

    }
}
