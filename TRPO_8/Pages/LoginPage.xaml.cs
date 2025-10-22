using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace TRPO_8.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private string doctorpath = System.IO.Path.Combine(AppContext.BaseDirectory, @"files\doctors");
        private Doctor currentDoctor;
        public LoginPage(Doctor? _currentDoctor=null)
        {
            InitializeComponent();
            CurrentDoctor = _currentDoctor ?? new Doctor();
            this.DataContext = CurrentDoctor;
        }

        public Doctor CurrentDoctor
        {
            get => currentDoctor;
            set
            {
                currentDoctor = value;
            }
        }

        private void LoginDoctor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filename = $"D_{CurrentDoctor.ID}.json";
                string filePath = System.IO.Path.Combine(doctorpath, filename);

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Доктор с таким ID не найден!");
                    return;
                }

                string json = File.ReadAllText(filePath);
                currentDoctor = JsonSerializer.Deserialize<Doctor>(json);

                if (currentDoctor.Password == CurrentDoctor.Password)
                {
                    MessageBox.Show("Авторизация успешна!");
                    NavigationService.Navigate(new MainPage(currentDoctor));
                }
                else
                {
                    MessageBox.Show("Неверный пароль!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении файла: {ex.Message}");
            }
        }

        private void Register(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RegisterDoctor());
        }
    }
}
