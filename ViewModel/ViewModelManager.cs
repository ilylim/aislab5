using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Interfaces;
using Model.Managers;
using Ninject;
using Entities;

namespace ViewModel
{
    public class ViewModelManager
    {
        /// <summary>
        /// ViewModel удаления студента
        /// </summary>
        private DeleteViewModel deleteViewModel;

        /// <summary>
        /// ViewModel добавления студента
        /// </summary>
        private AddViewModel addViewModel;

        /// <summary>
        /// ViewModel обновления студента
        /// </summary>
        private UpdateViewModel updateViewModel;

        /// <summary>
        /// Событие обновления UpdateViewModel
        /// </summary>
        public event Action<UpdateViewModel> UpdateViewModelChanged;

        /// <summary>
        /// Событие обновления AddViewModel
        /// </summary>
        public event Action<AddViewModel> AddViewModelChanged;

        /// <summary>
        /// Событие обнолвения DeleteViewModel
        /// </summary>
        public event Action<DeleteViewModel> DeleteViewModelChanged;

        static IKernel ninjectKernel = new StandardKernel(new ModelConfigModule());
        private IManager<Student> studentsManager = ninjectKernel.Get<IManager<Student>>();

        /// <summary>
        /// Конструктор ViewModelManager
        /// </summary>
        public ViewModelManager()
        {
            deleteViewModel = new DeleteViewModel(studentsManager);
            studentsManager.ReadAll();
        }

        /// <summary>
        /// Метод обновления DeleteViewModel
        /// </summary>
        public void GetDeleteVM()
        {
            DeleteViewModelChanged.Invoke(deleteViewModel);
        }

        /// <summary>
        /// Метод обновления UpdateViewModel
        /// </summary>
        public void GetUpdateVM()
        {
            updateViewModel = new UpdateViewModel(studentsManager, new Student()
            {
                Id = deleteViewModel.CurrentStudent.Id,
                Name = deleteViewModel.CurrentStudent.Name,
                Group = deleteViewModel.CurrentStudent.Group,
                Speciality = deleteViewModel.CurrentStudent.Speciality
            });

            UpdateViewModelChanged.Invoke(updateViewModel);
        }

        /// <summary>
        /// Метод обновления AddViewModel
        /// </summary>
        public void GetAddVM()
        {
            addViewModel = new AddViewModel(studentsManager);
            AddViewModelChanged.Invoke(addViewModel);
        }
    }
}
