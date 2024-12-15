using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccessLayer.EntityFramework
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        public Context context;
        public EntityFrameworkRepository()
        {
            context = new Context();
        }

        /// <summary>
        /// Метод сбора информации об объектах в БД (EF)
        /// </summary>
        /// <returns>коллекция с объектами</returns>
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        /// <summary>
        /// Метод удаления объекта из БД (EF)
        /// </summary>
        /// <param name="obj">удаляемый объект</param>
        public void Delete(T obj)
        {
            context.Set<T>().Remove(obj);
            context.SaveChanges();
        }

        /// <summary>
        /// Метод добавления объекта в БД (EF)
        /// </summary>
        /// <param name="obj">добавляемый объект</param>
        public void Create(T obj)
        {
            context.Set<T>().Add(obj);
            context.SaveChanges();
        }

        /// <summary>
        /// Метод обновления объекта в БД (EF)
        /// </summary>
        /// <param name="obj">измененный объект</param>
        public void Update(T obj)
        {
            context.Set<T>().AddOrUpdate(obj);
            context.SaveChanges();
        }
    }
}
