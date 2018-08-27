using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service
{
    public interface IBaseService<T>
    {

        K Get<K>(string resource) where K : new();

        T GetRequestToken();

        string GetUserAuthorizationUrlForToken(T requestToken);

        T ExchangeRequestTokenForAccessToken(T requestToken, string verificationCode);
    }
}
