using System.Windows.Controls;
using ConfluenceEX.ViewModel;
using ConfluenceEX.Common;

namespace ConfluenceEX.View
{
    /// <summary>
    /// Interaction logic for ContentListView.xaml
    /// </summary>
    public partial class ContentListView : UserControl
    {

        private ContentListViewModel _viewModel;

        public ContentListView()
        {
            InitializeComponent();

            _viewModel = new ContentListViewModel(SignedInUser.Username, SignedInUser.Password);
            this.DataContext = _viewModel;
        }
    }
}
