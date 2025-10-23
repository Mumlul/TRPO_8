using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TRPO_8;

public class Receprion: INotifyPropertyChanged
{
    private DateTime _date;

    public DateTime Date
    {
        get => _date;
        set
        {
            _date = value;
            OnPropertyChanged();
        }
    }

    private int _doctorid;

    public int DoctorID
    {
        get => _doctorid;
        set
        {
            _doctorid = value;
            OnPropertyChanged();
        }
    }

    private string _diagons;

    public string Diagons
    {
        get => _diagons;
        set
        {
            _diagons = value;
            OnPropertyChanged();
        }
    }
    
    private string _recomendation;

    public string Recomendation
    {
        get => _recomendation;
        set
        {
            _recomendation = value;
            OnPropertyChanged();
        }
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