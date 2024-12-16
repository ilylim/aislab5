using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Entities;
using Model.Interfaces;
using Model.Managers;

namespace ViewModel
{
    public class AddViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие на изменение свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private Student student;
        /// <summary>
        /// Текущий добавляемый студент
        /// </summary>
        public Student CurrentStudent
        {
            get
            {
                return student;
            }
            set
            {
                student = value;
                OnPropertyChanged("CurrentStudent");
            }
        }

        /// <summary>
        /// Менеджер студентов (модель с бизнес-логикой приложения)
        /// </summary>
        private IManager<Student> studentsManager;

        /// <summary>
        /// Конструктор AddViewModel
        /// </summary>
        /// <param name="manager">менеджер студентов (модель с бизнес-логикой приложения)</param>
        public AddViewModel(IManager<Student> manager)
        {
            CurrentStudent = new Student();
            this.studentsManager = manager;

            AddStudentCommand = new RelayCommand(execute => AddStudent(), canExecute => !string.IsNullOrEmpty(CurrentStudent.Name) & 
                !string.IsNullOrEmpty(CurrentStudent.Group) & !string.IsNullOrEmpty(CurrentStudent.Speciality));
        }

        /// <summary>
        /// Команда добавления нового студенте для вызова во вьюшке
        /// </summary>
        public RelayCommand AddStudentCommand { get; set; }

        /// <summary>
        /// Метод добавления нового студента
        /// </summary>
        private void AddStudent()
        {
            studentsManager.Create(CurrentStudent);
            CurrentStudent = new Student();
        }

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
