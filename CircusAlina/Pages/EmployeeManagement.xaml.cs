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
    /// Логика взаимодействия для EmployeeManagement.xaml
    /// </summary>
    public partial class EmployeeManagement : Page
    {
        private CircusContext _context;
        public EmployeeManagement()
        {
            InitializeComponent();
            _context = new CircusContext();
            LoadEmployees();
        }
        private void LoadEmployees()
        {
           
            EmployeeDataGrid.ItemsSource = _context.Employees.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем диалоговое окно для добавления нового сотрудника
            var addEditWindow = new AddEditEmployee();
            if (addEditWindow.ShowDialog() == true)
            {
                var newEmployee = addEditWindow.Employee;
                _context.Employees.Add(newEmployee);
                _context.SaveChanges();
                LoadEmployees();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранного сотрудника
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                var addEditWindow = new AddEditEmployee(selectedEmployee);
                if (addEditWindow.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите сотрудника для редактирования.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этого сотрудника?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Employees.Remove(selectedEmployee);
                    _context.SaveChanges();
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите сотрудника для удаления.");
            }
        }
    }
}
