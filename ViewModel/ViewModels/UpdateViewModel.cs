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
        /// <summary>
        /// Событие на изменение свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private Student student;
        /// <summary>
        /// Текущий студент для изменения
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
        /// Конструктор UpdateViewModel
        /// </summary>
        /// <param name="manager">менеджер студентов (модель с бизнес-логикой приложения)</param>
        /// <param name="currentStudent">текущий студент</param>
        public UpdateViewModel(IManager<Student> manager, Student currentStudent)
        {
            this.CurrentStudent = currentStudent;
            this.studentsManager = manager;

            UpdateStudentCommand = new RelayCommand(execute => UpdateStudent(execute), canExecute => !string.IsNullOrEmpty(CurrentStudent.Name) &&
                !string.IsNullOrEmpty(CurrentStudent.Group) && !string.IsNullOrEmpty(CurrentStudent.Speciality));
        }

        /// <summary>
        /// Команда обновления информации о студенте для вызова во вьюшке
        /// </summary>
        public RelayCommand UpdateStudentCommand { get; set; }

        /// <summary>
        /// Метод обновления студента
        /// </summary>
        /// <param name="parameter">параметр с типом вьюшки, вызывающей метод</param>
        private void UpdateStudent(object parameter) // Параметр нужен для того, чтобы закрывать окно после изменения студентов, иначе появляются баги (
        {
            studentsManager.Update(CurrentStudent);
            MessageBox.Show("Красава ! Студент успешно обновлен !", "МЯУ", MessageBoxButton.OK, MessageBoxImage.Information);

            if (parameter is Window window)
            {
                window.Close();
            }
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
