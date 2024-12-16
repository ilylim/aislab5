using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Entities;
using LiveCharts;
using LiveCharts.Wpf;
using Model.Interfaces;
using Model.Managers;

namespace ViewModel
{
    public class DeleteViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие на изменение свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private Student student;
        /// <summary>
        /// Текущий удаляемый студент
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
        private IManager<Student> studentsManager { get; set; }

        /// <summary>
        /// Конструктор DeleteViewModel
        /// </summary>
        /// <param name="manager">менеджер студентов (модель с бизнес-логикой приложения)</param>
        public DeleteViewModel(IManager<Student> manager)
        {
            CurrentStudent = null;
            this.studentsManager = (StudentsManager)manager;
            this.Students = ((StudentsManager)studentsManager).Students;

            Students.CollectionChanged += (sender, e) => RefreshChart();

            DeleteStudentCommand = new RelayCommand(execute => DeleteStudent(), canExecute => CurrentStudent != null);

            SwitchToAddViewCommand = new RelayCommand(execute => SwitchToAddViewEvent());
            SwitchToUpdateViewCommand = new RelayCommand(execute => SwitchToUpdateViewEvent(), canExecute => CurrentStudent != null);
        }

        /// <summary>
        /// Коллекция студентов
        /// </summary>
        public ObservableCollection<Student> Students { get; set; }

        /// <summary>
        /// Команда удаления студенте для вызова во вьюшке
        /// </summary>
        public RelayCommand DeleteStudentCommand { get; set; }

        /// <summary>
        /// Метод удаления студента
        /// </summary>
        private void DeleteStudent()
        {
            studentsManager.Delete(CurrentStudent.Id);
            CurrentStudent = null;
        }

        /// <summary>
        /// Метод вызыва события
        /// </summary>
        /// <param name="prop">имя свойства</param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Коллекция с информацией о гистограмме
        /// </summary>
        public SeriesCollection ChartSeries { get; set; }

        /// <summary>
        /// Коллекция с названиями подписей в гистограмме
        /// </summary>
        public List<string> ChartLabels { get; set; } = new List<string> { "ИСИТ", "ИБ", "ИВТ", "ПИ" };

        /// <summary>
        /// Коллекция со значениями в гистограмме
        /// </summary>
        public ChartValues<int> ChartValues { get; set; }

        /// <summary>
        /// Метод обновления гистограммы
        /// </summary>
        private void RefreshChart()
        {
            var updatedValues = new ChartValues<int>();
            foreach (var label in ChartLabels)
            {
                updatedValues.Add(Students.Count(student => student.Speciality == label));
            }

            ChartSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Количество студентов",
                    Values = updatedValues
                }
            };

            OnPropertyChanged(nameof(ChartSeries));
        }

        /// <summary>
        /// Команда для создания окна добавления студентов
        /// </summary>
        public RelayCommand SwitchToAddViewCommand { get; set; }

        /// <summary>
        /// Событие создания окна добавления студентов
        /// </summary>
        public event Action SwitchToAddViewEvent;

        /// <summary>
        /// Команда для создания окна изменения студентов
        /// </summary>
        public RelayCommand SwitchToUpdateViewCommand { get; set; }

        /// <summary>
        /// Событие создания окна изменения студентов
        /// </summary>
        public event Action SwitchToUpdateViewEvent;
    }
}
