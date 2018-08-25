using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfluenceRestClient.Model;
using ConfluenceRESTClient.Service;

namespace ConfluenceRestClient.Service.Implementation
{
    public class ContentService : IContentService
    {
        private BaseService _baseService;

        public ContentService()
        {
            this._baseService = BaseService.Instance;
        }

        public void CreateContent(Content content)
        {
            throw new NotImplementedException();
        }

        public ContentList GetAllContent()
        {
            ContentList ret = new ContentList();
            var resource = "content";

            ret = _baseService.Get<ContentList>(resource);

            return ret;
        }

        public ContentList GetContentBySpaceKey(string spaceKey)
        {
            ContentList ret = new ContentList();

            var resource = $"content?spaceKey={spaceKey}";
            
            ret = _baseService.Get<ContentList>(resource);

            return ret;
        }

        /*public Task<ContentList> GetContentBySpaceKeyAsync(string spaceKey)
        {
            var request = new RestRequest("content?spaceKey={spaceKey}");
            request.AddUrlSegment("spaceKey", spaceKey);

            return _baseService.GetAsync<ContentList>(request);
        }*/

    }
}
