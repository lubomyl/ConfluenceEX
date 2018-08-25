using ConfluenceEX.Command;
using ConfluenceEX.Common;
using ConfluenceEX.Main;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Service.Implementation;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Controls;
using System.Windows.Data;

namespace ConfluenceEX.ViewModel
{
    public class BeforeSignInViewModel : ViewModelBase
    {

        private IUserService _userService;
        private ConfluenceToolWindowNavigatorViewModel _parent;

        private string _username;
        private string _password;

        private bool _isAuthenticated;
        private bool _badSignInCredentials;

        public DelegateCommand SignInCommand { get; private set; }
        public DelegateCommand SignInOAuthCommand { get; private set; }

        public BeforeSignInViewModel(ConfluenceToolWindowNavigatorViewModel parent)
        {
            this._parent = parent;
            this._isAuthenticated = false;
            this._badSignInCredentials = false;

            this.SignInCommand = new DelegateCommand(SignIn);
            this.SignInOAuthCommand = new DelegateCommand(SignInOAuth);
        }

        private void SignIn(object parameter)
        {
            this._username = this.Username;
            GetPassword(parameter);

            SignedInUser.Username = this._username;
            SignedInUser.Password = this._password;

            this._userService = new UserService();

            //TODO basic sign-in using username/password form
            /*
            if (SignedInUser.IsComplete())
            {
                ConfluenceToolWindow.AuthenticatedUser = _userService.GetAuthenticatedUserAsync();
                this._isAuthenticated = _userService.IsAuthenticated(ConfluenceToolWindow.AuthenticatedUser);
                this.BadSignInCredentials = !this._isAuthenticated;
            }
            else
            {
                /*BindingExpression be = Username.GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();
            }
            */

            if (this._isAuthenticated)
            {
                this._parent.ShowAfterSignIn();
            }
        }

        private void SignInOAuth(object parameter)
        {
            this._parent.ShowOAuthVerificationConfirmation(null, null);
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
