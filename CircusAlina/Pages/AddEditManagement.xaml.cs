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
    /// Логика взаимодействия для AddEditManagement.xaml
    /// </summary>
    public partial class AddEditManagement : Page
    {
        public Show ShowDetails { get; private set; }
        public AddEditManagement(string title, Show existingShow = null)
        {
            InitializeComponent();
            Title = title;

            if (existingShow != null)
            {
                TitleTextBox.Text = existingShow.Title;
                DescriptionTextBox.Text = existingShow.Description;
                DurationTextBox.Text = existingShow.Duration.ToString();
                ShowDetails = existingShow;
            }
            else
            {
                ShowDetails = new Show();
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ShowDetails.Title = TitleTextBox.Text;
            ShowDetails.Description = DescriptionTextBox.Text;

            if (int.TryParse(DurationTextBox.Text, out int duration))
            {
                ShowDetails.Duration = duration;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректную продолжительность.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
