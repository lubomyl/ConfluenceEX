using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ConfluenceRestClient.Model;
using Newtonsoft.Json;

namespace ConfluenceRESTClient.Service
{

    //TODO implement error logging
    public class BaseService
    {
        private static BaseService _instance = null;

        private string _username = string.Empty;
        private string _password = string.Empty;

        private OAuthSession _session;

        private const string REST_URL = "https://lubomyl3.atlassian.net/wiki/rest/api/";

        public BaseService(string username, string password)
        {

        }

        private BaseService() { }

        public static BaseService Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new BaseService();
                }

                return _instance;
            }
        }

        public void ProcessOauthDance()
        {
            string consumerKey = Properties.Settings.Default.consumerKey;
            string consumerSecret = Properties.Settings.Default.consumerSecret;

            X509Certificate2 certificate = new X509Certificate2(Properties.Settings.Default.cerificatePath, Properties.Settings.Default.certificateSecret);

            string requestTokenUrl = "https://lubomyl3.atlassian.net/wiki/plugins/servlet/oauth/request-token";
            string userAuthorizeUrl = "https://lubomyl3.atlassian.net/wiki/plugins/servlet/oauth/authorize";
            string accessUrl = "https://lubomyl3.atlassian.net/wiki/plugins/servlet/oauth/access-token";

            var consumerContext = new OAuthConsumerContext
            {
                SignatureMethod = SignatureMethod.RsaSha1,
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                UseHeaderForOAuthParameters = true,
                Key = certificate.PrivateKey
            };

            this._session = new OAuthSession(consumerContext, requestTokenUrl, userAuthorizeUrl, accessUrl);
            IToken requestToken = _session.GetRequestToken("POST");

            string authorisationUrl = _session.GetUserAuthorizationUrlForToken(requestToken);

            //TODO oauth_verification need to be added on break after redirecting user to token authentication
            IToken accessToken = _session.ExchangeRequestTokenForAccessToken(requestToken, "POST", "APNRp8");
        }

        public T Get<T> (string resource) where T : new()
        {
            var response = _session.Request().Get().ForUrl(REST_URL + resource).ReadBody();

            if (response != null)
            {
                return JsonConvert.DeserializeObject<T>(response);
            }
            else
            {
                return default(T);
            }
        }

        /*public Task<T> GetAsync<T>(IRestRequest request) where T : new()
        {
            var taskCompletionSource = new TaskCompletionSource<T>();

            ExecuteAsync<T>(request, (response, handle) => 
                taskCompletionSource.SetResult(response.Data));

            return taskCompletionSource.Task;
        }*/

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
