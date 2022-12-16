using Stroymateriali.ApplicationData;
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

namespace Stroymateriali.PageMain
{
    /// <summary>
    /// Interaction logic for PageCreateAcc.xaml
    /// </summary>
    public partial class PageCreateAcc : Page
    {
        string cond = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
        int error = 0;
        public PageCreateAcc()
        {
            InitializeComponent();
            txbFName.Focus();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(AppConnect.modelOdb.Users.Count(x => x.users_login == txbLogin.Text)>0)
            {
                MessageBox.Show("Пользователь с таким логином есть!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                Users userObj = new Users()
                {
                    users_lastname = txbLName.Text,
                    users_firstname = txbFName.Text,
                    users_middlename = txbMName.Text,
                    users_login = txbLogin.Text,
                    users_password = txbPass.Text,
                    users_mail = txbEmail.Text,
                    users_phone = txbPhone.Text,
                    users_datebirth = dpBirth.DisplayDate,
                    users_role = 2
                };
                AppConnect.modelOdb.Users.Add(userObj);
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
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
        private void CheckCreate(int error)
        {
            if (error >= 6)
            {
                btnCreate.IsEnabled = true;
            }
            else
            {
                btnCreate.IsEnabled = false;
            }
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
