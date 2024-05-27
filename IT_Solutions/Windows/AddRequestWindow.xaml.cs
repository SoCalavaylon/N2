using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IT_Solutions.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddRequestWindow.xaml
    /// </summary>
    public partial class AddRequestWindow : Window
    {
        RepManageDBEntities1 _entities = new RepManageDBEntities1();

        public AddRequestWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _entities.Users.Add(new Users()
                {
                    Login = TextBoxUsername.Text,
                    Password = PasswordBoxPassword.Text,
                    Last_Name = TextBoxLastName.Text,
                    Name = TextBoxFirstName.Text,
                    Middle_Name = TextBoxMiddleName.Text,
                    Role = "executor"
                });
                _entities.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }
    }
}
