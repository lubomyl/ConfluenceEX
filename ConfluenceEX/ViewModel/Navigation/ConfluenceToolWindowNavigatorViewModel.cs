using ConfluenceEX.Common;
using ConfluenceEX.Main;
using ConfluenceEX.View;
using ConfluenceEX.ViewModel.Navigation;
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
using System.Windows.Controls;

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

        private HistoryNavigator _historyNavigator;

        public ConfluenceToolWindowNavigatorViewModel(ConfluenceToolWindow parent)
        {
            this._parent = parent;
            this._historyNavigator = new HistoryNavigator();

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
        //TODO create one function fow all command with switch by enum
        private void EnableSpacesRefresh(bool enable, OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommandRefreshID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_REFRESH_ID);
                MenuCommand onToolbarMenuCommandRefreshClick = service.FindCommand(toolbarMenuCommandRefreshID);

                onToolbarMenuCommandRefreshClick.Enabled = enable;
            }
        }

        private void EnableBack(bool enable, OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommandBackID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_BACK_ID);
                MenuCommand onToolbarMenuCommandBackClick = service.FindCommand(toolbarMenuCommandBackID);

                onToolbarMenuCommandBackClick.Enabled = enable;
            }
        }

        private void EnableForward(bool enable, OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommandForwardID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_FORWARD_ID);
                MenuCommand onToolbarMenuCommandForwardClick = service.FindCommand(toolbarMenuCommandForwardID);

                onToolbarMenuCommandForwardClick.Enabled = enable;
            }
        }

        //TODO - find better solution then initializing null commands
        private void InitializeCommandsEmpty(OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommandBackID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_BACK_ID);
                CommandID toolbarMenuCommandForwardID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_FORWARD_ID);
                CommandID toolbarMenuCommandHomeID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_HOME_ID);
                CommandID toolbarMenuCommandConnectionID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_CONNECTION_ID);
                CommandID toolbarMenuCommandRefreshID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_REFRESH_ID);

                MenuCommand onToolbarMenuCommandBackClick = new MenuCommand(null, toolbarMenuCommandBackID);
                MenuCommand onToolbarMenuCommandForwardClick = new MenuCommand(null, toolbarMenuCommandForwardID);
                MenuCommand onToolbarMenuCommandHomeClick = new MenuCommand(ShowSpaces, toolbarMenuCommandHomeID);
                MenuCommand onToolbarMenuCommandConnectionClick = new MenuCommand(ShowConnection, toolbarMenuCommandConnectionID);
                MenuCommand onToolbarMenuCommandRefreshClick = new MenuCommand(null, toolbarMenuCommandRefreshID);

                service.AddCommand(onToolbarMenuCommandBackClick);
                service.AddCommand(onToolbarMenuCommandForwardClick);
                service.AddCommand(onToolbarMenuCommandHomeClick);
                service.AddCommand(onToolbarMenuCommandConnectionClick);
                service.AddCommand(onToolbarMenuCommandRefreshClick);

                onToolbarMenuCommandBackClick.Enabled = false;
                onToolbarMenuCommandForwardClick.Enabled = false;
            }
        }

        private void EnableCommands(bool enable, OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommandHomeID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_HOME_ID);

                MenuCommand onToolbarMenuCommandHomeClick = service.FindCommand(toolbarMenuCommandHomeID);

                onToolbarMenuCommandHomeClick.Enabled = enable;
            }
        }

        public object SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                this._historyNavigator.AddView((UserControl) value);

                if (this._historyNavigator.CanGoBack())
                {
                    EnableBack(true, _service);
                }
                else
                {
                    EnableBack(false, _service);
                }

                if (this._historyNavigator.CanGoForward())
                {
                    EnableForward(true, _service);
                }
                else
                {
                    EnableForward(false, _service);
                }

                OnPropertyChanged("SelectedView");
            }
        }

    }
}
