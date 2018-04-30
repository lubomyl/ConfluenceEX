using ConfluenceEX.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceEX.ViewModel
{
    public class ConfluenceToolWindowNavigatorViewModel : ViewModelBase
    {

        private object _selectedView;
        private ConfluenceToolWindow _parent;

        private ContentListView _contentListView;
        private SignInNavigatorView _signInNavigatorView;

        public ConfluenceToolWindowNavigatorViewModel(ConfluenceToolWindow parent)
        {
            this._parent = parent;
        }

        public void ShowContent()
        {
            _parent.Caption = "Confluence";

            if(this._contentListView == null)
            {
                this._contentListView = new ContentListView();
                SelectedView = this._contentListView;
            }
            else
            {
                SelectedView = _contentListView;
            }
        }

        public void ShowSignInNavigatorView()
        {
            _parent.Caption = "Confluence Sign-in";

            if(this._signInNavigatorView == null)
            {
                this._signInNavigatorView = new SignInNavigatorView(this._parent);
                SelectedView = this._signInNavigatorView;
            }
            else
            {
                SelectedView = _signInNavigatorView;
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
