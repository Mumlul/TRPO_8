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
        private Patient oldPatient;
        private string path = System.IO.Path.Combine(AppContext.BaseDirectory, @"files\patients");
        private ObservableCollection<Patient> _userList;


        public AddPatient(ObservableCollection<Patient> PatientList,Patient? selected=null)
        {
            InitializeComponent();
            oldPatient=selected;
            NewPatient = selected ?? new Patient();
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
                string filename = "";
                string filePath = "";
                string json = "";
                if (oldPatient == null)
                {
                    NewPatient.FullName = $"{NewPatient.LastName} {NewPatient.Name} {NewPatient.MiddleName}";
                    NewPatient.ID = GeneratePatientId();

                    filename = $"P_{NewPatient.ID}.json";
                    filePath = System.IO.Path.Combine(path, filename);

                    json = JsonSerializer.Serialize(NewPatient, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(filePath, json);
                    MessageBox.Show($"Пациент добавлен! ID: {NewPatient.ID}");

                    var addedPatient = new Patient
                    {
                        LastName = NewPatient.LastName,
                        Name = NewPatient.Name,
                        MiddleName = NewPatient.MiddleName,
                        FullName = NewPatient.FullName,
                        ID = NewPatient.ID,
                        Birthday = NewPatient.Birthday,
                        Receprions = new List<Receprion>()
                    };
                    _userList.Add(addedPatient);
                    NavigationService.GoBack();
                }
                else
                {
                    NewPatient.FullName = $"{NewPatient.LastName} {NewPatient.Name} {NewPatient.MiddleName}";
                    filename = $"P_{NewPatient.ID}.json";
                    filePath = System.IO.Path.Combine(path, filename);
                    json = JsonSerializer.Serialize(NewPatient, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(filePath, json);

                    _userList.FirstOrDefault(p => p.ID == oldPatient.ID);
                    MessageBox.Show("Данные пациента успешно обновлены!");
                    NavigationService.GoBack();
                }
                    
               
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
