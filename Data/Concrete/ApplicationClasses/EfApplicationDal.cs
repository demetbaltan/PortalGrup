using Core.Data.Concrete.EntityFrameworkClasses;
using Data.Abstract.ApplicationClasses;
using Data.Context;
using Entities.Concrete.ApplicationClasses;

namespace Data.Concrete.ApplicationClasses
{
    public class EfApplicationDal : EntityRepositoryBase<Application, PortalDbContext>, IApplicationDal
    {
        private readonly PortalDbContext context;

        public EfApplicationDal(PortalDbContext context)
        {
            this.context = context;
        }
    }
}
