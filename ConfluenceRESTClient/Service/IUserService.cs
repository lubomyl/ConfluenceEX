using ConfluenceRESTClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service
{
    public interface IUserService
    {

        Task<AuthenticatedUser> GetAuthenticatedUserAsync();

        bool IsAuthenticated(AuthenticatedUser authenticatedUser);
    }
}
