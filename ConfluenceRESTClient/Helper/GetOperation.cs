using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace ConfluenceRestClient.Helper
{
    public static class GetOperation
    {

        private const string USERNAME = "lubomyl@gmail.com";
        private const string API_KEY = "PJrGnoLMM92u0ywHF4kR1748";

        public static T Execute<T>(RestRequest request, string endpointUrl) where T : class, new()
        {
            IRestClient restClient = new RestClient(endpointUrl)
            {
                Authenticator = new HttpBasicAuthenticator(USERNAME, API_KEY)
            };

            request.Method = Method.GET;
            request.AddHeader("Content-Type", "application/json");

            var response = restClient.Execute<T>(request);

            return response.Data;
        }

    }
}
