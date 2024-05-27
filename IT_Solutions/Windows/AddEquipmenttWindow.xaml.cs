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
    /// Логика взаимодействия для AddEquipmenttWindow.xaml
    /// </summary>
    public partial class AddEquipmenttWindow : Window
    {
        RepManageDBEntities1 _entities = new RepManageDBEntities1();

        public AddEquipmenttWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _entities.Equipment.Add(new Equipment()
                {
                    Equipment_Name = TextBoxName.Text,
                    Model = TextBoxModel.Text,
                    Serial_Number = TextBoxNumber.Text
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
