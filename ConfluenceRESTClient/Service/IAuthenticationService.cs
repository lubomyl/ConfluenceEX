using ConfluenceRESTClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service
{
    public interface IAuthenticationService
    {

        AuthenticatedUser Authenticate();

        bool IsAuthenticated(AuthenticatedUser authenticatedUser);

    }
}
