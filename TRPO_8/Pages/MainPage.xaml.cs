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

namespace TRPO_8.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    /// 
    public partial class MainPage : Page
    {
        public ObservableCollection<Patient> Patients { get; set; } = new();
        private string patientspath = System.IO.Path.Combine(AppContext.BaseDirectory, @"files\patients");

        public MainPage(Doctor d)
        {
            InitializeComponent();
            LoadPatients();
            ll.DataContext = this;
            Info.DataContext = d;
        }

        private void LoadPatients()
        {
            foreach (string path in Directory.EnumerateFiles(patientspath))
            {

                string json = File.ReadAllText(path);
                Patient pat = JsonSerializer.Deserialize<Patient>(json);
                Patients.Add(pat);
            }
        }
    }
}
