using ConfluenceRESTClient.Model;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service.Implementation
{
    public class UserService : IUserService
    {

        private BaseService2 _baseService;

        public UserService()
        {
            _baseService = BaseService2.Instance;
        }

        public Task<AuthenticatedUser> GetAuthenticatedUserAsync()
        {
            return Task.Run(() => {
                var resource = "user/current";

                return this._baseService.Get2<AuthenticatedUser>(resource);
            });
        }

    }
}
