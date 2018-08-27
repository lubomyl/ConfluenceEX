using ConfluenceEX.Command;
using ConfluenceEX.Common;
using ConfluenceRestClient.Model;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Service.Implementation;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceEX.ViewModel
{
    class AfterSignInViewModel : ViewModelBase
    {

        private ConfluenceToolWindowNavigatorViewModel _parent;

        private WritableSettingsStore _userSettingsStore;

        public DelegateCommand SignOutCommand { get; private set; }

        public AfterSignInViewModel(ConfluenceToolWindowNavigatorViewModel parent, string username, string password)
        {
            this._parent = parent;

            SettingsManager settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
            this._userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            this.SignOutCommand = new DelegateCommand(SignOut);
        }

        private void SignOut(object parameter)
        {
            this.DeletePropertyFromUserSettings("AccessToken");
            this.DeletePropertyFromUserSettings("AccessTokenSecret");

            this._parent.ShowBeforeSignIn();
        }

        //TODO refactor extract to Helper class
        private void DeletePropertyFromUserSettings(string propertyName)
        {
            this._userSettingsStore.DeleteProperty("External Tools", propertyName);
        }

    }

}