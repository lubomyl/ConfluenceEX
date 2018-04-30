using ConfluenceEX.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceEX.ViewModel
{
    public class SignInNavigatorViewModel : ViewModelBase
    {

        private object _selectedView;
        private ConfluenceToolWindow _parent;

        private AfterSignInView _afterSignInView;
        private ConnectView _connectView;

        public SignInNavigatorViewModel(ConfluenceToolWindow parent)
        {
            this._parent = parent;
            this.ShowConnect();
        }

        public void ShowAfterSignIn()
        {
            _parent.Caption = "Confluence - Connect";

            if (this._afterSignInView == null)
            {
                this._afterSignInView = new AfterSignInView(this);
                SelectedView = this._afterSignInView;
            }
            else
            {
                SelectedView = _afterSignInView;
            }
        }

        public void ShowConnect()
        {
            _parent.Caption = "Confluence - Connected";

            if (this._connectView == null)
            {
                this._connectView = new ConnectView(this);
                SelectedView = this._connectView;
            }
            else
            {
                SelectedView = _connectView;
            }

        }

        public object SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged("SelectedView");
            }
        }

    }
}
