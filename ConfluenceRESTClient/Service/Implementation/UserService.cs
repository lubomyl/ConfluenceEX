﻿using ConfluenceRESTClient.Model;
using DevDefined.OAuth.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service.Implementation
{

    /// <summary>
    /// Concrete implementation of IUserService utilizing <see cref="DevDefinedBaseService"/> as <see cref="IBaseService{T}"/>.
    /// <see cref="IUserService"/>
    /// </summary>
    public class UserService : IUserService
    {

        private IBaseService<IToken> _baseService;

        public UserService()
        {
            _baseService = DevDefinedBaseService.Instance;
        }

        /// <summary>
        /// <see cref="IUserService.GetAuthenticatedUserAsync"/>
        /// </summary>
        public Task<AuthenticatedUser> GetAuthenticatedUserAsync()
        {
            return Task.Run(() => {
                var resource = "user/current";

                return this._baseService.Get<AuthenticatedUser>(resource);
            });
        }

        public bool IsAuthenticated(AuthenticatedUser authenticatedUser)
        {
            bool ret = false;

            if (authenticatedUser != null)
            {
                ret = true;
            }

            return ret;
        }

    }
}
