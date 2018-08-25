using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service
{
    public class BaseService2
    {
        private OAuthSession _session;

        private static BaseService2 _instance = null;

        private const string REST_URL = "https://lubomyl3.atlassian.net/wiki/rest/api/";

        private BaseService2()
        {

        }

        public void ProcessOauthDance()
        {
            X509Certificate2 certificate = new X509Certificate2(Properties.Settings.Default.CertificatePath, Properties.Settings.Default.CertificateSecret);

            string requestTokenUrl = "https://lubomyl3.atlassian.net/wiki/plugins/servlet/oauth/request-token";
            string userAuthorizeTokenUrl = "https://lubomyl3.atlassian.net/wiki/plugins/servlet/oauth/authorize";
            string accessTokenUrl = "https://lubomyl3.atlassian.net/wiki/plugins/servlet/oauth/access-token";

            var consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = Properties.Settings.Default.ConsumerKey,
                ConsumerSecret = Properties.Settings.Default.ConsumerSecret,
                SignatureMethod = SignatureMethod.RsaSha1,
                Key = certificate.PrivateKey,
                UseHeaderForOAuthParameters = true
            };

            this._session = new OAuthSession(consumerContext, requestTokenUrl, userAuthorizeTokenUrl, accessTokenUrl);

            IToken requestToken = this._session.GetRequestToken("POST");

            string authorisationUrl = this._session.GetUserAuthorizationUrlForToken(requestToken);

            //TODO oauth_verification need to be added on break after redirecting user to token authentication
            IToken accessToken = this._session.ExchangeRequestTokenForAccessToken(requestToken, "POST", "FuSXUl");
        }

        public T Get2<T>(string resource) where T : new()
        {
            var response = this._session.Request().Get().ForUrl(REST_URL + resource).ReadBody();

                if (response != null)
                {
                    return JsonConvert.DeserializeObject<T>(response);
                }
                else
                {
                    return default(T);
                }
        }

        public static BaseService2 Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BaseService2();
                }

                return _instance;
            }
        }

    }
}
