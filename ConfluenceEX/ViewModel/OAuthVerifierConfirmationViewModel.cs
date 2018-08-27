using ConfluenceEX.Command;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Service.Implementation;
using DevDefined.OAuth.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Settings;
using Microsoft.VisualStudio.Shell;

namespace ConfluenceEX.ViewModel
{
    public class OAuthVerifierConfirmationViewModel : ViewModelBase
    {
        private ConfluenceToolWindowNavigatorViewModel _parent;

        private OAuthService _oAuthService;

        private string _oAuthVerificationCode;
        private IToken _requestToken;

        private string _errorMessage;

        private WritableSettingsStore _userSettingsStore;

        public DelegateCommand SignInCommand { get; private set; }

        public OAuthVerifierConfirmationViewModel(ConfluenceToolWindowNavigatorViewModel parent, IToken requestToken)
        {
            this._parent = parent;

            this._requestToken = requestToken;

            SettingsManager settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
            this._userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            this.SignInCommand = new DelegateCommand(SignIn);
        }

        private async void SignIn(object parameter)
        {
            //TODO check if accessToken is OK - if not do not change view else show afterSignIn
            this._oAuthService = new OAuthService();

            try
            {
                IToken accessToken = await this._oAuthService.ExchangeRequestTokenForAccessToken(this._requestToken, OAuthVerificationCode);

                this.WriteToUserSettings("AccessToken", accessToken.Token);

                this._parent.ShowAfterSignIn();
            }
            catch (OAuthException ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }

        private void WriteToUserSettings(string propertyName, string value)
        {
            this._userSettingsStore.SetString("External Tools", propertyName, value);
        }

        public string OAuthVerificationCode
        {
            get
            {
                return this._oAuthVerificationCode;
            }
            set
            {
                this._oAuthVerificationCode = value;
                OnPropertyChanged("OAuthVerificationCode");
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
    }
}
