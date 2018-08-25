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
    public class ContentService : IContentService
    {

        private BaseService2 _baseService;

        public ContentService()
        {
            _baseService = BaseService2.Instance;
        }

        public void CreateContent(Content content)
        {
            throw new NotImplementedException();
        }

        public Task<ContentList> GetAllContentAsync()
        {
            return Task.Run(() => {
                var resource = "content";

                return this._baseService.Get2<ContentList>(resource);
            });
        }

        public Task<ContentList> GetContentBySpaceKeyAsync(string spaceKey)
        {
            return Task.Run(() => {
                var resource = $"content?spaceKey={spaceKey}";

                return this._baseService.Get2<ContentList>(resource);
            });
        }

    }
}
