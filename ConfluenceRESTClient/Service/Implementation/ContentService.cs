using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfluenceRestClient.Model;
using ConfluenceRestClient.Helper;
using RestSharp;

namespace ConfluenceRestClient.Service.Implementation
{
    public class ContentService : IContentService
    {

        private const string ENDPOINT_URL = "https://lubomyl1.atlassian.net/wiki/rest/api/content/";

        public void CreateContent(Content content)
        {
            throw new NotImplementedException();
        }

        public ContentResults GetAllContent()
        {
            ContentResults ret = new ContentResults();
            var request = new RestRequest();

            ret = GetOperation.Execute<ContentResults>(request, ENDPOINT_URL);

            return ret;
        }

        public Content GetContentById(int id)
        {
            Content ret;
            var request = new RestRequest();

            request.Resource = string.Format("/{0}", id);

            ret = GetOperation.Execute<Content>(request, ENDPOINT_URL);

            return ret;

        }

    }
}
