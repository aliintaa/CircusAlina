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
    /// Логика взаимодействия для TicketSales.xaml
    /// </summary>
    public partial class TicketSales : Page
    {
        private CircusContext _context;
        public TicketSales()
        {
            InitializeComponent();
            _context = new CircusContext();
            LoadTicketSales();
        }
        private void LoadTicketSales()
        {
            try
            {
                // Загружаем данные о продажах билетов
                var ticketSales = _context.TicketSales
                    .Select(ts => new
                    {
                        ts.Id,
                        ShowTitle = ts.Show.Title, // Предполагается, что у вас есть навигационное свойство Show
                        ts.Price,
                        SaleDate = ts.SaleDate.ToString("dd.MM.yyyy") // Форматируем дату
                    })
                    .ToList();
                TicketSalesDataGrid.ItemsSource = ticketSales;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SellTicketButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Логика продажи билета
                var ticketSale = new TicketSale
                {
                    // Заполнение данных о продаже билета
                    ShowId = GetSelectedShowId(), // Метод для получения ID выбранного шоу
                    Price = GetTicketPrice(), // Метод для получения цены билета
                    SaleDate = DateTime.Now // Устанавливаем текущую дату продажи
                };

                _context.TicketSales.Add(ticketSale);
                _context.SaveChanges();

                LoadTicketSales(); // Обновляем список продаж
                MessageBox.Show("Билет успешно продан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при продаже билета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private int GetSelectedShowId()
        {
            // Здесь должна быть логика для получения ID выбранного шоу
            // Например, можно открыть диалоговое окно выбора шоу или использовать ComboBox
            return 1; // Замените на реальную логику
        }

        private decimal GetTicketPrice()
        {
            // Здесь должна быть логика для получения цены билета
            return 100.00m; // Замените на реальную логику
        }
    }
}
