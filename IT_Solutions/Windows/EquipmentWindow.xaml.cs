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
    /// Логика взаимодействия для EquipmentWindow.xaml
    /// </summary>
    public partial class EquipmentWindow : Window
    {
        RepManageDBEntities1 _entities = new RepManageDBEntities1();

        public EquipmentWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = _entities.Equipment.ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            homeWindow.Show();
            this.Close();
        }

        private void SendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmenttWindow addEquipmenttWindow = new AddEquipmenttWindow();
            addEquipmenttWindow.ShowDialog();
        }

        private void BtnEditRequest_Click(object sender, RoutedEventArgs e)
        {
            EditEquipmenttWindow equipmenttWindow = new EditEquipmenttWindow(_entities, sender, this);
            equipmenttWindow.ShowDialog();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredRequests = _entities.Equipment.Where(r =>
                r.Equipment_Name.ToString().Contains(searchText) ||
                r.Model.ToString().Contains(searchText) ||
                r.Serial_Number.ToLower().Contains(searchText)
            ).ToList();
            dataGrid.ItemsSource = filteredRequests;
        }
    }
}
