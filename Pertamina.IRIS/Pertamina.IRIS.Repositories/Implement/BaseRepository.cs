using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Repositories.Interfaces;
using System;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        protected readonly IMapper _mapper;
        protected readonly DB_PINMContext _context;
        protected readonly DbSet<T> _entity;

        public BaseRepository(DB_PINMContext context, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entity = _context.Set<T>();
        }
    }
}
