using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceEX.ViewModel
{
    public class ConnectViewModel : ViewModelBase
    {

        private IAuthenticationService _authenticationService;

        public ConnectViewModel(string username, string password)
        {
            this._authenticationService = new AuthenticationService(username, password);
        }
    }
}
