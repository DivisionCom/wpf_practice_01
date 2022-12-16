using Microsoft.Win32;
using Stroymateriali.ApplicationData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PageAddMaterial.xaml
    /// </summary>
    public partial class PageAddMaterial : Page
    {
        private Materials _materials = new Materials();
        bool shouldUpdate;
        Materials material;
        Materials newMaterial;
        Materials updateMaterial;
        public PageAddMaterial(Materials material, bool shouldUpdate, Materials updatematerial = null)
        {
            this.shouldUpdate = shouldUpdate;
            this.material = material;
            this.updateMaterial = updatematerial;

            InitializeComponent();

            txb_name.Focus();
            foreach (var item in AppConnect.modelOdb.Category.ToList())
            {
                cb_type.Items.Add(item.category_name);
            }
            foreach (var item in AppConnect.modelOdb.Providers.ToList())
            {
                cb_provider.Items.Add(item.providers_name);
            }
            foreach (var item in AppConnect.modelOdb.Makers.ToList())
            {
                cb_maker.Items.Add(item.makers_name);
            }

            _materials = material;

            if (material == null)
            {
                cb_type.SelectedIndex = 0;
                cb_provider.SelectedIndex = 0;
                cb_maker.SelectedIndex = 0;
            }

            if (shouldUpdate)
            {
                txb_name.Text = updateMaterial.materials_name;
                txb_price.Text = updateMaterial.materials_price.ToString();
                txb_amount.Text = updateMaterial.materials_count.ToString();
                txb_desc.Text = updateMaterial.materials_description;

                cb_provider.SelectedItem = AppConnect.modelOdb.Providers.FirstOrDefault
                    (x => x.id_providers == _materials.materials_providers).providers_name;

                cb_maker.SelectedItem = AppConnect.modelOdb.Makers.FirstOrDefault
                    (x => x.id_makers == _materials.materials_makers).makers_name;

                cb_type.Text = AppConnect.modelOdb.Category.FirstOrDefault
                    (x => x.id_category == _materials.materials_category).category_name;

                FindFilterCategoryMat();
                FindFilterMakerMat();
                FindFilterProviderMat();
            }
            else
            {
                newMaterial = new Materials();
            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            FindFilterCategoryMat();
            FindFilterMakerMat();
            FindFilterProviderMat();

            if (shouldUpdate)
            {
                localUpdateMaterials(material);
                updateMaterials();
                MessageBox.Show("Изменения сохранены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frameMain.Navigate(new PageMaterials());
            }
            else
            {
                localUpdateMaterials(newMaterial);
                addMaterial();
                MessageBox.Show("Товар добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frameMain.Navigate(new PageMaterials());
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageMaterials());
        }

        private void btn_image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.ShowDialog();

            try
            {
                string directory;
                directory = dialog.FileName.Substring(dialog.FileName.LastIndexOf('\\'), dialog.FileName.Length - dialog.FileName.Substring(0, dialog.FileName.LastIndexOf('\\')).Length);

                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\img\\" + directory))
                {
                    File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\img\\" + directory);
                }

                File.Copy(dialog.FileName, System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\" + directory);
                _materials.materials_photo = dialog.SafeFileName;
                AppConnect.modelOdb.SaveChanges();
                DataContext = null;
                DataContext = _materials;
            }
            catch
            {
                _materials.materials_photo = "picture.png";
                AppConnect.modelOdb.SaveChanges();
                DataContext = null;
                DataContext = _materials;
            }
        }

        private void txb_amount_SelectionChanged(object sender, RoutedEventArgs e)
        {
            txb_amount.Text = Regex.Replace(txb_amount.Text, "[^0-9]", "");
        }

        private void txb_price_SelectionChanged(object sender, RoutedEventArgs e)
        {
            txb_price.Text = Regex.Replace(txb_price.Text, "[^0-9.]", "");
        }

        private void FindFilterCategoryMat()
        {
            var _category = AppConnect.modelOdb.Category.FirstOrDefault(x => x.category_name == cb_type.SelectedItem.ToString());
            if (_category == null)
            {
                MessageBox.Show("Выберите элемент", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _materials.materials_category = _category.id_category;
            }
        }

        private void FindFilterProviderMat()
        {
            var _provider = AppConnect.modelOdb.Providers.FirstOrDefault(x => x.providers_name == cb_provider.SelectedItem.ToString());
            if (_provider == null)
            {
                MessageBox.Show("Выберите элемент", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _materials.materials_providers = _provider.id_providers;
            }
        }
        private void FindFilterMakerMat()
        {
            var _maker = AppConnect.modelOdb.Makers.FirstOrDefault(x => x.makers_name == cb_maker.SelectedItem.ToString());
            if (_maker == null)
            {
                MessageBox.Show("Выберите элемент", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _materials.materials_makers = _maker.id_makers;
            }
        }

        private void addMaterial()
        {
            Entities.GetContext().Materials.Add(newMaterial);
            Entities.GetContext().SaveChanges();
        }

        private void updateMaterials()
        {
            Materials updatedMaterial = Entities.GetContext().Materials.Where(x => x.id_materials == updateMaterial.id_materials).SingleOrDefault();
            updatedMaterial.materials_name = updateMaterial.materials_name;
            updatedMaterial.materials_count = updateMaterial.materials_count;
            updatedMaterial.materials_price = updateMaterial.materials_price;
            updatedMaterial.materials_description = updateMaterial.materials_description;
            updatedMaterial.materials_units = updateMaterial.materials_units;
            updatedMaterial.materials_category = _materials.materials_category;
            updatedMaterial.materials_makers = _materials.materials_makers;
            updatedMaterial.materials_providers = _materials.materials_providers;
            updatedMaterial.materials_photo = _materials.materials_photo;
        }

        private void localUpdateMaterials(Materials material)
        {
            material.materials_name = txb_name.Text;
            material.materials_count = Convert.ToInt32(txb_amount.Text);
            material.materials_price = Convert.ToDouble(txb_price.Text);
            material.materials_description = txb_desc.Text;
            material.materials_units = "шт.";
            material.materials_category = _materials.materials_category;
            material.materials_makers = _materials.materials_makers;
            material.materials_providers = _materials.materials_providers;
            material.materials_photo = _materials.materials_photo;
        }
    }
}
