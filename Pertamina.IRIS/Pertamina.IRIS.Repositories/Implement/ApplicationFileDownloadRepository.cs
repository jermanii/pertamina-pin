using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore; 
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Core; 
using System.Threading.Tasks;
using AutoMapper;
using Pertamina.IRIS.Models.Entities;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class ApplicationFileDownloadRepository : IApplicationFileDownloadRepository
    {
        protected readonly DB_PINMContext _context; 

        public ApplicationFileDownloadRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }
         
        public async Task<string> GetPath(string featureName)
        {
            string result = string.Empty;

            TblmDirectory GetRecord = await _context.TblmDirectories.Where(x => x.FeatureName == featureName).FirstOrDefaultAsync();

            if (GetRecord != null)
            {
                result = GetRecord.Path;
            }

            return result;
        }
         
    }
}