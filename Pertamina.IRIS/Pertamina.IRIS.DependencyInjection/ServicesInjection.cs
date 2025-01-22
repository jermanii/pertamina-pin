using Microsoft.Extensions.DependencyInjection;
using Pertamina.IRIS.Services.Implement;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.DependencyInjection
{
    public static class ServicesInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IDropDownListService, DropDownListService>();
            services.AddTransient<ISideMenuService, SideMenuService>();
            services.AddTransient<IEntitasPertaminaService, EntitasPertaminaService>();
            services.AddTransient<IFungsiService, FungsiService>();
            services.AddTransient<IPicFungsiService, PicFungsiService>();
            services.AddTransient<IKesiapanProyekService, KesiapanProyekService>();
            services.AddTransient<IInterestService, InterestService>();
            services.AddTransient<IStreamBusinessService, StreamBusinessService>();
            services.AddTransient<ITargetMitraService, TargetMitraService>();
            services.AddTransient<IKementrianLembagaService, KementrianLembagaService>();
            services.AddTransient<IProjectsToOfferService, ProjectsToOfferService>();
            services.AddTransient<IInitiativeService, InitiativeService>();
            services.AddTransient<IAgreementService, AgreementService>();
            services.AddTransient<IKlasifikasiKendalaService, KlasifikasiKendalaService>();
            services.AddTransient<IExternalContactService, ExternalContactService>();
            services.AddTransient<ILandingPageService, LandingPageService>();
            services.AddTransient<IIdamanService, IdamanService>();

            #region ExistingFootprints
            services.AddTransient<IExistingFootprintService, ExistingFootprintService>();
            services.AddTransient<IExistingFootprintsDashboardService, ExistingFootprintsDashboardService>();
            #endregion

            services.AddTransient<IHeaderMenuService, HeaderMenuService>();
            services.AddTransient<IInternationalBusinessIntellegenceService, InternationalBusinessIntellegenceService>();
            services.AddTransient<IApplicationFileDownloadService, ApplicationFileDownloadService>();
            services.AddTransient<IPotentialInitiativesDashboardService, PotentialInitiativesDashboardService>();
            services.AddTransient<ISignedAgreementDashboardService, SignedAgreementDashboardService>();
            services.AddTransient<IProjectsToOfferDashboardService, ProjectsToOfferDashboardService>();

            services.AddTransient<IPowerPointService, PowerPointService>();

            #region Metric
            services.AddTransient<IMetricsService, MetricsService>();
            #endregion
            return services;
        }
    }
}
