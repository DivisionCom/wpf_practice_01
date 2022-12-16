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
    /// Interaction logic for PageUsers.xaml
    /// </summary>
    public partial class PageUsers : Page
    {
        Users users = new Users();
        public PageUsers()
        {
            InitializeComponent();
            lv_users.ItemsSource = Entities.GetContext().Users.ToList();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageAddUser(users, false, users));
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = lv_users.SelectedItems.Cast<Users>().ToList().ElementAt(0);
                Users users = new Users()
                {
                    id_users = userObj.id_users,
                    users_firstname = userObj.users_firstname,
                    users_middlename = userObj.users_middlename,
                    users_lastname = userObj.users_lastname,
                    users_datebirth = userObj.users_datebirth,
                    users_mail = userObj.users_mail,
                    users_phone = userObj.users_phone,
                    users_login = userObj.users_login,
                    users_password = userObj.users_password,
                    users_role = userObj.users_role
                };
                AppFrame.frameMain.Navigate(new PageAddUser(userObj, true, userObj));
            }
            catch
            {
                MessageBox.Show("Выберите пользователя", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var userObj1 = lv_users.SelectedItems.Cast<Users>().ToList().ElementAt(0);
                if (Flag.flag == userObj1.users_login)
                {
                    MessageBox.Show("Вы не можете этого сделать!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    try
                    {
                        var userObj = lv_users.SelectedItems.Cast<Users>().ToList();
                        Entities.GetContext().Users.RemoveRange(userObj);
                        Entities.GetContext().SaveChanges();
                        MessageBox.Show("Пользователь успешно удалён!");

                        lv_users.ItemsSource = Entities.GetContext().Users.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageMenuAdmin());
        }
    }
}
