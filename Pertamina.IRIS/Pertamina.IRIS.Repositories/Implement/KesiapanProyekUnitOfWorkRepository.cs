using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class KesiapanProyekUnitOfWorkRepository : IKesiapanProyekUnitOfWorkRepository
    {
        private readonly DB_PINMContext context;

        public KesiapanProyekUnitOfWorkRepository(DB_PINMContext context, IKesiapanProyekRepository kesiapanProyekRepository)
        {
            this.context = context;
            KesiapanProyekRepository = kesiapanProyekRepository;
        }
        public IKesiapanProyekRepository KesiapanProyekRepository
        {
            get;
            private set;
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
