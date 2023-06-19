using Core.Data.Concrete.EntityFrameworkClasses;
using Data.Abstract.CustomerClasses;
using Data.Context;
using Entities.Concrete.CustomerClasses;

namespace Data.Concrete.CustomerClasses
{
    public class EfCustomerDal : EntityRepositoryBase<Customer, PortalDbContext>, ICustomerDal
    {
        private readonly PortalDbContext context;

        public EfCustomerDal(PortalDbContext context)
        {
            this.context = context;
        }
    }
}
