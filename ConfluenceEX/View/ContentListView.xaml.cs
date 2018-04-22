using System.Windows.Controls;
using ConfluenceEX.ViewModel;

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
            _viewModel = new ContentListViewModel();
            this.DataContext = _viewModel;
        }
    }
}
