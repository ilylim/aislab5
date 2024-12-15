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

namespace ViewModel
{
    public class UpdateViewModel : INotifyPropertyChanged
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

        public UpdateViewModel(IManager<Student> manager, Student currentStudent)
        {
            this.CurrentStudent = currentStudent;
            this.studentsManager = manager;
            UpdateStudentCommand = new RelayCommand(execute => UpdateStudent(execute), canExecute => !string.IsNullOrEmpty(CurrentStudent.Name) &&
                !string.IsNullOrEmpty(CurrentStudent.Group) && !string.IsNullOrEmpty(CurrentStudent.Speciality));
        }

        public RelayCommand UpdateStudentCommand { get; set; }

        private void UpdateStudent(object parameter)
        {
            studentsManager.Update(CurrentStudent);
            MessageBox.Show("Студент успешно обновлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            if (parameter is Window window)
            {
                window.Close();
            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
