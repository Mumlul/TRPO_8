using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPO_8
{
    public class Doctor : INotifyPropertyChanged
    {
        private int id;
        private string name = "";
        private string lastName = "";
        private string middleName = "";
        private string specialisation = "";
        private string password = "";

        public int ID
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
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

        public string Specialisation
        {
            get => specialisation;
            set { specialisation = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
