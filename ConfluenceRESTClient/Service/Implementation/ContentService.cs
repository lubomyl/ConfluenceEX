using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfluenceRestClient.Model;
using RestSharp;
using ConfluenceRESTClient.Service;
using DevDefined.OAuth.Framework;

namespace ConfluenceRestClient.Service.Implementation
{
    public class ContentService : IContentService
    {

        private IBaseService<IToken> _baseService;

        public ContentService()
        {
            _baseService = DevDefinedBaseService.Instance;
        }

        public void CreateContent(Content content)
        {
            throw new NotImplementedException();
        }

        public Task<ContentList> GetAllContentAsync()
        {
            return Task.Run(() => {
                var resource = "content";

                return this._baseService.Get<ContentList>(resource);
            });
        }

        public Task<ContentList> GetContentBySpaceKeyAsync(string spaceKey)
        {
            return Task.Run(() => {
                var resource = $"content?spaceKey={spaceKey}";

                return this._baseService.Get<ContentList>(resource);
            });
        }

    }
}
