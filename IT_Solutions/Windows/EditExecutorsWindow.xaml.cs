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
using System.Windows.Shapes;

namespace IT_Solutions.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditExecutorsWindow.xaml
    /// </summary>
    public partial class EditExecutorsWindow : Window
    {
        private RepManageDBEntities1 _context;
        private Users _user;
        private ExecutorsWindow _executorsWindow;


        public EditExecutorsWindow(RepManageDBEntities1 context, object o, ExecutorsWindow executorsWindow)
        {
            _context = context;
            InitializeComponent();

            _user = (o as Button).DataContext as Users;
            TextBoxUsername.Text = _user.Login.ToString();
            PasswordBoxPassword.Text = _user.Password.ToString();
            TextBoxLastName.Text = _user.Last_Name.ToString();
            TextBoxFirstName.Text = _user.Name.ToString();
            TextBoxMiddleName.Text = _user.Middle_Name.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Обновляем данные объекта Requests
                _user.Login = TextBoxUsername.Text;
                _user.Password = PasswordBoxPassword.Text;
                _user.Last_Name = TextBoxLastName.Text;
                _user.Name = TextBoxFirstName.Text;
                _user.Middle_Name = TextBoxMiddleName.Text;

                // Сохраняем изменения в базе данных
                _context.SaveChanges();

                // Если все успешно, закрываем окно
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }
    }
}
