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

        public object SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged("SelectedView");
            }
        }

        public NavigationViewModel()
        { 
        }

        public void ShowContent()
        {
            SelectedView = new ContentListView();
        }

        public void ShowTest()
        {
            SelectedView = new TestView();
        }

    }
}
