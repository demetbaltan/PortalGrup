using Core.Others;
using Entities.Concrete.CustomerClasses;

namespace Business.Abstract.CustomerClasses
{
    public interface ICustomerService : IGenericService<Customer>
    {
        Task<CoreResponse<Customer>> GetNviControl(Customer customer);
    }
}
