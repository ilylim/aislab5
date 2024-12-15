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
        private DeleteViewModel deleteViewModel;
        private AddViewModel addViewModel;
        private UpdateViewModel updateViewModel;
        public event Action<UpdateViewModel> UpdateViewModelChanged;
        public event Action<AddViewModel> AddViewModelChanged;
        public event Action<DeleteViewModel> DeleteViewModelChanged;

        static IKernel ninjectKernel = new StandardKernel(new ModelConfigModule());
        private IManager<Student> studentsManager = ninjectKernel.Get<IManager<Student>>();

        public ViewModelManager()
        {
            deleteViewModel = new DeleteViewModel(studentsManager);
            addViewModel = new AddViewModel(studentsManager);
            updateViewModel = new UpdateViewModel(studentsManager, new Student());
            studentsManager.ReadAll();
        }

        public void GetDeleteVM()
        {

            DeleteViewModelChanged.Invoke(deleteViewModel);
        }

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
        public void GetAddVM()
        {
            AddViewModelChanged.Invoke(addViewModel);
        }
    }
}
