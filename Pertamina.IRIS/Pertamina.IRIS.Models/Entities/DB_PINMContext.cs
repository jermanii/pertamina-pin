using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class DB_PINMContext : DbContext
    {
        public DB_PINMContext()
        {
        }

        public DB_PINMContext(DbContextOptions<DB_PINMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblmBilanganCurrency> TblmBilanganCurrencies { get; set; }
        public virtual DbSet<TblmCategorySortingHighPriority> TblmCategorySortingHighPriorities { get; set; }
        public virtual DbSet<TblmConfidentiality> TblmConfidentialities { get; set; }
        public virtual DbSet<TblmContinent> TblmContinents { get; set; }
        public virtual DbSet<TblmDirectory> TblmDirectories { get; set; }
        public virtual DbSet<TblmDiscussionStatus> TblmDiscussionStatuses { get; set; }
        public virtual DbSet<TblmEntitasPertamina> TblmEntitasPertaminas { get; set; }
        public virtual DbSet<TblmExportFile> TblmExportFiles { get; set; }
        public virtual DbSet<TblmFaktorKendala> TblmFaktorKendalas { get; set; }
        public virtual DbSet<TblmFeature> TblmFeatures { get; set; }
        public virtual DbSet<TblmFormTransaction> TblmFormTransactions { get; set; }
        public virtual DbSet<TblmFungsi> TblmFungsis { get; set; }
        public virtual DbSet<TblmHsh> TblmHshes { get; set; }
        public virtual DbSet<TblmHub> TblmHubs { get; set; }
        public virtual DbSet<TblmHubHead> TblmHubHeads { get; set; }
        public virtual DbSet<TblmIconArrowTrendChart> TblmIconArrowTrendCharts { get; set; }
        public virtual DbSet<TblmIconArrowUpdownChart> TblmIconArrowUpdownCharts { get; set; }
        public virtual DbSet<TblmIconBarChart> TblmIconBarCharts { get; set; }
        public virtual DbSet<TblmIdamanUrl> TblmIdamanUrls { get; set; }
        public virtual DbSet<TblmInitiativeMilestoneStatus> TblmInitiativeMilestoneStatuses { get; set; }
        public virtual DbSet<TblmInitiativeMilestoneStep> TblmInitiativeMilestoneSteps { get; set; }
        public virtual DbSet<TblmInitiativeStage> TblmInitiativeStages { get; set; }
        public virtual DbSet<TblmInitiativeStatus> TblmInitiativeStatuses { get; set; }
        public virtual DbSet<TblmInitiativeType> TblmInitiativeTypes { get; set; }
        public virtual DbSet<TblmInterest> TblmInterests { get; set; }
        public virtual DbSet<TblmJenisPerjanjian> TblmJenisPerjanjians { get; set; }
        public virtual DbSet<TblmKawasanMitra> TblmKawasanMitras { get; set; }
        public virtual DbSet<TblmKementrianLembaga> TblmKementrianLembagas { get; set; }
        public virtual DbSet<TblmKesiapanProyek> TblmKesiapanProyeks { get; set; }
        public virtual DbSet<TblmKlasifikasiJenisPerjanjian> TblmKlasifikasiJenisPerjanjians { get; set; }
        public virtual DbSet<TblmKlasifikasiKendala> TblmKlasifikasiKendalas { get; set; }
        public virtual DbSet<TblmLinkPowerBi> TblmLinkPowerBis { get; set; }
        public virtual DbSet<TblmMenuHome> TblmMenuHomes { get; set; }
        public virtual DbSet<TblmNegaraMitra> TblmNegaraMitras { get; set; }
        public virtual DbSet<TblmNegaraMitraExcluded> TblmNegaraMitraExcludeds { get; set; }
        public virtual DbSet<TblmNegaraMitraInfomation> TblmNegaraMitraInfomations { get; set; }
        public virtual DbSet<TblmPicFungsi> TblmPicFungsis { get; set; }
        public virtual DbSet<TblmPicLevel> TblmPicLevels { get; set; }
        public virtual DbSet<TblmProgramDevelopmentMilestone> TblmProgramDevelopmentMilestones { get; set; }
        public virtual DbSet<TblmProgramDevelopmentMilestoneStatus> TblmProgramDevelopmentMilestoneStatuses { get; set; }
        public virtual DbSet<TblmProvinsi> TblmProvinsis { get; set; }
        public virtual DbSet<TblmRole> TblmRoles { get; set; }
        public virtual DbSet<TblmRoleFeature> TblmRoleFeatures { get; set; }
        public virtual DbSet<TblmRoleMenuHome> TblmRoleMenuHomes { get; set; }
        public virtual DbSet<TblmSequenceCounter> TblmSequenceCounters { get; set; }
        public virtual DbSet<TblmSourceFormAgreement> TblmSourceFormAgreements { get; set; }
        public virtual DbSet<TblmSourceFormInitiative> TblmSourceFormInitiatives { get; set; }
        public virtual DbSet<TblmStatusBerlaku> TblmStatusBerlakus { get; set; }
        public virtual DbSet<TblmStatusType> TblmStatusTypes { get; set; }
        public virtual DbSet<TblmStepStatus> TblmStepStatuses { get; set; }
        public virtual DbSet<TblmStoredProcedure> TblmStoredProcedures { get; set; }
        public virtual DbSet<TblmStoredProcedureParam> TblmStoredProcedureParams { get; set; }
        public virtual DbSet<TblmStreamBusiness> TblmStreamBusinesses { get; set; }
        public virtual DbSet<TblmSumberPengisianDatum> TblmSumberPengisianData { get; set; }
        public virtual DbSet<TblmTargetMitra> TblmTargetMitras { get; set; }
        public virtual DbSet<TblmTrafficLight> TblmTrafficLights { get; set; }
        public virtual DbSet<TblmTypeStudy> TblmTypeStudies { get; set; }
        public virtual DbSet<TbltAgreement> TbltAgreements { get; set; }
        public virtual DbSet<TbltAgreementAddendum> TbltAgreementAddenda { get; set; }
        public virtual DbSet<TbltAgreementEntitasPertamina> TbltAgreementEntitasPertaminas { get; set; }
        public virtual DbSet<TbltAgreementHighPriority> TbltAgreementHighPriorities { get; set; }
        public virtual DbSet<TbltAgreementHubHead> TbltAgreementHubHeads { get; set; }
        public virtual DbSet<TbltAgreementKementrianLembaga> TbltAgreementKementrianLembagas { get; set; }
        public virtual DbSet<TbltAgreementLokasiProyek> TbltAgreementLokasiProyeks { get; set; }
        public virtual DbSet<TbltAgreementMilestoneTimeline> TbltAgreementMilestoneTimelines { get; set; }
        public virtual DbSet<TbltAgreementNegaraMitra> TbltAgreementNegaraMitras { get; set; }
        public virtual DbSet<TbltAgreementPartner> TbltAgreementPartners { get; set; }
        public virtual DbSet<TbltAgreementPicFungsi> TbltAgreementPicFungsis { get; set; }
        public virtual DbSet<TbltAgreementStreamBusiness> TbltAgreementStreamBusinesses { get; set; }
        public virtual DbSet<TbltExistingFootprint> TbltExistingFootprints { get; set; }
        public virtual DbSet<TbltExistingFootprintsDescMap> TbltExistingFootprintsDescMaps { get; set; }
        public virtual DbSet<TbltExistingFootprintsHighPriority> TbltExistingFootprintsHighPriorities { get; set; }
        public virtual DbSet<TbltExistingFootprintsHubHead> TbltExistingFootprintsHubHeads { get; set; }
        public virtual DbSet<TbltExistingFootprintsOperatingMetric> TbltExistingFootprintsOperatingMetrics { get; set; }
        public virtual DbSet<TbltExistingFootprintsPartner> TbltExistingFootprintsPartners { get; set; }
        public virtual DbSet<TbltExistingFootprintsPic> TbltExistingFootprintsPics { get; set; }
        public virtual DbSet<TbltExternalContact> TbltExternalContacts { get; set; }
        public virtual DbSet<TbltInitiative> TbltInitiatives { get; set; }
        public virtual DbSet<TbltInitiativeContactPerson> TbltInitiativeContactPeople { get; set; }
        public virtual DbSet<TbltInitiativeEntitasPertamina> TbltInitiativeEntitasPertaminas { get; set; }
        public virtual DbSet<TbltInitiativeHighPriority> TbltInitiativeHighPriorities { get; set; }
        public virtual DbSet<TbltInitiativeHubHead> TbltInitiativeHubHeads { get; set; }
        public virtual DbSet<TbltInitiativeKementrianLembaga> TbltInitiativeKementrianLembagas { get; set; }
        public virtual DbSet<TbltInitiativeLokasiProyek> TbltInitiativeLokasiProyeks { get; set; }
        public virtual DbSet<TbltInitiativeMilestoneTimeline> TbltInitiativeMilestoneTimelines { get; set; }
        public virtual DbSet<TbltInitiativeNegaraMitra> TbltInitiativeNegaraMitras { get; set; }
        public virtual DbSet<TbltInitiativePartner> TbltInitiativePartners { get; set; }
        public virtual DbSet<TbltInitiativePicFungsi> TbltInitiativePicFungsis { get; set; }
        public virtual DbSet<TbltInitiativeStreamBusiness> TbltInitiativeStreamBusinesses { get; set; }
        public virtual DbSet<TbltInternationalBusinessIntelligence> TbltInternationalBusinessIntelligences { get; set; }
        public virtual DbSet<TbltInternationalBusinessIntelligenceAuthor> TbltInternationalBusinessIntelligenceAuthors { get; set; }
        public virtual DbSet<TbltInternationalBusinessIntelligenceCountry> TbltInternationalBusinessIntelligenceCountries { get; set; }
        public virtual DbSet<TbltInternationalBusinessIntelligenceDocument> TbltInternationalBusinessIntelligenceDocuments { get; set; }
        public virtual DbSet<TbltInternationalBusinessIntelligenceMylist> TbltInternationalBusinessIntelligenceMylists { get; set; }
        public virtual DbSet<TbltMetric> TbltMetrics { get; set; }
        public virtual DbSet<TbltNotificationBell> TbltNotificationBells { get; set; }
        public virtual DbSet<TbltOpportunity> TbltOpportunities { get; set; }
        public virtual DbSet<TbltOpportunityEntitasPertamina> TbltOpportunityEntitasPertaminas { get; set; }
        public virtual DbSet<TbltOpportunityHighPriority> TbltOpportunityHighPriorities { get; set; }
        public virtual DbSet<TbltOpportunityKesiapanProyek> TbltOpportunityKesiapanProyeks { get; set; }
        public virtual DbSet<TbltOpportunityLokasiProyek> TbltOpportunityLokasiProyeks { get; set; }
        public virtual DbSet<TbltOpportunityNegaraMitra> TbltOpportunityNegaraMitras { get; set; }
        public virtual DbSet<TbltOpportunityPartner> TbltOpportunityPartners { get; set; }
        public virtual DbSet<TbltOpportunityPicFungsi> TbltOpportunityPicFungsis { get; set; }
        public virtual DbSet<TbltOpportunityStreamBusiness> TbltOpportunityStreamBusinesses { get; set; }
        public virtual DbSet<TbltOpportunityTargetMitra> TbltOpportunityTargetMitras { get; set; }
        public virtual DbSet<TbltProgramDevelopment> TbltProgramDevelopments { get; set; }
        public virtual DbSet<TbltProgramDevelopmentEntitasPertamina> TbltProgramDevelopmentEntitasPertaminas { get; set; }
        public virtual DbSet<TbltProgramDevelopmentHighPriority> TbltProgramDevelopmentHighPriorities { get; set; }
        public virtual DbSet<TbltProgramDevelopmentKementrianLembaga> TbltProgramDevelopmentKementrianLembagas { get; set; }
        public virtual DbSet<TbltProgramDevelopmentLokasiProyek> TbltProgramDevelopmentLokasiProyeks { get; set; }
        public virtual DbSet<TbltProgramDevelopmentMilestoneTimeline> TbltProgramDevelopmentMilestoneTimelines { get; set; }
        public virtual DbSet<TbltProgramDevelopmentNegaraMitra> TbltProgramDevelopmentNegaraMitras { get; set; }
        public virtual DbSet<TbltProgramDevelopmentPartner> TbltProgramDevelopmentPartners { get; set; }
        public virtual DbSet<TbltProgramDevelopmentPicFungsi> TbltProgramDevelopmentPicFungsis { get; set; }
        public virtual DbSet<TbltProgramDevelopmentStreamBusiness> TbltProgramDevelopmentStreamBusinesses { get; set; }
        public virtual DbSet<VwExportAgreement> VwExportAgreements { get; set; }
        public virtual DbSet<VwExportExistingFootprint> VwExportExistingFootprints { get; set; }
        public virtual DbSet<VwExportInitiative> VwExportInitiatives { get; set; }
        public virtual DbSet<VwExportOpportunity> VwExportOpportunities { get; set; }
        public virtual DbSet<VwExportProjectToOffer> VwExportProjectToOffers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblmBilanganCurrency>(entity =>
            {
                entity.ToTable("TBLM_Bilangan_Currency");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Satuan)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmCategorySortingHighPriority>(entity =>
            {
                entity.ToTable("TBLM_Category_Sorting_High_Priority");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Display_Name");

                entity.Property(e => e.FeatureName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Feature_Name");

                entity.Property(e => e.FieldName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Field_Name");

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmConfidentiality>(entity =>
            {
                entity.ToTable("TBLM_Confidentiality");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.HexColor)
                    .HasMaxLength(25)
                    .HasColumnName("Hex_Color");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmContinent>(entity =>
            {
                entity.ToTable("TBLM_Continent");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ContinentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Continent_Name");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmDirectory>(entity =>
            {
                entity.ToTable("TBLM_Directory");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.FeatureName)
                    .HasMaxLength(200)
                    .HasColumnName("Feature_Name");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmDiscussionStatus>(entity =>
            {
                entity.ToTable("TBLM_Discussion_Status");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ColorHexa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Color_Hexa");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Color_Name");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.RelationSequence).HasColumnName("Relation_Sequence");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmEntitasPertamina>(entity =>
            {
                entity.ToTable("TBLM_Entitas_Pertamina");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActiveStatus)
                    .HasColumnName("Active_Status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Company_Name");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.HshId).HasColumnName("HSH_Id");

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Hsh)
                    .WithMany(p => p.TblmEntitasPertaminas)
                    .HasForeignKey(d => d.HshId)
                    .HasConstraintName("FK_TBLM_Entitas_Pertamina_TBLM_HSH");
            });

            modelBuilder.Entity<TblmExportFile>(entity =>
            {
                entity.ToTable("TBLM_Export_File");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.HtmlTemplate).HasColumnName("HTML_Template");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmFaktorKendala>(entity =>
            {
                entity.ToTable("TBLM_Faktor_Kendala");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmFeature>(entity =>
            {
                entity.ToTable("TBLM_Feature");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.ActionName)
                    .HasMaxLength(200)
                    .HasColumnName("Action_Name");

                entity.Property(e => e.AreaName)
                    .HasMaxLength(200)
                    .HasColumnName("Area_Name");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.ControllerName)
                    .HasMaxLength(200)
                    .HasColumnName("Controller_Name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.FeatureName)
                    .HasMaxLength(200)
                    .HasColumnName("Feature_Name");

                entity.Property(e => e.IsDashboard).HasColumnName("Is_Dashboard");

                entity.Property(e => e.IsMenu).HasColumnName("Is_Menu");

                entity.Property(e => e.MenuIcon)
                    .HasMaxLength(200)
                    .HasColumnName("Menu_Icon");

                entity.Property(e => e.MenuLink)
                    .HasMaxLength(50)
                    .HasColumnName("Menu_Link");

                entity.Property(e => e.ParentFeatureCode)
                    .HasMaxLength(50)
                    .HasColumnName("Parent_Feature_Code");

                entity.Property(e => e.SequenceChild).HasColumnName("Sequence_Child");

                entity.Property(e => e.SubMenuDesc)
                    .HasMaxLength(200)
                    .HasColumnName("Sub_Menu_Desc");

                entity.Property(e => e.SubMenuIcon)
                    .HasMaxLength(50)
                    .HasColumnName("Sub_Menu_Icon");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmFormTransaction>(entity =>
            {
                entity.ToTable("TBLM_Form_Transaction");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.FeatureId)
                    .HasMaxLength(50)
                    .HasColumnName("Feature_Id");

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.RelationSequence).HasColumnName("Relation_Sequence");

                entity.Property(e => e.TransactionForm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Transaction_Form");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.TblmFormTransactions)
                    .HasForeignKey(d => d.FeatureId)
                    .HasConstraintName("FK_TBLM_Form_Transaction_TBLM_Feature");
            });

            modelBuilder.Entity<TblmFungsi>(entity =>
            {
                entity.ToTable("TBLM_Fungsi");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EntitasPertaminaId).HasColumnName("Entitas_Pertamina_Id");

                entity.Property(e => e.FungsiName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Fungsi_Name");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.EntitasPertamina)
                    .WithMany(p => p.TblmFungsis)
                    .HasForeignKey(d => d.EntitasPertaminaId)
                    .HasConstraintName("FK_TBLM_Fungsi_TBLM_Entitas_Pertamina");
            });

            modelBuilder.Entity<TblmHsh>(entity =>
            {
                entity.ToTable("TBLM_HSH");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_seq");

                entity.Property(e => e.RelationSequence).HasColumnName("Relation_Sequence");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmHub>(entity =>
            {
                entity.ToTable("TBLM_Hub");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ColorHexa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Color_Hexa");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Color_Name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.Latitude).HasMaxLength(50);

                entity.Property(e => e.Longitude).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmHubHead>(entity =>
            {
                entity.ToTable("TBLM_Hub_Head");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Contact_Number");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.HubId).HasColumnName("Hub_Id");

                entity.Property(e => e.IsActive).HasColumnName("Is_Active");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(100)
                    .HasColumnName("User_Email");

                entity.HasOne(d => d.Hub)
                    .WithMany(p => p.TblmHubHeads)
                    .HasForeignKey(d => d.HubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLM_Hub_Head_TBLM_Hub");
            });

            modelBuilder.Entity<TblmIconArrowTrendChart>(entity =>
            {
                entity.ToTable("TBLM_Icon_Arrow_Trend_Chart");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ColorCode)
                    .HasMaxLength(25)
                    .HasColumnName("Color_Code");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.IsUp).HasColumnName("Is_Up");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Src).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmIconArrowUpdownChart>(entity =>
            {
                entity.ToTable("TBLM_Icon_Arrow_Updown_Chart");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ColorCode)
                    .HasMaxLength(25)
                    .HasColumnName("Color_Code");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.IsUp).HasColumnName("Is_Up");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Src).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmIconBarChart>(entity =>
            {
                entity.ToTable("TBLM_Icon_Bar_Chart");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Src).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");

                entity.Property(e => e.Year1)
                    .HasMaxLength(25)
                    .HasColumnName("Year_1");

                entity.Property(e => e.Year2)
                    .HasMaxLength(25)
                    .HasColumnName("Year_2");

                entity.Property(e => e.Year3)
                    .HasMaxLength(25)
                    .HasColumnName("Year_3");
            });

            modelBuilder.Entity<TblmIdamanUrl>(entity =>
            {
                entity.ToTable("TBLM_Idaman_Url");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.EndPointUrl)
                    .HasMaxLength(500)
                    .HasColumnName("End_Point_Url");

                entity.Property(e => e.NameEndPoint)
                    .HasMaxLength(250)
                    .HasColumnName("Name_End_Point");

                entity.Property(e => e.ParameterTake)
                    .HasMaxLength(500)
                    .HasColumnName("Parameter_Take");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmInitiativeMilestoneStatus>(entity =>
            {
                entity.ToTable("TBLM_Initiative_Milestone_Status");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ColorHexa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Color_Hexa");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Color_Name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Status_Name");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmInitiativeMilestoneStep>(entity =>
            {
                entity.ToTable("TBLM_Initiative_Milestone_Step");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ColorHexa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Color_Hexa");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Color_Name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RelationSequence).HasColumnName("Relation_Sequence");

                entity.Property(e => e.StepSequence).HasColumnName("Step_Sequence");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmInitiativeStage>(entity =>
            {
                entity.ToTable("TBLM_Initiative_Stages");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ColorHexa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Color_Hexa");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Color_Name");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.RelationSequence).HasColumnName("Relation_Sequence");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmInitiativeStatus>(entity =>
            {
                entity.ToTable("TBLM_Initiative_Status");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ColorHexa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Color_Hexa");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Color_Name");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.RelationSequence).HasColumnName("Relation_Sequence");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Status_Name");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmInitiativeType>(entity =>
            {
                entity.ToTable("TBLM_Initiative_Types");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ColorHexa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Color_Hexa");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Color_Name");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.RelationSequence).HasColumnName("Relation_Sequence");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Status_Name");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmInterest>(entity =>
            {
                entity.ToTable("TBLM_Interest");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmJenisPerjanjian>(entity =>
            {
                entity.ToTable("TBLM_Jenis_Perjanjian");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsFirst).HasColumnName("Is_First");

                entity.Property(e => e.IsNext).HasColumnName("Is_Next");

                entity.Property(e => e.IsOther).HasColumnName("Is_Other");

                entity.Property(e => e.IsSecret).HasColumnName("Is_Secret");

                entity.Property(e => e.IsStrategic)
                    .HasColumnName("Is_Strategic")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsValue).HasColumnName("Is_Value");

                entity.Property(e => e.KlasifikasiJenisPerjanjianId).HasColumnName("Klasifikasi_Jenis_Perjanjian_Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.Shortname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.KlasifikasiJenisPerjanjian)
                    .WithMany(p => p.TblmJenisPerjanjians)
                    .HasForeignKey(d => d.KlasifikasiJenisPerjanjianId)
                    .HasConstraintName("FK_TBLM_Jenis_Perjanjian_TBLM_Klasifikasi_Jenis_Perjanjian");
            });

            modelBuilder.Entity<TblmKawasanMitra>(entity =>
            {
                entity.ToTable("TBLM_Kawasan_Mitra");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ContinentId).HasColumnName("Continent_Id");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NamaKawasan)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Kawasan");

                entity.Property(e => e.OrderSeq).HasColumnName("Order_seq");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Continent)
                    .WithMany(p => p.TblmKawasanMitras)
                    .HasForeignKey(d => d.ContinentId)
                    .HasConstraintName("FK_TBLM_Kawasan_Mitra_TBLM_Continent");
            });

            modelBuilder.Entity<TblmKementrianLembaga>(entity =>
            {
                entity.ToTable("TBLM_Kementrian_Lembaga");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LembagaName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Lembaga_Name");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmKesiapanProyek>(entity =>
            {
                entity.ToTable("TBLM_Kesiapan_Proyek");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmKlasifikasiJenisPerjanjian>(entity =>
            {
                entity.ToTable("TBLM_Klasifikasi_Jenis_Perjanjian");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmKlasifikasiKendala>(entity =>
            {
                entity.ToTable("TBLM_Klasifikasi_Kendala");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmLinkPowerBi>(entity =>
            {
                entity.ToTable("TBLM_Link_Power_BI");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Menu)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Param1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Param2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblmMenuHome>(entity =>
            {
                entity.ToTable("TBLM_Menu_Home");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ActionName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Action_Name");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ControllerName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Controller_Name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MenuName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Menu_Name");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmNegaraMitra>(entity =>
            {
                entity.ToTable("TBLM_Negara_Mitra");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Flag).HasMaxLength(200);

                entity.Property(e => e.HubId).HasColumnName("Hub_Id");

                entity.Property(e => e.KawasanMitraId).HasColumnName("Kawasan_Mitra_Id");

                entity.Property(e => e.NamaNegara)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Negara");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Hub)
                    .WithMany(p => p.TblmNegaraMitras)
                    .HasForeignKey(d => d.HubId)
                    .HasConstraintName("FK_TBLM_Negara_Mitra_TBLM_Hub");

                entity.HasOne(d => d.KawasanMitra)
                    .WithMany(p => p.TblmNegaraMitras)
                    .HasForeignKey(d => d.KawasanMitraId)
                    .HasConstraintName("FK_TBLM_Negara_Mitra_TBLM_Kawasan_Mitra");
            });

            modelBuilder.Entity<TblmNegaraMitraExcluded>(entity =>
            {
                entity.ToTable("TBLM_Negara_Mitra_Excluded");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.NegaraMitraId).HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");

                entity.HasOne(d => d.NegaraMitra)
                    .WithMany(p => p.TblmNegaraMitraExcludeds)
                    .HasForeignKey(d => d.NegaraMitraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLM_Negara_Mitra_Excluded_TBLM_Negara_Mitra");
            });

            modelBuilder.Entity<TblmNegaraMitraInfomation>(entity =>
            {
                entity.ToTable("TBLM_Negara_Mitra_Infomation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CountryAcronyms)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Country_Acronyms");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.Gdp)
                    .HasMaxLength(200)
                    .HasColumnName("GDP");

                entity.Property(e => e.GdpPerCapita)
                    .HasMaxLength(200)
                    .HasColumnName("GDP_Per_Capita");

                entity.Property(e => e.NegaraMitraId).HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.OilGasReserves)
                    .HasMaxLength(200)
                    .HasColumnName("Oil_Gas_Reserves");

                entity.Property(e => e.OilProduction)
                    .HasMaxLength(200)
                    .HasColumnName("Oil_Production");

                entity.Property(e => e.Population).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");

                entity.HasOne(d => d.NegaraMitra)
                    .WithMany(p => p.TblmNegaraMitraInfomations)
                    .HasForeignKey(d => d.NegaraMitraId)
                    .HasConstraintName("FK_TBLM_Negara_Mitra_Infomation_TBLM_Negara_Mitra");
            });

            modelBuilder.Entity<TblmPicFungsi>(entity =>
            {
                entity.ToTable("TBLM_PIC_Fungsi");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActiveStatus).HasColumnName("Active_Status");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FungsiId).HasColumnName("Fungsi_Id");

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.Property(e => e.PicName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PIC_Name");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Fungsi)
                    .WithMany(p => p.TblmPicFungsis)
                    .HasForeignKey(d => d.FungsiId)
                    .HasConstraintName("FK_TBLM_PIC_Fungsi_TBLM_Fungsi");
            });

            modelBuilder.Entity<TblmPicLevel>(entity =>
            {
                entity.ToTable("TBLM_PIC_Level");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.IsLead).HasColumnName("Is_Lead");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmProgramDevelopmentMilestone>(entity =>
            {
                entity.ToTable("TBLM_Program_Development_Milestone");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ColorHexa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Color_Hexa");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Color_Name");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RelationSequence).HasColumnName("Relation_Sequence");

                entity.Property(e => e.StepSequence).HasColumnName("Step_Sequence");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmProgramDevelopmentMilestoneStatus>(entity =>
            {
                entity.ToTable("TBLM_Program_Development_Milestone_Status");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ColorHexa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Color_Hexa");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Color_Name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Status_Name");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmProvinsi>(entity =>
            {
                entity.ToTable("TBLM_Provinsi");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.NamaProvinsi)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Provinsi");

                entity.Property(e => e.NegaraMitraId).HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.OrderSeq).HasColumnName("Order_seq");

                entity.Property(e => e.ProvinceAcronyms)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Province_Acronyms");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.NegaraMitra)
                    .WithMany(p => p.TblmProvinsis)
                    .HasForeignKey(d => d.NegaraMitraId)
                    .HasConstraintName("FK_TBLM_Provinsi_TBLM_Negara_Mitra");
            });

            modelBuilder.Entity<TblmRole>(entity =>
            {
                entity.ToTable("TBLM_Role");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.IsAdd)
                    .HasColumnName("Is_Add")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAddExternalContact)
                    .HasColumnName("Is_Add_External_Contact")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("Is_Delete")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleteExternalContact)
                    .HasColumnName("Is_Delete_External_Contact")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDuplicateExternalContact)
                    .HasColumnName("Is_Duplicate_External_Contact")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsEdit)
                    .HasColumnName("Is_Edit")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsEditExternalContact)
                    .HasColumnName("Is_Edit_External_Contact")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmRoleFeature>(entity =>
            {
                entity.ToTable("TBLM_Role_Feature");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.FeatureId)
                    .HasMaxLength(50)
                    .HasColumnName("Feature_Id");

                entity.Property(e => e.IsCreate)
                    .HasColumnName("Is_Create")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("Is_Delete")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsRead)
                    .HasColumnName("Is_Read")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsUpdate)
                    .HasColumnName("Is_Update")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.TblmRoleFeatures)
                    .HasForeignKey(d => d.FeatureId)
                    .HasConstraintName("FK_TBLM_Role_Feature_TBLM_Feature");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblmRoleFeatures)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_TBLM_Role_Feature_TBLM_Role");
            });

            modelBuilder.Entity<TblmRoleMenuHome>(entity =>
            {
                entity.ToTable("TBLM_Role_Menu_Home");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.IsCreate).HasColumnName("Is_Create");

                entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");

                entity.Property(e => e.IsRead).HasColumnName("Is_Read");

                entity.Property(e => e.IsUpdate).HasColumnName("Is_Update");

                entity.Property(e => e.MenuHomeId).HasColumnName("Menu_Home_Id");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");

                entity.HasOne(d => d.MenuHome)
                    .WithMany(p => p.TblmRoleMenuHomes)
                    .HasForeignKey(d => d.MenuHomeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLM_Role_Menu_Home_TBLM_Menu_Home");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblmRoleMenuHomes)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLM_Role_Menu_Home_TBLM_Role");
            });

            modelBuilder.Entity<TblmSequenceCounter>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK_TBLM_Sequence_Counter_Name");

                entity.ToTable("TBLM_Sequence_Counter");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<TblmSourceFormAgreement>(entity =>
            {
                entity.ToTable("TBLM_Source_Form_Agreement");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.PilihanSumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Pilihan_Sumber");

                entity.Property(e => e.RelationSequence).HasColumnName("Relation_Sequence");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmSourceFormInitiative>(entity =>
            {
                entity.ToTable("TBLM_Source_Form_Initiative");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.PilihanSumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Pilihan_Sumber");

                entity.Property(e => e.RelationSequence).HasColumnName("Relation_Sequence");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmStatusBerlaku>(entity =>
            {
                entity.ToTable("TBLM_Status_Berlaku");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ColorHexa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Color_Hexa");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Color_Name");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.IsExpired).HasColumnName("Is_Expired");

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.RelationSequence).HasColumnName("Relation_Sequence");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Status_Name");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmStatusType>(entity =>
            {
                entity.ToTable("TBLM_Status_Type");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.FormTransactionId).HasColumnName("Form_Transaction_Id");

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");

                entity.HasOne(d => d.FormTransaction)
                    .WithMany(p => p.TblmStatusTypes)
                    .HasForeignKey(d => d.FormTransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLM_Status_Type_TBLM_Form_Transaction");
            });

            modelBuilder.Entity<TblmStepStatus>(entity =>
            {
                entity.ToTable("TBLM_Step_Status");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Group_Name");

                entity.Property(e => e.StepDescription)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Step_Description");

                entity.Property(e => e.StepNo).HasColumnName("Step_No");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmStoredProcedure>(entity =>
            {
                entity.ToTable("TBLM_Stored_Procedure");

                entity.HasIndex(e => e.SpName, "Uq_Sp_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Group)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.SpName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Sp_Name");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmStoredProcedureParam>(entity =>
            {
                entity.ToTable("TBLM_Stored_Procedure_Param");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.Parameter)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.StoredProcedureId).HasColumnName("Stored_Procedure_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.StoredProcedure)
                    .WithMany(p => p.TblmStoredProcedureParams)
                    .HasForeignKey(d => d.StoredProcedureId)
                    .HasConstraintName("FK_TBLM_Stored_Procedure_Param_TBLM_Stored_Procedure");
            });

            modelBuilder.Entity<TblmStreamBusiness>(entity =>
            {
                entity.ToTable("TBLM_Stream_Business");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ColorHexa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Color_Hexa");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Color_Name");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsGreenBusiness).HasColumnName("Is_Green_Business");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmSumberPengisianDatum>(entity =>
            {
                entity.ToTable("TBLM_Sumber_Pengisian_Data");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.NamaForm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Form");

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.PilihanSumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Pilihan_Sumber");

                entity.Property(e => e.RelationSequence).HasColumnName("Relation_Sequence");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmTargetMitra>(entity =>
            {
                entity.ToTable("TBLM_Target_Mitra");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TblmTrafficLight>(entity =>
            {
                entity.ToTable("TBLM_Traffic_Light");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.HexColor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Hex_Color");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TblmTypeStudy>(entity =>
            {
                entity.ToTable("TBLM_Type_Study");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TbltAgreement>(entity =>
            {
                entity.ToTable("TBLT_Agreement");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Capex).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CatatanTambahan)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Catatan_Tambahan");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .HasColumnName("Company_Code");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.DeskripsiKendala)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Deskripsi_Kendala");

                entity.Property(e => e.DiscussionStatusId).HasColumnName("Discussion_Status_Id");

                entity.Property(e => e.FaktorKendalaId).HasColumnName("Faktor_Kendala_Id");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.IsAddendum)
                    .HasColumnName("Is_Addendum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDraft).HasColumnName("Is_Draft");

                entity.Property(e => e.IsGtg)
                    .HasColumnName("Is_Gtg")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.JenisPerjanjianId).HasColumnName("Jenis_Perjanjian_Id");

                entity.Property(e => e.JudulPerjanjian)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Judul_Perjanjian");

                entity.Property(e => e.KlasifikasiKendalaId).HasColumnName("Klasifikasi_Kendala_Id");

                entity.Property(e => e.KodeAgreement)
                    .HasMaxLength(15)
                    .HasColumnName("Kode_Agreement");

                entity.Property(e => e.NilaiProyek)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nilai_Proyek");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.PotensiEskalasi)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Potensi_Eskalasi");

                entity.Property(e => e.PotentialRevenuePerYear)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Potential_Revenue_Per_Year");

                entity.Property(e => e.Progress)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.RelatedAgreementId).HasColumnName("Related_Agreement_Id");

                entity.Property(e => e.Scope)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.StatusBerlakuId).HasColumnName("Status_Berlaku_Id");

                entity.Property(e => e.SupportPemerintah)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Support_Pemerintah");

                entity.Property(e => e.TanggalBerakhir)
                    .HasColumnType("date")
                    .HasColumnName("Tanggal_Berakhir");

                entity.Property(e => e.TanggalTtd)
                    .HasColumnType("date")
                    .HasColumnName("Tanggal_TTD");

                entity.Property(e => e.TindakLanjut).HasColumnName("Tindak_Lanjut");

                entity.Property(e => e.TrafficLightId).HasColumnName("Traffic_Light_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.Property(e => e.Valuation).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<TbltAgreementAddendum>(entity =>
            {
                entity.ToTable("TBLT_Agreement_Addendum");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AddendumDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Addendum_Date");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.TbltAgreementAddenda)
                    .HasForeignKey(d => d.AgreementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Agreement_Addendum_TBLT_Agreement");
            });

            modelBuilder.Entity<TbltAgreementEntitasPertamina>(entity =>
            {
                entity.ToTable("TBLT_Agreement_Entitas_Pertamina");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.EntitasPertaminaId).HasColumnName("Entitas_Pertamina_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.TbltAgreementEntitasPertaminas)
                    .HasForeignKey(d => d.AgreementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Agreement_Entitas_Pertamina_TBLT_Agreement");

                entity.HasOne(d => d.EntitasPertamina)
                    .WithMany(p => p.TbltAgreementEntitasPertaminas)
                    .HasForeignKey(d => d.EntitasPertaminaId)
                    .HasConstraintName("FK_TBLT_Agreement_Entitas_Pertamina_TBLM_Entitas_Pertamina");
            });

            modelBuilder.Entity<TbltAgreementHighPriority>(entity =>
            {
                entity.ToTable("TBLT_Agreement_High_Priority");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.TbltAgreementHighPriorities)
                    .HasForeignKey(d => d.AgreementId)
                    .HasConstraintName("FK_TBLT_Agreement_High_Priority_TBLT_Agreement");
            });

            modelBuilder.Entity<TbltAgreementHubHead>(entity =>
            {
                entity.ToTable("TBLT_Agreement_Hub_Head");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.HubHeadId).HasColumnName("Hub_Head_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.TbltAgreementHubHeads)
                    .HasForeignKey(d => d.AgreementId)
                    .HasConstraintName("FK_TBLT_Agreement_Hub_Head_TBLT_Agreement");

                entity.HasOne(d => d.HubHead)
                    .WithMany(p => p.TbltAgreementHubHeads)
                    .HasForeignKey(d => d.HubHeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLT_Agreement_Hub_Head_TBLM_Hub_Head");
            });

            modelBuilder.Entity<TbltAgreementKementrianLembaga>(entity =>
            {
                entity.ToTable("TBLT_Agreement_Kementrian_Lembaga");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.KementrianLembagaId).HasColumnName("Kementrian_Lembaga_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.TbltAgreementKementrianLembagas)
                    .HasForeignKey(d => d.AgreementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Agreement_Kementrian_Lembaga_TBLT_Agreement");

                entity.HasOne(d => d.KementrianLembaga)
                    .WithMany(p => p.TbltAgreementKementrianLembagas)
                    .HasForeignKey(d => d.KementrianLembagaId)
                    .HasConstraintName("FK_TBLT_Agreement_Kementrian_Lembaga_TBLM_Kementrian_Lembaga");
            });

            modelBuilder.Entity<TbltAgreementLokasiProyek>(entity =>
            {
                entity.ToTable("TBLT_Agreement_Lokasi_Proyek");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.LokasiProyek)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Lokasi_Proyek");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.TbltAgreementLokasiProyeks)
                    .HasForeignKey(d => d.AgreementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Agreement_Lokasi_Proyek_TBLT_Agreement");
            });

            modelBuilder.Entity<TbltAgreementMilestoneTimeline>(entity =>
            {
                entity.ToTable("TBLT_Agreement_Milestone_Timeline");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ActualDate).HasColumnName("Actual_Date");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.MilestoneName)
                    .HasMaxLength(50)
                    .HasColumnName("Milestone_Name");

                entity.Property(e => e.TargetDate).HasColumnName("Target_Date");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.TbltAgreementMilestoneTimelines)
                    .HasForeignKey(d => d.AgreementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Agreement_Milestone_Timeline_TBLT_Agreement");
            });

            modelBuilder.Entity<TbltAgreementNegaraMitra>(entity =>
            {
                entity.ToTable("TBLT_Agreement_Negara_Mitra");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.NegaraMitraId).HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.TbltAgreementNegaraMitras)
                    .HasForeignKey(d => d.AgreementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Agreement_Negara_Mitra_TBLT_Agreement");

                entity.HasOne(d => d.NegaraMitra)
                    .WithMany(p => p.TbltAgreementNegaraMitras)
                    .HasForeignKey(d => d.NegaraMitraId)
                    .HasConstraintName("FK_TBLT_Agreement_Negara_Mitra_TBLM_Negara_Mitra");
            });

            modelBuilder.Entity<TbltAgreementPartner>(entity =>
            {
                entity.ToTable("TBLT_Agreement_Partners");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.PartnerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Partner_Name");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.TbltAgreementPartners)
                    .HasForeignKey(d => d.AgreementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Agreement_Partners_TBLT_Agreement");
            });

            modelBuilder.Entity<TbltAgreementPicFungsi>(entity =>
            {
                entity.ToTable("TBLT_Agreement_PIC_Fungsi");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.PicFungsiId).HasColumnName("PIC_Fungsi_Id");

                entity.Property(e => e.PicLevelId).HasColumnName("PIC_Level_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.TbltAgreementPicFungsis)
                    .HasForeignKey(d => d.AgreementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Agreement_PIC_Fungsi_TBLT_Agreement");

                entity.HasOne(d => d.PicFungsi)
                    .WithMany(p => p.TbltAgreementPicFungsis)
                    .HasForeignKey(d => d.PicFungsiId)
                    .HasConstraintName("FK_TBLT_Agreement_PIC_Fungsi_TBLM_Pic_Fungsi");

                entity.HasOne(d => d.PicLevel)
                    .WithMany(p => p.TbltAgreementPicFungsis)
                    .HasForeignKey(d => d.PicLevelId)
                    .HasConstraintName("FK_TBLT_Agreement_PIC_Fungsi_TBLM_PIC_Level");
            });

            modelBuilder.Entity<TbltAgreementStreamBusiness>(entity =>
            {
                entity.ToTable("TBLT_Agreement_Stream_Business");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.StreamBusinessId).HasColumnName("Stream_Business_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.TbltAgreementStreamBusinesses)
                    .HasForeignKey(d => d.AgreementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Agreement_Stream_Business_TBLT_Agreement");

                entity.HasOne(d => d.StreamBusiness)
                    .WithMany(p => p.TbltAgreementStreamBusinesses)
                    .HasForeignKey(d => d.StreamBusinessId)
                    .HasConstraintName("FK_TBLT_Agreement_Stream_Business_TBLM_Stream_Business");
            });

            modelBuilder.Entity<TbltExistingFootprint>(entity =>
            {
                entity.ToTable("TBLT_Existing_Footprints");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.EntitasPertaminaId).HasColumnName("Entitas_Pertamina_Id");

                entity.Property(e => e.HubId).HasColumnName("Hub_Id");

                entity.Property(e => e.NegaraMitraId).HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.StreamBusinessId).HasColumnName("Stream_Business_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.EntitasPertamina)
                    .WithMany(p => p.TbltExistingFootprints)
                    .HasForeignKey(d => d.EntitasPertaminaId)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_TBLM_Entitas_Pertamina");

                entity.HasOne(d => d.Hub)
                    .WithMany(p => p.TbltExistingFootprints)
                    .HasForeignKey(d => d.HubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_TBLM_Hub");

                entity.HasOne(d => d.NegaraMitra)
                    .WithMany(p => p.TbltExistingFootprints)
                    .HasForeignKey(d => d.NegaraMitraId)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_TBLM_Negara_Mitra");

                entity.HasOne(d => d.StreamBusiness)
                    .WithMany(p => p.TbltExistingFootprints)
                    .HasForeignKey(d => d.StreamBusinessId)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_TBLM_Stream_Business");
            });

            modelBuilder.Entity<TbltExistingFootprintsDescMap>(entity =>
            {
                entity.ToTable("TBLT_Existing_Footprints_Desc_Maps");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExistingFootprintsId).HasColumnName("Existing_Footprints_Id");

                entity.Property(e => e.NegaraMitraId).HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.OrderSeq).HasColumnName("Order_Seq");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.ExistingFootprints)
                    .WithMany(p => p.TbltExistingFootprintsDescMaps)
                    .HasForeignKey(d => d.ExistingFootprintsId)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_Desc_Maps_TBLT_Existing_Footprints");
            });

            modelBuilder.Entity<TbltExistingFootprintsHighPriority>(entity =>
            {
                entity.ToTable("TBLT_Existing_Footprints_High_Priority");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.ExistingFootprintsId).HasColumnName("Existing_Footprints_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.ExistingFootprints)
                    .WithMany(p => p.TbltExistingFootprintsHighPriorities)
                    .HasForeignKey(d => d.ExistingFootprintsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_High_Priority_TBLT_Existing_Footprints");
            });

            modelBuilder.Entity<TbltExistingFootprintsHubHead>(entity =>
            {
                entity.ToTable("TBLT_Existing_Footprints_Hub_Head");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.ExistingFootprintsId).HasColumnName("Existing_Footprints_Id");

                entity.Property(e => e.HubHeadId).HasColumnName("Hub_Head_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.ExistingFootprints)
                    .WithMany(p => p.TbltExistingFootprintsHubHeads)
                    .HasForeignKey(d => d.ExistingFootprintsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_Hub_Head_TBLT_Existing_FootPrints");

                entity.HasOne(d => d.HubHead)
                    .WithMany(p => p.TbltExistingFootprintsHubHeads)
                    .HasForeignKey(d => d.HubHeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_Hub_Head_TBLM_Hub_Head");
            });

            modelBuilder.Entity<TbltExistingFootprintsOperatingMetric>(entity =>
            {
                entity.ToTable("TBLT_Existing_Footprints_Operating_Metrics");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Ebitda).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExistingFootprintsId).HasColumnName("Existing_Footprints_Id");

                entity.Property(e => e.Revenue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalAsset)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Total_Asset");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.ExistingFootprints)
                    .WithMany(p => p.TbltExistingFootprintsOperatingMetrics)
                    .HasForeignKey(d => d.ExistingFootprintsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_Operating_Metrics_TBLT_Existing_Footprints");
            });

            modelBuilder.Entity<TbltExistingFootprintsPartner>(entity =>
            {
                entity.ToTable("TBLT_Existing_Footprints_Partners");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.ExistingFootPrintsId).HasColumnName("Existing_FootPrints_Id");

                entity.Property(e => e.Location).HasMaxLength(200);

                entity.Property(e => e.Partners).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.ExistingFootPrints)
                    .WithMany(p => p.TbltExistingFootprintsPartners)
                    .HasForeignKey(d => d.ExistingFootPrintsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_Partners_TBLT_Existing_Footprints");
            });

            modelBuilder.Entity<TbltExistingFootprintsPic>(entity =>
            {
                entity.ToTable("TBLT_Existing_Footprints_PIC");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.ExistingFootprintsId).HasColumnName("Existing_Footprints_Id");

                entity.Property(e => e.PicFungsiId).HasColumnName("PIC_Fungsi_Id");

                entity.Property(e => e.PicLevelId).HasColumnName("PIC_Level_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.ExistingFootprints)
                    .WithMany(p => p.TbltExistingFootprintsPics)
                    .HasForeignKey(d => d.ExistingFootprintsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_PIC_TBLT_Existing_Footprints");

                entity.HasOne(d => d.PicFungsi)
                    .WithMany(p => p.TbltExistingFootprintsPics)
                    .HasForeignKey(d => d.PicFungsiId)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_PIC_TBLM_PIC_Fungsi");

                entity.HasOne(d => d.PicLevel)
                    .WithMany(p => p.TbltExistingFootprintsPics)
                    .HasForeignKey(d => d.PicLevelId)
                    .HasConstraintName("FK_TBLT_Existing_Footprints_PIC_TBLM_PIC_Level");
            });

            modelBuilder.Entity<TbltExternalContact>(entity =>
            {
                entity.ToTable("TBLT_External_Contact");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AlamatCompany)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("Alamat_Company");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.EmailCompany)
                    .HasMaxLength(100)
                    .HasColumnName("Email_Company");

                entity.Property(e => e.EmailPerson)
                    .HasMaxLength(255)
                    .HasColumnName("Email_Person");

                entity.Property(e => e.JabatanPerson)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Jabatan_Person");

                entity.Property(e => e.KoneksiCompany)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Koneksi_Company");

                entity.Property(e => e.NamaCompany)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Company");

                entity.Property(e => e.NamaPerson)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Person");

                entity.Property(e => e.NoTelpCompany)
                    .HasMaxLength(50)
                    .HasColumnName("No_Telp_Company");

                entity.Property(e => e.NoTelpPerson)
                    .HasMaxLength(255)
                    .HasColumnName("No_Telp_Person");

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TbltInitiative>(entity =>
            {
                entity.ToTable("TBLT_Initiative");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.Capex).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CatatanTambahan)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Catatan_Tambahan");

                entity.Property(e => e.Comments)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .HasColumnName("Company_Code");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.InitiativeStageId).HasColumnName("Initiative_Stage_Id");

                entity.Property(e => e.InitiativeStatusId).HasColumnName("Initiative_Status_Id");

                entity.Property(e => e.InitiativeTypesId).HasColumnName("Initiative_Types_Id");

                entity.Property(e => e.InterestId).HasColumnName("Interest_Id");

                entity.Property(e => e.IsDraft).HasColumnName("Is_Draft");

                entity.Property(e => e.IsuKendala)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Isu_Kendala");

                entity.Property(e => e.JudulInisiasi)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Judul_Inisiasi");

                entity.Property(e => e.NilaiProyek)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nilai_Proyek");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.PotensiEskalasi)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Potensi Eskalasi");

                entity.Property(e => e.PotentialRevenuePerYear)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Potential_Revenue_Per_Year");

                entity.Property(e => e.Progress)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Referal)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Scope)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.SupportPemerintah)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Support_Pemerintah");

                entity.Property(e => e.TargetDefinitiveAgreement)
                    .HasColumnType("date")
                    .HasColumnName("Target_Definitive_Agreement");

                entity.Property(e => e.TargetWaktuProyek)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("Target_Waktu_Proyek");

                entity.Property(e => e.TindakLanjut).HasColumnName("Tindak_Lanjut");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.Property(e => e.ValueForIndonesia)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("Value_For_Indonesia");
            });

            modelBuilder.Entity<TbltInitiativeContactPerson>(entity =>
            {
                entity.ToTable("TBLT_Initiative_Contact_Person");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(200)
                    .HasColumnName("Phone_Number");

                entity.Property(e => e.Role)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Initiative)
                    .WithMany(p => p.TbltInitiativeContactPeople)
                    .HasForeignKey(d => d.InitiativeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Initiative_Contact_Person_TBLT_Initiative");
            });

            modelBuilder.Entity<TbltInitiativeEntitasPertamina>(entity =>
            {
                entity.ToTable("TBLT_Initiative_Entitas_Pertamina");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.EntitasPertaminaId).HasColumnName("Entitas_Pertamina_Id");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.EntitasPertamina)
                    .WithMany(p => p.TbltInitiativeEntitasPertaminas)
                    .HasForeignKey(d => d.EntitasPertaminaId)
                    .HasConstraintName("FK_TBLT_Initiative_Entitas_Pertamina_TBLM_Entitas_Pertamina");

                entity.HasOne(d => d.Initiative)
                    .WithMany(p => p.TbltInitiativeEntitasPertaminas)
                    .HasForeignKey(d => d.InitiativeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Initiative_Entitas_Pertamina_TBLT_Initiative");
            });

            modelBuilder.Entity<TbltInitiativeHighPriority>(entity =>
            {
                entity.ToTable("TBLT_Initiative_High_Priority");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Initiative)
                    .WithMany(p => p.TbltInitiativeHighPriorities)
                    .HasForeignKey(d => d.InitiativeId)
                    .HasConstraintName("FK_TBLT_Initiative_High_Priority_TBLT_Initiative");
            });

            modelBuilder.Entity<TbltInitiativeHubHead>(entity =>
            {
                entity.ToTable("TBLT_Initiative_Hub_Head");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.HubHeadId).HasColumnName("Hub_Head_Id");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.HubHead)
                    .WithMany(p => p.TbltInitiativeHubHeads)
                    .HasForeignKey(d => d.HubHeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBLT_Initiative_Hub_Head_TBLM_Hub_Head");

                entity.HasOne(d => d.Initiative)
                    .WithMany(p => p.TbltInitiativeHubHeads)
                    .HasForeignKey(d => d.InitiativeId)
                    .HasConstraintName("FK_TBLT_Initiative_Hub_Head_TBLT_Initiative");
            });

            modelBuilder.Entity<TbltInitiativeKementrianLembaga>(entity =>
            {
                entity.ToTable("TBLT_Initiative_Kementrian_Lembaga");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.KementrianLembagaId).HasColumnName("Kementrian_Lembaga_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Initiative)
                    .WithMany(p => p.TbltInitiativeKementrianLembagas)
                    .HasForeignKey(d => d.InitiativeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Initiative_Kementrian_Lembaga_TBLT_Initiative");

                entity.HasOne(d => d.KementrianLembaga)
                    .WithMany(p => p.TbltInitiativeKementrianLembagas)
                    .HasForeignKey(d => d.KementrianLembagaId)
                    .HasConstraintName("FK_TBLT_Initiative_Kementrian_Lembaga_TBLM_Kementrian_Lembaga");
            });

            modelBuilder.Entity<TbltInitiativeLokasiProyek>(entity =>
            {
                entity.ToTable("TBLT_Initiative_Lokasi_Proyek");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.LokasiProyek)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Lokasi_Proyek");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Initiative)
                    .WithMany(p => p.TbltInitiativeLokasiProyeks)
                    .HasForeignKey(d => d.InitiativeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Initiative_Lokasi_Proyek_TBLT_Initiative");
            });

            modelBuilder.Entity<TbltInitiativeMilestoneTimeline>(entity =>
            {
                entity.ToTable("TBLT_Initiative_Milestone_Timeline");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ActualDate).HasColumnName("Actual_Date");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.IsDone)
                    .HasColumnName("Is_Done")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MilestoneStatusId).HasColumnName("Milestone_Status_Id");

                entity.Property(e => e.MilestoneStepId).HasColumnName("Milestone_Step_Id");

                entity.Property(e => e.TargetDate).HasColumnName("Target_Date");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Initiative)
                    .WithMany(p => p.TbltInitiativeMilestoneTimelines)
                    .HasForeignKey(d => d.InitiativeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Initiative_Milestone_Timeline_TBLT_Initiative");

                entity.HasOne(d => d.MilestoneStatus)
                    .WithMany(p => p.TbltInitiativeMilestoneTimelines)
                    .HasForeignKey(d => d.MilestoneStatusId)
                    .HasConstraintName("FK_TBLT_Initiative_Milestone_Timeline_TBLM_Initiative_Milestone_Status");

                entity.HasOne(d => d.MilestoneStep)
                    .WithMany(p => p.TbltInitiativeMilestoneTimelines)
                    .HasForeignKey(d => d.MilestoneStepId)
                    .HasConstraintName("FK_TBLT_Initiative_Milestone_Timeline_TBLM_Initiative_Milestone_Step");
            });

            modelBuilder.Entity<TbltInitiativeNegaraMitra>(entity =>
            {
                entity.ToTable("TBLT_Initiative_Negara_Mitra");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus)
                    .HasColumnName("Deleted_Status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.NegaraMitraId).HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Initiative)
                    .WithMany(p => p.TbltInitiativeNegaraMitras)
                    .HasForeignKey(d => d.InitiativeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Initiative_Negara_Mitra_TBLT_Initiative");

                entity.HasOne(d => d.NegaraMitra)
                    .WithMany(p => p.TbltInitiativeNegaraMitras)
                    .HasForeignKey(d => d.NegaraMitraId)
                    .HasConstraintName("FK_TBLT_Initiative_Negara_Mitra_TBLM_Negara_Mitra");
            });

            modelBuilder.Entity<TbltInitiativePartner>(entity =>
            {
                entity.ToTable("TBLT_Initiative_Partners");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Contact_Number");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.PartnerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Partner_Name");

                entity.Property(e => e.Position)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Initiative)
                    .WithMany(p => p.TbltInitiativePartners)
                    .HasForeignKey(d => d.InitiativeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Initiative_Partners_TBLT_Initiative");
            });

            modelBuilder.Entity<TbltInitiativePicFungsi>(entity =>
            {
                entity.ToTable("TBLT_Initiative_PIC_Fungsi");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus)
                    .HasColumnName("Deleted_Status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.PicFungsiId).HasColumnName("PIC_Fungsi_Id");

                entity.Property(e => e.PicLevelId).HasColumnName("PIC_Level_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Initiative)
                    .WithMany(p => p.TbltInitiativePicFungsis)
                    .HasForeignKey(d => d.InitiativeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Initiative_PIC_Fungsi_TBLT_Initiative");

                entity.HasOne(d => d.PicFungsi)
                    .WithMany(p => p.TbltInitiativePicFungsis)
                    .HasForeignKey(d => d.PicFungsiId)
                    .HasConstraintName("FK_TBLT_Initiative_PIC_Fungsi_TBLM_PIC_Fungsi");

                entity.HasOne(d => d.PicLevel)
                    .WithMany(p => p.TbltInitiativePicFungsis)
                    .HasForeignKey(d => d.PicLevelId)
                    .HasConstraintName("FK_TBLT_Initiative_PIC_Fungsi_TBLM_PIC_Level");
            });

            modelBuilder.Entity<TbltInitiativeStreamBusiness>(entity =>
            {
                entity.ToTable("TBLT_Initiative_Stream_Business");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.StreamBusinessId).HasColumnName("Stream_Business_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Initiative)
                    .WithMany(p => p.TbltInitiativeStreamBusinesses)
                    .HasForeignKey(d => d.InitiativeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Initiative_Stream_Business_TBLT_Initiative");

                entity.HasOne(d => d.StreamBusiness)
                    .WithMany(p => p.TbltInitiativeStreamBusinesses)
                    .HasForeignKey(d => d.StreamBusinessId)
                    .HasConstraintName("FK_TBLT_Initiative_Stream_Business_TBLM_Stream_Business");
            });

            modelBuilder.Entity<TbltInternationalBusinessIntelligence>(entity =>
            {
                entity.ToTable("TBLT_International_Business_Intelligence");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ConfidentialityId).HasColumnName("Confidentiality_Id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.EntitasPertaminaId).HasColumnName("Entitas_Pertamina_Id");

                entity.Property(e => e.IsDraft).HasColumnName("Is_Draft");

                entity.Property(e => e.ResearchDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Research_Date");

                entity.Property(e => e.StreamBusinessId).HasColumnName("Stream_Business_Id");

                entity.Property(e => e.Title).HasMaxLength(400);

                entity.Property(e => e.TypeStudyId).HasColumnName("Type_Study_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");

                entity.HasOne(d => d.Confidentiality)
                    .WithMany(p => p.TbltInternationalBusinessIntelligences)
                    .HasForeignKey(d => d.ConfidentialityId)
                    .HasConstraintName("FK_TBLT_International_Business_Intelligence_TBLM_Confidentiality");

                entity.HasOne(d => d.EntitasPertamina)
                    .WithMany(p => p.TbltInternationalBusinessIntelligences)
                    .HasForeignKey(d => d.EntitasPertaminaId)
                    .HasConstraintName("FK_TBLT_International_Business_Intelligence_TBLM_Entitas_Pertamina");

                entity.HasOne(d => d.StreamBusiness)
                    .WithMany(p => p.TbltInternationalBusinessIntelligences)
                    .HasForeignKey(d => d.StreamBusinessId)
                    .HasConstraintName("FK_TBLT_International_Business_Intelligence_TBLM_Stream_Business");

                entity.HasOne(d => d.TypeStudy)
                    .WithMany(p => p.TbltInternationalBusinessIntelligences)
                    .HasForeignKey(d => d.TypeStudyId)
                    .HasConstraintName("FK_TBLT_International_Business_Intelligence_TBLM_Type_Study");
            });

            modelBuilder.Entity<TbltInternationalBusinessIntelligenceAuthor>(entity =>
            {
                entity.ToTable("TBLT_International_Business_Intelligence_Authors");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Company_Code");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.InternationalBusinessIntelligenceId).HasColumnName("International_Business_Intelligence_Id");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Position)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");

                entity.HasOne(d => d.InternationalBusinessIntelligence)
                    .WithMany(p => p.TbltInternationalBusinessIntelligenceAuthors)
                    .HasForeignKey(d => d.InternationalBusinessIntelligenceId)
                    .HasConstraintName("FK_TBLT_International_Business_Intelligence_Authors_TBLT_International_Business_Intelligence");
            });

            modelBuilder.Entity<TbltInternationalBusinessIntelligenceCountry>(entity =>
            {
                entity.ToTable("TBLT_International_Business_Intelligence_Country");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.InternationalBusinessIntelligenceId).HasColumnName("International_Business_Intelligence_Id");

                entity.Property(e => e.NegaraMitraId).HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");

                entity.HasOne(d => d.InternationalBusinessIntelligence)
                    .WithMany(p => p.TbltInternationalBusinessIntelligenceCountries)
                    .HasForeignKey(d => d.InternationalBusinessIntelligenceId)
                    .HasConstraintName("FK_TBLT_International_Business_Intelligence_Country_TBLT_International_Business_Intelligence");

                entity.HasOne(d => d.NegaraMitra)
                    .WithMany(p => p.TbltInternationalBusinessIntelligenceCountries)
                    .HasForeignKey(d => d.NegaraMitraId)
                    .HasConstraintName("FK_TBLT_International_Business_Intelligence_Country_TBLM_Negara_Mitra");
            });

            modelBuilder.Entity<TbltInternationalBusinessIntelligenceDocument>(entity =>
            {
                entity.ToTable("TBLT_International_Business_Intelligence_Document");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.FileNameSystem)
                    .HasMaxLength(200)
                    .HasColumnName("File_Name_System");

                entity.Property(e => e.FileNameUser)
                    .HasMaxLength(200)
                    .HasColumnName("File_Name_User");

                entity.Property(e => e.InternationalBusinessIntelligenceId).HasColumnName("International_Business_Intelligence_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");

                entity.HasOne(d => d.InternationalBusinessIntelligence)
                    .WithMany(p => p.TbltInternationalBusinessIntelligenceDocuments)
                    .HasForeignKey(d => d.InternationalBusinessIntelligenceId)
                    .HasConstraintName("FK_TBLT_International_Business_Intelligence_Document_TBLT_International_Business_Intelligence");
            });

            modelBuilder.Entity<TbltInternationalBusinessIntelligenceMylist>(entity =>
            {
                entity.ToTable("TBLT_International_Business_Intelligence_Mylist");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.InternationalBusinessIntelligenceId).HasColumnName("International_Business_Intelligence_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");

                entity.HasOne(d => d.InternationalBusinessIntelligence)
                    .WithMany(p => p.TbltInternationalBusinessIntelligenceMylists)
                    .HasForeignKey(d => d.InternationalBusinessIntelligenceId)
                    .HasConstraintName("FK_TBLT_International_Business_Intelligence_Mylist_TBLT_International_Business_Intelligence");
            });

            modelBuilder.Entity<TbltMetric>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TBLT_Metrics");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.Point).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TbltNotificationBell>(entity =>
            {
                entity.ToTable("TBLT_Notification_Bell");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.After).HasMaxLength(100);

                entity.Property(e => e.Before).HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate).HasColumnName("Deleted_Date");

                entity.Property(e => e.FormTransactionId).HasColumnName("Form_Transaction_Id");

                entity.Property(e => e.IsRead).HasColumnName("Is_Read");

                entity.Property(e => e.SentTo)
                    .HasMaxLength(50)
                    .HasColumnName("Sent_To");

                entity.Property(e => e.StatusTypeId).HasColumnName("Status_Type_Id");

                entity.Property(e => e.TransactionId).HasColumnName("Transaction_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate).HasColumnName("Updated_Date");
            });

            modelBuilder.Entity<TbltOpportunity>(entity =>
            {
                entity.ToTable("TBLT_Opportunity");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Capex).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CatatanTambahan)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Catatan_Tambahan");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .HasColumnName("Company_Code");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.Deliverables)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.IsDraft).HasColumnName("Is_Draft");

                entity.Property(e => e.NamaProyek)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Proyek");

                entity.Property(e => e.NilaiProyek)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nilai_Proyek");

                entity.Property(e => e.PotentialRevenuePerYear)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Potential_Revenue_Per_Year");

                entity.Property(e => e.Progress)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectProfile)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Project_Profile");

                entity.Property(e => e.Timeline)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.TindakLanjut).HasColumnName("Tindak_Lanjut");

                entity.Property(e => e.TypeOfPartnerNeeded)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Type_Of_Partner_Needed");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");
            });

            modelBuilder.Entity<TbltOpportunityEntitasPertamina>(entity =>
            {
                entity.ToTable("TBLT_Opportunity_Entitas_Pertamina");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.EntitasPertaminaId).HasColumnName("Entitas_Pertamina_Id");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.EntitasPertamina)
                    .WithMany(p => p.TbltOpportunityEntitasPertaminas)
                    .HasForeignKey(d => d.EntitasPertaminaId)
                    .HasConstraintName("FK_TBLT_Opportunity_Entitas_Pertamina_TBLM_Entitas_Pertamina");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.TbltOpportunityEntitasPertaminas)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Opportunity_Entitas_Pertamina_TBLT_Opportunity");
            });

            modelBuilder.Entity<TbltOpportunityHighPriority>(entity =>
            {
                entity.ToTable("TBLT_Opportunity_High_Priority");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.TbltOpportunityHighPriorities)
                    .HasForeignKey(d => d.OpportunityId)
                    .HasConstraintName("FK_TBLT_Opportunity_High_Priority_TBLT_Opportunity");
            });

            modelBuilder.Entity<TbltOpportunityKesiapanProyek>(entity =>
            {
                entity.ToTable("TBLT_Opportunity_Kesiapan_Proyek");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.KesiapanProyekId).HasColumnName("Kesiapan_Proyek_Id");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.KesiapanProyek)
                    .WithMany(p => p.TbltOpportunityKesiapanProyeks)
                    .HasForeignKey(d => d.KesiapanProyekId)
                    .HasConstraintName("FK_TBLT_Opportunity_Kesiapan_Proyek_TBLM_Kesiapan_Proyek");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.TbltOpportunityKesiapanProyeks)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Opportunity_Kesiapan_Proyek_TBLT_Opportunity");
            });

            modelBuilder.Entity<TbltOpportunityLokasiProyek>(entity =>
            {
                entity.ToTable("TBLT_Opportunity_Lokasi_Proyek");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.LokasiProyek)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Lokasi_Proyek");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.TbltOpportunityLokasiProyeks)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Opportunity_Lokasi_Proyek_TBLT_Opportunity");

                entity.HasOne(d => d.Provinsi)
                    .WithMany(p => p.TbltOpportunityLokasiProyeks)
                    .HasForeignKey(d => d.ProvinsiId)
                    .HasConstraintName("FK_TBLT_Opportunity_Lokasi_Proyek_TBLM_Provinsi");
            });

            modelBuilder.Entity<TbltOpportunityNegaraMitra>(entity =>
            {
                entity.ToTable("TBLT_Opportunity_Negara_Mitra");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.NegaraMitraId).HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.NegaraMitra)
                    .WithMany(p => p.TbltOpportunityNegaraMitras)
                    .HasForeignKey(d => d.NegaraMitraId)
                    .HasConstraintName("FK_TBLT_Opportunity_Negara_Mitra_TBLM_Negara_Mitra");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.TbltOpportunityNegaraMitras)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Opportunity_Negara_Mitra_TBLT_Opportunity");
            });

            modelBuilder.Entity<TbltOpportunityPartner>(entity =>
            {
                entity.ToTable("TBLT_Opportunity_Partners");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.PartnerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Partner_Name");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.TbltOpportunityPartners)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Opportunity_Partners_TBLT_Opportunity");
            });

            modelBuilder.Entity<TbltOpportunityPicFungsi>(entity =>
            {
                entity.ToTable("TBLT_Opportunity_PIC_Fungsi");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.PicFungsiId).HasColumnName("PIC_Fungsi_Id");

                entity.Property(e => e.PicLevelId).HasColumnName("PIC_Level_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.TbltOpportunityPicFungsis)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Opportunity_PIC_Fungsi_TBLT_Opportunity");

                entity.HasOne(d => d.PicFungsi)
                    .WithMany(p => p.TbltOpportunityPicFungsis)
                    .HasForeignKey(d => d.PicFungsiId)
                    .HasConstraintName("FK_TBLT_Opportunity_PIC_Fungsi_TBLM_PIC_Fungsi");

                entity.HasOne(d => d.PicLevel)
                    .WithMany(p => p.TbltOpportunityPicFungsis)
                    .HasForeignKey(d => d.PicLevelId)
                    .HasConstraintName("FK_TBLT_Opportunity_PIC_Fungsi_TBLM_PIC_Level");
            });

            modelBuilder.Entity<TbltOpportunityStreamBusiness>(entity =>
            {
                entity.ToTable("TBLT_Opportunity_Stream_Business");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.StreamBusinessId).HasColumnName("Stream_Business_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.TbltOpportunityStreamBusinesses)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Opportunity_Stream_Business_TBLT_Opportunity");

                entity.HasOne(d => d.StreamBusiness)
                    .WithMany(p => p.TbltOpportunityStreamBusinesses)
                    .HasForeignKey(d => d.StreamBusinessId)
                    .HasConstraintName("FK_TBLT_Opportunity_Stream_Business_TBLM_Stream_Business");
            });

            modelBuilder.Entity<TbltOpportunityTargetMitra>(entity =>
            {
                entity.ToTable("TBLT_Opportunity_Target_Mitra");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.TargetMitraId).HasColumnName("Target_Mitra_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.TbltOpportunityTargetMitras)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Opportunity_Target_Mitra_TBLT_Opportunity");

                entity.HasOne(d => d.TargetMitra)
                    .WithMany(p => p.TbltOpportunityTargetMitras)
                    .HasForeignKey(d => d.TargetMitraId)
                    .HasConstraintName("FK_TBLT_Opportunity_Target_Mitra_TBLM_Target_Mitra");
            });

            modelBuilder.Entity<TbltProgramDevelopment>(entity =>
            {
                entity.ToTable("TBLT_Program_Development");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.Capex).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CatatanTambahan)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Catatan_Tambahan");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .HasColumnName("Company_Code");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.InterestId).HasColumnName("Interest_Id");

                entity.Property(e => e.IsDraft).HasColumnName("Is_Draft");

                entity.Property(e => e.IsuKendala)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Isu_Kendala");

                entity.Property(e => e.JudulInisiasi)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Judul_Inisiasi");

                entity.Property(e => e.NilaiProyek)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nilai_Proyek");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.PotensiEskalasi)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Potensi Eskalasi");

                entity.Property(e => e.PotentialRevenue)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Potential_Revenue");

                entity.Property(e => e.ProgramDevelopmentStatusId).HasColumnName("Program_Development_Status_Id");

                entity.Property(e => e.Progress)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Referal)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Scope)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.SupportPemerintah)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Support_Pemerintah");

                entity.Property(e => e.TargetWaktuProyek)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("Target_Waktu_Proyek");

                entity.Property(e => e.TindakLanjut).HasColumnName("Tindak_Lanjut");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.Property(e => e.ValueForIndonesia)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("Value_For_Indonesia");
            });

            modelBuilder.Entity<TbltProgramDevelopmentEntitasPertamina>(entity =>
            {
                entity.ToTable("TBLT_Program_Development_Entitas_Pertamina");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.EntitasPertaminaId).HasColumnName("Entitas_Pertamina_Id");

                entity.Property(e => e.ProgramDevelopmentId).HasColumnName("Program_Development_Id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Update_By");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Date");

                entity.HasOne(d => d.EntitasPertamina)
                    .WithMany(p => p.TbltProgramDevelopmentEntitasPertaminas)
                    .HasForeignKey(d => d.EntitasPertaminaId)
                    .HasConstraintName("FK_TBLT_Program_Development_Entitas_Pertamina_TBLM_Entitas_Pertamina");

                entity.HasOne(d => d.ProgramDevelopment)
                    .WithMany(p => p.TbltProgramDevelopmentEntitasPertaminas)
                    .HasForeignKey(d => d.ProgramDevelopmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Program_Development_Entitas_Pertamina_TBLT_Program_Development");
            });

            modelBuilder.Entity<TbltProgramDevelopmentHighPriority>(entity =>
            {
                entity.ToTable("TBLT_Program_Development_High_Priority");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.ProgramDevelopmentId).HasColumnName("Program_Development_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.ProgramDevelopment)
                    .WithMany(p => p.TbltProgramDevelopmentHighPriorities)
                    .HasForeignKey(d => d.ProgramDevelopmentId)
                    .HasConstraintName("FK_TBLT_Program_Development_High_Priority_TBLT_Program_Development");
            });

            modelBuilder.Entity<TbltProgramDevelopmentKementrianLembaga>(entity =>
            {
                entity.ToTable("TBLT_Program_Development_Kementrian_Lembaga");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.KementrianLembagaId).HasColumnName("Kementrian_Lembaga_Id");

                entity.Property(e => e.ProgramDevelopmentId).HasColumnName("Program_Development_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.KementrianLembaga)
                    .WithMany(p => p.TbltProgramDevelopmentKementrianLembagas)
                    .HasForeignKey(d => d.KementrianLembagaId)
                    .HasConstraintName("FK_TBLT_Program_Development_Kementrian_Lembaga_TBLM_Kementrian_Lembaga");

                entity.HasOne(d => d.ProgramDevelopment)
                    .WithMany(p => p.TbltProgramDevelopmentKementrianLembagas)
                    .HasForeignKey(d => d.ProgramDevelopmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Program_Development_Kementrian_Lembaga_TBLT_Program_development");
            });

            modelBuilder.Entity<TbltProgramDevelopmentLokasiProyek>(entity =>
            {
                entity.ToTable("TBLT_Program_Development_Lokasi_Proyek");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.LokasiProyek)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Lokasi_Proyek");

                entity.Property(e => e.ProgramDevelopmentId).HasColumnName("Program_Development_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.ProgramDevelopment)
                    .WithMany(p => p.TbltProgramDevelopmentLokasiProyeks)
                    .HasForeignKey(d => d.ProgramDevelopmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Program_Development_Lokasi_Proyek_TBLT_Program_Development");
            });

            modelBuilder.Entity<TbltProgramDevelopmentMilestoneTimeline>(entity =>
            {
                entity.ToTable("TBLT_Program_Development_Milestone_Timeline");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ActualDate).HasColumnName("Actual_Date");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.MilestoneId).HasColumnName("Milestone_Id");

                entity.Property(e => e.ProgramDevelopmentId).HasColumnName("Program_Development_Id");

                entity.Property(e => e.TargetDate).HasColumnName("Target_Date");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.Milestone)
                    .WithMany(p => p.TbltProgramDevelopmentMilestoneTimelines)
                    .HasForeignKey(d => d.MilestoneId)
                    .HasConstraintName("FK_TBLT_Program_Development_Milestone_Timeline_TBLM_Program_Development_Milestone");

                entity.HasOne(d => d.ProgramDevelopment)
                    .WithMany(p => p.TbltProgramDevelopmentMilestoneTimelines)
                    .HasForeignKey(d => d.ProgramDevelopmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Program_Development_Milestone_Timeline_TBLT_Program_Development");
            });

            modelBuilder.Entity<TbltProgramDevelopmentNegaraMitra>(entity =>
            {
                entity.ToTable("TBLT_Program_Development_Negara_Mitra");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.NegaraMitraId).HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.ProgramDevelopmentId).HasColumnName("Program_Development_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.NegaraMitra)
                    .WithMany(p => p.TbltProgramDevelopmentNegaraMitras)
                    .HasForeignKey(d => d.NegaraMitraId)
                    .HasConstraintName("FK_TBLT_Program_Development_Negara_Mitra_TBLM_Negara_Mitra");

                entity.HasOne(d => d.ProgramDevelopment)
                    .WithMany(p => p.TbltProgramDevelopmentNegaraMitras)
                    .HasForeignKey(d => d.ProgramDevelopmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Program_Development_Negara_Mitra_TBLT_Program_Development");
            });

            modelBuilder.Entity<TbltProgramDevelopmentPartner>(entity =>
            {
                entity.ToTable("TBLT_Program_Development_Partners");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.PartnerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Partner_Name");

                entity.Property(e => e.ProgramDevelopmentId).HasColumnName("Program_Development_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.ProgramDevelopment)
                    .WithMany(p => p.TbltProgramDevelopmentPartners)
                    .HasForeignKey(d => d.ProgramDevelopmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Program_Development_Partners_TBLT_Program_Development");
            });

            modelBuilder.Entity<TbltProgramDevelopmentPicFungsi>(entity =>
            {
                entity.ToTable("TBLT_Program_Development_PIC_Fungsi");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus)
                    .HasColumnName("Deleted_Status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PicFungsiId).HasColumnName("PIC_Fungsi_Id");

                entity.Property(e => e.PicLevelId).HasColumnName("PIC_Level_Id");

                entity.Property(e => e.ProgramDevelopmentId).HasColumnName("Program_Development_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.PicFungsi)
                    .WithMany(p => p.TbltProgramDevelopmentPicFungsis)
                    .HasForeignKey(d => d.PicFungsiId)
                    .HasConstraintName("FK_TBLT_Program_Development_PIC_Fungsi_TBLM_PIC_Fungsi");

                entity.HasOne(d => d.PicLevel)
                    .WithMany(p => p.TbltProgramDevelopmentPicFungsis)
                    .HasForeignKey(d => d.PicLevelId)
                    .HasConstraintName("FK_TBLT_Program_Development_PIC_Fungsi_TBLM_PIC_Level");

                entity.HasOne(d => d.ProgramDevelopment)
                    .WithMany(p => p.TbltProgramDevelopmentPicFungsis)
                    .HasForeignKey(d => d.ProgramDevelopmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Program_Development_PIC_Fungsi_TBLT_Program_Development");
            });

            modelBuilder.Entity<TbltProgramDevelopmentStreamBusiness>(entity =>
            {
                entity.ToTable("TBLT_Program_Development_Stream_Business");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_Date");

                entity.Property(e => e.DeletedStatus).HasColumnName("Deleted_Status");

                entity.Property(e => e.ProgramDevelopmentId).HasColumnName("Program_Development_Id");

                entity.Property(e => e.StreamBusinessId).HasColumnName("Stream_Business_Id");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.HasOne(d => d.ProgramDevelopment)
                    .WithMany(p => p.TbltProgramDevelopmentStreamBusinesses)
                    .HasForeignKey(d => d.ProgramDevelopmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBLT_Program_Development_Stream_Business_TBLT_Program_Development");

                entity.HasOne(d => d.StreamBusiness)
                    .WithMany(p => p.TbltProgramDevelopmentStreamBusinesses)
                    .HasForeignKey(d => d.StreamBusinessId)
                    .HasConstraintName("FK_TBLT_Program_Development_Stream_Business_TBLM_Stream_Business");
            });

            modelBuilder.Entity<VwExportAgreement>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Export_Agreement");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_id");

                entity.Property(e => e.Amandement)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.Capex).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CatatanTambahan)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Catatan_Tambahan");

                entity.Property(e => e.DeskripsiKendala)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Deskripsi_Kendala");

                entity.Property(e => e.DiscussionStatus)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Discussion Status");

                entity.Property(e => e.DiscussionStatusId).HasColumnName("Discussion_Status_Id");

                entity.Property(e => e.EntitasPertamina)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Entitas Pertamina");

                entity.Property(e => e.EntitasPertaminaId)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Entitas_Pertamina_ID");

                entity.Property(e => e.FaktorKendala)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Faktor kendala");

                entity.Property(e => e.HshId)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("HSH_Id");

                entity.Property(e => e.HshName)
                    .HasMaxLength(4000)
                    .HasColumnName("HSH_Name");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.IsDraft).HasColumnName("Is_Draft");

                entity.Property(e => e.IsGreenBusiness).HasColumnName("Is_Green_Business");

                entity.Property(e => e.IsGtG).HasColumnName("Is_GtG");

                entity.Property(e => e.IsStrategic).HasColumnName("Is_Strategic");

                entity.Property(e => e.JenisPerjanjian)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Jenis Perjanjian");

                entity.Property(e => e.JenisPerjanjianId).HasColumnName("Jenis_Perjanjian_Id");

                entity.Property(e => e.JudulPerjanjian)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Judul_Perjanjian");

                entity.Property(e => e.KawasanMitraName)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Kawasan_Mitra_Name");

                entity.Property(e => e.KawsanMitraId)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Kawsan_Mitra_Id");

                entity.Property(e => e.KeterlibatanKementrian)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Keterlibatan Kementrian");

                entity.Property(e => e.KlasifikasiKendala)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Klasifikasi Kendala");

                entity.Property(e => e.KodeAgreement)
                    .HasMaxLength(15)
                    .HasColumnName("Kode_Agreement");

                entity.Property(e => e.KodeRelatedPerjanjian)
                    .HasMaxLength(15)
                    .HasColumnName("Kode_Related_Perjanjian");

                entity.Property(e => e.LokasiProyek)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Lokasi Proyek");

                entity.Property(e => e.NegaraMitra)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Negara_Mitra");

                entity.Property(e => e.NegaraMitraId)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.NilaiProyek)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Nilai_Proyek");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.Partners)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.PicLead)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("PIC Lead");

                entity.Property(e => e.PicMember)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("PIC Member");

                entity.Property(e => e.PotensiEskalasi)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Potensi_Eskalasi");

                entity.Property(e => e.Progress)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.RelatedPerjanjian)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Related Perjanjian");

                entity.Property(e => e.Revenue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Scope)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.StatusBerlaku)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Status Berlaku");

                entity.Property(e => e.StatusBerlakuId).HasColumnName("Status_Berlaku_Id");

                entity.Property(e => e.StreamBusiness)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Stream Business");

                entity.Property(e => e.StreamBusinessId)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Stream_Business_Id");

                entity.Property(e => e.SupportPemerintah)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Support_Pemerintah");

                entity.Property(e => e.TanggalBerakhir)
                    .HasColumnType("date")
                    .HasColumnName("Tanggal_Berakhir");

                entity.Property(e => e.TanggalDibuat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Tanggal_Dibuat");

                entity.Property(e => e.TanggalTtd)
                    .HasColumnType("date")
                    .HasColumnName("Tanggal_TTD");

                entity.Property(e => e.TindakLanjut).HasColumnName("Tindak_Lanjut");

                entity.Property(e => e.TrafficLight)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Traffic Light");

                entity.Property(e => e.Valuation).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<VwExportExistingFootprint>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Export_Existing_Footprints");

                entity.Property(e => e.Ebitda).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EntitasPertaminaId).HasColumnName("Entitas_Pertamina_Id");

                entity.Property(e => e.EntitasPertaminaName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Entitas_Pertamina_Name");

                entity.Property(e => e.ExistingFootPrintId).HasColumnName("Existing_FootPrint_Id");

                entity.Property(e => e.HubHead)
                    .HasMaxLength(100)
                    .HasColumnName("Hub_Head");

                entity.Property(e => e.HubId).HasColumnName("Hub_Id");

                entity.Property(e => e.HubName)
                    .HasMaxLength(200)
                    .HasColumnName("Hub_Name");

                entity.Property(e => e.LastYearDataMetrics).HasColumnName("Last_Year_Data_Metrics");

                entity.Property(e => e.NegaraMitraId).HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.PartnerCountry)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Partner_Country");

                entity.Property(e => e.Partners).HasMaxLength(4000);

                entity.Property(e => e.Pic).HasColumnName("PIC");

                entity.Property(e => e.Revenue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StreamBusinessId).HasColumnName("Stream_Business_Id");

                entity.Property(e => e.StreamBusinessName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Stream_Business_Name");

                entity.Property(e => e.TotalAsset)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Total_Asset");
            });

            modelBuilder.Entity<VwExportInitiative>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Export_Initiative");

                entity.Property(e => e.Capex).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CatatanTambahan)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Catatan_Tambahan");

                entity.Property(e => e.Comments)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(4000)
                    .HasColumnName("Contact_Person");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Create_By");

                entity.Property(e => e.EntitasPertaminaId)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Entitas_Pertamina_Id");

                entity.Property(e => e.HshId)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("HSH_id");

                entity.Property(e => e.HshName)
                    .HasMaxLength(4000)
                    .HasColumnName("HSH Name");

                entity.Property(e => e.HubHeadName)
                    .HasMaxLength(4000)
                    .HasColumnName("Hub_Head_Name");

                entity.Property(e => e.HubId)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Hub_Id");

                entity.Property(e => e.HubName)
                    .HasMaxLength(4000)
                    .HasColumnName("Hub_Name");

                entity.Property(e => e.InitiativeId).HasColumnName("Initiative_Id");

                entity.Property(e => e.InitiativeStageId).HasColumnName("Initiative_Stage_Id");

                entity.Property(e => e.InitiativeStatus)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Initiative_Status");

                entity.Property(e => e.InitiativeType)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Initiative_type");

                entity.Property(e => e.InitiativeTypeId).HasColumnName("Initiative_Type_Id");

                entity.Property(e => e.InititativeStatusId).HasColumnName("Inititative_Status_Id");

                entity.Property(e => e.InterestName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Interest_Name");

                entity.Property(e => e.IntiativeStage)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Intiative_Stage");

                entity.Property(e => e.IsDraft).HasColumnName("Is_Draft");

                entity.Property(e => e.IsuKendala)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Isu_Kendala");

                entity.Property(e => e.JudulInisiasi)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Judul_Inisiasi");

                entity.Property(e => e.KawasanMitra)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Kawasan Mitra");

                entity.Property(e => e.KawasanMitraId)
                    .HasMaxLength(4000)
                    .HasColumnName("Kawasan_Mitra_Id");

                entity.Property(e => e.KementrianLembaga)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Kementrian_Lembaga");

                entity.Property(e => e.KementrianLembagaId)
                    .HasMaxLength(4000)
                    .HasColumnName("Kementrian_Lembaga_Id");

                entity.Property(e => e.KodeAgreement)
                    .HasMaxLength(15)
                    .HasColumnName("Kode_Agreement");

                entity.Property(e => e.LastDateMilestone)
                    .HasColumnType("date")
                    .HasColumnName("Last_Date_Milestone");

                entity.Property(e => e.LastMilestone)
                    .HasMaxLength(115)
                    .IsUnicode(false)
                    .HasColumnName("Last_Milestone");

                entity.Property(e => e.LokasiProyek)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Lokasi Proyek");

                entity.Property(e => e.NegaraMitra)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Negara Mitra");

                entity.Property(e => e.NegaraMitraId)
                    .HasMaxLength(4000)
                    .HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.NextMilestone)
                    .IsRequired()
                    .HasMaxLength(115)
                    .IsUnicode(false)
                    .HasColumnName("Next_Milestone");

                entity.Property(e => e.NilaiProyek)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nilai_Proyek");

                entity.Property(e => e.Partners)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.Pertamina)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.PicLead)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("PIC_Lead");

                entity.Property(e => e.PicMembers)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("PIC_Members");

                entity.Property(e => e.PotensiEskalasi)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Potensi Eskalasi");

                entity.Property(e => e.Progress)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Referal)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.RelatedAgreement)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Related_Agreement");

                entity.Property(e => e.Revenue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Scope)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.StreamBusiness)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Stream Business");

                entity.Property(e => e.StreamBusinessId)
                    .HasMaxLength(4000)
                    .HasColumnName("Stream_Business_Id");

                entity.Property(e => e.SupportPemerintah)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Support_Pemerintah");

                entity.Property(e => e.TanggalDibuat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Tanggal_Dibuat");

                entity.Property(e => e.TargetDefinitiveAgreement)
                    .HasColumnType("date")
                    .HasColumnName("Target_Definitive_Agreement");

                entity.Property(e => e.TargetWaktuProyek)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("Target_Waktu_Proyek");

                entity.Property(e => e.TindakLanjut).HasColumnName("Tindak_Lanjut");

                entity.Property(e => e.ValueForIndonesia)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("Value_For_Indonesia");
            });

            modelBuilder.Entity<VwExportOpportunity>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Export_Opportunity");

                entity.Property(e => e.CatatanTambahan)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Catatan_Tambahan");

                entity.Property(e => e.Deliverables)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.DibuatOleh)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Dibuat_Oleh");

                entity.Property(e => e.EntitasPertamina)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Entitas_Pertamina");

                entity.Property(e => e.EntitasPertaminaId)
                    .HasMaxLength(4000)
                    .HasColumnName("Entitas_Pertamina_Id");

                entity.Property(e => e.ExistingParties)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Existing_Parties");

                entity.Property(e => e.HshId)
                    .HasMaxLength(4000)
                    .HasColumnName("HSH_Id");

                entity.Property(e => e.HshName)
                    .HasMaxLength(4000)
                    .HasColumnName("HSH_Name");

                entity.Property(e => e.IsDraft).HasColumnName("Is_Draft");

                entity.Property(e => e.KawasanMitra)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Kawasan_Mitra");

                entity.Property(e => e.KawasanMitraId)
                    .HasMaxLength(4000)
                    .HasColumnName("Kawasan_Mitra_Id");

                entity.Property(e => e.KesiapanProyek).HasColumnName("Kesiapan_Proyek");

                entity.Property(e => e.LokasiNegaraProyek)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Lokasi_Negara_Proyek");

                entity.Property(e => e.LokasiProyek)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Lokasi_Proyek");

                entity.Property(e => e.NamaProyek)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Proyek");

                entity.Property(e => e.NegaraMitraId)
                    .HasMaxLength(4000)
                    .HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.NilaiProyek)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nilai_Proyek");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.PicName)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Pic_Name");

                entity.Property(e => e.Progress)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectProfile)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Project_Profile");

                entity.Property(e => e.StreamBusiness)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Stream_Business");

                entity.Property(e => e.StreamBusinessId)
                    .HasMaxLength(4000)
                    .HasColumnName("Stream_Business_Id");

                entity.Property(e => e.TanggalBuat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Tanggal_Buat");

                entity.Property(e => e.TargetMitra)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Target_Mitra");

                entity.Property(e => e.Timeline)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.TindakLanjut).HasColumnName("Tindak_Lanjut");
            });

            modelBuilder.Entity<VwExportProjectToOffer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Export_Project_To_Offer");

                entity.Property(e => e.Capex).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CatatanTambahan)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Catatan_Tambahan");

                entity.Property(e => e.Deliverables)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.DibuatOleh)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Dibuat_Oleh");

                entity.Property(e => e.EntitasPertamina)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Entitas_Pertamina");

                entity.Property(e => e.EntitasPertaminaId)
                    .HasMaxLength(4000)
                    .HasColumnName("Entitas_Pertamina_Id");

                entity.Property(e => e.ExistingParties)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Existing_Parties");

                entity.Property(e => e.HshId)
                    .HasMaxLength(4000)
                    .HasColumnName("HSH_Id");

                entity.Property(e => e.HshName)
                    .HasMaxLength(4000)
                    .HasColumnName("HSH_Name");

                entity.Property(e => e.IsDraft).HasColumnName("Is_Draft");

                entity.Property(e => e.KawasanMitra)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Kawasan_Mitra");

                entity.Property(e => e.KawasanMitraId)
                    .HasMaxLength(4000)
                    .HasColumnName("Kawasan_Mitra_Id");

                entity.Property(e => e.KesiapanProyek).HasColumnName("Kesiapan_Proyek");

                entity.Property(e => e.LokasiNegaraProyek)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Lokasi_Negara_Proyek");

                entity.Property(e => e.LokasiProyek)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Lokasi_Proyek");

                entity.Property(e => e.NamaProyek)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Nama_Proyek");

                entity.Property(e => e.NegaraMitraId)
                    .HasMaxLength(4000)
                    .HasColumnName("Negara_Mitra_Id");

                entity.Property(e => e.NilaiProyek)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nilai_Proyek");

                entity.Property(e => e.OpportunityId).HasColumnName("Opportunity_Id");

                entity.Property(e => e.PicLead)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("PIC_Lead");

                entity.Property(e => e.PicMember)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("PIC_Member");

                entity.Property(e => e.PotentialRevenue)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Potential_Revenue");

                entity.Property(e => e.Progress)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectProfile)
                    .HasMaxLength(6000)
                    .IsUnicode(false)
                    .HasColumnName("Project_Profile");

                entity.Property(e => e.ProyekCountry)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Proyek_Country");

                entity.Property(e => e.StreamBusiness)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Stream_Business");

                entity.Property(e => e.StreamBusinessId)
                    .HasMaxLength(4000)
                    .HasColumnName("Stream_Business_Id");

                entity.Property(e => e.TanggalBuat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Tanggal_Buat");

                entity.Property(e => e.TargetMitra)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Target_Mitra");

                entity.Property(e => e.Timeline)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.TindakLanjut).HasColumnName("Tindak_Lanjut");

                entity.Property(e => e.TypeOfPartnerNeeded)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Type_Of_Partner_Needed");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
