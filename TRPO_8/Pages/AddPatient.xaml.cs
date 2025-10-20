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
    /// Логика взаимодействия для AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Page
    {
        private Patient newPatient;
        private string path = System.IO.Path.Combine(AppContext.BaseDirectory, "files");
        private ObservableCollection<Patient> _userList;


        public AddPatient(ObservableCollection<Patient> PatientList)
        {
            InitializeComponent();
            NewPatient = new Patient();
            this.DataContext = NewPatient;
            _userList = PatientList;
        }

        public Patient NewPatient
        {
            get => newPatient;
            set
            {
                newPatient = value;
            }
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewPatient.FullName = $"{NewPatient.LastName} {NewPatient.Name} {NewPatient.MiddleName}";
                NewPatient.ID = GeneratePatientId();

                string filename = $"P_{NewPatient.ID}.json";
                string filePath = System.IO.Path.Combine(path, filename);

                string json = JsonSerializer.Serialize(NewPatient, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
                MessageBox.Show($"Пациент добавлен! ID: {NewPatient.ID}");

                NewPatient = new Patient();
                _userList.Add(newPatient);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }

        private int GeneratePatientId()
        {
            Random rnd = new Random();
            int id;
            string filename;

            do
            {
                id = rnd.Next(1000000, 9999999);
                filename = $"P_{id}.json";
            }
            while (File.Exists(System.IO.Path.Combine(path, filename)));

            return id;
        }
    }
}
