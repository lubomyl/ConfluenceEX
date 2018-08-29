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
        /// Async method to get <see cref="User"/> resources like username or displayName.
        /// </summary>
        /// <returns>Task containing deserialized <see cref="User"/> model class object.</returns>
        Task<User> GetAuthenticatedUserAsync();

        //TODO refactor extract to ?Helper? class. Find any other implementation? Basic authentication.
        bool IsAuthenticated(User authenticatedUser);
    }
}
