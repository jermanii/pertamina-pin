using AutoMapper;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class SequenceCounterRepository : ISequenceCounterRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public SequenceCounterRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public TblmSequenceCounter GetNewSequenceNumber(string name)
        {
            int year = DateTime.Now.Year;
            TblmSequenceCounter newItem = _context.Set<TblmSequenceCounter>().Find(name);
            if (newItem == null)
            {
                newItem = new TblmSequenceCounter();
                newItem.Name = name;
                newItem.Sequence = 1;
                newItem.Year = year;
                _context.Set<TblmSequenceCounter>().Add(newItem);
                _context.SaveChanges();
            }
            else
            {
                if (year != newItem.Year)
                {
                    newItem.Sequence = 1;
                }
                else
                {
                    newItem.Sequence = newItem.Sequence + 1;
                }
                newItem.Year = year;
                _context.Set<TblmSequenceCounter>().Update(newItem);
                _context.SaveChanges();
            }
            return newItem;
        }
    }

}
