using System.Windows.Controls;
using ConfluenceEX.ViewModel;
using ConfluenceEX.Common;
using ConfluenceRestClient.Model;

namespace ConfluenceEX.View
{
    /// <summary>
    /// Interaction logic for SpaceContentView.xaml
    /// </summary>
    public partial class ContentListView : UserControl
    {

        private ContentListViewModel _viewModel;

        public ContentListView(Space space)
        {
            InitializeComponent();

            this._viewModel = new ContentListViewModel(space);
            this.DataContext = this._viewModel;
        }
    }
}
