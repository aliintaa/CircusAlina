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
    /// Логика взаимодействия для Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        private CircusContext _context;
        public Reports()
        {
            InitializeComponent();
            _context = new CircusContext();
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedReportType = (ReportTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (string.IsNullOrEmpty(selectedReportType))
                {
                    MessageBox.Show("Пожалуйста, выберите тип отчета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                switch (selectedReportType)
                {
                    case "Отчет по продажам билетов":
                        GenerateTicketSalesReport();
                        break;
                    case "Отчет по репертуару":
                        GenerateRepertoireReport();
                        break;
                    case "Отчет по доходам":
                        GenerateIncomeReport();
                        break;
                    default:
                        MessageBox.Show("Неизвестный тип отчета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при генерации отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GenerateTicketSalesReport()
        {
            var ticketSalesReport = _context.TicketSales
                .Select(ts => new
                {
                    ts.Id,
                    ShowTitle = ts.Show.Title,
                    ts.Price,
                    SaleDate = ts.SaleDate.ToString("dd.MM.yyyy")
                })
                .ToList();

            ReportsDataGrid.ItemsSource = ticketSalesReport;
        }

        private void GenerateRepertoireReport()
        {
            var repertoireReport = _context.Shows
                .Select(show => new
                {
                    show.Id,
                    show.Title,
                    show.StartDate,
                    show.EndDate
                })
                .ToList();

            ReportsDataGrid.ItemsSource = repertoireReport;
        }

        private void GenerateIncomeReport()
        {
            var incomeReport = _context.TicketSales
                .GroupBy(ts => ts.Show.Title)
                .Select(g => new
                {
                    ShowTitle = g.Key,
                    TotalIncome = g.Sum(ts => ts.Price)
                })
            .ToList();

            ReportsDataGrid.ItemsSource = incomeReport;
        }
    }
}