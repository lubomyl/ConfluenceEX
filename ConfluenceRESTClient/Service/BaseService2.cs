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
            X509Certificate2 certificate = new X509Certificate2(@"C:\cygwin64\home\RODINA-PC\mycer.pfx", "confluenceex");

            string requestTokenUrl = "https://lubomyl3.atlassian.net/wiki/plugins/servlet/oauth/request-token";
            string userAuthorizeTokenUrl = "https://lubomyl3.atlassian.net/wiki/plugins/servlet/oauth/authorize";
            string accessTokenUrl = "https://lubomyl3.atlassian.net/wiki/plugins/servlet/oauth/access-token";

            var consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = "OauthKey",
                ConsumerSecret = "MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAOWOLtvZOhMegCEjt04ZbMJsnb79VKmxK7KbdYSVJYGitQQ6ZCLoEQz70BAl1WW/VFIM7dH5YJ+ghbeEE8eo6Hr02yKzs44h0AFu1yFvGxU7JvXohJ+UArms9L/OGQFTcX16F2h75T7+v8XIfHVxJ3LcMJgvAbmLv1zMpBhkiRYxAgMBAAECgYEAgms/cCSAfDBN94YFNNf5FJUFImdnXGmOPBFauRLLllVMprROBA748Pl4AlScYwxK6bryuuMF5GsczWC6pCrwuScjbjh68UGDlbjwxdzY/FZnreJ5CMjVYsod5T/fPj4pR312KNev2JGb06Tqqyqk5a/xeKlLDXF2b/pC6WNidAECQQD5ybfTLS8YRrsB30tq5w+eUJjoEsQBMWDrQqSItUq5NrWfMJMdFXkL+gVxskdja/feDxbA4aFNfx2Bu2/baACRAkEA60OnYapDSOwGfoZKHh4mXxUCoUTGEWu+fS8lxz/WIyZ8nnSMQg3GL5N7t5D2QeAONCnxyIaMIcmYRQt6W7KLoQJAdKH8xI0/hT1ZiplB9MupHkoR66L/hHMTBybZ/r9wAaWLDqa2uojROYdnzVvqO4EpfrVa8XspPy9Qjsf/hdo9EQJBAJgTDFBSWdn6T5xbu+9J9+3ATAnoxcufBOwwnM/2ELp591YJ6lwMQU6hm/glqTkd1rCbaGIBrvVsNZRz/ezmNeECQBXDncxQ1YCnWIfb8sy9H74hESrhtE47LlKpsLfsJOxE7bd/CeNCqB/IjI1C7eO+PQGcmb+2eKCH8cwvpHitxRo=",
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
