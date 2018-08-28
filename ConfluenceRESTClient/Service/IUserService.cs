using ConfluenceRESTClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service
{

    /// <summary>
    /// Interface providing methods to access resources connected to User object from Confluence app.
    /// </summary>
    public interface IUserService
    {


        /// <summary>
        /// Async method to get <see cref="AuthenticatedUser"/> resources like username or displayName.
        /// </summary>
        /// <returns>Task containing deserialized <see cref="AuthenticatedUser"/> model class object.</returns>
        Task<AuthenticatedUser> GetAuthenticatedUserAsync();

        //TODO refactor extract to ?Helper? class. Find any other implementation? Basic authentication.
        bool IsAuthenticated(AuthenticatedUser authenticatedUser);
    }
}
