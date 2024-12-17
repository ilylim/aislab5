using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Model.Interfaces;
using Model.Managers;
using Ninject.Modules;
using DataAccessLayer.Dapper;
using DataAccessLayer.EntityFramework;
using DataAccessLayer;

namespace Model
{
    public class ModelConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Student>>().To<EntityFrameworkRepository<Student>>().InSingletonScope();
            //Bind<IRepository<Student>>().To<DapperRepository<Student>>().InSingletonScope();
            Bind<IManager<Student>>().To<StudentsManager>().InSingletonScope();
        }
    }
}
