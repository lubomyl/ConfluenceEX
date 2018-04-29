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
        private TestView _testView;

        public NavigationViewModel(ConfluenceToolWindow parent)
        {
            this._parent = parent;
        }

        public void ShowContent()
        {
            _parent.Caption = "Confluence";

            if(this._contentListView == null)
            {
                SelectedView = new ContentListView();
            }
            else
            {
                SelectedView = _contentListView;
            }
        }

        public void ShowTest()
        {
            _parent.Caption = "Confluence - Connect";

            if(this._testView == null)
            {
                SelectedView = new TestView();
            }
            else
            {
                SelectedView = _testView;
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
