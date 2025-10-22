using System;
using System.Collections.Generic;
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
using System.IO;
using TRPO_8.Pages;

namespace TRPO_8.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterDoctor.xaml
    /// </summary>
    public partial class RegisterDoctor : Page
    {
        private string path = System.IO.Path.Combine(AppContext.BaseDirectory, @"files\doctors");
        private Doctor currentDoctor;

        public RegisterDoctor()
        {
            InitializeComponent();
            CurrentDoctor = new Doctor();
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

        private void Add_Doctor(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentDoctor.ID = GenerateDoctorId();

                string filename = $"D_{CurrentDoctor.ID}.json";
                string filePath = System.IO.Path.Combine(path, filename);
                string json = JsonSerializer.Serialize(CurrentDoctor, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
                MessageBox.Show($"Доктор зарегистрирован! ID: {CurrentDoctor.ID}");

                NavigationService.Navigate(new LoginPage(CurrentDoctor));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }

        private int GenerateDoctorId()
        {
            Random rnd = new Random();
            int id;
            string filename;

            do
            {
                id = rnd.Next(10000, 99999);
                filename = $"D_{id}.json";
            }
            while (File.Exists(System.IO.Path.Combine(path, filename)));

            return id;
        }

        
    }
}
