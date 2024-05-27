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
    /// Логика взаимодействия для ExecutorsWindow.xaml
    /// </summary>
    public partial class ExecutorsWindow : Window
    {
        RepManageDBEntities1 _entities = new RepManageDBEntities1();

        public ExecutorsWindow()
        {
            InitializeComponent();

            dataGrid.ItemsSource = _entities.Users.ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            homeWindow.Show();
            this.Close();
        }

        private void SendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            AddRequestWindow addRequestWindow = new AddRequestWindow();
            addRequestWindow.ShowDialog();
        }

        private void BtnEditRequest_Click(object sender, RoutedEventArgs e)
        {
            EditExecutorsWindow editExecutorsWindow = new EditExecutorsWindow(_entities, sender, this);
            editExecutorsWindow.ShowDialog();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredRequests = _entities.Users.Where(r =>
                r.Last_Name.ToString().Contains(searchText) ||
                r.Name.ToString().Contains(searchText) ||
                r.Middle_Name.ToLower().Contains(searchText)
            ).ToList();
            dataGrid.ItemsSource = filteredRequests;
        }
    }
}
