﻿using DevDefined.OAuth.Consumer;
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

        public void InitializeOAuthSession()
        {
            this._baseService.InitializeOAuthSession();
        }

        public void ReinitializeOAuthSessionAccessToken(string token, string tokenSecret)
        {
            this._baseService.ReinitializeOAuthSessionAccessToken(token, tokenSecret);
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
