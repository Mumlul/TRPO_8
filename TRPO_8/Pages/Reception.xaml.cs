using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Text.Json;

namespace TRPO_8.Pages;

public partial class Reception : Page
{
    private Patient _patient=null;
    private Doctor _doctor;
    
    private string patientsPath = System.IO.Path.Combine(AppContext.BaseDirectory, @"files\patients");

    public Patient SelectedPatient
    {
        get=>_patient;
        set
        {
            _patient = value;
        }
    }

    public Doctor CurrentDoctor
    {
        get => _doctor;
        set=>_doctor = value;
    }

    public Reception(Patient _selectedPatient,Doctor _currentDoctor)
    {
        InitializeComponent();
        SelectedPatient = _selectedPatient;
        CurrentDoctor = _currentDoctor;
        DataContext = _selectedPatient;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            // Проверяем что пациент выбран
            if (SelectedPatient == null)
            {
                MessageBox.Show("Ошибка: пациент не выбран!");
                return;
            }

            // Проверяем обязательные поля
            if (string.IsNullOrWhiteSpace(Diagnos.Text))
            {
                MessageBox.Show("Введите диагноз!");
                return;
            }

            string filename = $"P_{SelectedPatient.ID}.json";
            string filePath = System.IO.Path.Combine(patientsPath, filename);
            
            Patient patientToSave;

            // Загружаем существующие данные или создаем новые
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                patientToSave = JsonSerializer.Deserialize<Patient>(json);
                
                // Обновляем базовые данные
                patientToSave.LastName = SelectedPatient.LastName;
                patientToSave.Name = SelectedPatient.Name;
                patientToSave.MiddleName = SelectedPatient.MiddleName;
                patientToSave.Birthday = SelectedPatient.Birthday;
            }
            else
            {
                patientToSave = SelectedPatient;
            }

            // Инициализируем список приемов если он null
            if (patientToSave.Receprions == null)
            {
                patientToSave.Receprions = new List<Receprion>();
            }

            // ИСПРАВЛЕННЫЕ НАЗВАНИЯ СВОЙСТВ:
            patientToSave.Receprions.Add(new Receprion()
            {
                Date = DateTime.Now,
                Diagons = Diagnos.Text, 
                Recomendation = Recomendations.Text, 
                DoctorID = CurrentDoctor.ID
            });

            // Сохраняем patientToSave, а не SelectedPatient
            string updatedJson = JsonSerializer.Serialize(patientToSave, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updatedJson);

            MessageBox.Show("Данные приема успешно сохранены!");
            NavigationService.GoBack();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении: {ex.Message}\n\nПодробности: {ex.InnerException?.Message}");
        }
    }
}