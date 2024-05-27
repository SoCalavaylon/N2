using IT_Solutions.Windows;
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

namespace IT_Solutions
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RepManageDBEntities1 _entities = new RepManageDBEntities1();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            string userLogin = "";
            var userPass = "";
            if (LoginTxt.Text != "" || LoginTxt.Text != null)
            {
                userLogin = LoginTxt.Text;
                if (PassTxtMask.Password != "" || PassTxtMask.Password != null)
                {
                    userPass = ShowPass.IsChecked.Value ? PassTxtUnmask.Text : PassTxtMask.Password;
                }
                else
                {
                    MessageBox.Show("Проверте поле пароля");
                }
            }
            else
            {
                MessageBox.Show("Проверте поле логина");
            }


            Users user = _entities.Users.FirstOrDefault(u => u.Login == userLogin && u.Password == userPass);
            if (user != null)
            {
                if (user.Role == "executor")
                {
                    
                    Window1 window1 = new Window1(user);
                    window1.Show();
                }
                else
                {
                    HomeWindow homeWindow = new HomeWindow();
                    homeWindow.Show();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден!");
            };
        }

        private void ShowPass_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox.IsChecked.Value)
            {
                PassTxtUnmask.Text = PassTxtMask.Password;
                PassTxtUnmask.Visibility = Visibility.Visible;
                PassTxtMask.Visibility = Visibility.Hidden;
            }
            else
            {
                PassTxtMask.Password = PassTxtUnmask.Text;
                PassTxtUnmask.Visibility = Visibility.Hidden;
                PassTxtMask.Visibility = Visibility.Visible;
            }
        }
    }
}
