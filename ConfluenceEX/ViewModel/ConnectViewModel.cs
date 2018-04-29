using ConfluenceEX.Command;
using ConfluenceEX.Common;
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

        private string _username;
        private string _password;

        private bool _isAuthenticated;

        public DelegateCommand SignInCommand { get; private set; }

        public ConnectViewModel(string username, string password)
        {
            this._authenticationService = new AuthenticationService(username, password);

            this.IsAuthenticated = _authenticationService.IsAuthenticated(_authenticationService.Authenticate(username, password));

            this.SignInCommand = new DelegateCommand(o => this.SignIn());
        }

        private void SignIn()
        {
            this._username = this.Username;
            this._password = password.Password;

            SignedInUser.Username = this.Username;
            SignedInUser.Password = this._password;

            if (SignedInUser.IsComplete())
            {
                this.IsAuthenticated = _authenticationService.IsAuthenticated(_authenticationService.Authenticate(SignedInUser.Username, SignedInUser.Password));
            } else
            {
                //TODO 1
                Console.WriteLine("ERROR: Missing username or password");
            }
        }

        //TODO 1
        //Check if can execute sign-in
        //On Textbox change notify and update state
        private bool CanExecuteSignIn()
        {
            return Username != null;
        }

        #region ConnectViewModel Members

        public string Username
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
                OnPropertyChanged("Username");
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return this._isAuthenticated;
            }
            set
            {
                this._isAuthenticated = value;
                OnPropertyChanged("IsAuthenticated");
            }
        }

        #endregion
    }
}
