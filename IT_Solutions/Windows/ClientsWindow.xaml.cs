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
    /// Логика взаимодействия для ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        RepManageDBEntities1 _entities = new RepManageDBEntities1();

        public ClientsWindow()
        {
            InitializeComponent();

            dataGrid.ItemsSource = _entities.Clients.ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            homeWindow.Show();
            this.Close();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredRequests = _entities.Clients.Where(r =>
                r.Last_Name.ToString().Contains(searchText) ||
                r.First_Name.ToString().Contains(searchText) ||
                r.Middle_Name.ToLower().Contains(searchText) ||
                r.Email.ToLower().Contains(searchText) ||
                r.Phone_Number.ToLower().Contains(searchText)
            ).ToList();
            dataGrid.ItemsSource = filteredRequests;
        }
    }
}
