using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Utility.Filtering.Interfaces
{
    public interface IHighPriorityFilteringUtility
    {
        IQueryable<ExistingFootprintsDashboardHighPriorityViewModel> GetFilterListExistingFootprints(IQueryable<ExistingFootprintsDashboardHighPriorityViewModel> query, Guid? negaraMitraId, Guid? streamBusinessId, Guid? entitasPertaminaId);
        IQueryable<SignedAgreementDashboardHighPriorityViewModel> GetFilterListSignedAgreement(IQueryable<SignedAgreementDashboardHighPriorityViewModel> query, Guid? negaraMitraId, Guid? streamBusinessId, Guid? entitasPertaminaId);
    }
}