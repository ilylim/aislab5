using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interfaces;
using Entities;
using DataAccessLayer;

namespace Model.Managers
{
    public class StudentsManager : IManager<Student>
    {
        /// <summary>
        /// Репозиторий для работы с БД
        /// </summary>
        private IRepository<Student> Repository { get; set; }
        
        /// <summary>
        /// Конструктор StudentsManager
        /// </summary>
        /// <param name="repository">репозиторий для работы с БД</param>
        public StudentsManager(IRepository<Student> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Коллекция со всеми студентами
        /// </summary>
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();

        /// <summary>
        /// Метод добавления студентов
        /// </summary>
        /// <param name="newStudent">новый студент</param>
        public void Create(Student newStudent)
        {
            Repository.Create(newStudent);
            Students.Add(newStudent);
        }

        /// <summary>
        /// Метод добавления информации из БД в коллекцию студентов
        /// </summary>
        public void ReadAll()
        {
            foreach(Student student in Repository.GetAll())
            {
                Students.Add(student);
            }
        }

        /// <summary>
        /// Метод удаления студентов
        /// </summary>
        /// <param name="id">Id студента</param>
        public void Delete(int id)
        {
            for(int i = 0; i < Students.Count; i++)
            {
                if (Students[i].Id == id)
                {
                    Repository.Delete(Students[i]);
                    Students.Remove(Students[i]);
                }
            }
        }

        /// <summary>
        /// Метод изменения студента
        /// </summary>
        /// <param name="updateStudent">экземпляр с измененной информацией о студенте</param>
        public void Update(Student updateStudent)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].Id == updateStudent.Id)
                {
                    Students[i] = updateStudent;
                }
            }
            Repository.Update(updateStudent);
        }
    }
}
