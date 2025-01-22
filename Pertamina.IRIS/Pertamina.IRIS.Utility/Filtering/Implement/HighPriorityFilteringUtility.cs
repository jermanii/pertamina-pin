using LinqKit;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Utility.Filtering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Utility.Filtering.Implement
{
    public class HighPriorityFilteringUtility : IHighPriorityFilteringUtility
    {
        public IQueryable<ExistingFootprintsDashboardHighPriorityViewModel> GetFilterListExistingFootprints(IQueryable<ExistingFootprintsDashboardHighPriorityViewModel> query, Guid? negaraMitraId, Guid? streamBusinessId, Guid? entitasPertaminaId)
        {
            var predicateBuilder = PredicateBuilder.New<ExistingFootprintsDashboardHighPriorityViewModel>(true);

            if (negaraMitraId.HasValue && negaraMitraId != Guid.Empty)
                predicateBuilder.And(x => x.NegaraMitraId == negaraMitraId);

            if (streamBusinessId.HasValue && streamBusinessId != Guid.Empty)
                predicateBuilder.And(x => x.StreamBusinessId == streamBusinessId);

            if (entitasPertaminaId.HasValue && entitasPertaminaId != Guid.Empty)
                predicateBuilder.And(x => x.EntitasPertaminaId == entitasPertaminaId);

            return query.Where(predicateBuilder).OrderByDescending(x => x.Year);
        }

        public IQueryable<SignedAgreementDashboardHighPriorityViewModel> GetFilterListSignedAgreement(IQueryable<SignedAgreementDashboardHighPriorityViewModel> query, Guid? negaraMitraId, Guid? streamBusinessId, Guid? entitasPertaminaId)
        {
            var predicateBuilder = PredicateBuilder.New<SignedAgreementDashboardHighPriorityViewModel>(true);
            
            if (negaraMitraId.HasValue && negaraMitraId != Guid.Empty)
                predicateBuilder.And(x => x.NegaraMitraId == negaraMitraId);

            if (streamBusinessId.HasValue && streamBusinessId != Guid.Empty)
                predicateBuilder.And(x => x.Streams.Where(x => x.StreamBusinessId == streamBusinessId).Any());

            if (entitasPertaminaId.HasValue && entitasPertaminaId != Guid.Empty)
                predicateBuilder.And(x => x.Entitas.Where(x => x.EntitasPertaminaId == entitasPertaminaId).Any());

            return query.Where(predicateBuilder).OrderByDescending(x => x.TanggalTtd);
        }
    }
}