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
    public class ConnectViewModel : ViewModelBase
    {

        private IAuthenticationService _authenticationService;

        private string _username;
        private string _password;

        private bool _isAuthenticated;

        public DelegateCommand SignInCommand { get; private set; }

        public ConnectViewModel(string username, string password)
        {
            this._authenticationService = new AuthenticationService();

            this.IsAuthenticated = _authenticationService.IsAuthenticated(_authenticationService.Authenticate(username, password));

            this.SignInCommand = new DelegateCommand(SignIn);
        }

        private void SignIn(object parameter)
        {
            this._username = this.Username;

            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                this._password = ConvertToUnsecureString(secureString);
            }

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
