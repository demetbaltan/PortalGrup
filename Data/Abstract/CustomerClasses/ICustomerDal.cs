using Core.Data.Abstract.EntityFrameworkClasses;
using Entities.Concrete.CustomerClasses;

namespace Data.Abstract.CustomerClasses
{
    public interface ICustomerDal : IEntityRepository<Customer> { }
}
