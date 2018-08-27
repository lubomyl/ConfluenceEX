using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel.Design;
using ConfluenceEX.ViewModel;
using ConfluenceRESTClient.Service.Implementation;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Model;
using ConfluenceEX.Common;
using System.Collections.ObjectModel;
using ConfluenceRestClient.Model;

namespace ConfluenceEX.Main
{

    [Guid(Guids.GUID_CONFLUENCE_TOOL_WINDOW_STRING)]
    public partial class ConfluenceToolWindow : ToolWindowPane
    {
        private readonly object _view;
        private ConfluenceToolWindowNavigatorViewModel _navigator;
        private bool _isAuthenticated;

        private static AuthenticatedUser _authenticatedUser;

        private IUserService _userService;
        private IOAuthService _oAuthService;

        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public ConfluenceToolWindow() : base(null)
        {
            this.Caption = Resources.ConflueceToolWindowTitle;
            this._navigator = new ConfluenceToolWindowNavigatorViewModel(this);

            this._oAuthService = new OAuthService();

            /*
            this._authenticationService = new AuthenticationService(SignedInUser.Username, SignedInUser.Password);

            _authenticatedUser = _authenticationService.Authenticate();

            if (_authenticationService.IsAuthenticated(_authenticatedUser))
            {
                this._navigator.ShowSpaces(null, null);
            } 
            else
            {
                this._navigator.ShowBeforeSignIn();
            }
            */
            _oAuthService.InitializeOAuthSession();

            this._view = new ConfluenceToolWindowNavigator(this._navigator);
            base.Content = _view;

            this.ToolBar = new CommandID(Guids.guidConfluencePackage, Guids.CONFLUENCE_TOOLBAR_ID);

            this._navigator.ShowBeforeSignIn(); 
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public ConfluenceToolWindowNavigatorViewModel Navigator
        {
            get { return this._navigator; }
            set { this._navigator = value; }
        }

        public bool IsAuthenticated
        {
            get { return this._isAuthenticated; }
            private set { this._isAuthenticated = value; }
        }

        public static AuthenticatedUser AuthenticatedUser
        {
            get { return _authenticatedUser; }
            set { _authenticatedUser = value; }
        }
    }
}
