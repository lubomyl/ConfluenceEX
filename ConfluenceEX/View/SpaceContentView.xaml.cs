using System.Windows.Controls;
using ConfluenceEX.ViewModel;
using ConfluenceEX.Common;

namespace ConfluenceEX.View
{
    /// <summary>
    /// Interaction logic for SpaceContentView.xaml
    /// </summary>
    public partial class SpaceContentView : UserControl
    {

        private SpaceContentViewModel _viewModel;

        public SpaceContentView()
        {
            InitializeComponent();

            _viewModel = new SpaceContentViewModel(SignedInUser.Username, SignedInUser.Password);
            this.DataContext = _viewModel;
        }
    }
}
