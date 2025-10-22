using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPO_8
{
    public class Patient : INotifyPropertyChanged
    {
        private int id;
        private string name = "";
        private string fullname = "";
        private string lastName = "";
        private string middleName = "";
        private DateTime birthday = DateTime.Now;

        private List<Receprion> _receprions;

        public int ID
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }

        public string FullName
        {
            get => fullname;
            set { fullname = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get => lastName;
            set { lastName = value; OnPropertyChanged(); }
        }

        public string MiddleName
        {
            get => middleName;
            set { middleName = value; OnPropertyChanged(); }
        }

        public DateTime Birthday
        {
            get => birthday;
            set { birthday = value; OnPropertyChanged(); }
        }

        public List<Receprion> Receprions
        {
            get => _receprions;
            set { _receprions = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
