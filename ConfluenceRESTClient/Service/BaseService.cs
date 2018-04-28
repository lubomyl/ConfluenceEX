using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service
{

    //TODO implement error logging
    public class BaseService : RestSharp.RestClient
    {

        private const string USERNAME = "lubomyl@gmail.com";
        private const string API_KEY = "PJrGnoLMM92u0ywHF4kR1748";

        public BaseService(string baseUrl)
        {
            this.BaseUrl = new Uri(baseUrl);
            this.Authenticator = new HttpBasicAuthenticator(USERNAME, API_KEY);
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

    }
}
