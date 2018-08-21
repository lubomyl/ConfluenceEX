﻿using ConfluenceEX.Command;
using ConfluenceEX.Common;
using ConfluenceRestClient.Model;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Service.Implementation;
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

        private string _username;
        private string _password;

        private bool _isAuthenticated;

        public DelegateCommand SignOutCommand { get; private set; }

        public AfterSignInViewModel(ConfluenceToolWindowNavigatorViewModel parent, string username, string password)
        {
            this._parent = parent;

            this.SignOutCommand = new DelegateCommand(SignOut);
        }

        private void SignOut(object parameter)
        {
            SignedInUser.Username = string.Empty;
            SignedInUser.Password = string.Empty;

            this._parent.ShowBeforeSignIn();
        }

    }

}