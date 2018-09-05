using AtlassianConnector.Base.Implementation.DevDefined;
using AtlassianConnector.Service;
using ConfluenceRESTClient.Model;
using DevDefined.OAuth.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service.Implementation
{

    /// <summary>
    /// Concrete implementation of IUserService utilizing <see cref="BaseService"/> as <see cref="IBaseService{T}"/>.
    /// <see cref="IUserService"/>
    /// </summary>
    public class UserService : IUserService
    {

        private IBaseService<IToken> _baseService;

        public UserService()
        {
            _baseService = BaseService.ConfluenceInstance;
        }

        /// <summary>
        /// <see cref="IUserService.GetAuthenticatedUserAsync"/>
        /// </summary>
        public Task<User> GetAuthenticatedUserAsync()
        {
            return Task.Run(() => {
                var resource = "user/current";

                return this._baseService.Get<User>(resource);
            });
        }

        public bool IsAuthenticated(User authenticatedUser)
        {
            bool ret = false;

            if (authenticatedUser != null)
            {
                ret = true;
            }

            return ret;
        }

    }
}
