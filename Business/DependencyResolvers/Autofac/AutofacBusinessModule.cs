using Autofac;
using Business.Abstract.ApplicationClasses;
using Business.Abstract.CustomerClasses;
using Business.Concrete.ApplicationClasses;
using Business.Concrete.CustomerClasses;
using Data.Abstract.ApplicationClasses;
using Data.Abstract.CustomerClasses;
using Data.Concrete.ApplicationClasses;
using Data.Concrete.CustomerClasses;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
 
             builder.RegisterType<EfApplicationDal>().As<IApplicationDal>();
             builder.RegisterType<ApplicationManager>().As<IApplicationService>();

             builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();
             builder.RegisterType<CustomerManager>().As<ICustomerService>();
        }
    }
}
