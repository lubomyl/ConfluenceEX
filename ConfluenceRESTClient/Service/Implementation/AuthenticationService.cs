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
    public class AuthenticationService : BaseService, IAuthenticationService
    {

        public AuthenticationService() : base(){ }

        public AuthenticatedUser Authenticate(string username, string password)
        {
            AuthenticatedUser ret;
            this.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("user/current");

            ret = Get<AuthenticatedUser>(request);

            return ret;
        }

        public bool IsAuthenticated(AuthenticatedUser authenticatedUser)
        {
            bool ret = false;

            if(authenticatedUser != null)
            {
                ret = true;
            }

            return ret;
        }

    }
}
