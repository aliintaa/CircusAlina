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
    /// Логика взаимодействия для RepertoireManagement.xaml
    /// </summary>
    public partial class RepertoireManagement : Page
    {
        private CircusContext _context;
        public RepertoireManagement()
        {
            InitializeComponent();
            _context = new CircusContext();
            LoadRepertoire();
        }
        private void LoadRepertoire()
        {
            RepertoireDataGrid.ItemsSource = _context.Shows.ToList();
        }
        private void AddShowButton_Click(object sender, RoutedEventArgs e)
        {
            var newShow = ShowDialog("Добавление нового шоу");
            if (newShow != null)
            {
                _context.Shows.Add(newShow);
                _context.SaveChanges();
                LoadRepertoire();
            }
        }

        private void EditShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (RepertoireDataGrid.SelectedItem is Show selectedShow)
            {
                var updatedShow = ShowDialog("Редактирование шоу", selectedShow);
                if (updatedShow != null)
                {
                    _context.Entry(selectedShow).CurrentValues.SetValues(updatedShow);
                    _context.SaveChanges();
                    LoadRepertoire();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите шоу для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (RepertoireDataGrid.SelectedItem is Show selectedShow)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить шоу '{selectedShow.Title}'?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Shows.Remove(selectedShow);
                    _context.SaveChanges();
                    LoadRepertoire();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите шоу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private Show ShowDialog(string title, Show existingShow = null)
        {
            var dialog = new ShowDialog(title, existingShow);
            if (dialog.ShowDialog() == true)
            {
                return dialog.ShowDetails;
            }
            return null;
        }
    }
  }

