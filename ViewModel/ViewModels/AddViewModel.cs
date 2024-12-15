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
        public event PropertyChangedEventHandler PropertyChanged;
        private Student student;
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
        private IManager<Student> studentsManager;

        public AddViewModel(IManager<Student> manager)
        {
            CurrentStudent = new Student();
            this.studentsManager = manager;
            AddStudentCommand = new RelayCommand(execute => AddStudent(), canExecute => !string.IsNullOrEmpty(CurrentStudent.Name) & 
                !string.IsNullOrEmpty(CurrentStudent.Group) & !string.IsNullOrEmpty(CurrentStudent.Speciality));
        }

        public RelayCommand AddStudentCommand { get; set; }

        private void AddStudent()
        {
            studentsManager.Create(CurrentStudent);
            CurrentStudent = new Student();
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
