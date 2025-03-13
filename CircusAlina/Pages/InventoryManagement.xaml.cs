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
    /// Логика взаимодействия для InventoryManagement.xaml
    /// </summary>
    public partial class InventoryManagement : Page
    {
        private CircusContext _context;
        public InventoryManagement()
        {
            InitializeComponent();
            _context = new CircusContext();
            LoadInventory();
        }
        private void LoadInventory()
        {
            InventoryDataGrid.ItemsSource = _context.Inventory.ToList();
        }
        private void AddInventoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var name = NameTextBox.Text;
                var quantityText = QuantityTextBox.Text;
                var description = DescriptionTextBox.Text;

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(quantityText))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(quantityText, out int quantity) || quantity < 0)
                {
                    MessageBox.Show("Количество должно быть положительным целым числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newInventoryItem = new InventoryItem
                {
                    Name = name,
                    Quantity = quantity,
                    Description = description
                };

                _context.Inventory.Add(newInventoryItem);
                _context.SaveChanges();
                LoadInventory();

                // Очистка полей ввода
                NameTextBox.Clear();
                QuantityTextBox.Clear();
                DescriptionTextBox.Clear();
            }
            catch (Exception ex)
            {
            MessageBox.Show($"Ошибка при добавлении инвентаря: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteInventoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryDataGrid.SelectedItem is InventoryItem selectedItem)
            {
                if (MessageBox.Show($"Вы уверены, что хотите удалить '{selectedItem.Name}'?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _context.Inventory.Remove(selectedItem);
                    _context.SaveChanges();
                    LoadInventory();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите элемент для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
    }

