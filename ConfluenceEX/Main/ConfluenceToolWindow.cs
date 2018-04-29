using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using ConfluenceEX.Main;
using ConfluenceEX.View;
using System.ComponentModel.Design;
using ConfluenceEX.ViewModel;

namespace ConfluenceEX
{

    [Guid(Guids.guidConfluenceToolWindow)]
    public class ConfluenceToolWindow : ToolWindowPane
    {
        private readonly object _view;
        private NavigationViewModel _navigation { get; set; }

        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public ConfluenceToolWindow() : base(null)
        {
            this.Caption = Resources.ConflueceToolWindowTitle;

            this._navigation = new NavigationViewModel(this);

            //TODO authenticate with stored credentials
            //if not null and authentication ok -> show content
            //else
            //show login form
            this._navigation.ShowContent();

            this._view = new ConfluenceToolWindowNavigator(this._navigation);
            base.Content = _view;

            this.ToolBar = new CommandID(Guids.guidConfluencePackage, Guids.ConfluenceToolbar);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public NavigationViewModel Navigation
        {
            get { return this._navigation; }
            set { this._navigation = value; }
        }
    }
}
