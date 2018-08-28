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

        public SpaceListView(ConfluenceToolWindowNavigatorViewModel parent)
        {
            InitializeComponent();

            this._viewModel = new SpaceListViewModel(parent);
            this.DataContext = this._viewModel;
        }
    }
}
