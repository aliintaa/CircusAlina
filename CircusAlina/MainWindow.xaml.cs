using CircusAlina.Pages;
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

namespace CircusAlina
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void EmployeeManagement_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new EmployeeManagement();
        }

        private void RepertoireManagement_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new RepertoireManagement();
        }

        private void TicketSales_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TicketSales();
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Reports();
        }

        private void InventoryManagement_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new InventoryManagement();
        }
    }
}
