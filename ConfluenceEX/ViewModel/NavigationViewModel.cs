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

        public NavigationViewModel(ConfluenceToolWindow parent)
        {
            this._parent = parent;
        }

        public void ShowContent()
        {
            _parent.Caption = "Confluence";
            SelectedView = new ContentListView();
        }

        public void ShowTest()
        {
            _parent.Caption = "Confluence - Connect";
            SelectedView = new TestView();
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
