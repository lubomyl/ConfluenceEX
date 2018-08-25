﻿using ConfluenceRESTClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service.Implementation
{
    public class BasicAuthenticationService : BaseService, IAuthenticationService
    {

        public BasicAuthenticationService(string username, string password) : base(username, password){ }

        public AuthenticatedUser Authenticate()
        {
            AuthenticatedUser ret;

            var resource = "user/current";

            ret = Get<AuthenticatedUser>(resource);

            return ret;
        }

        public bool IsAuthenticated(AuthenticatedUser authenticatedUser)
        {
            bool ret = false;

            if(authenticatedUser != null)
            {
                ret = true;
            }

            return ret;
        }

        public string Username
        {
            set
            {
                base.Username = value;
            }
        }

        public string Password
        {
            set
            {
                base.Password = value;
            }
        }

    }
}
