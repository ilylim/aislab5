using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Model.Interfaces;
using Model.Managers;

namespace ViewModel
{
    public class DeleteViewModel : INotifyPropertyChanged
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
        private StudentsManager studentsManager { get; set; }

        public DeleteViewModel(IManager<Student> manager)
        {
            CurrentStudent = null;
            this.studentsManager = (StudentsManager)manager;
            this.Students = studentsManager.Students;
            DeleteStudentCommand = new RelayCommand(execute => DeleteStudent(), canExecute => CurrentStudent != null);
            SwitchToAddViewCommand = new RelayCommand(execute => SwitchToAddViewEvent());
            SwitchToUpdateViewCommand = new RelayCommand(execute => SwitchToUpdateViewEvent(), canExecute => CurrentStudent != null);
        }

        public ObservableCollection<Student> Students { get; set; }

        public RelayCommand DeleteStudentCommand { get; set; }

        public RelayCommand SwitchToAddViewCommand { get; set; }

        public event Action SwitchToAddViewEvent;
        public RelayCommand SwitchToUpdateViewCommand { get; set; }

        public event Action SwitchToUpdateViewEvent;

        private void DeleteStudent()
        {
            studentsManager.Delete(CurrentStudent.Id);
            CurrentStudent = null;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
