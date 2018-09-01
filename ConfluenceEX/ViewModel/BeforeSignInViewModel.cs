using ConfluenceEX.Command;
using ConfluenceEX.Common;
using ConfluenceEX.Main;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Service.Implementation;
using DevDefined.OAuth.Framework;
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
        private IOAuthService _oAuthService;

        private ConfluenceToolWindowNavigatorViewModel _parent;

        private string _username;
        private string _password;

        private bool _isAuthenticated;
        private string _errorMessage;
        private string _baseUrl;

        public DelegateCommand SignInCommand { get; private set; }
        public DelegateCommand SignInOAuthCommand { get; private set; }

        public BeforeSignInViewModel(ConfluenceToolWindowNavigatorViewModel parent)
        {
            this._parent = parent;
            this._isAuthenticated = false;

            this.SignInCommand = new DelegateCommand(SignIn);
            this.SignInOAuthCommand = new DelegateCommand(SignInOAuth);
        }

        private async void SignIn(object parameter)
        {
            this._username = this.Username;
            GetPassword(parameter);

            SignedInUser.Username = this._username;
            SignedInUser.Password = this._password;

            this._userService = new UserService();

            if (SignedInUser.IsComplete())
            {
                ConfluenceToolWindow.AuthenticatedUser = await this._userService.GetAuthenticatedUserAsync();
                this._isAuthenticated = _userService.IsAuthenticated(ConfluenceToolWindow.AuthenticatedUser);
                //this.ErrorMessage = !this._isAuthenticated;
            }
            else
            {
                /*BindingExpression be = Username.GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();*/
            }

            if (this._isAuthenticated)
            {
                this._parent.ShowAfterSignIn();
            }
        }

        private async void SignInOAuth(object parameter)
        {
            this._oAuthService = new OAuthService();

            IToken requestToken;
            string authorizationUrl;

            try
            {
                this._oAuthService.InitializeOAuthSession(this.BaseUrl);
                requestToken = await this._oAuthService.GetRequestToken();
                authorizationUrl = await this._oAuthService.GetUserAuthorizationUrlForToken(requestToken);

                System.Diagnostics.Process.Start(authorizationUrl);
                this._parent.ShowOAuthVerificationConfirmation(null, null, requestToken);
            }
            catch(OAuthException ex)
            {
                this.ErrorMessage = ex.Message;
            } 
            catch(SecurityException ex)
            {
                this.ErrorMessage = ex.Message;
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

        public string ErrorMessage
        {
            get
            {
                return this._errorMessage;
            }
            set
            {
                this._errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public string BaseUrl
        {
            get
            {
                return this._baseUrl;
            }
            set
            {
                this._baseUrl = value;
                OnPropertyChanged("BaseUrl");
            }
        }

        #endregion
    }
}
