using ConfluenceEX.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceEX.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {

        private object _selectedView;
        private ConfluenceToolWindow _parent;

        private ContentListView _contentListView;
        private ConnectView _connectView;

        public NavigationViewModel(ConfluenceToolWindow parent)
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

        public void ShowConnect()
        {
            _parent.Caption = "Confluence - Connect";

            if(this._connectView == null)
            {
                this._connectView = new ConnectView();
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
