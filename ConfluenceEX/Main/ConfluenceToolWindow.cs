using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using ConfluenceEX.Main;
using ConfluenceEX.View;
using System.ComponentModel.Design;
using ConfluenceEX.ViewModel;
using ConfluenceRESTClient.Service.Implementation;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Model;
using ConfluenceEX.Common;

namespace ConfluenceEX
{

    [Guid(Guids.GUID_CONFLUENCE_TOOL_WINDOW_STRING)]
    public class ConfluenceToolWindow : ToolWindowPane
    {
        private readonly object _view;
        private ConfluenceToolWindowNavigatorViewModel _navigator;
        private bool _isAuthenticated;

        private static AuthenticatedUser _authenticatedUser;

        public IAuthenticationService _authenticationService;

        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public ConfluenceToolWindow() : base(null)
        {
            this.Caption = Resources.ConflueceToolWindowTitle;
            this._authenticationService = new AuthenticationService(SignedInUser.Username, SignedInUser.Password);
            this._navigator = new ConfluenceToolWindowNavigatorViewModel(this);

            _authenticatedUser = _authenticationService.Authenticate();

            if (_authenticationService.IsAuthenticated(_authenticatedUser))
            {
                this._navigator.ShowSpaces(null, null);
            } 
            else
            {
                this._navigator.ShowBeforeSignIn();
            }

            this._view = new ConfluenceToolWindowNavigator(this._navigator);
            base.Content = _view;

            this.ToolBar = new CommandID(Guids.guidConfluencePackage, Guids.CONFLUENCE_TOOLBAR_ID);
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
