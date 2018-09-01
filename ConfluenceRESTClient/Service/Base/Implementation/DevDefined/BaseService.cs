﻿using ConfluenceRESTClient.Service;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service.DevDefined
{


    /// <summary>
    /// DevDefined.OAuth library concrete implementation of IBaseService.
    /// Singleton class pattern used to not to reinitialize OAuth session on every reference of this class.
    /// </summary>
    public class BaseService : IBaseService<IToken>
    {
        private OAuthSession _session;

        private static BaseService _instance = null;

        private const string REST_URL = "https://lubomyl3.atlassian.net/wiki/rest/api/";

        private BaseService()
        {
        }


        /// <summary>
        /// Initializes OAtuh session object with parameters needed like requestTokenUrl, userAuthorieUrl, accessTokenUrl, consumerKey, privateKey, signatureMethod or consumerSecret 
        /// </summary>
        public void InitializeOAuthSession()
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
        }


        /// <summary>
        /// Reinitializes OAuth session object with provided access token properties. Used on restart of application. Important for remember me like function.
        /// </summary>
        /// <param name="token">Access token string.</param>
        /// <param name="tokenSecret">Access token secret string.</param>
        public void ReinitializeOAuthSessionAccessToken(string token, string tokenSecret)
        {
            this.InitializeOAuthSession();

            IToken accessToken = new TokenBase();
            accessToken.Token = token;
            accessToken.TokenSecret = tokenSecret;

            this._session.AccessToken = accessToken;
        }


        /// <summary>
        /// <see cref="IBaseService{T}.Get{K}(string)"/>
        /// </summary>
        public K Get<K>(string resource) where K : new()
        {
            var response = this._session.Request().Get().ForUrl(REST_URL + resource).ReadBody();

                if (response != null)
                {
                    return JsonConvert.DeserializeObject<K>(response);
                }
                else
                {
                    return default(K);
                }
        }

        /// <summary>
        /// <see cref="IBaseService{T}.GetRequestToken"/>
        /// </summary>
        public IToken GetRequestToken()
        {
            IToken ret = this._session.GetRequestToken("POST");

            return ret;
        }

        /// <summary>
        /// <see cref="IBaseService{T}.GetUserAuthorizationUrlForToken(T)"/>
        /// </summary>
        public string GetUserAuthorizationUrlForToken(IToken requestToken)
        {
            string ret = this._session.GetUserAuthorizationUrlForToken(requestToken);

            return ret;
        }

        /// <summary>
        /// <see cref="IBaseService{T}.ExchangeRequestTokenForAccessToken(T, string)"/>
        /// </summary>
        public IToken ExchangeRequestTokenForAccessToken(IToken requestToken, string verificationCode)
        {

            IToken ret = this._session.ExchangeRequestTokenForAccessToken(requestToken, "POST", verificationCode);

            return ret;
        }

        public static BaseService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BaseService();
                }

                return _instance;
            }
        }
    }
}