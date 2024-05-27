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
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        RepManageDBEntities1 _entities = new RepManageDBEntities1();

        public HomeWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = _entities.Requests.ToList();
            ProblemTypeComboBox.SelectedIndex = 0;
            StatusComboBox.SelectedIndex = 0;
            RefreshProduct();
        }

        public void RefreshProduct()
        {
            dataGrid.ItemsSource = _entities.Requests.ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredRequests = _entities.Requests.Where(r =>
                r.Request_Number.ToString().Contains(searchText) ||
                r.Date_Added.ToString().Contains(searchText) ||
                r.Equipment.Equipment_Name.ToLower().Contains(searchText) ||
                r.Issue_Type.ToLower().Contains(searchText) ||
                r.Issue_Description.ToLower().Contains(searchText) ||
                r.Request_Status.ToLower().Contains(searchText) ||
                r.Clients.Last_Name.ToLower().Contains(searchText) ||
                r.Users.Last_Name.ToLower().Contains(searchText) ||
                r.Response.ToLower().Contains(searchText)
            ).ToList();
            dataGrid.ItemsSource = filteredRequests;

        }
       
        private void ProblemTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)ProblemTypeComboBox.SelectedItem;

            if (selectedItem != null)
            {
                string selectedType = selectedItem.Content.ToString();
                var allRequests = _entities.Requests.ToList();
                List<Requests> filteredRequests;

                switch (selectedType)
                {
                    case "Hardware":
                        filteredRequests = allRequests.Where(r => r.Issue_Type == "Hardware").ToList();
                        break;
                    case "Software":
                        filteredRequests = allRequests.Where(r => r.Issue_Type == "Software").ToList();
                        break;
                    case "Все":
                    default:
                        filteredRequests = allRequests;
                        break;
                }

                dataGrid.ItemsSource = filteredRequests;
            }
        }
       
        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)StatusComboBox.SelectedItem;

            if (selectedItem != null)
            {
                string selectedStatus = selectedItem.Content.ToString();
                var allRequests = _entities.Requests.ToList();
                List<Requests> filteredRequests;

                switch (selectedStatus)
                {
                    case "В работе":
                        filteredRequests = allRequests.Where(r => r.Request_Status == "В работе").ToList();
                        break;
                    case "В ожидании":
                        filteredRequests = allRequests.Where(r => r.Request_Status == "В ожидании").ToList();
                        break;
                    case "Выполнено":
                        filteredRequests = allRequests.Where(r => r.Request_Status == "Выполнено").ToList();
                        break;
                    case "Все":
                    default:
                        filteredRequests = allRequests;
                        break;
                }

                dataGrid.ItemsSource = filteredRequests;
            }
        }

        private void BtnEditRequest_Click(object sender, RoutedEventArgs e)
        {
            EditRequestsWindow editRequestsWindow = new EditRequestsWindow(_entities, sender, this);
            editRequestsWindow.ShowDialog();
        }

        private void ExecutorsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ExecutorsWindow executorsWindow = new ExecutorsWindow();
            executorsWindow.Show();
            this.Close();
        }

        private void EquipmentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EquipmentWindow equipmentWindow = new EquipmentWindow();
            equipmentWindow.Show();
            this.Close();
        }

        private void ClientsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            NewRequestsWindow newRequestsWindow = new NewRequestsWindow();
            newRequestsWindow.ShowDialog();
        }
    }
}
