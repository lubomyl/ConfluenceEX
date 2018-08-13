using ConfluenceEX.Main;
using ConfluenceEX.View;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceEX.ViewModel
{
    public class ConfluenceToolWindowNavigatorViewModel : ViewModelBase
    {

        private object _selectedView;
        private ConfluenceToolWindow _parent;

        private SpaceListView _contentListView;
        private SignInNavigatorView _signInNavigatorView;

        public ConfluenceToolWindowNavigatorViewModel(ConfluenceToolWindow parent)
        {
            this._parent = parent;

            OleMenuCommandService service = ConfluencePackage.Mcs;

            InitializeCommandsEmpty(service);
        }

        public void ShowContent()
        {
            _parent.Caption = "Confluence";

            if(this._contentListView == null)
            {
                this._contentListView = new SpaceListView();
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

        //TODO 2 - find better solution then initializing null commands
        private void InitializeCommandsEmpty(OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommand1ID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommand1Id);
                CommandID toolbarMenuCommand2ID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommand2Id);

                MenuCommand onToolbarMenuCommand1Click = new MenuCommand(null, toolbarMenuCommand1ID);
                MenuCommand onToolbarMenuCommand2Click = new MenuCommand(null, toolbarMenuCommand2ID);

                service.AddCommand(onToolbarMenuCommand1Click);
                service.AddCommand(onToolbarMenuCommand2Click);
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
