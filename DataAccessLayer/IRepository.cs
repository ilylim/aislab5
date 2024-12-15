using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Метод сбора информации из БД
        /// </summary>
        /// <returns>коллекция с объектами</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Метод создания объекта в БД
        /// </summary>
        /// <param name="obj">создаваемый объект</param>
        void Create(T obj);

        /// <summary>
        /// Метод обновления информации об объекте в БД
        /// </summary>
        /// <param name="obj">измененный объект</param>
        void Update(T obj);

        /// <summary>
        /// Метод удаления объекта из БД
        /// </summary>
        /// <param name="obj">удаляемый объект</param>
        void Delete(T obj);
    }
}
