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
        private static AddViewModel AddViewModel {  get; set; }
        private static UpdateViewModel UpdateViewModel {  get; set; }
        private static ViewModelManager ViewModelManager { get; set; }
        [STAThread] // Требуется для запуска WPF
        static void Main(string[] args)
        {
            ViewModelManager = new ViewModelManager();

            ViewModelManager.AddViewModelChanged += CreateAddView;

            ViewModelManager.UpdateViewModelChanged += CreateUpdateView;

            ViewModelManager.DeleteViewModelChanged += CreateDeleteView;

            Application app = new Application();

            ViewModelManager.GetDeleteVM();

            app.Run();
        }

        private static void CreateDeleteView(DeleteViewModel deleteViewModel)
        {
            deleteViewModel.SwitchToAddViewEvent += OnAddViewOpen;
            deleteViewModel.SwitchToUpdateViewEvent += OnUpdateViewOpen;
            var mainWindow = new MainWindow();
            mainWindow.DataContext = deleteViewModel;
            mainWindow.Show();
        }

        private static void CreateUpdateView(UpdateViewModel updateViewModel)
        {
            var updateWindow = new UpdateWindow();
            UpdateViewModel = updateViewModel;
            updateWindow.DataContext = updateViewModel;
            updateWindow.Show();
        }

        private static void CreateAddView(AddViewModel addViewModel)
        {
            var addWindow = new AddWindow();
            AddViewModel = addViewModel;
            addWindow.DataContext = addViewModel;
            addWindow.Show();
        }

        private static void OnAddViewOpen()
        {
            ViewModelManager.GetAddVM();
        }
        private static void OnUpdateViewOpen()
        {
            ViewModelManager.GetUpdateVM();
        }
    }
}
