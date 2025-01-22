using Microsoft.Extensions.DependencyInjection;
using Pertamina.IRIS.Repositories.Implement;
using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.DependencyInjection
{
    public static class RepositoryInjection
    {
        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddTransient<IDropDownListRepository, DropDownListRepository>();
            services.AddTransient<ISideMenuRepository, SideMenuRepository>();
            services.AddTransient<IEntitasPertaminaRepository, EntitasPertaminaRepository>();
            services.AddTransient<IFungsiRepository, FungsiRepository>();
            services.AddTransient<IPicFungsiRepository, PicFungsiRepository>();
            services.AddTransient<IKesiapanProyekUnitOfWorkRepository, KesiapanProyekUnitOfWorkRepository>();
            services.AddTransient<IKesiapanProyekRepository, KesiapanProyekRepository>();
            services.AddTransient<IInterestRepository, InterestRepository>();
            services.AddTransient<IStreamBusinessRepository, StreamBusinessRepository>();
            services.AddTransient<ITargetMitraRepository, TargetMitraRepository>();
            services.AddTransient<IKementrianLembagaRepository, KementrianLembagaRepository>();
            services.AddTransient<IProjectsToOfferRepository, ProjectsToOfferRepository>();
            services.AddTransient<IInitiativeRepository, InitiativeRepository>();
            services.AddTransient<IAgreementRepository, AgreementRepository>();
            services.AddTransient<IKlasifikasiKendalaRepository, KlasifikasiKendalaRepository>();
            services.AddTransient<IExternalContactRepository, ExternalContactRepository>();
            services.AddTransient<ILandingPageRepository, LandingPageRepository>();
            services.AddTransient<IIdamanRepository, IdamanRepository>();
            services.AddTransient<ISequenceCounterRepository, SequenceCounterRepository>();

            #region existing footprints
            services.AddTransient<IExistingFootprintRepository, ExistingFootprintRepository>();
            services.AddTransient<IExistingFootprintsDashboardRepository, ExistingFootprintsDashboardRepository>();
            #endregion

            services.AddTransient<IHeaderMenuRepository, HeaderMenuRepository>();
            services.AddTransient<IFeatureRepository, FeatureRepository>();
            services.AddTransient<IInternationalBusinessIntellegenceRepository, InternationalBusinessIntellegenceRepository>();
            services.AddTransient<IPotentialInitiativeDashboardRepository, PotentialInitiativeDashboardRepository>();
            services.AddTransient<IApplicationFileDownloadRepository, ApplicationFileDownloadRepository>();
            services.AddTransient<ISignedAgreementDashboardRepository, SignedAgreementDashboardRepository>();
            services.AddTransient<IProjectsToOfferDashboardRepository, ProjectsToOfferDashboardRepository>();
            services.AddTransient<IDownloadPresentationRepository, DownloadPresentationRepository>();  

            #region Metric
            services.AddTransient<IMetricsRepository, MetricsRepository>();
            #endregion
            return services;
        }
    }
}
