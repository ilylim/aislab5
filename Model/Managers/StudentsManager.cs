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
        private IRepository<Student> Repository { get; set; }
        public StudentsManager(IRepository<Student> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Событие оповещения об изменении сущностей
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
        /// Метод добавления информации из БД в словарь студентов
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
        /// <param name="code">код студента</param>
        public void Delete(int code)
        {
            for(int i = 0; i < Students.Count; i++)
            {
                if (Students[i].Id == code)
                {
                    Repository.Delete(Students[i]);
                    Students.Remove(Students[i]);
                }
            }
        }

        /// <summary>
        /// Метод изменения студента
        /// </summary>
        /// <param name="code">код студента</param>
        /// <param name="newName">измененное ФИО</param>
        /// <param name="newGroup">измененная группа</param>
        /// <param name="newSpeciality">измененная специальность</param>
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
