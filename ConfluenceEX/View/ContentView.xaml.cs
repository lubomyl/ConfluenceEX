using System.Windows.Controls;
using ConfluenceEX.ViewModel;
using ConfluenceEX.Common;

namespace ConfluenceEX.View
{
    /// <summary>
    /// Interaction logic for SpaceContentView.xaml
    /// </summary>
    public partial class ContentView : UserControl
    {

        private ContentViewModel _viewModel;

        public ContentView()
        {
            InitializeComponent();

            _viewModel = new ContentViewModel(SignedInUser.Username, SignedInUser.Password);
            this.DataContext = _viewModel;
        }
    }
}
