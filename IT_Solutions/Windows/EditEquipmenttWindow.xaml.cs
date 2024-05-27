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
    /// Логика взаимодействия для EditEquipmenttWindow.xaml
    /// </summary>
    public partial class EditEquipmenttWindow : Window
    {
        private RepManageDBEntities1 _context;
        private Equipment _equipment;
        private EquipmentWindow _equipmentWindow;


        public EditEquipmenttWindow(RepManageDBEntities1 context, object o, EquipmentWindow equipmenttWindow)
        {
            _context = context;
            InitializeComponent();

            _equipment = (o as Button).DataContext as Equipment;
            TextBoxName.Text = _equipment.Equipment_Name.ToString();
            TextBoxModel.Text = _equipment.Model.ToString();
            TextBoxNumber.Text = _equipment.Serial_Number.ToString();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Обновляем данные объекта Requests
                _equipment.Equipment_Name = TextBoxName.Text;
                _equipment.Model = TextBoxModel.Text;
                _equipment.Serial_Number = TextBoxNumber.Text;
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
