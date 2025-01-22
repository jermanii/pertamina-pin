using AutoMapper;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class FeatureRepository: IFeatureRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;
        public FeatureRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public FeatureViewModel GetFeatureById(string guid)
        {
            FeatureViewModel result = _mapper.Map<FeatureViewModel>(_context.TblmFeatures.Where(x => x.Id == guid).FirstOrDefault());
            return result;
        }
    }
}
