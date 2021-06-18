using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.DAL.Repositories;

namespace ToDoList.Web.Utilities
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        readonly IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            kernel.Unbind<ModelValidatorProvider>();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>)).InSingletonScope();
            kernel.Bind<IDbContextProvider>().To<RequestLivesDbContextProvider>();
        }
    }
}