using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service.Implementation
{
    public class OAuthService : IOAuthService
    {
        private DevDefinedBaseService _baseService;

        public OAuthService()
        {
            this._baseService = DevDefinedBaseService.Instance;
        }

        public void CreateOAuthSession()
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

            this._baseService.Session = new OAuthSession(consumerContext, requestTokenUrl, userAuthorizeTokenUrl, accessTokenUrl);
        }

        public Task<IToken> GetRequestToken()
        {
            return Task.Run(() =>
            {
                IToken requestToken = this._baseService.Session.GetRequestToken("POST");
                return requestToken;
            });
        }

        public Task<string> GetUserAuthorizationUrlForToken(IToken requestToken)
        {
            return Task.Run(() => {
                string authorizationUrl = this._baseService.Session.GetUserAuthorizationUrlForToken(requestToken);
                return authorizationUrl;
            });
        }

        public Task<IToken> ExchangeRequestTokenForAccessToken(IToken requestToken, string oAuthVerificationCode)
        {
            return Task.Run(() =>
            {
                IToken accessToken = this._baseService.Session.ExchangeRequestTokenForAccessToken(requestToken, "POST", oAuthVerificationCode);
                return accessToken;
            });
        }
    }
}
