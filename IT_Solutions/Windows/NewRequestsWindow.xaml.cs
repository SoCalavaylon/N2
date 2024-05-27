using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

namespace IT_Solutions.Windows
{
    /// <summary>
    /// Логика взаимодействия для NewRequestsWindow.xaml
    /// </summary>
    public partial class NewRequestsWindow : Window
    {
        RepManageDBEntities1 _entities = new RepManageDBEntities1();
        private readonly List<string> _issueTypes;
        private readonly List<string> _requestStatuses;
        private List<string> _assignedUsers;

        private string newRequestNumber;

        public NewRequestsWindow()
        {
            InitializeComponent();

            _issueTypes = new List<string>() { "Hardware", "Software"};
            ComboBoxIssueType.ItemsSource = _issueTypes;

            _requestStatuses = new List<string>() { "В работе", "В ожидании", "Выполнено" };
            ComboBoxRequestStatus.ItemsSource = _requestStatuses;

            _assignedUsers = _entities.Users.Select(u => u.Last_Name).ToList();
            ComboBoxAssignedUser.ItemsSource = _assignedUsers;

            var users = _entities.Users.ToList();
            var usertList = users.Select(c => $"{c.Last_Name} {c.Name} {c.Middle_Name}").ToList();
            ComboBoxAssignedUser.ItemsSource = usertList;

            var equipmentList = _entities.Equipment.Select(e => e.Equipment_Name).ToList();
            TextBoxEquipment.ItemsSource = equipmentList;

            var clients = _entities.Clients.ToList();
            var clientList = clients.Select(c => $"{c.Last_Name} {c.First_Name} {c.Middle_Name}").ToList();
            TextBoxClient.ItemsSource = clientList;

            int maxRequestNumber = _entities.Requests.Any() ? _entities.Requests.Max(r => r.Request_Number) : 0;
            maxRequestNumber++;
            newRequestNumber = maxRequestNumber.ToString();



            TextBoxRequestNumber.Text = newRequestNumber;
            DatePickerAdded.SelectedDate = DateTime.Now;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedUserName = ComboBoxAssignedUser.SelectedItem.ToString();
            var assignedUser = _entities.Users.FirstOrDefault(u => u.Last_Name + " " + u.Name + " " + u.Middle_Name == selectedUserName);

            string selectedEquipmentName = TextBoxEquipment.SelectedItem.ToString();
            var selectedEquipment = _entities.Equipment.FirstOrDefault(eq => eq.Equipment_Name == selectedEquipmentName);

            string selectedClientName = TextBoxClient.SelectedItem.ToString();
            var selectedClient = _entities.Clients.FirstOrDefault(u => u.Last_Name + " " + u.First_Name + " " + u.Middle_Name == selectedClientName);


            try
            {
                _entities.Requests.Add(new Requests()
                {
                    Request_Number = int.Parse(newRequestNumber),
                    Date_Added = DateTime.Now,
                    Equipment_ID = selectedEquipment.Equipment_ID,
                    Issue_Type = ComboBoxIssueType.SelectedItem?.ToString(),
                    Issue_Description = TextBoxIssueDescription.Text,
                    Client_ID = selectedClient.Client_ID,
                    Request_Status = ComboBoxRequestStatus.SelectedItem?.ToString(),
                    Response_Date = DatePickerResponse.SelectedDate,
                    Response = TextBoxResponse.Text,
                    User_ID = assignedUser.User_ID
                });
                _entities.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении заявки: {ex.Message}");
            }
        }
    }
}
