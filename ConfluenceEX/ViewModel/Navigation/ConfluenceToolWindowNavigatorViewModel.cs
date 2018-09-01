using ConfluenceEX.Common;
using ConfluenceEX.Main;
using ConfluenceEX.View;
using ConfluenceEX.ViewModel.Navigation;
using ConfluenceRestClient.Model;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Service.Implementation;
using DevDefined.OAuth.Framework;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
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

        private IOAuthService _oAuthService;

        private SpaceListView _spacesListView;
        private ContentListView _contentListView;
        private AfterSignInView _afterSignInView;
        private BeforeSignInView _beforeSignInView;
        private OAuthVerifierConfirmationView _oAuthVerifierConfirmationView;
        private ContentView _contentView;

        private WritableSettingsStore _userSettingsStore;

        private OleMenuCommandService _service;

        private HistoryNavigator _historyNavigator;

        public ConfluenceToolWindowNavigatorViewModel(ConfluenceToolWindow parent)
        {
            this._parent = parent;
            this._historyNavigator = new HistoryNavigator();

            _service = ConfluencePackage.Mcs;

            this._oAuthService = new OAuthService();

            SettingsManager settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
            this._userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            InitializeCommands(_service);
        }

        public void ShowAfterSignIn()
        {     
            _parent.Caption = "Confluence - Signed-in";
            this.EnableCommand(true, _service, Guids.COMMAND_HOME_ID);
            this.EnableCommand(false, _service, Guids.COMMAND_REFRESH_ID);

            if (this._afterSignInView == null)
            {
                this._afterSignInView = new AfterSignInView(this);
                this._historyNavigator.AddView(this._afterSignInView);

                SelectedView = this._afterSignInView;
            }
            else
            {
                this._historyNavigator.AddView(this._afterSignInView);

                SelectedView = _afterSignInView;
            }
        }

        public void ShowBeforeSignIn()
        {
            _parent.Caption = "Confluence - Sign-in";
            
            if (this._beforeSignInView == null)
            {
                this._beforeSignInView = new BeforeSignInView(this);

                SelectedView = this._beforeSignInView;
            }
            else
            {
                SelectedView = _beforeSignInView;
            }

            this._historyNavigator.ClearStack();
            this.EnableCommand(false, _service, Guids.COMMAND_HOME_ID);
            this.EnableCommand(false, _service, Guids.COMMAND_REFRESH_ID);
            this.EnableCommand(false, _service, Guids.COMMAND_BACK_ID);
        }

        public void ShowSpaces(object sender, EventArgs e)
        {
            _parent.Caption = "Confluence Spaces";
            this.EnableCommand(true, _service, Guids.COMMAND_REFRESH_ID);

            if (this._spacesListView == null)
            {
                this._spacesListView = new SpaceListView(this);
                this._historyNavigator.AddView(this._spacesListView);

                SelectedView = this._spacesListView;
            }
            else
            {
                this._historyNavigator.AddView(this._spacesListView);

                SelectedView = _spacesListView;
            }     
        }

        public void ShowSpaceContent(Space space)
        {
            _parent.Caption = "Confluence " + space.Name + " Content";
            this.EnableCommand(false, _service, Guids.COMMAND_REFRESH_ID);

            this._contentListView = new ContentListView(space, this);
            this._historyNavigator.AddView(this._contentListView);

            SelectedView = this._contentListView;
        }

        public void ShowConnection(object sender, EventArgs e)
        {
            try
            {
                string accessToken = this.ReadFromUserSettings("AccessToken");
                string accessTokenSecret = this.ReadFromUserSettings("AccessTokenSecret");
                string baseUrl = this.ReadFromUserSettings("BaseUrl");

                if (accessToken != null && accessTokenSecret != null && baseUrl != null)
                {
                    this._oAuthService.ReinitializeOAuthSessionAccessToken(accessToken, accessTokenSecret, baseUrl);

                    this.ShowAfterSignIn();
                }
            }
            catch (Exception ex)
            {
                this.ShowBeforeSignIn();
            }
        }

        public void ShowOAuthVerificationConfirmation(object sender, EventArgs e, IToken requestToken)
        {
            _parent.Caption = "Confirm OAuth Verification Code";

            this._oAuthVerifierConfirmationView = new OAuthVerifierConfirmationView(this, requestToken);

            SelectedView = this._oAuthVerifierConfirmationView;

            this._historyNavigator.ClearStack();
            this.EnableCommand(false, _service, Guids.COMMAND_HOME_ID);
            this.EnableCommand(false, _service, Guids.COMMAND_REFRESH_ID);
            this.EnableCommand(false, _service, Guids.COMMAND_BACK_ID);
        }

        public void ShowContent(int contentId)
        {
            _parent.Caption = "Confluence Content page";

            this._contentView = new ContentView(contentId);

            SelectedView = this._contentView;

            this._historyNavigator.AddView(this._contentView);

            this.EnableCommand(false, _service, Guids.COMMAND_REFRESH_ID);
        }

        private void EnableCommand(bool enable, OleMenuCommandService service, int commandGuid)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommandID = new CommandID(Guids.guidConfluenceToolbarMenu, commandGuid);
                MenuCommand onToolbarMenuCommandClick = service.FindCommand(toolbarMenuCommandID);

                onToolbarMenuCommandClick.Enabled = enable;
            }
        }

        private void InitializeCommands(OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommandBackID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_BACK_ID);
                CommandID toolbarMenuCommandForwardID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_FORWARD_ID);
                CommandID toolbarMenuCommandHomeID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_HOME_ID);
                CommandID toolbarMenuCommandConnectionID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_CONNECTION_ID);
                CommandID toolbarMenuCommandRefreshID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_REFRESH_ID);

                MenuCommand onToolbarMenuCommandBackClick = new MenuCommand(GoBack, toolbarMenuCommandBackID);
                MenuCommand onToolbarMenuCommandForwardClick = new MenuCommand(GoForward, toolbarMenuCommandForwardID);
                MenuCommand onToolbarMenuCommandHomeClick = new MenuCommand(ShowSpaces, toolbarMenuCommandHomeID);
                MenuCommand onToolbarMenuCommandConnectionClick = new MenuCommand(ShowConnection, toolbarMenuCommandConnectionID);
                MenuCommand onToolbarMenuCommandRefreshClick = new MenuCommand(null, toolbarMenuCommandRefreshID);

                service.AddCommand(onToolbarMenuCommandBackClick);
                service.AddCommand(onToolbarMenuCommandForwardClick);
                service.AddCommand(onToolbarMenuCommandHomeClick);
                service.AddCommand(onToolbarMenuCommandConnectionClick);
                service.AddCommand(onToolbarMenuCommandRefreshClick);
            }
        }

        //TODO refactor extract to Helper class
        private string ReadFromUserSettings(string propertyName)
        {
            string ret = this._userSettingsStore.GetString("External Tools", propertyName);

            return ret;
        }

        private void GoBack(object sender, EventArgs e)
        {
            if (this._historyNavigator.CanGoBack())
            {
                this.SelectedView = this._historyNavigator.GetBackView();

                if(this.SelectedView.GetType() == typeof(SpaceListView))
                {
                    this.EnableCommand(true, _service, Guids.COMMAND_REFRESH_ID);
                } 
                else
                {
                    this.EnableCommand(false, _service, Guids.COMMAND_REFRESH_ID);
                }
            }
        }

        private void GoForward(object sender, EventArgs e)
        {
            if (this._historyNavigator.CanGoForward())
            {
                this.SelectedView = this._historyNavigator.GetForwardView();

                if (this.SelectedView.GetType() == typeof(SpaceListView))
                {
                    this.EnableCommand(true, _service, Guids.COMMAND_REFRESH_ID);
                }
                else
                {
                    this.EnableCommand(false, _service, Guids.COMMAND_REFRESH_ID);
                }
            }
        }

        public object SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;

                if (this._historyNavigator.CanGoBack())
                {
                    this.EnableCommand(true, _service, Guids.COMMAND_BACK_ID);
                }
                else
                {
                    this.EnableCommand(false, _service, Guids.COMMAND_BACK_ID);
                }

                if (this._historyNavigator.CanGoForward())
                {
                    this.EnableCommand(true, _service, Guids.COMMAND_FORWARD_ID);
                }
                else
                {
                    this.EnableCommand(false, _service, Guids.COMMAND_FORWARD_ID);
                }

                OnPropertyChanged("SelectedView");
            }
        }

    }
}
