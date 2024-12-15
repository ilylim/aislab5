using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Student : INotifyPropertyChanged, IDomainObject
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private string group;
        public string Group
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
                OnPropertyChanged("Group");
            }
        }
        private string speciality;
        public string Speciality
        {
            get
            {
                return speciality;
            }
            set
            {
                speciality = value;
                OnPropertyChanged("Speciality");
            }
        }
        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
