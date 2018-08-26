using DevDefined.OAuth.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service
{
    public interface IOAuthService
    {

        void CreateOAuthSession();

        //Step 1 - Get Request token to be able to generate authorization url for user
        Task<IToken> GetRequestToken();

        //Step 2 - Get authorization url by provided requestToken - used to redirect user on authorization page with oauth_verification code
        Task<string> GetUserAuthorizationUrlForToken(IToken requestToken);

        //Step 3 - Get Access token by provided requestToken and oauth_verification code - used to access resources on api
        Task<IToken> ExchangeRequestTokenForAccessToken(IToken requestToken, string oAuthVerificationCode);

    }
}
