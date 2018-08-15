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
    public class SignInNavigatorViewModel : ViewModelBase
    {

        private object _selectedView;
        private ConfluenceToolWindow _parent;

        private AfterSignInView _afterSignInView;
        private BeforeSignInView _beforeSignInView;

        public SignInNavigatorViewModel(ConfluenceToolWindow parent)
        {
            this._parent = parent;
            this.ShowBeforeSignIn();
        }

        public void ShowAfterSignIn()
        {
            _parent.Caption = "Confluence - Signed-in";

            if (this._afterSignInView == null)
            {
                this._afterSignInView = new AfterSignInView(this);
                SelectedView = this._afterSignInView;
            }
            else
            {
                SelectedView = _afterSignInView;
            }

            this.EnableCommands(true);
        }

        public void ShowBeforeSignIn()
        {
            this.EnableCommands(false);
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
    
        }

        //TODO 2 - find better solution how to initialize commands on one place and expose them in public
        private void EnableCommands(bool enable)
        {
            OleMenuCommandService service = ConfluencePackage.Mcs;

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
