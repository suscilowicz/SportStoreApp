using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entitlies;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver (IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product {Name = "Pilka nozna", Price = 25 },
                new Product {Name = "Deska surfingowa", Price=179 },
                new Product {Name = "Buty", Price = 95 }
            });
            _kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}