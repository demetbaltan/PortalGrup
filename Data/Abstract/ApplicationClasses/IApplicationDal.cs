using Core.Data.Abstract.EntityFrameworkClasses;
using Entities.Concrete.ApplicationClasses;

namespace Data.Abstract.ApplicationClasses
{
    public interface IApplicationDal : IEntityRepository<Application> { }
}
