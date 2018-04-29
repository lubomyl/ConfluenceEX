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
    public partial class ConnectView : UserControl
    {

        private ConnectViewModel _viewModel;

        public ConnectView()
        {
            InitializeComponent();

            _viewModel = new ConnectViewModel("lubomyl@gmail.com", "Ostrava111");
            this.DataContext = _viewModel;
        }
    }
}
