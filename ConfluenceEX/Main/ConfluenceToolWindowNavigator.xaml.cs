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

namespace ConfluenceEX.Main
{
    /// <summary>
    /// Interaction logic for ConfluenceToolWindowNAvigator.xaml
    /// </summary>
    public partial class ConfluenceToolWindowNavigator : UserControl
    {
        public ConfluenceToolWindowNavigator(NavigationViewModel navigation)
        {
            InitializeComponent();

            this.DataContext = navigation;
        }
    }
}
