using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;
using System.Configuration;


namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory(){
            ninjectKernel = new StandardKernel();
            AddBindings();
    }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IProductsRepository>().To<EFProductRepository>();
            ninjectKernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();
            ninjectKernel.Bind<IStatesRepository>().To<EFStatesRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                .AppSettings["Email.WriteAsFile"] ?? "false")
            };
            ninjectKernel.Bind<IOrderProcessor>()
            .To<EmailOrderProcessor>()
            .WithConstructorArgument("settings", emailSettings);

            //putbindings here
           // Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>{
              //  new Product { Name = "Football", Price = 25},
                //new Product { Name = "Surf Board", Price = 179},
                //new Product { Name = "Shoes", Price = 95},
            //}/.AsQueryable());
            //ninjectKernel.Bind<IProductsRepository>().ToConstant(mock.Object);
        }
    }
}