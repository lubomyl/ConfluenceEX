﻿using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service
{

    //TODO implement error logging
    public class BaseService : RestSharp.RestClient
    {

        private string _username = string.Empty;
        private string _password = string.Empty;

        private const string RestUrl = "https://lubomyl3.atlassian.net/wiki/rest/api";

        public BaseService(string username, string password)
        {

        }

        public BaseService()
        {
            this.BaseUrl = new Uri(RestUrl);
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

            var session = new OAuthSession(consumerContext, requestTokenUrl, userAuthorizeUrl, accessUrl);
            IToken requestToken = session.GetRequestToken("POST");

            string authorisationUrl = session.GetUserAuthorizationUrlForToken(requestToken);

            //TODO oauth_verification need to be added on break after redirecting user to token authentication
            IToken accessToken = session.ExchangeRequestTokenForAccessToken(requestToken, "POST", "5BDeJN");

            this.Authenticator = OAuth1Authenticator.ForProtectedResource(consumerKey, consumerSecret, accessToken.Token,
                accessToken.TokenSecret);
        }

        public T Get<T> (IRestRequest request) where T : new()
        {
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
