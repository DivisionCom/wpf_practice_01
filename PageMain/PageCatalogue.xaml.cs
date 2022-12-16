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

namespace Stroymateriali.PageMain
{
    /// <summary>
    /// Interaction logic for PageCatalogue.xaml
    /// </summary>
    public partial class PageCatalogue : Page
    {
        Materials[] FindMaterials()
        {
            List<Materials> materials = AppConnect.modelOdb.Materials.ToList();
            var materialsAll = materials;
            if (txb_search != null)
            {
                materials = materials.Where(x => x.materials_name.ToLower().Contains(txb_search.Text.ToLower())).ToList();
                switch (cb_price.SelectedIndex)
                {
                    case 1:
                        materials = materials.OrderBy(x => x.materials_price).ToList();
                        break;
                    case 2:
                        materials = materials.OrderByDescending(x => x.materials_price).ToList();
                        break;
                }
            }

            if (cb_self.SelectedIndex > 0)
            {
                materials = materials.Where(x => x.Makers.makers_name == cb_self.SelectedItem.ToString()).ToList();
            }

            if (materials.Count > 0)
            {
                label_material.Text = "Найдено " + materials.Count.ToString() + " из " + materialsAll.Count.ToString();
            }
            else
            {
                label_material.Text = "Элементы не найдены";
            }

            return materials.ToArray();
        }
        private void SetPriceMaterials()
        {
            cb_price.Items.Add("Не выбрано");
            cb_price.Items.Add("Сначала дешевле");
            cb_price.Items.Add("Сначала дороже");
            cb_price.SelectedIndex = 0;
        }

        private void SetSelfMaterials()
        {
            cb_self.Items.Add("Производитель");
            foreach (var makers in AppConnect.modelOdb.Makers)
            {
                cb_self.Items.Add(makers.makers_name);
            }
            cb_self.SelectedIndex = 0;
        }

        public PageCatalogue()
        {
            InitializeComponent();
            SetPriceMaterials();
            SetSelfMaterials();
            lb_catalogue.ItemsSource = FindMaterials();

            switch (Flag.role)
            {
                case 1:
                    btn_adminpanel.Visibility = Visibility.Visible;
                    break;
                case 2:
                    btn_adminpanel.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    btn_adminpanel.Visibility = Visibility.Visible;
                    break;
                case 4:
                    btn_adminpanel.Visibility = Visibility.Hidden;
                    btn_exit.Content = "Войти";
                    break;
            }
        }

        private void grid_Loading(object sender, RoutedEventArgs e)
        {
            var CurrentMaterials = Entities.GetContext().Materials.ToList();
            lb_catalogue.ItemsSource = CurrentMaterials;
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Flag.flag = null;
            AppFrame.frameMain.Navigate(new PageLogin());
        }

        private void cb_price_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lb_catalogue.ItemsSource = FindMaterials();
        }
        
        private void cb_self_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lb_catalogue.ItemsSource = FindMaterials();
        }

        private void tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            lb_catalogue.ItemsSource = FindMaterials();
        }

        private void page_loading(object sender, RoutedEventArgs e)
        {
            AppConnect.modelOdb.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            lb_catalogue.ItemsSource = FindMaterials();
        }

        private void btn_adminpanel_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageAdmin.PageMenuAdmin());
        }
    }
}
