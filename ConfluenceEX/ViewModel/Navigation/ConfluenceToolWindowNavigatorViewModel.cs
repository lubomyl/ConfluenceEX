using ConfluenceEX.Common;
using ConfluenceEX.Main;
using ConfluenceEX.View;
using ConfluenceRestClient.Model;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Service.Implementation;
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
        private AfterSignInView _afterSignInView;
        private BeforeSignInView _beforeSignInView;

        private OleMenuCommandService _service;

        public ConfluenceToolWindowNavigatorViewModel(ConfluenceToolWindow parent)
        {
            this._parent = parent;

            _service = ConfluencePackage.Mcs;

            InitializeCommandsEmpty(_service);
        }

        public void ShowAfterSignIn()
        {     
            _parent.Caption = "Confluence - Signed-in";
            this.EnableCommands(true, _service);
            EnableSpacesRefresh(false, _service);

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
            _parent.Caption = "Confluence - Sign-in";
            this.EnableCommands(false, _service);
            EnableSpacesRefresh(false, _service);

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

        public void ShowSpaces(object sender, EventArgs e)
        {
            _parent.Caption = "Confluence Spaces";
            EnableSpacesRefresh(true, _service);

            if (this._spacesListView == null)
            {
                this._spacesListView = new SpaceListView(this);
                SelectedView = this._spacesListView;
            }
            else
            {
                SelectedView = _spacesListView;
            }
        }

        public void ShowSpaceContent(Space space)
        {
            _parent.Caption = "Confluence " + space.Name + " Content";
            EnableSpacesRefresh(false, _service);

            this._contentListView = new ContentListView(space);
            SelectedView = this._contentListView;
        }

        public void ShowConnection(object sender, EventArgs e)
        {
            if (ConfluenceToolWindow.AuthenticatedUser != null)
            {
                ShowAfterSignIn();
            }
            else
            {
                ShowBeforeSignIn();
            }
        }

        private static void EnableSpacesRefresh(bool enable, OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommandRefreshID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandRefreshId);
                MenuCommand onToolbarMenuCommandRefreshClick = service.FindCommand(toolbarMenuCommandRefreshID);

                onToolbarMenuCommandRefreshClick.Enabled = enable;
            }
        }

        //TODO 2 - find better solution then initializing null commands
        private void InitializeCommandsEmpty(OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommandEditID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandEditId);
                CommandID toolbarMenuCommandAddID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandAddId);
                CommandID toolbarMenuCommandHomeID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandHome);
                CommandID toolbarMenuCommandConnectionID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandConnectionId);
                CommandID toolbarMenuCommandRefreshID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandRefreshId);

                MenuCommand onToolbarMenuCommandEditClick = new MenuCommand(null, toolbarMenuCommandEditID);
                MenuCommand onToolbarMenuCommandAddClick = new MenuCommand(null, toolbarMenuCommandAddID);
                MenuCommand onToolbarMenuCommandHomeClick = new MenuCommand(ShowSpaces, toolbarMenuCommandHomeID);
                MenuCommand onToolbarMenuCommandConnectionClick = new MenuCommand(ShowConnection, toolbarMenuCommandConnectionID);
                MenuCommand onToolbarMenuCommandRefreshClick = new MenuCommand(null, toolbarMenuCommandRefreshID);

                service.AddCommand(onToolbarMenuCommandEditClick);
                service.AddCommand(onToolbarMenuCommandAddClick);
                service.AddCommand(onToolbarMenuCommandHomeClick);
                service.AddCommand(onToolbarMenuCommandConnectionClick);
                service.AddCommand(onToolbarMenuCommandRefreshClick);
            }
        }

        private void EnableCommands(bool enable, OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommandEditID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandEditId);
                CommandID toolbarMenuCommandAddID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandAddId);
                CommandID toolbarMenuCommandHomeID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandHome);

                MenuCommand onToolbarMenuCommandEditClick = service.FindCommand(toolbarMenuCommandEditID);
                MenuCommand onToolbarMenuCommandAddClick = service.FindCommand(toolbarMenuCommandAddID);
                MenuCommand onToolbarMenuCommandHomeClick = service.FindCommand(toolbarMenuCommandHomeID);

                onToolbarMenuCommandEditClick.Enabled = enable;
                onToolbarMenuCommandAddClick.Enabled = enable;
                onToolbarMenuCommandHomeClick.Enabled = enable;
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
