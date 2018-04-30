﻿using ConfluenceEX.Common;
using ConfluenceEX.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConfluenceEX.View
{
    /// <summary>
    /// Interaction logic for TestView.xaml
    /// </summary>
    public partial class ConnectView : UserControl, IHavePassword
    {

        private ConnectViewModel _viewModel;

        public ConnectView(SignInNavigatorViewModel parent)
        {
            InitializeComponent();

            _viewModel = new ConnectViewModel(SignedInUser.Username, SignedInUser.Password, parent);
            this.DataContext = _viewModel;
        }

        public System.Security.SecureString Password
        {
            get
            {
                return UserPassword.SecurePassword;
            }
        }
    }
}
