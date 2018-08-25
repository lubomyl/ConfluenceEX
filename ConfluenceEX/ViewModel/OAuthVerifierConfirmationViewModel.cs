using ConfluenceEX.Command;
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

        private string _oAuthVerificationCode;

        public DelegateCommand SignInCommand { get; private set; }

        public OAuthVerifierConfirmationViewModel(ConfluenceToolWindowNavigatorViewModel parent)
        {
            this._parent = parent;

            this.SignInCommand = new DelegateCommand(SignIn);
        }

        private void SignIn(object parameter)
        {
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
