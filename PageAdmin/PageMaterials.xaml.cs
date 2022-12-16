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
    /// Interaction logic for PageMaterials.xaml
    /// </summary>
    public partial class PageMaterials : Page
    {
        Materials material = new Materials();
        public PageMaterials()
        {
            InitializeComponent();
            lv_materials.ItemsSource = Entities.GetContext().Materials.ToList();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageAddMaterial(material, false, material));
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var materialObj = lv_materials.SelectedItems.Cast<Materials>().ToList().ElementAt(0);
                Materials materials = new Materials()
                {
                    materials_name = materialObj.materials_name,
                    materials_units = materialObj.materials_units,
                    materials_count = materialObj.materials_count,
                    materials_category = materialObj.materials_category,
                    materials_price = materialObj.materials_price,
                    materials_providers = materialObj.materials_providers,
                    materials_makers = materialObj.materials_makers,
                    materials_available = materialObj.materials_available,
                    materials_description = materialObj.materials_description,
                    materials_photo = materialObj.materials_photo
                };
                var materialObj2 = lv_materials.SelectedItems.Cast<Materials>().ToList();
                try
                {
                    AppFrame.frameMain.Navigate(new PageAddMaterial(materialObj, true, materialObj));
                    Entities.GetContext().SaveChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
            catch
            {
                MessageBox.Show("Ошибка! Выберите товар", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить материал?", "Предупреждение",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var userObj = lv_materials.SelectedItems.Cast<Materials>().ToList();
                try
                {
                    Entities.GetContext().Materials.RemoveRange(userObj);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Материал успешно удалён!");

                    lv_materials.ItemsSource = Entities.GetContext().Materials.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageMenuAdmin());
        }
    }
}
