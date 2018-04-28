using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfluenceRestClient.Model;
using RestSharp;
using ConfluenceRESTClient.Service;

namespace ConfluenceRestClient.Service.Implementation
{
    public class ContentService : BaseService, IContentService
    {

        private const string RestUrl = "https://lubomyl1.atlassian.net/wiki/rest/api/";

        public ContentService() : base(RestUrl) { }

        public void CreateContent(Content content)
        {
            throw new NotImplementedException();
        }

        public ContentResults GetAllContent()
        {
            ContentResults ret = new ContentResults();
            var request = new RestRequest("/content");

            ret = Get<ContentResults>(request);

            return ret;
        }

        public Content GetContentById(int id)
        {
            Content ret;
            var request = new RestRequest("content/{id}");
            request.AddUrlSegment("id", id.ToString());

            ret = Get<Content>(request);

            return ret;

        }

    }
}
