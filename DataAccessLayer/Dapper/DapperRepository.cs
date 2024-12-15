using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Entities;

namespace DataAccessLayer.Dapper
{
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Сергей\\source\\repos\\aislab5\\DataAccessLayer\\StudentsDB.mdf;Integrated Security=True";
        IDbConnection db = new SqlConnection(connectionString);

        /// <summary>
        /// Метод добавления объекта в БД (Dapper)
        /// </summary>
        /// <param name="obj">добавляемый объект</param>
        public void Create(T obj)
        {
            var sqlQuery = $"INSERT INTO [{GetTableName()}] ({GetPropertyNames()}) VALUES({GetPropertyNamesRef()}); SELECT CAST(SCOPE_IDENTITY() as int)";
            int studentId = db.Query<int>(sqlQuery, obj).FirstOrDefault();
            obj.Id = studentId;
        }

        /// <summary>
        /// Метод удаления объекта из БД (Dapper)
        /// </summary>
        /// <param name="obj">удаляемый объект</param>
        public void Delete(T obj)
        {
            var sqlQuery = $"DELETE FROM [{GetTableName()}] WHERE Id = @Id";
            db.Query<T>(sqlQuery, obj);
        }

        /// <summary>
        /// Метод сбора информации из БД (Dapper)
        /// </summary>
        /// <returns>коллекция со всеми объектами опр. таблицы БД</returns>
        public IEnumerable<T> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<T>($"SELECT * FROM [{GetTableName()}]").ToList();
            }
        }

        /// <summary>
        /// Метод обновления информации в БД (Dapper)
        /// </summary>
        /// <param name="obj">измененный объект</param>
        public void Update(T obj)
        {
            Student student = obj as Student;
            var sqlQuery = $"UPDATE [{GetTableName()}] SET {GetChangeProperties()} WHERE Id = @Id";
            db.Query<T>(sqlQuery, obj);
        }

        /// <summary>
        /// Метод поиска названия таблицы в БД с объектами T класса
        /// </summary>
        /// <returns>название таблицы</returns>
        private string GetTableName()
        {
            var type = typeof(T);
            var tableAttribute = type.GetCustomAttribute<TableAttribute>();
            if (tableAttribute != null)
                return tableAttribute.Name;

            return type.Name + "s";
        }

        /// <summary>
        /// Метод поска названий свойств класса T
        /// </summary>
        /// <param name="excludeKey"></param>
        /// <returns>строка со всеми свойствами объекта класса T</returns>
        private string GetPropertyNames(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

            var values = "[" + string.Join("], [", properties.Where(p => p.Name != "Id").Select(p => p.Name)) + "]";

            return values;
        }

        /// <summary>
        /// Метод поиска свойств класса T с @
        /// </summary>
        /// <param name="excludeKey"></param>
        /// <returns>строка со всеми свойствами объекта класса T c @</returns>
        private string GetPropertyNamesRef(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

            var values = "@" + string.Join(", @", properties.Where(p => p.Name != "Id").Select(p => p.Name));

            return values;
        }

        /// <summary>
        /// Метод создания строки для изменения объекта класса T
        /// </summary>
        /// <param name="excludeKey"></param>
        /// <returns>строка для изменения объекта</returns>
        private string GetChangeProperties(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties().Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);
            var changeProperties = properties.Where(p => p.Name != "Id").Select(p => p.Name).ToList();

            for (int i = 0; i < changeProperties.Count(); i++)
            {
                changeProperties[i] = "[" + (string)changeProperties[i] + "]" + " = @" + (string)changeProperties[i];
            }
            var values = string.Join(", ", changeProperties);
            return values;
        }
    }
}
