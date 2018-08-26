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
    public class DevDefinedBaseService : IBaseService
    {
        private OAuthSession _session;

        private static DevDefinedBaseService _instance = null;

        private const string REST_URL = "https://lubomyl3.atlassian.net/wiki/rest/api/";

        private DevDefinedBaseService()
        {
        }

        public T Get<T>(string resource) where T : new()
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

        public static DevDefinedBaseService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DevDefinedBaseService();
                }

                return _instance;
            }
        }

        public OAuthSession Session
        {
            get
            {
                return this._session;
            }
            set
            {
                this._session = value;
            }
        }

    }
}
