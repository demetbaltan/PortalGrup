using Business.Abstract.CustomerClasses;
using Business.ValidationRules.FluentValidation;
using Core.Others;
using Data.Abstract.CustomerClasses;
using Entities.Concrete.CustomerClasses;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using NviService;
using System.Linq.Expressions;
using static NviService.KPSPublicSoapClient;

namespace Business.Concrete.CustomerClasses
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal customerDal;
        private readonly IConfiguration configuration;

        public CustomerManager(ICustomerDal customerDal, IConfiguration configuration)
        {
            this.customerDal = customerDal;
            this.configuration = configuration;
        }

        public CoreResponse<Customer> Add(Customer entity, int userId = default)
        {
            try
            {
                var validator = new CustomerValidator();
                validator.ValidateAndThrow(entity);

                customerDal.Add(entity);
                customerDal.Save();
                return new CoreResponse<Customer>(entity, "Başarılı");
            }
            catch (Exception e)
            {
                return new CoreResponse<Customer>(e.Message);
            }
        }

        public async Task<CoreResponse<Customer>> AddAsync(Customer entity, int userId = default)
        {
            try
            {
                var validator = new CustomerValidator();
                await validator.ValidateAndThrowAsync(entity);

                await customerDal.AddAsync(entity);
                await customerDal.SaveAsync();

                return new CoreResponse<Customer>(entity, "Başarılı");
            }
            catch (Exception e)
            {
                return new CoreResponse<Customer>(e.Message);
            }
        }

        public CoreResponse<Customer> Delete(Customer entity, int userId = default)
        {
            try
            {
                customerDal.Delete(entity);
                customerDal.Save();

                return new CoreResponse<Customer>(entity, "Silme başarılı.");
            }
            catch (Exception e)
            {
                return new CoreResponse<Customer>(e.Message);
            }
        }

        public CoreResponse<Customer> DeleteById(int id, int userId = default)
        {
            try
            {
                var willdelete = customerDal.FindById(id);

                if (willdelete is null)
                {
                    return new CoreResponse<Customer>("Nesne bulunamadı.");
                }

                customerDal.Delete(willdelete);
                customerDal.Save();

                return new CoreResponse<Customer>(willdelete, "Silme başarılı.");
            }
            catch (Exception e)
            {
                return new CoreResponse<Customer>(e.Message);
            }
        }

        public async Task<CoreResponse<Customer>> DeleteByIdAsync(int id, int userId = default)
        {
            try
            {
                var willdelete = await customerDal.FindByIdAsync(id);

                if (willdelete is null)
                {
                    return new CoreResponse<Customer>("Nesne bulunamadı.");
                }

                customerDal.Delete(willdelete);
                customerDal.Save();

                return new CoreResponse<Customer>(willdelete, "Silme başarılı.");
            }
            catch (Exception e)
            {
                return new CoreResponse<Customer>(e.Message);
            }
        }

        public CoreResponse<Customer> Get(Expression<Func<Customer, bool>> filter, string includeProperties = "", int userId = default)
        {
            try
            {
                var entity = customerDal.Get(filter, includeProperties);
                if (entity is null)
                {
                    return new CoreResponse<Customer>("Nesne bulunamadı.");
                }

                return new CoreResponse<Customer>(entity);
            }
            catch (Exception e)
            {
                return new CoreResponse<Customer>(e.Message);
            }
        }

        public async Task<CoreResponse<Customer>> GetAsync(Expression<Func<Customer, bool>> filter, string includeProperties = "", int userId = default)
        {
            try
            {
                var entity = await customerDal.GetAsync(filter, includeProperties);

                if (entity is null)
                {
                    return new CoreResponse<Customer>("Nesne bulunamadı.");
                }

                return new CoreResponse<Customer>(entity);
            }
            catch (Exception e)
            {
                return new CoreResponse<Customer>(e.Message);
            }
        }

        public CoreResponse<IEnumerable<Customer>> GetList(Expression<Func<Customer, bool>> filter = null, Func<IQueryable<Customer>, IOrderedQueryable<Customer>> orderBy = null, string includeProperties = "", int userId = default)
        {
            try
            {
                return new CoreResponse<IEnumerable<Customer>>(customerDal.GetList(filter, orderBy, includeProperties));
            }
            catch (Exception e)
            {
                return new CoreResponse<IEnumerable<Customer>>(e.Message);
            }
        }

        public async Task<CoreResponse<IEnumerable<Customer>>> GetListAsync(Expression<Func<Customer, bool>> filter = null, Func<IQueryable<Customer>, IOrderedQueryable<Customer>> orderBy = null, string includeProperties = "", int userId = default)
        {
            try
            {
                return new CoreResponse<IEnumerable<Customer>>(await customerDal.GetListAsync(filter, orderBy, includeProperties));
            }
            catch (Exception e)
            {
                return new CoreResponse<IEnumerable<Customer>>(e.Message);
            }
        }

        public CoreResponse<Customer> Update(Customer entity, int userId = default)
        {
            try
            {
                var validator = new CustomerValidator();

                validator.ValidateAndThrow(entity);

                customerDal.Update(entity);
                customerDal.Save();

                return new CoreResponse<Customer>(entity, "Güncelleme başarılı.");
            }
            catch (Exception e)
            {
                return new CoreResponse<Customer>(e.Message);
            }
        }

        public async Task<CoreResponse<Customer>> GetNviControl(Customer customer)
        {
            try
            {
                var wsclient = new KPSPublicSoapClient(EndpointConfiguration.KPSPublicSoap);

                var response = await wsclient.TCKimlikNoDogrulaAsync(customer.GovernmentId, customer.Name, customer.Surname, customer.BirthYear);

                return (response.Body.TCKimlikNoDogrulaResult ? new CoreResponse<Customer>(customer) : new CoreResponse<Customer>("Kişi bulunamadı."));
            }
            catch (Exception ex)
            {
                return new CoreResponse<Customer>(ex.Message);
            }
        }
    }
}
