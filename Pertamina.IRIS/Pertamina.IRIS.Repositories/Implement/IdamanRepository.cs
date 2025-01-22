using AutoMapper;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class IdamanRepository : BaseRepository<TblmIdamanUrl>, IIdamanRepository
    {
        public IdamanRepository(DB_PINMContext context, IMapper mapper) : base(context, mapper)
        {

        }
        public TblmIdamanUrl GetIdamanUrl(string nameEndPoint)
        {
            var query = _context.TblmIdamanUrls.Where(x => x.DeletedDate == null && x.NameEndPoint == nameEndPoint);
            return query.FirstOrDefault();
        }
    }
}
