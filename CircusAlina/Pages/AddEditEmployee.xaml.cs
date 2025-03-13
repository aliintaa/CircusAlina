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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CircusAlina.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditEmployee.xaml
    /// </summary>
    public partial class AddEditEmployee : Page
    {
        public AddEditEmployee()
        {
            InitializeComponent();
            Employee = new Employee();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Employee.Name = NameTextBox.Text;
            Employee.Role = RoleTextBox.Text;
            if (int.TryParse(AgeTextBox.Text, out int age))
            {
                Employee.Age = age;
                DialogResult = true; // Закрываем окно с результатом "ОК"
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректный возраст.");
            }
        }
    }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Закрываем окно с результатом "Отмена"
            Close();
        }
    }
}

