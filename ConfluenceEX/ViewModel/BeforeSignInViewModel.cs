using ConfluenceEX.Command;
using ConfluenceEX.Common;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConfluenceEX.ViewModel
{
    public class BeforeSignInViewModel : ViewModelBase
    {

        private IAuthenticationService _authenticationService;
        private ConfluenceToolWindowNavigatorViewModel _parent;

        private string _username;
        private string _password;

        private bool _isAuthenticated;
        private bool _badSignInCredentials;

        public DelegateCommand SignInCommand { get; private set; }

        public BeforeSignInViewModel(ConfluenceToolWindowNavigatorViewModel parent)
        {
            this._parent = parent;
            this._isAuthenticated = false;
            this._badSignInCredentials = false;

            this.SignInCommand = new DelegateCommand(SignIn);
        }

        private void SignIn(object parameter)
        {
            this._username = this.Username;
            GetPassword(parameter);

            SignedInUser.Username = this._username;
            SignedInUser.Password = this._password;

            this._authenticationService = new AuthenticationService(this._username, this._password);

            if (SignedInUser.IsComplete())
            {
                ConfluenceToolWindow.AuthenticatedUser = _authenticationService.Authenticate();
                this._isAuthenticated = _authenticationService.IsAuthenticated(ConfluenceToolWindow.AuthenticatedUser);
                this.BadSignInCredentials = !this._isAuthenticated;
            }
            else
            {
                //TODO
                Console.WriteLine("ERROR: Missing username or password");
            }

            if (this._isAuthenticated)
            {
                this._parent.ShowAfterSignIn();
            }
        }

        private void GetPassword(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                this._password = ConvertToUnsecureString(secureString);
            }
        }

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        //TODO
        //Check if can execute sign-in
        //On Textbox change notify and update state
        private bool CanExecuteSignIn()
        {
            return Username != null;
        }

        #region BeforeSignInViewModel Members

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

        public bool BadSignInCredentials
        {
            get
            {
                return this._badSignInCredentials;
            }
            set
            {
                this._badSignInCredentials = value;
                OnPropertyChanged("BadSignInCredentials");
            }
        }

        #endregion
    }
}
