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

        public ContentService() : base() { }

        public void CreateContent(Content content)
        {
            throw new NotImplementedException();
        }

        public ContentList GetAllContent()
        {
            ContentList ret = new ContentList();
            var request = new RestRequest("content");

            ret = Get<ContentList>(request);

            return ret;
        }

        public ContentList GetContentBySpaceKey(string spaceKey)
        {
            ContentList ret = new ContentList();
            var request = new RestRequest("content?spaceKey={spaceKey}");
            request.AddUrlSegment("spaceKey", spaceKey);

            ret = Get<ContentList>(request);

            return ret;
        }

        public Task<ContentList> GetContentBySpaceKeyAsync(string spaceKey)
        {
            var request = new RestRequest("content?spaceKey={spaceKey}");
            request.AddUrlSegment("spaceKey", spaceKey);

            return GetAsync<ContentList>(request);
        }

    }
}
