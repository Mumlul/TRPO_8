using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TRPO_8;

namespace TRPO_8.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    /// 
    public partial class MainPage : Page
    {
        public ObservableCollection<Patient> Patients { get; set; } = new();
        public Statistic Statistics { get; set; } = new Statistic();
        public Patient? SelectedPatient { get; set; }
        private string patientspath = System.IO.Path.Combine(AppContext.BaseDirectory, @"files\patients");
        
        private Doctor CurrentDoctor;
        public MainPage(Doctor d)
        {
            InitializeComponent();
            LoadPatients();
            ll.DataContext = this;
            Info.DataContext = d;
            CurrentDoctor = d;
            Statistic.DataContext = Statistics;
        }

        private void LoadPatients()
        {
            foreach (string path in Directory.EnumerateFiles(patientspath))
            {
                string json = File.ReadAllText(path);
                Patient pat = JsonSerializer.Deserialize<Patient>(json);
                Patients.Add(pat);
            }
            Statistics.Update();
        }

        private void Add_Reception(object sender, MouseButtonEventArgs e)
        {
            if (SelectedPatient == null)
            {
                MessageBox.Show("sdad");
                return;
            }
            NavigationService.Navigate(new Reception(SelectedPatient,CurrentDoctor,Patients));
            Statistics.Update();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPatient(Patients,null));
            Statistics.Update();
        }


        private void Change_data(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPatient(Patients,SelectedPatient));
            Statistics.Update();
        }
    }
}
