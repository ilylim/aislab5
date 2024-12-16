using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using WPFView;
using System.Windows;
using System.Runtime.InteropServices;

namespace ViewManager
{
    internal class Program
    {
        /// <summary>
        /// ViewModel для окна с добавлением студнта
        /// </summary>
        private static AddViewModel AddViewModel {  get; set; }

        /// <summary>
        /// ViewModel для окна с изменением студента
        /// </summary>
        private static UpdateViewModel UpdateViewModel {  get; set; }

        /// <summary>
        /// ViewModelManager для организации работы нескольких ViewModel многооконного приложения
        /// </summary>
        private static ViewModelManager ViewModelManager { get; set; }

        [STAThread] // Нужен для запуска WPF
        static void Main(string[] args)
        {
            ViewModelManager = new ViewModelManager();

            ViewModelManager.AddViewModelChanged += CreateAddView;

            ViewModelManager.UpdateViewModelChanged += CreateUpdateView;

            ViewModelManager.DeleteViewModelChanged += CreateDeleteView;

            Application app = new Application();

            // Вызываем событие обновления DeleteViewModel для запуска начального окна (т.к. начальное окно у нас реализует удаление студентов)
            ViewModelManager.GetDeleteVM();

            app.Run();
        }

        /// <summary>
        /// Метод создания стартового окна удаления студентов и связывания его DataContext с DeleteViewModel
        /// </summary>
        /// <param name="deleteViewModel">ViewModel для удаления студентов</param>
        private static void CreateDeleteView(DeleteViewModel deleteViewModel)
        {
            deleteViewModel.SwitchToAddViewEvent += ViewModelManager.GetAddVM;
            deleteViewModel.SwitchToUpdateViewEvent += ViewModelManager.GetUpdateVM;
            var mainWindow = new MainWindow();
            mainWindow.DataContext = deleteViewModel;
            mainWindow.Show();
        }

        /// <summary>
        /// Метод создания окна изменения студентов и связывания его DataContext с UpdateViewModel
        /// </summary>
        /// <param name="updateViewModel">ViewModel для изменения студентов</param>
        private static void CreateUpdateView(UpdateViewModel updateViewModel)
        {
            var updateWindow = new UpdateWindow();
            UpdateViewModel = updateViewModel;
            updateWindow.DataContext = updateViewModel;
            updateWindow.Show();
        }

        /// <summary>
        /// Метод создания окна добавления студентов и связывания его DataContext с AddViewModel
        /// </summary>
        /// <param name="addViewModel">ViewModel для добавления студентов</param>
        private static void CreateAddView(AddViewModel addViewModel)
        {
            var addWindow = new AddWindow();
            AddViewModel = addViewModel;
            addWindow.DataContext = addViewModel;
            addWindow.Show();
        }
    }
}
