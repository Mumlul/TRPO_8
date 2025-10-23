using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace TRPO_8;

public class Statistic:INotifyPropertyChanged
{
    private int _countdoctors = 0;
    private int _countpatients = 0;
    

    public int CountDoctors
    {
        get => _countdoctors;
        set
        {
            _countdoctors = value;
            OnPropertyChanged();
        }
    }

    public int CountPatients
    {
        get => _countpatients;
        set
        {
            _countpatients = value;
            OnPropertyChanged();
        }
    }

    public Statistic()
    {
        CountingDoctors();
        CountingPatients();
    }

    private void CountingPatients()
    {
        if (!Directory.Exists(@"files\patients"))
        {
            
            CountPatients = 0;
            return;
        }
        var files = Directory.GetFiles(@"files\patients", "*.json");
        CountPatients = files.Count(f => Path.GetFileName(f).StartsWith("P_"));
    }

    private void CountingDoctors()
    {
        if (!Directory.Exists(@"files\doctors"))
        {
            
            CountDoctors = 0;
            return;
        }
        var files = Directory.GetFiles(@"files\doctors", "*.json");
        CountDoctors = files.Count(f => Path.GetFileName(f).StartsWith("D_"));
    }

    public void Update()
    {
        if (!Directory.Exists(@"files\patients"))
        {

            CountPatients = 0;
            return;
        }
        var files = Directory.GetFiles(@"files\patients", "*.json");
        CountPatients = files.Count(f => Path.GetFileName(f).StartsWith("P_"));

        if (!Directory.Exists(@"files\doctors"))
        {

            CountDoctors = 0;
            return;
        }
        var files2 = Directory.GetFiles(@"files\doctors", "*.json");
        CountDoctors = files2.Count(f => Path.GetFileName(f).StartsWith("D_"));
    }
    
    
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}