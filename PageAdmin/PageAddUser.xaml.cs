using Stroymateriali.ApplicationData;
using Stroymateriali.PageMain;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for PageAddUser.xaml
    /// </summary>
    public partial class PageAddUser : Page
    {
        private Users person = new Users();
        bool shouldUpdate;
        Users user;
        Users newUser;
        Users updateUser;
        string cond = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
        int error = 0;
        public PageAddUser(Users user, bool shouldUpdate, Users updateuser = null)
        {
            this.shouldUpdate = shouldUpdate;
            this.user = user;
            this.updateUser = updateuser;

            InitializeComponent();

            txbFName.Focus();

            if (shouldUpdate)
            {
                txbFName.Text = updateUser.users_firstname;
                txbLName.Text = updateUser.users_lastname;
                txbMName.Text = updateUser.users_middlename;
                dpBirth.Text = updateUser.users_datebirth.ToString("dd/MM/yyyy");
                txbEmail.Text = updateUser.users_mail;
                txbPhone.Text = updateUser.users_phone;
                txbLogin.Text = updateUser.users_login;
                txbPass.Text = updateUser.users_password;
                psbPass.Password = updateUser.users_password;
                txb_role.Text = updateUser.users_role.ToString();
            }
            else
            {
                txb_role.Text = "2 || 3";
                newUser = new Users();
            }

        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            if (shouldUpdate)
            {
                localUpdateUsers(user);
                updateUsers();
                MessageBox.Show("Изменения сохранены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frameMain.Navigate(new PageUsers());
            }
            else
            {
                localUpdateUsers(newUser);
                addUser();
                MessageBox.Show("Пользователь добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frameMain.Navigate(new PageUsers());
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageUsers());
        }

        private void psbPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (psbPass.Password != txbPass.Text)
            {
                psbPass.Background = Brushes.LightCoral;
                psbPass.BorderBrush = Brushes.Red;
            }
            else
            {
                error++;
                CheckCreate(error);
                psbPass.Background = Brushes.LightGreen;
                psbPass.BorderBrush = Brushes.Green;
            }
        }

        private void txb_role_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txb_role.Text != "2" && txb_role.Text != "3")
            {
                txb_role.Background = Brushes.LightCoral;
                txb_role.BorderBrush = Brushes.Red;
            }
            else
            {
                error++;
                CheckCreate(error);
                txb_role.Background = Brushes.LightGreen;
                txb_role.BorderBrush = Brushes.Green;
            }
        }
        private void CheckCreate(int error)
        {
            if (error >= 7)
            {
                btn_create.IsEnabled = true;
            }
            else
            {
                btn_create.IsEnabled = false;
            }
        }
        private void addUser()
        {
            Entities.GetContext().Users.Add(newUser);
            Entities.GetContext().SaveChanges();
        }
        private void localUpdateUsers(Users user)
        {
            user.users_firstname = txbFName.Text;
            user.users_middlename = txbMName.Text;
            user.users_lastname = txbLName.Text;
            user.users_phone = txbPhone.Text;
            user.users_mail = txbEmail.Text;
            user.users_datebirth = dpBirth.SelectedDate.Value;
            user.users_login = txbLogin.Text;
            user.users_password = psbPass.Password;
            user.users_role = int.Parse(txb_role.Text);
        }
        private void updateUsers()
        {
            Users updatedUser = Entities.GetContext().Users.Where(x => x.id_users == updateUser.id_users).SingleOrDefault();
            updatedUser.users_firstname = updateUser.users_firstname;
            updatedUser.users_middlename = updateUser.users_middlename;
            updatedUser.users_lastname = updateUser.users_lastname;
            updatedUser.users_phone = updateUser.users_phone;
            updatedUser.users_mail = updateUser.users_mail;
            updatedUser.users_datebirth = updateUser.users_datebirth;
            updatedUser.users_login = updateUser.users_login;
            updatedUser.users_password = updateUser.users_password;
        }

        private void label_phone_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((txbPhone.Text.Length != 11 && !txbPhone.Text.StartsWith("7")) || string.IsNullOrEmpty(txbPhone.Text))
            {

                txbPhone.Background = Brushes.LightCoral;
                txbPhone.BorderBrush = Brushes.Red;
            }
            else
            {
                error++;
                CheckCreate(error);
                txbPhone.Background = Brushes.LightGreen;
                txbPhone.BorderBrush = Brushes.Green;
            }
        }

        private void txbEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(txbEmail.Text.ToString(), cond))
            {

                txbEmail.Background = Brushes.LightCoral;
                txbEmail.BorderBrush = Brushes.Red;

            }
            else
            {
                error++;
                CheckCreate(error);
                txbEmail.Background = Brushes.LightGreen;
                txbEmail.BorderBrush = Brushes.Green;

            }
        }

        private void txbPass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txbPass.Text.Length < 4 && !Regex.IsMatch(txbPass.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                text_password_warning.Content = "Пароль должен содержать не меньше 4 символов";
                txbPass.Background = Brushes.LightCoral;
                txbPass.BorderBrush = Brushes.Red;

            }
            else
            {
                error++;
                CheckCreate(error);
                txbPass.Background = Brushes.LightGreen;
                txbPass.BorderBrush = Brushes.Green;
            }
        }

        private void label_datebirth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpBirth.SelectedDate > DateTime.Now.AddYears(-18) || dpBirth.SelectedDate < DateTime.Now.AddYears(-99))
            {
                MessageBox.Show("Регистрироваться могут только люди страше 18 лет и младше 99", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                error++;
                CheckCreate(error);
                dpBirth.Background = Brushes.LightGreen;
                dpBirth.BorderBrush = Brushes.Green;

            }
        }

        private void label_firstname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            txbFName.Text = Regex.Replace(txbFName.Text, "[^a-zA-zА-Яа-я]", "");
        }

        private void label_lastname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            txbLName.Text = Regex.Replace(txbLName.Text, "[^a-zA-zА-Яа-я]", "");
        }

        private void label_middlename_SelectionChanged(object sender, RoutedEventArgs e)
        {
            txbMName.Text = Regex.Replace(txbMName.Text, "[^a-zA-zА-Яа-я]", "");
        }
        private void label_phone_SelectionChanged(object sender, RoutedEventArgs e)
        {
            txbPhone.Text = Regex.Replace(txbPhone.Text, "[^0-9+]", "");

        }

       

        private void txbLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txbLogin.Text.Length < 4 || string.IsNullOrEmpty(txbLogin.Text))
            {
                MessageBox.Show("Логин не может быть короче 4х символов", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                txbLogin.Background = Brushes.LightCoral;
                txbLogin.BorderBrush = Brushes.Red;

            }
            else
            {
                error++;
                CheckCreate(error);
                txbLogin.Background = Brushes.LightGreen;
                txbLogin.BorderBrush = Brushes.Green;

            }
        }

    }
}
