using System.Windows.Controls;
using ConfluenceEX.ViewModel;
using ConfluenceEX.Common;

namespace ConfluenceEX.View
{
    /// <summary>
    /// Interaction logic for ContentListView.xaml
    /// </summary>
    public partial class SpaceListView : UserControl
    {

        private SpaceListViewModel _viewModel;

        public SpaceListView()
        {
            InitializeComponent();

            _viewModel = new SpaceListViewModel(SignedInUser.Username, SignedInUser.Password);
            this.DataContext = _viewModel;
        }
    }
}
