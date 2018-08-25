using ConfluenceEX.Command;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Service.Implementation;
using DevDefined.OAuth.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceEX.ViewModel
{
    public class OAuthVerifierConfirmationViewModel : ViewModelBase
    {
        private ConfluenceToolWindowNavigatorViewModel _parent;

        private OAuthService _oAuthService;

        private string _oAuthVerificationCode;
        private IToken _requestToken;

        public DelegateCommand SignInCommand { get; private set; }

        public OAuthVerifierConfirmationViewModel(ConfluenceToolWindowNavigatorViewModel parent, IToken requestToken)
        {
            this._parent = parent;

            this._requestToken = requestToken;

            this.SignInCommand = new DelegateCommand(SignIn);
        }

        private async void SignIn(object parameter)
        {
            //TODO check if accessToken is OK - if not do not change view else show afterSignIn
            this._oAuthService = new OAuthService();

            IToken accessToken = await this._oAuthService.ExchangeRequestTokenForAccessToken(this._requestToken, OAuthVerificationCode);

            this._parent.ShowAfterSignIn();
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
    }
}
