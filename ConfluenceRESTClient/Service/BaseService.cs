﻿using RestSharp;
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

        private string _username = string.Empty;
        private string _password = string.Empty;

        private const string RestUrl = "https://lubomyl1.atlassian.net/wiki/rest/api";

        public BaseService()
        {
            this.BaseUrl = new Uri(RestUrl);
        }

        public BaseService(string username, string password)
        {
            this.BaseUrl = new Uri(RestUrl);
            this._username = username;
            this._password = password;

            this.Authenticator = new HttpBasicAuthenticator(_username, _password);
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