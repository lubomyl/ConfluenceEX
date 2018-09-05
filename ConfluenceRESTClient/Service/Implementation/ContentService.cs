using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfluenceRestClient.Model;
using RestSharp;
using ConfluenceRESTClient.Service;
using DevDefined.OAuth.Framework;
using AtlassianConnector.Service;
using AtlassianConnector.Base.Implementation.DevDefined;

namespace ConfluenceRestClient.Service.Implementation
{


    /// <summary>
    /// Concrete implementation of IContentService utilizing <see cref="BaseService"/> as <see cref="IBaseService{T}"/>.
    /// <see cref="IContentService"/>
    /// </summary>
    public class ContentService : IContentService
    {

        private IBaseService<IToken> _baseService;

        public ContentService()
        {
            _baseService = BaseService.ConfluenceInstance;
        }

        /// <summary>
        /// <see cref="IContentService.CreateContent(Content)"/>
        /// </summary>
        public void CreateContent(Content content)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IContentService.GetAllContentAsync"/>
        /// </summary>
        public Task<ContentList> GetAllContentAsync()
        {
            return Task.Run(() => {
                var resource = "content";

                return this._baseService.Get<ContentList>(resource);
            });
        }

        /// <summary>
        /// <see cref="IContentService.GetAllContentBySpaceKeyAsync(string)"/>
        /// </summary>
        public Task<ContentList> GetAllContentBySpaceKeyAsync(string spaceKey)
        {
            return Task.Run(() => {
                var resource = $"content?spaceKey={spaceKey}";

                return this._baseService.Get<ContentList>(resource);
            });
        }

        /// <summary>
        /// <see cref="IContentService.GetContentByIdAsync(int)"/>
        /// </summary>
        public Task<Content> GetContentByIdAsync(int contentId)
        {
            return Task.Run(() => {
                var resource = $"content/{contentId}";

                return this._baseService.Get<Content>(resource);
            });
        }
    }
}
