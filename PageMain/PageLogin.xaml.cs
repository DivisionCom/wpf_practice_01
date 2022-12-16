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
    /// Interaction logic for PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public bool fails = true; 
        public PageLogin()
        {
            InitializeComponent();
            capt.Text = "---------------->";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (capIn.Text == capt.Text && capIn.Text != "")
            {
                try
                {
                    var userObj = AppConnect.modelOdb.Users.FirstOrDefault(x => x.users_login == txblogin.Text && x.users_password == psbPassword.Password);
                    if (userObj == null)
                    {
                        MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        Flag.role = userObj.users_role;
                        switch (Flag.role)
                        {
                            case 1:
                                Flag.flag = userObj.users_login;
                                MessageBox.Show("Здравствуйте, Администратор " + userObj.users_firstname + "!",
                                "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                                AppFrame.frameMain.Navigate(new PageCatalogue());
                                break;
                            case 2:
                                Flag.flag = userObj.users_login;
                                MessageBox.Show("Здравствуйте, Пользователь " + userObj.users_firstname + "!",
                                "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                                AppFrame.frameMain.Navigate(new PageCatalogue());
                                break;
                            case 3:
                                Flag.flag = userObj.users_login;
                                MessageBox.Show("Здравствуйте, Менеджер " + userObj.users_firstname + "!",
                                "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                                AppFrame.frameMain.Navigate(new PageCatalogue());
                                break;
                            default:
                                MessageBox.Show("Данные не обнаружены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                                break;
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Ошибка " + Ex.Message.ToString() + "Критическая работа приложения",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (capIn.Text != capt.Text)
            {
                MessageBox.Show("Капча введена неправильно!", "Ошибка при авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Введите капчу!", "Ошибка при авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageCreateAcc());
        }

        private void GetCap_Click(object sender, RoutedEventArgs e)
        {
            String allowchar = " ";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = "";
            string temp = " ";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            capt.Text = pwd;
        }

        private void btn_guest_Click(object sender, RoutedEventArgs e)
        {
            Flag.flag = "Гость";
            Flag.role = 4;
            AppFrame.frameMain.Navigate(new PageCatalogue());
        }
    }
}
