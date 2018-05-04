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

    [Guid(Guids.guidConfluenceToolWindow)]
    public class ConfluenceToolWindow : ToolWindowPane
    {
        private readonly object _view;
        private ConfluenceToolWindowNavigatorViewModel _navigator;
        private bool _isAuthenticated;
        private IAuthenticationService _authenticationService;

        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public ConfluenceToolWindow() : base(null)
        {
            AuthenticatedUser authenticatedUser;

            this.Caption = Resources.ConflueceToolWindowTitle;
            this._authenticationService = new AuthenticationService();
            this._navigator = new ConfluenceToolWindowNavigatorViewModel(this);

            authenticatedUser = _authenticationService.Authenticate(SignedInUser.Username, SignedInUser.Password);

            //TODO try to authenticate with stored credentials first
            if (_authenticationService.IsAuthenticated(authenticatedUser))
            {
                this._navigator.ShowContent();
            } 
            else
            {
                this._navigator.ShowSignInNavigatorView();
            }

            this._view = new ConfluenceToolWindowNavigator(this._navigator);
            base.Content = _view;

            this.ToolBar = new CommandID(Guids.guidConfluencePackage, Guids.ConfluenceToolbar);
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
    }
}
