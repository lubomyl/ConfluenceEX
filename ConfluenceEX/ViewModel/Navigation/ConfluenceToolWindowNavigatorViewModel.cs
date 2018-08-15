using ConfluenceEX.Main;
using ConfluenceEX.View;
using ConfluenceRestClient.Model;
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

        private SpaceListView _spacesListView;
        private ContentListView _contentListView;
        private SignInNavigatorView _signInNavigatorView;

        public ConfluenceToolWindowNavigatorViewModel(ConfluenceToolWindow parent)
        {
            this._parent = parent;

            OleMenuCommandService service = ConfluencePackage.Mcs;

            InitializeCommandsEmpty(service);
        }

        public void RefreshSpaces()
        {
            this._spacesListView = new SpaceListView(this);
            SelectedView = this._spacesListView;
        }

        public void ShowSpaces()
        {
            _parent.Caption = "Confluence Spaces";

            if(this._spacesListView == null)
            {
                this._spacesListView = new SpaceListView(this);
                SelectedView = this._spacesListView;
            }
            else
            {
                SelectedView = _spacesListView;
            }

            EnableSpacesRefresh(true);
        }

        public void ShowSpaceContent(Space space)
        {
            _parent.Caption = "Confluence " + space.Name + " Content";

            this._contentListView = new ContentListView(space);
            SelectedView = this._contentListView;

            EnableSpacesRefresh(false);
        }

        private static void EnableSpacesRefresh(bool enable)
        {
            OleMenuCommandService service = ConfluencePackage.Mcs;

            if (service != null)
            {
                CommandID toolbarMenuCommandRefreshID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandRefreshId);
                MenuCommand onToolbarMenuCommandRefreshClick = service.FindCommand(toolbarMenuCommandRefreshID);

                onToolbarMenuCommandRefreshClick.Enabled = enable;
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

            EnableSpacesRefresh(false);
        }

        //TODO 2 - find better solution then initializing null commands
        private void InitializeCommandsEmpty(OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommandEditID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandEditId);
                CommandID toolbarMenuCommandAddID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandAddId);
                CommandID toolbarMenuCommandRefreshID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandRefreshId);

                MenuCommand onToolbarMenuCommandEditClick = new MenuCommand(null, toolbarMenuCommandEditID);
                MenuCommand onToolbarMenuCommandAddClick = new MenuCommand(null, toolbarMenuCommandAddID);
                MenuCommand onToolbarMenuCommandRefreshClick = new MenuCommand(null, toolbarMenuCommandRefreshID);

                service.AddCommand(onToolbarMenuCommandEditClick);
                service.AddCommand(onToolbarMenuCommandAddClick);
                service.AddCommand(onToolbarMenuCommandRefreshClick);
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
