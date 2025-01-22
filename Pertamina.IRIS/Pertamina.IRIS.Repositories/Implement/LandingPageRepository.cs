using AutoMapper;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class LandingPageRepository : ILandingPageRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public LandingPageRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<LinkPowerBiViewModel> GetLinkPowerBI()
        {
            LinkPowerBiViewModel result = await Task.FromResult(_mapper.Map<LinkPowerBiViewModel>(_context.TblmLinkPowerBis.FirstOrDefault()));
            return result;
        }
        public Task<string> GetDownloadUserManual(string featureName)
        {
            string result = string.Empty;

            result = _context.TblmDirectories.Where(x => x.FeatureName == featureName).FirstOrDefault() .Path;

            return Task.FromResult(result);
        }
    }
}
