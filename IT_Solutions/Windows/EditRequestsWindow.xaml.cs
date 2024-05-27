using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using System.Xml.Linq;

namespace IT_Solutions.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditRequestsWindow.xaml
    /// </summary>
    public partial class EditRequestsWindow : Window
    {
        private RepManageDBEntities1 _context;
        private Requests _requests;
        private HomeWindow _HomeWindow;

        private List<string> _issueTypes = new List<string> { "Hardware", "Software"};
        private List<string> _requestStatuses = new List<string> { "В работе", "В ожидании", "Выполнено" };
        private List<string> _assignedUsers;

        public EditRequestsWindow(RepManageDBEntities1 context, object o, HomeWindow HomeWindow)
        {
            _context = context;
            InitializeComponent();

            _requests = (o as Button).DataContext as Requests;
            TextBoxRequestNumber.Text = _requests.Request_Number.ToString();
            DatePickerAdded.SelectedDate = _requests.Date_Added;
            TextBoxEquipment.Text = _requests.Equipment.Equipment_Name;
            ComboBoxIssueType.ItemsSource = _issueTypes;
            ComboBoxIssueType.Text = _requests.Issue_Type;
            TextBoxIssueDescription.Text = _requests.Issue_Description;
            TextBoxClient.Text = _requests.Clients.Last_Name;
            ComboBoxRequestStatus.ItemsSource = _requestStatuses;
            ComboBoxRequestStatus.Text = _requests.Request_Status;
            DatePickerResponse.SelectedDate = _requests.Response_Date;
            TextBoxResponse.Text = _requests.Response;

            _assignedUsers = _context.Users.Select(u => u.Last_Name).ToList();
            ComboBoxAssignedUser.ItemsSource = _assignedUsers;
            ComboBoxAssignedUser.Text = _requests.Users.Last_Name;

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Обновляем данные объекта Requests
                _requests.Request_Number = int.Parse(TextBoxRequestNumber.Text);
                _requests.Date_Added = DatePickerAdded.SelectedDate;
                _requests.Issue_Type = ComboBoxIssueType.Text;
                _requests.Issue_Description = TextBoxIssueDescription.Text;
                _requests.Request_Status = ComboBoxRequestStatus.Text;
                _requests.Response_Date = DatePickerResponse.SelectedDate;
                _requests.Response = TextBoxResponse.Text;

                // Находим и обновляем связанные объекты Equipment, Client и User
                _requests.Equipment = _context.Equipment.FirstOrDefault(eq => eq.Equipment_Name == TextBoxEquipment.Text);
                _requests.Clients = _context.Clients.FirstOrDefault(c => c.Last_Name == TextBoxClient.Text);
                _requests.Users = _context.Users.FirstOrDefault(u => u.Last_Name == ComboBoxAssignedUser.Text);

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
