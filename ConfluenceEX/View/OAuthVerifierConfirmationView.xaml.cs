using ConfluenceEX.ViewModel;
using DevDefined.OAuth.Framework;
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
    /// Interaction logic for OAuthVerifierConfirmationView.xaml
    /// </summary>
    public partial class OAuthVerifierConfirmationView : UserControl
    {

        private OAuthVerifierConfirmationViewModel _viewModel;

        public OAuthVerifierConfirmationView(ConfluenceToolWindowNavigatorViewModel parent, IToken requestToken)
        {
            InitializeComponent();

            _viewModel = new OAuthVerifierConfirmationViewModel(parent, requestToken);
            this.DataContext = _viewModel;
        }
    }
}
