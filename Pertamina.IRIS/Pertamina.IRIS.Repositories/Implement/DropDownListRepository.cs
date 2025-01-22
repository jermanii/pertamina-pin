using AutoMapper;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class DropDownListRepository : IDropDownListRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public DropDownListRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IQueryable<Select2BaseModel> DdlHsh(string request)
        {
            var result = (from rec in _context.TblmHshes
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlEntitasPertamina(string request)
        {
            var result = (from rec in _context.TblmEntitasPertaminas
                          where rec.DeletedDate == null && rec.ActiveStatus == true
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.CompanyName,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlEntitasPertaminaGrid(string request)
        {
            var result = (from rec in _context.TblmEntitasPertaminas
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.CompanyName,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlProvinsiIndonesia(string request)
        {
            var result = (from rec in _context.TblmProvinsis
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.NamaProvinsi,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlFungsi(string query)
        {
            var result = (from rec in _context.TblmFungsis
                          join entitas in _context.TblmEntitasPertaminas
                          on rec.EntitasPertaminaId equals entitas.Id
                          where rec.DeletedDate == null
                          orderby rec.FungsiName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = entitas.CompanyName + " - " + rec.FungsiName,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlStreamBusiness(string request)
        {
            var result = (from rec in _context.TblmStreamBusinesses
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlPic(string query)
        {
            var result = (from rec in _context.TblmPicFungsis
                          join fungsi in _context.TblmFungsis
                          on rec.FungsiId equals fungsi.Id
                          join entitas in _context.TblmEntitasPertaminas
                          on fungsi.EntitasPertaminaId equals entitas.Id
                          where rec.DeletedDate == null && rec.ActiveStatus == true
                          orderby rec.PicName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.PicName + " - " + fungsi.FungsiName + " - " + entitas.Code,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlPicLevel(string query)
        {
            var result = (from rec in _context.TblmPicLevels
                          where rec.DeletedDate == null
                          orderby rec.Name
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text =  rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlHubHead(string query)
        {
            var result = (from rec in _context.TblmHubHeads
                          where rec.DeletedDate == null && rec.IsActive == true
                          orderby rec.Name
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text =  rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlHub(string query)
        {
            var result = (from rec in _context.TblmHubs
                          where rec.DeletedDate == null
                          orderby rec.Name
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text =  rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlHshEntitasPertamina(string query)
        {
            var result = (from rec in _context.TblmEntitasPertaminas
                          join hsh in _context.TblmHshes
                          on rec.HshId equals hsh.Id
                          where rec.DeletedDate == null && rec.ActiveStatus == true
                          orderby rec.CompanyName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = hsh.Name + " - " + rec.CompanyName,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlRelatedAgreement(string query)
        {

            var result = (from rec in _context.TbltAgreements
                          join partner in _context.TbltAgreementPartners
                          on rec.Id equals partner.AgreementId
                          where rec.DeletedDate == null && rec.IsDraft != true
                          orderby rec.JudulPerjanjian
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = partner.PartnerName + " _ " + rec.KodeAgreement + " _ " + rec.JudulPerjanjian,
                          });

            return result;

        }
        public IQueryable<Select2BaseModel> DdlInterest(string request)
        {
            var result = (from rec in _context.TblmInterests
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlInitiativeStatus(string request)
        {
            var result = (from rec in _context.TblmInitiativeStatuses
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.StatusName,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlInitiativeTypes(string request)
        {
            var result = (from rec in _context.TblmInitiativeTypes
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.StatusName,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlInitiativeStages(string request)
        {
            var result = (from rec in _context.TblmInitiativeStages
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlKawasanMitra(string request)
        {
            var result = (from rec in _context.TblmKawasanMitras
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.NamaKawasan,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlNegaraMitra(string request)
        {
            var result = (from rec in _context.TblmNegaraMitras
                          join kawasan in _context.TblmKawasanMitras
                          on rec.KawasanMitraId equals kawasan.Id
                          where rec.DeletedDate == null
                          orderby rec.NamaNegara
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = kawasan.NamaKawasan + " - " + rec.NamaNegara,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlNegaraMitraOnlyNegara(string request)
        {
            var result = (from rec in _context.TblmNegaraMitras
                          where rec.DeletedDate == null
                          orderby rec.NamaNegara
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text =  rec.NamaNegara,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlNegaraMitraWithoutKawasan(string request)
        {
            var result = (from rec in _context.TblmNegaraMitras
                          where rec.DeletedDate == null
                          orderby rec.NamaNegara
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.NamaNegara,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlOpportunityEntitasPertamina(string request)
        {
            var result = (from rec in _context.TblmEntitasPertaminas
                          join hsh in _context.TblmHshes
                          on rec.HshId equals hsh.Id
                          where rec.DeletedDate == null && rec.ActiveStatus == true
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = hsh.Name + " - " + rec.CompanyName,
                          });
            return result.OrderBy(m => m.text);
        }
        public IQueryable<Select2BaseModel> DdlOpportunityPic(string request)
        {
            var result = (from rec in _context.TblmPicFungsis
                          join fungsi in _context.TblmFungsis
                          on rec.FungsiId equals fungsi.Id
                          join entitas in _context.TblmEntitasPertaminas
                          on fungsi.EntitasPertaminaId equals entitas.Id
                          where rec.DeletedDate == null && rec.ActiveStatus == true
                          orderby rec.PicName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.PicName + " - " + fungsi.FungsiName + " - " + entitas.Code,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlOpportunityKesiapanProyek(string request)
        {
            var result = (from rec in _context.TblmKesiapanProyeks
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlOpportunityNegara(string request)
        {
            var result = (from rec in _context.TblmNegaraMitras
                          join kawasan in _context.TblmKawasanMitras
                          on rec.KawasanMitraId equals kawasan.Id
                          where rec.DeletedDate == null
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = kawasan.NamaKawasan + " - " + rec.NamaNegara,
                          });
            return result.OrderBy(m => m.text);
        }
        public IQueryable<Select2BaseModel> DdlOpportunityTargetMitra(string request)
        {
            var result = (from rec in _context.TblmTargetMitras
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlOpportunity(string request)
        {
            var result = (from rec in _context.TbltOpportunities
                          where rec.DeletedDate == null
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.NamaProyek,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlStatusBerlaku(string query)
        {
            var result = (from rec in _context.TblmStatusBerlakus
                          where rec.DeletedDate == null
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.StatusName,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlTrafficLight(string query)
        {
            var result = (from rec in _context.TblmTrafficLights
                          where rec.DeletedDate == null
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                              color = rec.HexColor,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlStatusDiscussion(string query)
        {
            var result = (from rec in _context.TblmDiscussionStatuses
                          where rec.DeletedDate == null
                          orderby rec.Name ascending
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlJenisPerjanjian(string query)
        {
            var result = (from rec in _context.TblmJenisPerjanjians
                          where rec.DeletedDate == null
                          orderby rec.Name ascending
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlStreamBusiness(Guid guid)
        {
            var result = (from rec in _context.TblmStreamBusinesses
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IEnumerable<Select2BaseModel> GetDdlOpportunityStreamBusiness(Guid guid)
        {
            var result = (from rec in _context.TblmStreamBusinesses
                          join oep in _context.TbltOpportunityStreamBusinesses
                          on rec.Id equals oep.StreamBusinessId
                          where rec.DeletedDate == null && oep.OpportunityId == guid
                          orderby rec.Name
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name
                          }).ToList();
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlHshEntitasPertamina(Guid guid)
        {
            var result = (from rec in _context.TblmEntitasPertaminas
                          join hsh in _context.TblmHshes
                          on rec.HshId equals hsh.Id
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.CompanyName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = hsh.Name + " - " + rec.CompanyName,
                          });
            return result.OrderBy(m => m.text);
        }
        public IQueryable<Select2BaseModel> GetDdlFungsi(Guid guid)
        {
            var result = (from rec in _context.TblmFungsis
                          join entitas in _context.TblmEntitasPertaminas
                          on rec.EntitasPertaminaId equals entitas.Id
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.FungsiName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = entitas.CompanyName + " - " + rec.FungsiName,
                          });
            return result.OrderBy(m => m.text);
        }
        public IQueryable<Select2BaseModel> GetDdlPicLevel(Guid guid)
        {
            var result = (from rec in _context.TblmPicLevels
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.Name
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result.OrderBy(m => m.text);
        }
        public IQueryable<Select2BaseModel> GetDdlPicFungsi(Guid guid)
        {
            var result = (from rec in _context.TblmPicFungsis
                          join fungsi in _context.TblmFungsis
                          on rec.FungsiId equals fungsi.Id
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.PicName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.PicName + " - " + rec.Email + " - " + rec.Phone,
                          });
            return result.OrderBy(m => m.text);
        }
        public IQueryable<Select2BaseModel> GetDdlEntitasPertamina(Guid guid)
        {
            var result = (from rec in _context.TblmEntitasPertaminas
                          where rec.DeletedDate == null && rec.Id == guid && rec.ActiveStatus == true
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.CompanyName,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlHub(Guid guid)
        {
            var result = (from rec in _context.TblmHubs
                          where rec.DeletedDate == null && rec.Id == guid 
                          orderby rec.Name
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlHsh(Guid guid)
        {
            var result = (from rec in _context.TblmHshes
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlInterest(Guid guid)
        {
            var result = (from rec in _context.TblmInterests
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlInitiativeTypes(Guid guid)
        {
            var result = (from rec in _context.TblmInitiativeTypes
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.StatusName,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlInitiativeStages(Guid guid)
        {
            var result = (from rec in _context.TblmInitiativeStages
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlKawasanMitra(Guid guid)
        {
            var result = (from rec in _context.TblmKawasanMitras
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.NamaKawasan,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlNegaraMitra(Guid guid)
        {
            var result = (from rec in _context.TblmNegaraMitras
                          join kawasan in _context.TblmKawasanMitras
                          on rec.KawasanMitraId equals kawasan.Id
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.NamaNegara
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = kawasan.NamaKawasan + " - " + rec.NamaNegara,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlNegaraMitraOnlyNegara(Guid guid)
        {
            var result = (from rec in _context.TblmNegaraMitras
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.NamaNegara
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.NamaNegara,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlRelatedAgreement(Guid guid)
        {
            var result = (from rec in _context.TbltAgreements
                          join partner in _context.TbltAgreementPartners
                          on rec.Id equals partner.AgreementId
                          join initiative in _context.TbltInitiatives
                          on rec.Id equals initiative.AgreementId
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.JudulPerjanjian
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = partner.PartnerName + " _ " + rec.KodeAgreement + " _ " + rec.JudulPerjanjian,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlOpportunity(Guid guid)
        {
            var result = (from rec in _context.TbltOpportunities
                          where rec.DeletedDate == null && rec.Id == guid
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.NamaProyek,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> GetDdlInitiativeStatus(Guid guid)
        {
            var result = (from rec in _context.TblmInitiativeStatuses
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.StatusName,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlInitiativeForm(string request)
        {
            var result = (from rec in _context.TblmSourceFormInitiatives
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.RelationSequence.ToString(),
                              text = rec.PilihanSumber,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlAgreementForm(string request)
        {
            var result = (from rec in _context.TblmSourceFormAgreements
                          where rec.DeletedDate == null
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.RelationSequence.ToString(),
                              text = rec.PilihanSumber,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlNamaProyek(string query, string value)
        {
            if (query == null)
            {
                var result = (from rec in _context.TbltOpportunities
                              where rec.DeletedDate == null && rec.IsDraft != true &&
                                !_context.TbltInitiatives.Any(opportunity => opportunity.OpportunityId == rec.Id)
                              select new Select2BaseModel()
                              {
                                  id = rec.Id.ToString(),
                                  text = rec.NamaProyek,
                              });

                return result;
            }
            else
            {
                var result = (from rec in _context.TbltOpportunities
                              where rec.DeletedDate == null && rec.IsDraft != true &&
                                 !_context.TbltInitiatives.Any(opportunity => opportunity.OpportunityId == rec.Id) &&
                                    rec.NamaProyek.Contains(query)
                              select new Select2BaseModel()
                              {
                                  id = rec.Id.ToString(),
                                  text = rec.NamaProyek,
                              });

                return result;
            }

        }
        public IQueryable<Select2BaseModel> DdlNamaProyekInitiative(string query, string value)
        {
            if (query == null)
            {
                var result = (from rec in _context.TbltInitiatives
                              where rec.DeletedDate == null && rec.IsDraft != true &&
                                !_context.TbltAgreements.Any(initiative => initiative.InitiativeId == rec.Id)
                              select new Select2BaseModel()
                              {
                                  id = rec.Id.ToString(),
                                  text = rec.JudulInisiasi,
                              });

                return result;
            }
            else
            {
                var result = (from rec in _context.TbltInitiatives
                              where rec.DeletedDate == null && rec.IsDraft != true &&
                                !_context.TbltAgreements.Any(initiative => initiative.InitiativeId == rec.Id) &&
                                    rec.JudulInisiasi.Contains(query)
                              select new Select2BaseModel()
                              {
                                  id = rec.Id.ToString(),
                                  text = rec.JudulInisiasi,
                              });

                return result;
            }
        }
        public IEnumerable<Select2BaseModel> GetDdlInitiativePicFungsi(Guid guid)
        {
            var result = (from rec in _context.TblmPicFungsis
                          join fungsi in _context.TblmFungsis
                          on rec.FungsiId equals fungsi.Id
                          join entitas in _context.TblmEntitasPertaminas
                          on fungsi.EntitasPertaminaId equals entitas.Id
                          join oep in _context.TbltInitiativePicFungsis
                          on rec.Id equals oep.PicFungsiId
                          where rec.DeletedDate == null && oep.InitiativeId == guid && rec.ActiveStatus == true
                          orderby rec.PicName ascending
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.PicName + " - " + fungsi.FungsiName + " - " + entitas.Code,
                          }).ToList();
            return result.OrderBy(m => m.text);
        }
        public IEnumerable<Select2BaseModel> GetDdlInitiativeStreamBusiness(Guid guid)
        {
            var result = (from rec in _context.TblmStreamBusinesses
                          join oep in _context.TbltInitiativeStreamBusinesses
                          on rec.Id equals oep.StreamBusinessId
                          where rec.DeletedDate == null && oep.InitiativeId == guid
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name
                          }).ToList();
            return result.OrderBy(m => m.text);
        }
        public IEnumerable<Select2BaseModel> GetDdlInitiativeEntitasPertamina(Guid guid)
        {
            var result = (from rec in _context.TblmEntitasPertaminas
                          join hsh in _context.TblmHshes
                          on rec.HshId equals hsh.Id
                          join oep in _context.TbltInitiativeEntitasPertaminas
                          on rec.Id equals oep.EntitasPertaminaId
                          where rec.DeletedDate == null && oep.InitiativeId == guid && rec.ActiveStatus == true
                          orderby rec.CompanyName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = hsh.Name + " - " + rec.CompanyName,
                          }).ToList();
            return result.OrderBy(m => m.text);
        }
        public IQueryable<Select2BaseModel> DdlExistingFootprintsSortingHighPriority(string query, string existingFootprintsSortingHighPriority)
        {
            var result = (from rec in _context.TblmCategorySortingHighPriorities
                          where rec.DeletedDate == null && rec.FeatureName == existingFootprintsSortingHighPriority
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.FieldName.ToString(),
                              text = rec.DisplayName,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlSignedAgreementSortingHighPriority(string query, string SignedAgreementSortingHighPriority)
        {
            var result = (from rec in _context.TblmCategorySortingHighPriorities
                          where rec.DeletedDate == null && rec.FeatureName == SignedAgreementSortingHighPriority
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.FieldName.ToString(),
                              text = rec.DisplayName,
                          });
            return result;
        }

        //Get By OpportunityId
        public IEnumerable<Select2BaseModel> GetDdlOpportunityEntitasPertamina(Guid guid)
        {
            var result = (from rec in _context.TblmEntitasPertaminas
                          join hsh in _context.TblmHshes
                          on rec.HshId equals hsh.Id
                          join oep in _context.TbltOpportunityEntitasPertaminas
                          on rec.Id equals oep.EntitasPertaminaId
                          where rec.DeletedDate == null && oep.OpportunityId == guid && rec.ActiveStatus == true
                          orderby rec.CompanyName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = hsh.Name + " - " + rec.CompanyName,
                          }).ToList();
            return result.OrderBy(m => m.text);
        }
        public IEnumerable<Select2BaseModel> GetDdlOpportunityPic(Guid guid)
        {
            var result = (from rec in _context.TblmPicFungsis
                          join fungsi in _context.TblmFungsis
                          on rec.FungsiId equals fungsi.Id
                          join entitas in _context.TblmEntitasPertaminas
                          on fungsi.EntitasPertaminaId equals entitas.Id
                          join opf in _context.TbltOpportunityPicFungsis
                          on rec.Id equals opf.PicFungsiId
                          where rec.DeletedDate == null && rec.ActiveStatus == true && opf.OpportunityId == guid
                          orderby rec.PicName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.PicName + " - " + fungsi.FungsiName + " - " + entitas.Code,
                          }).ToList();
            return result;
        }
        public IEnumerable<Select2BaseModel> GetDdlOpportunityKesiapanProyek(Guid guid)
        {
            var result = (from rec in _context.TblmKesiapanProyeks
                          join okp in _context.TbltOpportunityKesiapanProyeks
                          on rec.Id equals okp.KesiapanProyekId
                          where rec.DeletedDate == null && okp.OpportunityId == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          }).ToList();
            return result;
        }
        public IEnumerable<Select2BaseModel> GetDdlOpportunityNegara(Guid guid)
        {
            var result = (from rec in _context.TblmNegaraMitras
                          join kawasan in _context.TblmKawasanMitras
                          on rec.KawasanMitraId equals kawasan.Id
                          join onm in _context.TbltOpportunityNegaraMitras
                          on rec.Id equals onm.NegaraMitraId
                          where rec.DeletedDate == null && onm.OpportunityId == guid
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = kawasan.NamaKawasan + " - " + rec.NamaNegara,
                          }).ToList();
            return result.OrderBy(m => m.text);
        }
        public IEnumerable<Select2BaseModel> GetDdlInitiativeNegara(Guid guid)
        {
            var result = (from rec in _context.TblmNegaraMitras
                          join kawasan in _context.TblmKawasanMitras
                          on rec.KawasanMitraId equals kawasan.Id
                          join onm in _context.TbltInitiativeNegaraMitras
                          on rec.Id equals onm.NegaraMitraId
                          where rec.DeletedDate == null && onm.InitiativeId == guid
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = kawasan.NamaKawasan + " - " + rec.NamaNegara,
                          }).ToList();
            return result.OrderBy(m => m.text);
        }
        public IEnumerable<Select2BaseModel> GetDdlOpportunityTargetMitra(Guid guid)
        {
            var result = (from rec in _context.TblmTargetMitras
                          join otm in _context.TbltOpportunityTargetMitras
                          on rec.Id equals otm.TargetMitraId
                          where rec.DeletedDate == null && otm.OpportunityId == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          }).ToList();
            return result;
        }
        public IQueryable<Select2BaseModel> DdlFaktorKendala(string query)
        {
            var result = (from rec in _context.TblmFaktorKendalas
                          where rec.DeletedDate == null
                          orderby rec.Name ascending
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlKlasifikasiKendala(string query)
        {
            var result = (from rec in _context.TblmKlasifikasiKendalas
                          where rec.DeletedDate == null
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlKeterlibatanLembaga(string query)
        {
            var result = (from rec in _context.TblmKementrianLembagas
                          where rec.DeletedDate == null
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.LembagaName,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlYear(string query)
        {
            int startYear = 1990;
            int endYear = DateTime.Now.Year +10  ;
            var yearList = Enumerable.Range(startYear, endYear - startYear +1)
                .Select(year => new Select2BaseModel()
                {
                    id = year.ToString(),
                    text = year.ToString(),
                }).AsQueryable();
            return yearList;
        }
        public IEnumerable<Select2BaseModel> GetDdlAgreementStreamBusiness(Guid guid)
        {
            var result = (from rec in _context.TblmStreamBusinesses
                          join oep in _context.TbltAgreementStreamBusinesses
                          on rec.Id equals oep.StreamBusinessId
                          where rec.DeletedDate == null && oep.AgreementId == guid
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name
                          }).ToList();
            return result.OrderBy(m => m.text);
        }
        public IEnumerable<Select2BaseModel> GetDdlAgreementPicFungsi(Guid guid)
        {
            var result = (from rec in _context.TblmPicFungsis
                          join fungsi in _context.TblmFungsis
                          on rec.FungsiId equals fungsi.Id
                          join entitas in _context.TblmEntitasPertaminas
                          on fungsi.EntitasPertaminaId equals entitas.Id
                          join oep in _context.TbltAgreementPicFungsis
                          on rec.Id equals oep.PicFungsiId
                          where rec.DeletedDate == null && oep.AgreementId == guid && rec.ActiveStatus == true
                          orderby rec.PicName ascending
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.PicName + " - " + fungsi.FungsiName + " - " + entitas.Code,
                          }).ToList();
            return result.OrderBy(m => m.text);
        }
        public IEnumerable<Select2BaseModel> GetDdlAgreementEntitasPertamina(Guid guid)
        {
            var result = (from rec in _context.TblmEntitasPertaminas
                          join hsh in _context.TblmHshes
                          on rec.HshId equals hsh.Id
                          join oep in _context.TbltAgreementEntitasPertaminas
                          on rec.Id equals oep.EntitasPertaminaId
                          where rec.DeletedDate == null && oep.AgreementId == guid && rec.ActiveStatus == true
                          orderby rec.CompanyName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = hsh.Name + " - " + rec.CompanyName,
                          }).ToList();
            return result.OrderBy(m => m.text);
        }
        public IEnumerable<Select2BaseModel> GetDdlAgreementNegara(Guid guid)
        {
            var result = (from rec in _context.TblmNegaraMitras
                          join kawasan in _context.TblmKawasanMitras
                          on rec.KawasanMitraId equals kawasan.Id
                          join onm in _context.TbltAgreementNegaraMitras
                          on rec.Id equals onm.NegaraMitraId
                          where rec.DeletedDate == null && onm.AgreementId == guid
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = kawasan.NamaKawasan + " - " + rec.NamaNegara,
                          }).ToList();
            return result.OrderBy(m => m.text);
        }
        public IEnumerable<Select2BaseModel> GetDdlAgreementJenisPerjanjian(Guid guid)
        {
            var result = (from rec in _context.TblmJenisPerjanjians
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IEnumerable<Select2BaseModel> GetDdlAgreementStatusDiscussion(Guid guid)
        {
            var result = (from rec in _context.TblmDiscussionStatuses
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IEnumerable<Select2BaseModel> GetDdlAgreementFaktorKendala(Guid guid)
        {
            var result = (from rec in _context.TblmFaktorKendalas
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IEnumerable<Select2BaseModel> GetDdlAgreementTrafficLight(Guid guid)
        {
            var result = (from rec in _context.TblmTrafficLights
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.Sequence
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                              color = rec.HexColor
                          });
            return result;
        }
        public IEnumerable<Select2BaseModel> GetDdlAgreementKlasifikasiKendala(Guid guid)
        {
            var result = (from rec in _context.TblmKlasifikasiKendalas
                          where rec.DeletedDate == null && rec.Id == guid
                          orderby rec.OrderSeq
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IEnumerable<Select2BaseModel> GetDdlAgreementKeterlibatanLembaga(Guid guid)
        {
            var result = (from rec in _context.TblmKementrianLembagas
                          join x in _context.TbltAgreementKementrianLembagas
                           on rec.Id equals x.KementrianLembagaId
                          where rec.DeletedDate == null && x.AgreementId == guid
                          orderby rec.LembagaName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.LembagaName,
                          });
            return result;
        }
        public IEnumerable<Select2BaseModel> GetDdlAgreementRelatedAgreement(Guid guid)
        {
            var result = (from rec in _context.TbltAgreements
                          join partner in _context.TbltAgreementPartners
                          on rec.Id equals partner.AgreementId
                          where rec.DeletedDate == null && partner.AgreementId == guid
                          orderby rec.JudulPerjanjian
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = partner.PartnerName + " _ " + rec.KodeAgreement + " _ " + rec.JudulPerjanjian,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlTypeOfStudy(string query)
        {
            var result = (from rec in _context.TblmTypeStudies
                          where rec.DeletedDate == null
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlConfidentiality(string query)
        {
            var result = (from rec in _context.TblmConfidentialities
                          where rec.DeletedDate == null
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.Name,
                              color = rec.HexColor,
                          });
            return result;
        }
        public IQueryable<Select2BaseModel> DdlOwnerEntity(string query)
        {
            var result = (from rec in _context.TblmEntitasPertaminas
                          where rec.DeletedDate == null
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.CompanyName,
                          });
            return result;
        }

        public IQueryable<Select2BaseModel> GetDdlInitiativeKeterlibatanLembaga(Guid guid)
        {
            var result = (from rec in _context.TblmKementrianLembagas
                          join x in _context.TbltInitiativeKementrianLembagas
                          on rec.Id equals x.KementrianLembagaId
                          where rec.DeletedDate == null && x.InitiativeId == guid
                          orderby rec.LembagaName
                          select new Select2BaseModel()
                          {
                              id = rec.Id.ToString(),
                              text = rec.LembagaName,
                          });
            return result;
        }
    }
}
