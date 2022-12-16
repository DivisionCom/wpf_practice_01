using Stroymateriali.ApplicationData;
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

namespace Stroymateriali.PageAdmin
{
    /// <summary>
    /// Interaction logic for PageMenuAdmin.xaml
    /// </summary>
    public partial class PageMenuAdmin : Page
    {
        public PageMenuAdmin()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageMain.PageCatalogue());
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Flag.flag = null;
            AppFrame.frameMain.Navigate(new PageMain.PageLogin());
        }

        private void btn_gotomaterial_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageMaterials());
        }

        private void btn_gotousers_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageUsers());
        }
    }
}
