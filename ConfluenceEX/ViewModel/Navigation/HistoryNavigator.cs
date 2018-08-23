using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ConfluenceEX.ViewModel.Navigation
{
    public class HistoryNavigator
    {
        private const int STACK_SIZE = 5;
        private const int STEP = 1;

        private List<UserControl> _viewStack;

        private int _index;

        public HistoryNavigator()
        {
            this._viewStack = new List<UserControl>();

            _index = 0;
        }

        public bool CanGoBack()
        {
            if (_index > 0) {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CanGoForward()
        {
            if(_index < STACK_SIZE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UserControl GetBackView()
        {
            if (CanGoBack())
            {
                this._index--;
                return _viewStack[_index - STEP];
            } else
            {
                //TODO throw exception
                return null;
            }
        }

        public UserControl GetForwardView()
        {
            if (CanGoForward())
            {
                this._index++;
                return _viewStack[_index + STEP];
            } else
            {
                //TODO throw exception
                return null;
            }
        }

        public void AddView(UserControl view)
        {
            for (int i = _index + STEP; i < this._viewStack.Count; i++)
            {
                this._viewStack.RemoveAt(i);
            }

            this._viewStack.Add(view);
            this._index++;
        }
    }
}
