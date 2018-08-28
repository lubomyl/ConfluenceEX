using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service
{

    //TODO implement error logging
    public class RestSharpBaseService : RestSharp.RestClient, IBaseService<Object>
    {

        private string _username = string.Empty;
        private string _password = string.Empty;

        private const string RestUrl = "https://lubomyl3.atlassian.net/wiki/rest/api";

        public RestSharpBaseService()
        {
            this.BaseUrl = new Uri(RestUrl);
        }

        public RestSharpBaseService(string username, string password)
        {
            this.BaseUrl = new Uri(RestUrl);
            this._username = username;
            this._password = password;

            this.Authenticator = new HttpBasicAuthenticator(_username, _password);
        }

        public T Get<T> (string resource) where T : new()
        {
            var request = new RestRequest(resource);

            var response = Execute<T>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return default(T);
            }
        }

        public Task<T> GetAsync<T>(IRestRequest request) where T : new()
        {
            var taskCompletionSource = new TaskCompletionSource<T>();

            ExecuteAsync<T>(request, (response, handle) => 
                taskCompletionSource.SetResult(response.Data));

            return taskCompletionSource.Task;
        }

        //Doesn't support RSA with separate certificate yet https://github.com/restsharp/RestSharp/issues/1088
        public Object GetRequestToken()
        {
            throw new NotImplementedException();
        }

        public string GetUserAuthorizationUrlForToken(Object requestToken)
        {
            throw new NotImplementedException();
        }

        public Object ExchangeRequestTokenForAccessToken(Object requestToken, string verificationCode)
        {
            throw new NotImplementedException();
        }

        #region BaseService Members

        public string Username
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        #endregion

    }
}
