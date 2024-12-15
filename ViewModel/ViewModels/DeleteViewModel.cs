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
            Students.CollectionChanged += (sender, e) => RefreshChart();
            DeleteStudentCommand = new RelayCommand(execute => DeleteStudent(), canExecute => CurrentStudent != null);
            SwitchToAddViewCommand = new RelayCommand(execute => SwitchToAddViewEvent());
            SwitchToUpdateViewCommand = new RelayCommand(execute => SwitchToUpdateViewEvent(), canExecute => CurrentStudent != null);
        }
        public ObservableCollection<Student> Students { get; set; }

        public RelayCommand DeleteStudentCommand { get; set; }

        private void DeleteStudent()
        {
            studentsManager.Delete(CurrentStudent.Id);
            CurrentStudent = null;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public SeriesCollection ChartSeries { get; set; }

        public List<string> ChartLabels { get; set; } = new List<string> { "ИСИТ", "ИБ", "ИВТ", "ПИ" };
        public ChartValues<int> ChartValues { get; set; }

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

        public RelayCommand SwitchToAddViewCommand { get; set; }

        public event Action SwitchToAddViewEvent;

        public RelayCommand SwitchToUpdateViewCommand { get; set; }

        public event Action SwitchToUpdateViewEvent;
    }
}
