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
            this._contentListView = new ContentListView();
            this._testView = new TestView();
        }

        public void ShowContent()
        {
            _parent.Caption = "Confluence";
            SelectedView = _contentListView;
        }

        public void ShowTest()
        {
            _parent.Caption = "Confluence - Connect";
            SelectedView = _testView;
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
