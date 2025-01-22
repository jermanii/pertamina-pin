using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;

namespace Pertamina.IRIS.DependencyInjection
{
    public static class AutoMapperExtension
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<TblmEntitasPertamina, EntitasPertaminaViewModel>().ReverseMap();
                CreateMap<TblmFungsi, FungsiViewModel>().ReverseMap();
                CreateMap<TblmPicFungsi, PicFungsiViewModel>().ReverseMap();
                CreateMap<TblmInterest, InterestViewModel>().ReverseMap();
                CreateMap<TblmStreamBusiness, StreamBusinessViewModel>().ReverseMap();
                CreateMap<TblmTargetMitra, TargetMitraViewModel>().ReverseMap();
                CreateMap<TblmNegaraMitra, NegaraMitraViewModel>().ReverseMap();
                CreateMap<TblmNegaraMitraExcluded, NegaraMitraExcludedViewModel>().ReverseMap();
                CreateMap<TblmKesiapanProyek, KesiapanProyekViewModel>().ReverseMap();
                CreateMap<TblmKementrianLembaga, KementrianLembagaViewModel>().ReverseMap();
                CreateMap<TblmStatusBerlaku, StatusBerlakuViewModel>().ReverseMap();
                CreateMap<TblmDiscussionStatus, StatusDiscussionViewModel>().ReverseMap();
                CreateMap<TblmHsh, HshViewModel>().ReverseMap();
                CreateMap<TblmKlasifikasiKendala, KlasifikasiKendalaViewModel>().ReverseMap();
                CreateMap<TblmFaktorKendala, FaktorKendalaViewModel>().ReverseMap();
                CreateMap<TblmInitiativeStage, InitiativeStageViewModel>().ReverseMap();
                CreateMap<TblmInitiativeStatus, InitiativeStatusViewModel>().ReverseMap();
                CreateMap<TblmLinkPowerBi, LinkPowerBiViewModel>().ReverseMap();
                CreateMap<TblmRoleFeature, RoleFeatureViewModel>().ReverseMap();
                CreateMap<TblmRole, RoleViewModel>().ReverseMap();
                CreateMap<TblmProvinsi, ProvinsiViewModel>().ReverseMap();

                CreateMap<TbltAgreement, AgreementViewModel>().ReverseMap();
                CreateMap<TbltAgreementEntitasPertamina, AgreementEntitasPertaminaViewModel>().ReverseMap();
                CreateMap<TbltAgreementNegaraMitra, AgreementNegaraMitraViewModel>().ReverseMap();
                CreateMap<TbltAgreementPartner, AgreementPartnerViewModel>().ReverseMap();
                CreateMap<TbltExternalContact, ExternalContactViewModel>().ReverseMap();
                CreateMap<TbltAgreementPicFungsi, AgreementPicFungsiViewModel>().ReverseMap();
                CreateMap<TbltAgreementStreamBusiness, AgreementStreamBusinessViewModel>().ReverseMap();
                CreateMap<TbltAgreementLokasiProyek, AgreementLokasiProyekViewModel>().ReverseMap();
                CreateMap<TbltAgreementKementrianLembaga, AgreementKementrianLembagaViewModel>().ReverseMap();
                CreateMap<TbltAgreementHubHead, AgreementHubHeadViewModel>().ReverseMap();
                CreateMap<TbltAgreementAddendum, AgreementAddendumViewModel>().ReverseMap();

                CreateMap<TbltOpportunity, OpportunityViewModel>().ReverseMap();
                CreateMap<TbltOpportunityEntitasPertamina, OpportunityEntitasPertaminaViewModel>().ReverseMap();
                CreateMap<TbltOpportunityKesiapanProyek, OpportunityKesiapanProyekViewModel>().ReverseMap();
                CreateMap<TbltOpportunityNegaraMitra, OpportunityNegaraMitraViewModel>().ReverseMap();
                CreateMap<TbltOpportunityPicFungsi, OpportunityPicFungsiViewModel>().ReverseMap();
                CreateMap<TbltOpportunityTargetMitra, OpportunityTargetMitraViewModel>().ReverseMap();
                CreateMap<OpportunityViewModel, OpportunityStreamBusinessViewModel>().ReverseMap();
                CreateMap<TbltOpportunityStreamBusiness, OpportunityStreamBusinessViewModel>().ReverseMap();
                CreateMap<TbltOpportunityPartner, OpportunityPartnerViewModel>().ReverseMap();
                CreateMap<TbltOpportunityLokasiProyek, OpportunityLokasiProyekViewModel>().ReverseMap();
                CreateMap<TbltOpportunityHighPriority, OpportunityHighPriorityViewModel>().ReverseMap();

                CreateMap<TbltInitiative, InitiativeViewModel>().ReverseMap();
                CreateMap<TbltInitiativeEntitasPertamina, InitiativeEntitasPertaminaViewModel>().ReverseMap();
                CreateMap<TbltInitiativeNegaraMitra, InitiativeNegaraMitraViewModel>().ReverseMap();
                CreateMap<TbltInitiativeOpportunity, InitiativeOpportunityViewModel>().ReverseMap();
                CreateMap<TbltInitiativePicFungsi, InitiativePicFungsiViewModel>().ReverseMap();
                CreateMap<TbltInitiativeStreamBusiness, InitiativeStreamBusinessViewModel>().ReverseMap();
                CreateMap<TbltInitiativePartner, InitiativePartnerViewModel>().ReverseMap();
                CreateMap<TbltInitiativeLokasiProyek, InitiativeLokasiProyekViewModel>().ReverseMap();
                CreateMap<TbltInitiativeKementrianLembaga, InitiativeKementrianLembagaViewModel>().ReverseMap();

                CreateMap<TbltInternationalBusinessIntelligence, InternationalBusinessIntelligenceViewModel>().ReverseMap().ForMember(x => x.Confidentiality, x => x.Ignore());
                CreateMap<TbltInternationalBusinessIntelligenceAuthor, InternationalBusinessIntelligenceAuthorViewModel>().ReverseMap();
                CreateMap<TbltInternationalBusinessIntelligenceCountry, InternationalBusinessIntelligenceCountryViewModel>().ReverseMap();
                CreateMap<TbltInternationalBusinessIntelligenceDocument, InternationalBusinessIntelligenceDocumentViewModel>().ReverseMap();
                CreateMap<TbltInternationalBusinessIntelligenceMylist, InternationalBusinessIntelligenceMyListViewModel>().ReverseMap();

                CreateMap<TbltExistingFootprint, ExistingFootprintViewModel>().ReverseMap();
                CreateMap<TbltExistingFootprintsHubHead, ExistingFootprintsHubHeadViewModel>().ReverseMap();
                CreateMap<TbltExistingFootprintsPartner, ExistingFootprintsPartnersViewModel>().ReverseMap();
                CreateMap<TbltExistingFootprintsPic, ExistingFootprintsPICViewModel>().ReverseMap();
                CreateMap<TbltExistingFootprintsOperatingMetric, ExistingFootprintsOperatingMetricViewModel>().ReverseMap();
                CreateMap<TbltExistingFootprintsHighPriority, ExistingFootprintsHighPriorityViewModel>().ReverseMap();

                CreateMap<TbltNotificationBell, NotificationBellViewModel>().ReverseMap();
                CreateMap<TblmFormTransaction, FormTransactionViewModel>().ReverseMap();
                CreateMap<TblmStatusType, TransactionStatusTypeViewModel>().ReverseMap();
                CreateMap<TblmFeature, FeatureViewModel>().ReverseMap();
                CreateMap<TbltInitiativeHighPriority, InitiativeHighPriorityViewModel>().ReverseMap();
                CreateMap<TbltInitiativeMilestoneTimeline, InitiativeMilestoneTimelineViewModel>().ReverseMap();
                CreateMap<TblmHubHead, HubHeadViewModel>().ReverseMap();

                CreateMap<TblmStreamBusiness, ExistingFootprintsDashboardLegendViewModel>().ReverseMap();
                CreateMap<TbltAgreementHighPriority, SignedAgreementDashboardHighPriorityViewModel>().ReverseMap();
                CreateMap<TbltInitiativeHighPriority, PotentialInitiativeHighPriorityViewModel>().ReverseMap();

                CreateMap<TblmStoredProcedure, StoredProcedureViewModel>().ReverseMap();

                CreateMap<TbltMetric, MetricViewModel>().ReverseMap();                
            }
        }
    }
}
