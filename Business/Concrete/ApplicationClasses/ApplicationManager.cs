using Business.Abstract.ApplicationClasses;
using Business.ValidationRules.FluentValidation;
using Core.Others;
using Data.Abstract.ApplicationClasses;
using Entities.Concrete.ApplicationClasses;
using FluentValidation;
using System.Linq.Expressions;

namespace Business.Concrete.ApplicationClasses
{
    public class ApplicationManager : IApplicationService
    {
        private readonly IApplicationDal applicationDal;

        public ApplicationManager(IApplicationDal applicationDal)
        {
            this.applicationDal = applicationDal;
        }

        public CoreResponse<Application> Add(Application entity, int userId = default)
        {
            try
            {
                var validator = new ApplicationValidator();
                validator.ValidateAndThrow(entity);

                applicationDal.Add(entity);
                applicationDal.Save();
                return new CoreResponse<Application>(entity,"Başarılı");
            }
            catch (Exception e)
            {
                return new CoreResponse<Application>(e.Message);
            }
        }

        public async Task<CoreResponse<Application>> AddAsync(Application entity, int userId = default)
        {
            try
            {
                var validator = new ApplicationValidator();
                await validator.ValidateAndThrowAsync(entity);

                await applicationDal.AddAsync(entity);
                await applicationDal.SaveAsync();

                return new CoreResponse<Application>(entity, "Başarılı");
            }
            catch (Exception e)
            {
                return new CoreResponse<Application>(e.Message);
            }
        }

        public CoreResponse<Application> Delete(Application entity, int userId = default)
        {
            try
            {
                applicationDal.Delete(entity);
                applicationDal.Save();

                return new CoreResponse<Application>(entity, "Silme başarılı.");
            }
            catch (Exception e)
            {
                return new CoreResponse<Application>(e.Message);
            }
        }

        public CoreResponse<Application> DeleteById(int id, int userId = default)
        {
            try
            {
                var willdelete = applicationDal.FindById(id);

                if (willdelete is null)
                {
                    return new CoreResponse<Application>("Nesne bulunamadı.");
                }

                applicationDal.Delete(willdelete);
                applicationDal.Save();

                return new CoreResponse<Application>(willdelete, "Silme başarılı.");
            }
            catch (Exception e)
            {
                return new CoreResponse<Application>(e.Message);
            }
        }

        public async Task<CoreResponse<Application>> DeleteByIdAsync(int id, int userId = default)
        {
            try
            {
                var willdelete = await applicationDal.FindByIdAsync(id);

                if (willdelete is null)
                {
                    return new CoreResponse<Application>("Nesne bulunamadı.");
                }

                applicationDal.Delete(willdelete);
                applicationDal.Save();

                return new CoreResponse<Application>(willdelete, "Silme başarılı.");
            }
            catch (Exception e)
            {
                return new CoreResponse<Application>(e.Message);
            }
        }

        public CoreResponse<Application> Get(Expression<Func<Application, bool>> filter, string includeProperties = "", int userId = default)
        {
            try
            {
                var entity = applicationDal.Get(filter, includeProperties);
                if (entity is null)
                {
                    return new CoreResponse<Application>("Nesne bulunamadı.");
                }

                return new CoreResponse<Application>(entity);
            }
            catch (Exception e)
            {
                return new CoreResponse<Application>(e.Message);
            }
        }

        public async Task<CoreResponse<Application>> GetAsync(Expression<Func<Application, bool>> filter, string includeProperties = "", int userId = default)
        {
            try
            {
                var entity = await applicationDal.GetAsync(filter, includeProperties);

                if (entity is null)
                {
                    return new CoreResponse<Application>("Nesne bulunamadı.");
                }

                return new CoreResponse<Application>(entity);
            }
            catch (Exception e)
            {
                return new CoreResponse<Application>(e.Message);
            }
        }

        public CoreResponse<IEnumerable<Application>> GetList(Expression<Func<Application, bool>> filter = null, Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = null, string includeProperties = "", int userId = default)
        {
            try
            {
                return new CoreResponse<IEnumerable<Application>>(applicationDal.GetList(filter, orderBy, includeProperties));
            }
            catch (Exception e)
            {
                return new CoreResponse<IEnumerable<Application>>(e.Message);
            }
        }

        public async Task<CoreResponse<IEnumerable<Application>>> GetListAsync(Expression<Func<Application, bool>> filter = null, Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = null, string includeProperties = "", int userId = default)
        {
            try
            {
                return new CoreResponse<IEnumerable<Application>>(await applicationDal.GetListAsync(filter, orderBy, includeProperties));
            }
            catch (Exception e)
            {
                return new CoreResponse<IEnumerable<Application>>(e.Message);
            }
        }

        public CoreResponse<Application> Update(Application entity, int userId = default)
        {
            try
            {
                var validator = new ApplicationValidator();

                validator.ValidateAndThrow(entity);

                applicationDal.Update(entity);
                applicationDal.Save();

                return new CoreResponse<Application>(entity, "Güncelleme başarılı.");
            }
            catch (Exception e)
            {
                return new CoreResponse<Application>(e.Message);
            }
        }
    }
}
