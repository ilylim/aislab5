using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Model.Interfaces
{
    public interface IManager<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Метод создания сущности
        /// </summary>
        /// <param name="obj"></param>
        void Create(T obj);
        /// <summary>
        /// Метод вывода сущностей
        /// </summary>
        void ReadAll();
        /// <summary>
        /// Метод обновления сущности
        /// </summary>
        /// <param name="obj"></param>
        void Update(T obj);
        /// <summary>
        /// Метод удаления сущности
        /// </summary>
        /// <param name="Id"></param>
        void Delete(int Id);
    }
}
