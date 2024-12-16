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
        /// <summary>
        /// ФИО студента
        /// </summary>
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
        /// <summary>
        /// Группа студента
        /// </summary>
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
        /// <summary>
        /// Специальность студента
        /// </summary>
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

        /// <summary>
        /// Id студента
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Событие на изменение свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод вызыва события
        /// </summary>
        /// <param name="prop">имя свойства</param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
