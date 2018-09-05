using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfluenceRestClient.Model;
using RestSharp;
using DevDefined.OAuth.Framework;
using AtlassianConnector.Service;
using AtlassianConnector.Base.Implementation.DevDefined;

namespace ConfluenceRESTClient.Service.Implementation
{

    /// <summary>
    /// Concrete implementation of ISpaceService utilizing <see cref="BaseService"/> as <see cref="IBaseService{T}"/>.
    /// <see cref="ISpaceService"/>
    /// </summary>
    public class SpaceService : ISpaceService
    {
        private IBaseService<IToken> _baseService;

        public SpaceService()
        {
            this._baseService = BaseService.ConfluenceInstance;
        }

        /// <summary>
        /// <see cref="ISpaceService.CreateSpaceAsync(Space)"/>
        /// </summary>
        public void CreateSpaceAsync(Space space)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="ISpaceService.GetAllSpacesAsync"/>
        /// </summary>
        public Task<SpaceList> GetAllSpacesAsync()
        {
            return Task.Run(() => {
                var resource = "space";

                return this._baseService.Get<SpaceList>(resource);
            });
        }

        /// <summary>
        /// <see cref="ISpaceService.GetSpaceByNameAsync(string)"/>
        /// </summary>
        public Task<Space> GetSpaceByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

    }
}
