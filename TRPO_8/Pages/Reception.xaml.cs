using System.Collections.ObjectModel;
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
    public ObservableCollection<Receprion> ReceptionsList { get; set; } = new();
    public ObservableCollection<Patient> _userList;
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

    public Reception(Patient _selectedPatient,Doctor _currentDoctor, ObservableCollection<Patient>? userList=null)
    {
        InitializeComponent();
        SelectedPatient = _selectedPatient;
        CurrentDoctor = _currentDoctor;
        DataContext = _selectedPatient;
        LoadSpisok();
        Spisok.DataContext = this;
        _userList = userList;
        data.DataContext = _currentDoctor;
    }

    private void LoadSpisok()
    {
        foreach (Receprion rec in SelectedPatient.Receprions)
        {
            ReceptionsList.Add(rec);
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (SelectedPatient == null)
            {
                MessageBox.Show("Ошибка: пациент не выбран!");
                return;
            }

            string filename = $"P_{SelectedPatient.ID}.json";
            string filePath = System.IO.Path.Combine(patientsPath, filename);
            
            Patient patientToSave=SelectedPatient;
            
            if (patientToSave.Receprions == null)
            {
                patientToSave.Receprions = new List<Receprion>();
            }

            patientToSave.Receprions.Add(new Receprion()
            {
                Date = DateTime.Now,
                Diagons = Diagnos.Text, 
                Recomendation = Recomendations.Text, 
                DoctorID = CurrentDoctor.ID
            });

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

    public void Edit_patient(object sender,RoutedEventArgs e)
    {
        NavigationService.Navigate(new AddPatient(_userList,SelectedPatient));
    }
}