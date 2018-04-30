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
        private BeforeSignInView _beforeSignInView;

        public SignInNavigatorViewModel(ConfluenceToolWindow parent)
        {
            this._parent = parent;
            this.ShowBeforeSignIn();
        }

        public void ShowAfterSignIn()
        {
            _parent.Caption = "Confluence - Spaces";

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

        public void ShowBeforeSignIn()
        {
            _parent.Caption = "Confluence - Signed-in";

            if (this._beforeSignInView == null)
            {
                this._beforeSignInView = new BeforeSignInView(this);
                SelectedView = this._beforeSignInView;
            }
            else
            {
                SelectedView = _beforeSignInView;
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
