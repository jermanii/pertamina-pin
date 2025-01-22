using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Implement;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Implement
{
    public class AgreementService : IAgreementService
    {
        private readonly IAgreementRepository _repository;
        private readonly IInitiativeRepository _initiativeRepository;
        private readonly IProjectsToOfferRepository _opportunityRepository;
        private readonly ISequenceCounterRepository _sequenceCounterRepository;

        public AgreementService(IAgreementRepository repository, IInitiativeRepository initiativeRepository, IProjectsToOfferRepository opportunityRepository, ISequenceCounterRepository sequenceCounterRepository)
        {
            _repository = repository;
            _initiativeRepository = initiativeRepository;
            _opportunityRepository = opportunityRepository;
            _sequenceCounterRepository = sequenceCounterRepository;
        }
        public async Task<PaginationBaseModel<AgreementViewModel>> GetListPaging(RequestFormDtBaseModel request, AgreementViewModel decodeModel)
        {
            var resultData = await _repository.GetList(request, decodeModel);

            var result = new PaginationBaseModel<AgreementViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }
        public AgreementViewModel GetAgreementById(Guid guid)
        {

            AgreementViewModel model = new AgreementViewModel();
           

            try
            {
                if (guid != Guid.Empty)
                {
                    model = _repository.GetAgreementByIdCrud(guid);
                    if (model.PotentialRevenuePerYear!= null || model.Capex != null || model.Valuation != null)
                    {
                        model.PotentialRevenuePerYearToString = model.PotentialRevenuePerYear.HasValue ? model.PotentialRevenuePerYear.Value.ToString("#,##0", new System.Globalization.CultureInfo("id-ID")) : null;
                        model.CapexToString = model.Capex.HasValue ? model.Capex.Value.ToString("#,##0", new System.Globalization.CultureInfo("id-ID")) : null;
                        model.ValuationToString= model.Valuation.HasValue ? model.Valuation.Value.ToString("#,##0", new System.Globalization.CultureInfo("id-ID")) : null;
                    }
                    model.PicLevelId = _repository.GetPicLevelLeadId();
                    model.PicLevelMemberId = _repository.GetPicLevelMemberId();
                    model.AgreementEntitasPertamina = _repository.GetAgreementEntitasById(guid);
                    model.AgreementStreamBusiness = _repository.GetAgreementStreamBusinessById(guid);
                    model.AgreementNegaraMitra = _repository.GetAgreementNegaraMitraById(guid);
                    model.AgreementAddendums = _repository.GetAgreementAddendumById(guid);
                    model.AgreementPicFungsi = _repository.GetAgreementPicFungsiLeadById(guid, model.PicLevelId);
                    model.AgreementPicFungsis = _repository.GetAgreementPicFungsisById(guid, model.PicLevelMemberId);
                    model.AgreementLokasiProyeks = _repository.GetAgreementLokasiById(guid);
                    model.AgreementPartners = _repository.GetAgreementPartnerById(guid);
                    model.AgreementKementrianLembaga = _repository.GetAgreementLembagaById(guid);
                    if (model.StatusBerlakuId != null)
                    {
                        model.StatusBerlakuName = _repository.GetStatusBerlakuName(model.StatusBerlakuId).StatusName;
                    }

                   
                    model.IsError = false;

                    //Read
                    model.JenisPerjanjianRead = _repository.GetJenisPerjanjianById(guid);
                    model.PicFungsiLeadRead = _repository.GetPicFungsiById(guid, model.PicLevelId).FirstOrDefault();
                    model.PicFungsiRead = _repository.GetPicFungsiById(guid, model.PicLevelMemberId).ToList();
                    model.EntitasPertaminaRead = _repository.GetEntitasPertaminaById(guid);
                    model.StatusBerlakuRead = _repository.GetStatusBerlakuById(guid);
                    model.StatusDiscussionRead = _repository.GetStatusDiscussionById(guid);
                    model.PartnerRead = _repository.GetPartnerById(guid);
                    model.NegaraMitraRead = _repository.GetNegaraMitraById(guid);
                    model.LokasiProyekRead = _repository.GetLokasiProyekById(guid);
                    model.StreamBusinessRead = _repository.GetAllStreamBusinessById(guid);
                    model.FaktorKendalaRead = _repository.GetFaktorKendalaById(guid);
                    model.KlasifikasiKendalaRead = _repository.GetKlasifikasiKendalaById(guid);
                    model.KementrianLembagaRead = _repository.GetKementrianLembagaById(guid);
                    model.RelatedAgreementRead = _repository.GetRelatedAgreementById(guid);
                    model.TrafficLightRead = _repository.GetTrafficLightReadById(guid);
                    model.AddendumRead= _repository.GetAddendumRomanById(guid);

                    if (model.AddendumRead.Count>0)
                    {
                        foreach (var addendum in model.AddendumRead) 
                        {
                            var replaceRoman = _repository.ToRoman(addendum.Sequence.Value);
                            addendum.RomanSequence = replaceRoman;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        public async Task<StatusBerlakuViewModel> StatusBerlakuCounting(DateTime endDate)
        {
            StatusBerlakuViewModel model = new StatusBerlakuViewModel();
            DateTime today = DateTime.Now;
            DateTime sixMonthsBefore = endDate.AddMonths(-6);

            if (today > endDate)
            {
                model.RelationSequence = 3;
            }
            else if (today >= sixMonthsBefore)
            {
                model.RelationSequence = 2;
            }
            else
            {
                model.RelationSequence = 1;
            }
            var result = await _repository.StatusBerlakuCounting(model.RelationSequence);

            return result;

        }
        public AgreementViewModel GenerelizeDataAgreement(AgreementViewModel rec)
        {
            List<AgreementPartnerViewModel> partners = new List<AgreementPartnerViewModel>();
            partners = _repository.GetPartners(rec.Id);
            string strPartner = string.Empty;
            foreach (var partner in partners)
            {
                if (partners.Count() > 1)
                {
                    strPartner += partner.PartnerName;
                    if (partner == partners.LastOrDefault())
                    {
                    }
                    else
                    {
                        strPartner += ", ";
                    }
                }
                else
                {
                    strPartner += partner.PartnerName;
                }
            }

            List<AgreementEntitasPertaminaViewModel> entitasPertaminas = new List<AgreementEntitasPertaminaViewModel>();
            entitasPertaminas = _repository.GetEntitasPertamina(rec.Id);
            string strEntitasPertamina = string.Empty;
            foreach (var entitasPertamina in entitasPertaminas)
            {
                if (entitasPertaminas.Count() > 1)
                {
                    strEntitasPertamina += entitasPertamina.CompanyName;
                    if (entitasPertamina == entitasPertaminas.LastOrDefault())
                    {
                    }
                    else
                    {
                        strEntitasPertamina += ", ";
                    }
                }
                else
                {
                    strEntitasPertamina += entitasPertamina.CompanyName;
                }
            }

            List<AgreementStreamBusinessViewModel> streamBusinesses = new List<AgreementStreamBusinessViewModel>();
            streamBusinesses = _repository.GetStreamBusiness(rec.Id);
            string strStreamBusiness = string.Empty;
            foreach (var streamBusiness in streamBusinesses)
            {
                if (streamBusinesses.Count() > 1)
                {
                    strStreamBusiness += streamBusiness.QueryStreamBusinessName;
                    if (streamBusiness == streamBusinesses.LastOrDefault())
                    {
                    }
                    else
                    {
                        strStreamBusiness += ", ";
                    }
                }
                else
                {
                    strStreamBusiness += streamBusiness.QueryStreamBusinessName;
                }
            }

            List<AgreementNegaraMitraViewModel> negaraMitras = new List<AgreementNegaraMitraViewModel>();
            negaraMitras = _repository.GetNamaNegara(rec.Id);
            string strNegaraMitra = string.Empty;
            string strKawasanMitra = string.Empty;
            foreach (var negaraMitra in negaraMitras)
            {
                if (negaraMitras.Count() > 1)
                {
                    strNegaraMitra += negaraMitra.NamaNegara;
                    strKawasanMitra += negaraMitra.NamaKawasan;
                    if (negaraMitra == negaraMitras.LastOrDefault())
                    {
                    }
                    else
                    {
                        strNegaraMitra += ", ";
                        strKawasanMitra += ", ";
                    }
                }
                else
                {
                    strNegaraMitra += negaraMitra.NamaNegara;
                    strKawasanMitra += negaraMitra.NamaKawasan;
                }
            }

            rec.RowStatus = _repository.GetStatusDraft(rec.Id);
            rec.JudulPerjanjian = _repository.GetJudulPerjanjian(rec.Id);
            rec.RowPartner = strPartner;
            rec.RowEntitasPertamina = strEntitasPertamina;
            rec.RowStreamBusiness = strStreamBusiness;
            rec.RowJenisPerjanjian = _repository.GetJenisPerjanjian(rec.Id);
            rec.RowNegaraMitra = strNegaraMitra;
            rec.RowKawasanMitra = strKawasanMitra;
            rec.RowStatusBerlaku = _repository.GetStatusBerlaku(rec.Id);
            rec.RowStatusBerlakuColorHexa = _repository.GetStatusBerlakuColorHexa(rec.Id);
            rec.RowStatusBerlakuColorName = _repository.GetStatusBerlakuColorName(rec.Id);
            rec.RowDiscussionStatus = _repository.GetDiscussionStatus(rec.Id);
            rec.RowDiscussionStatusColorHexa = _repository.GetDiscussionStatusColorHexa(rec.Id);
            rec.RowDiscussionStatusColorName = _repository.GetDiscussionStatusColorName(rec.Id);

            return rec;
        }
        public AgreementViewModel GetAgreementCount()
        {
            AgreementViewModel model = new AgreementViewModel();
            try
            {
                List<StatusBerlakuViewModel> allRecordStatusBerlaku = _repository.GetAllDataStatusBerlaku();
                List<StatusBerlakuViewModel> setRecordStatusBerlaku = new List<StatusBerlakuViewModel>();
                foreach (var rec in allRecordStatusBerlaku)
                {
                    rec.Count = _repository.GetCountStatusBerlaku(rec.RelationSequence.Value);
                    setRecordStatusBerlaku.Add(rec);
                }

                List<StatusDiscussionViewModel> allRecordStatusDiscussion = _repository.GetAllDataStatusDiscussion();
                List<StatusDiscussionViewModel> setRecordStatusDiscussion = new List<StatusDiscussionViewModel>();
                foreach (var rec in allRecordStatusDiscussion)
                {
                    rec.Count = _repository.GetCountStatusDiscussion(rec.RelationSequence.Value);
                    setRecordStatusDiscussion.Add(rec);
                }

                List<HshViewModel> allRecordHsh = _repository.GetAllDataHsh();
                List<HshViewModel> setRecordHsh = new List<HshViewModel>();
                foreach (var rec in allRecordHsh)
                {
                    rec.Count = _repository.GetCountHsh(rec.RelationSequence.Value);
                    setRecordHsh.Add(rec);
                }

                model.StatusBerlaku = setRecordStatusBerlaku.OrderBy(x => x.OrderSeq).ToList();
                model.DiscussionStatus = setRecordStatusDiscussion.OrderBy(x => x.OrderSeq).ToList();
                model.AgreementHolder = setRecordHsh.OrderBy(x => x.OrderSeq).ToList();
                model.Perjanjian = _repository.GetAllAgreement();
                model.NegaraMitra = _repository.GetAllNegaraMitra();
                model.IsError = false;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        public async Task<AgreementViewModel> GetAgreementCountWithFilter(AgreementViewModel model)
        {
            try
            {
                List<StatusBerlakuViewModel> allRecordStatusBerlaku = await _repository.GetAllDataStatusBerlakuAsync();
                List<StatusBerlakuViewModel> setRecordStatusBerlaku = new List<StatusBerlakuViewModel>();
                foreach (var rec in allRecordStatusBerlaku)
                {
                    rec.Count = _repository.GetAgreementWithFilter(model).Where(x => x.StatusBerlakuId == rec.Id).GroupBy(x => x.Id).Count();
                    setRecordStatusBerlaku.Add(rec);
                }

                List<StatusDiscussionViewModel> allRecordStatusDiscussion = await _repository.GetAllDataStatusDiscussionAsync();
                List<StatusDiscussionViewModel> setRecordStatusDiscussion = new List<StatusDiscussionViewModel>();
                foreach (var rec in allRecordStatusDiscussion)
                {
                    rec.Count = _repository.GetAgreementWithFilter(model).Where(x => x.DiscussionStatusId == rec.Id).GroupBy(x => x.Id).Count();
                    setRecordStatusDiscussion.Add(rec);
                }

                List<HshViewModel> allRecordHsh = await _repository.GetAllDataHshAsync();
                List<HshViewModel> setRecordHsh = new List<HshViewModel>();
                foreach (var rec in allRecordHsh)
                {
                    rec.Count = _repository.GetAgreementWithFilter(model).Where(x => x.HshId == rec.Id).GroupBy(x => x.Id).Count();
                    setRecordHsh.Add(rec);
                }

                model.StatusBerlaku = setRecordStatusBerlaku.OrderBy(x => x.OrderSeq).ToList();
                model.DiscussionStatus = setRecordStatusDiscussion.OrderBy(x => x.OrderSeq).ToList();
                model.AgreementHolder = setRecordHsh.OrderBy(x => x.OrderSeq).ToList();
                model.Perjanjian = _repository.GetAllAgreementWithFilter(model);
                model.NegaraMitra = _repository.GetAllNegaraMitraWithFilter(model);
                model.IsError = false;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        public async Task<AgreementViewModel> GetAgreementCountWithFilter(AgreementViewModel model, string strSearch)
        {
            try
            {
                List<StatusBerlakuViewModel> allRecordStatusBerlaku = await _repository.GetAllDataStatusBerlakuAsync();
                List<StatusBerlakuViewModel> setRecordStatusBerlaku = new List<StatusBerlakuViewModel>();
                foreach (var rec in allRecordStatusBerlaku)
                {
                    rec.Count = _repository.GetAgreementWithFilter(model).Where(x => x.StatusBerlakuId == rec.Id && (x.JudulPerjanjian.Contains(strSearch) ||
                                                                                                                    x.KodeAgreement.Contains(strSearch) ||
                                                                                                                    //x.Valuation.HasValue.ToString().Contains(strSearch) ||
                                                                                                                    x.Scope.Contains(strSearch) ||
                                                                                                                    x.Progress.Contains(strSearch) ||
                                                                                                                    x.DeskripsiKendala.Contains(strSearch) ||
                                                                                                                    x.SupportPemerintah.Contains(strSearch) ||
                                                                                                                    x.PotensiEskalasi.Contains(strSearch) ||
                                                                                                                    x.CatatanTambahan.Contains(strSearch))).GroupBy(x => x.Id).Count();
                    setRecordStatusBerlaku.Add(rec);
                }

                List<StatusDiscussionViewModel> allRecordStatusDiscussion = await _repository.GetAllDataStatusDiscussionAsync();
                List<StatusDiscussionViewModel> setRecordStatusDiscussion = new List<StatusDiscussionViewModel>();
                foreach (var rec in allRecordStatusDiscussion)
                {
                    rec.Count = _repository.GetAgreementWithFilter(model).Where(x => x.DiscussionStatusId == rec.Id && (x.JudulPerjanjian.Contains(strSearch) ||
                                                                                                                        x.KodeAgreement.Contains(strSearch) ||
                                                                                                                        //x.Valuation.HasValue.ToString().Contains(strSearch) ||
                                                                                                                        x.Scope.Contains(strSearch) ||
                                                                                                                        x.Progress.Contains(strSearch) ||
                                                                                                                        x.DeskripsiKendala.Contains(strSearch) ||
                                                                                                                        x.SupportPemerintah.Contains(strSearch) ||
                                                                                                                        x.PotensiEskalasi.Contains(strSearch) ||
                                                                                                                        x.CatatanTambahan.Contains(strSearch))).GroupBy(x => x.Id).Count();
                        setRecordStatusDiscussion.Add(rec);
                }

                List<HshViewModel> allRecordHsh = await _repository.GetAllDataHshAsync();
                List<HshViewModel> setRecordHsh = new List<HshViewModel>();
                foreach (var rec in allRecordHsh)
                {
                    rec.Count = _repository.GetAgreementWithFilter(model).Where(x => x.HshId == rec.Id && (x.JudulPerjanjian.Contains(strSearch) ||
                                                                                                            x.KodeAgreement.Contains(strSearch) ||
                                                                                                            //x.Valuation.HasValue.ToString().Contains(strSearch) ||
                                                                                                            x.Scope.Contains(strSearch) ||
                                                                                                            x.Progress.Contains(strSearch) ||
                                                                                                            x.DeskripsiKendala.Contains(strSearch) ||
                                                                                                            x.SupportPemerintah.Contains(strSearch) ||
                                                                                                            x.PotensiEskalasi.Contains(strSearch) ||
                                                                                                            x.CatatanTambahan.Contains(strSearch))).GroupBy(x => x.Id).Count();
                    setRecordHsh.Add(rec);
                }

                model.StatusBerlaku = setRecordStatusBerlaku.OrderBy(x => x.OrderSeq).ToList();
                model.DiscussionStatus = setRecordStatusDiscussion.OrderBy(x => x.OrderSeq).ToList();
                model.AgreementHolder = setRecordHsh.OrderBy(x => x.OrderSeq).ToList();
                model.Perjanjian = _repository.GetAllAgreementWithFilter(model, strSearch);
                model.NegaraMitra = _repository.GetAllNegaraMitraWithFilter(model, strSearch);
                model.IsError = false;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        public AgreementViewModel IsValidation (AgreementViewModel model)
        {
            if (model.AgreementPartners == null || string.IsNullOrEmpty(model.AgreementPartners.FirstOrDefault().PartnerName))
            {
                model.IsError = true;
                model.ErrorMessage += "Minimum 1 data required for Partner(s) ";
            }
            else if (model.AgreementLokasiProyeks == null || string.IsNullOrEmpty(model.AgreementLokasiProyeks.FirstOrDefault().LokasiProyek))
            {
                model.IsError = true;
                model.ErrorMessage += "Minimum 1 data required for Project Location ";
            }
            if (model.PicFungsiId == null)
            {
                
                    model.IsError = true;
                    model.ErrorMessage += "Minimum 1 data required for Pic Lead ";
            }
            if (model.AgreementPicFungsis.Count > 0)
            {
                // HashSet to track unique PicFungsiId
                var fungsiIds = new HashSet<Guid?>();
                var duplicateIds = new HashSet<Guid?>();

                foreach (var pic in model.AgreementPicFungsis)
                {
                    if (pic.PicFungsiId != null)
                    {
                        if (!fungsiIds.Add(pic.PicFungsiId))
                        {
                            duplicateIds.Add(pic.PicFungsiId);
                        }

                        if (pic.PicFungsiId == model.PicFungsiId)
                        {
                            model.IsError = true;
                            model.ErrorMessage += "Pic member can't same  ";
                        }
                    }
                }

                if (duplicateIds.Count > 0)
                {
                    model.IsError = true;
                    model.ErrorMessage += "Pic member can't same ";
                }
            }

            return model;
        }
        public AgreementViewModel Save(AgreementViewModel model, string userName, string companyCode)
        {
            try
            {
               
                    model.IsError = false;
                    DateTime now = DateTime.Now;
                    model.Id = Guid.NewGuid();
                    model.CreateBy = userName;
                    model.CreateDate = now;
                    model.IsDraft = false;
                    model.CompanyCode = companyCode;
                    model.KodeAgreement = GetNewRequestNumber();

                if (model.PotentialRevenuePerYearToString != null)
                {
                    model.PotentialRevenuePerYear =_repository.ParseRevenue(model.PotentialRevenuePerYearToString);
                }
                if (model.CapexToString  != null)
                {
                    model.Capex =_repository.ParseRevenue(model.CapexToString);
                }
                if (model.ValuationToString != null)
                {
                    model.Valuation =_repository.ParseRevenue(model.ValuationToString);
                }

                _repository.Save(model);

                    AgreementNegaraMitraViewModel negara = new AgreementNegaraMitraViewModel();
                    negara.Id = Guid.NewGuid();
                    negara.CreateDate = now;
                    negara.CreateBy = userName;
                    negara.AgreementId = model.Id;
                    negara.NegaraMitraId = model.NegaraMitraId;
                    _repository.SaveNegaraMitra(negara); 
                    
                    if(model.PicFungsiId != null)
                    {
                        AgreementPicFungsiViewModel picFungsiLead = new AgreementPicFungsiViewModel();
                        picFungsiLead.Id = Guid.NewGuid();
                        picFungsiLead.CreateBy = model.CreateBy;
                        picFungsiLead.CreateDate = model.CreateDate;
                        picFungsiLead.AgreementId = model.Id;
                        picFungsiLead.PicFungsiId = model.PicFungsiId;
                        picFungsiLead.PicLevelId = model.PicLevelId;
                        _repository.SavePic(picFungsiLead);
                    }
                   


                    if (model.AgreementPicFungsis.Count > 0 )
                    {
                        foreach (var pic in model.AgreementPicFungsis)
                        {
                            if (model.PicFungsiId != null)
                            {
                                AgreementPicFungsiViewModel picFungsiMember = new AgreementPicFungsiViewModel();
                                picFungsiMember.Id = Guid.NewGuid();
                                picFungsiMember.CreateBy = model.CreateBy;
                                picFungsiMember.CreateDate = model.CreateDate;
                                picFungsiMember.AgreementId = model.Id;
                                picFungsiMember.PicFungsiId = pic.PicFungsiId;
                                picFungsiMember.PicLevelId = _repository.GetPicLevelMemberId();
                                _repository.SavePic(picFungsiMember);
                            }
                        }
                    }

                    foreach (var entitas in model.EntitasPertaminaEvent)
                    {
                        AgreementEntitasPertaminaViewModel entitasPertamina = new AgreementEntitasPertaminaViewModel();
                        entitasPertamina.Id = Guid.NewGuid();
                        entitasPertamina.CreateBy = model.CreateBy;
                        entitasPertamina.CreateDate = model.CreateDate;
                        entitasPertamina.AgreementId = model.Id;
                        entitasPertamina.EntitasPertaminaId = entitas;
                        _repository.SaveEntitasPertamina(entitasPertamina);
                    }
                    foreach (var stream in model.StreamBusinessIds)
                    {
                        AgreementStreamBusinessViewModel streamBusiness = new AgreementStreamBusinessViewModel();
                        streamBusiness.Id = Guid.NewGuid();
                        streamBusiness.CreateBy = model.CreateBy;
                        streamBusiness.CreateDate = model.CreateDate;
                        streamBusiness.AgreementId = model.Id;
                        streamBusiness.StreamBusinessId = stream;
                        _repository.SaveStreamBusiness(streamBusiness);
                    }

                    if (model.KementrianLembagaIds != null)
                    {
                        foreach (var lembagas in model.KementrianLembagaIds)
                        {
                            AgreementKementrianLembagaViewModel lembaga = new AgreementKementrianLembagaViewModel();
                            lembaga.Id = Guid.NewGuid();
                            lembaga.CreateBy = model.CreateBy;
                            lembaga.CreateDate = model.CreateDate;
                            lembaga.AgreementId = model.Id;
                            lembaga.KementrianLembagaId = lembagas;
                            _repository.SaveLembaga(lembaga);
                        }
                    }
                    if (model.AgreementPartners != null)
                    {
                        bool isAnyNonEmpty = false;
                        foreach (var partner in model.AgreementPartners)
                        {
                            if (!string.IsNullOrEmpty(partner.PartnerName))
                            {
                                AgreementPartnerViewModel pr = new AgreementPartnerViewModel();
                                pr.Id = Guid.NewGuid();
                                pr.CreateDate = now;
                                pr.CreateBy = userName;
                                pr.AgreementId = model.Id;
                                pr.PartnerName = partner.PartnerName;
                                _repository.SaveToPartner(pr);
                                isAnyNonEmpty = true;
                            }
                        }

                        if (!isAnyNonEmpty)
                        {
                            model.IsError = true;
                            model.ErrorMessage += "Minimum 1 data required for Partner(s) ";
                        }
                    }
                    else
                    {
                        model.IsError = true;
                        model.ErrorMessage += "Minimum 1 data required for Partner(s) ";
                    }


                    if (model.AgreementLokasiProyeks != null)
                    {
                        bool isAnyNonEmpty = false;
                        foreach (var lokasiProyek in model.AgreementLokasiProyeks)
                        {
                            if (!string.IsNullOrEmpty(lokasiProyek.LokasiProyek))
                            {
                                AgreementLokasiProyekViewModel lp = new AgreementLokasiProyekViewModel();
                                lp.Id = Guid.NewGuid();
                                lp.CreateDate = now;
                                lp.CreateBy = userName;
                                lp.AgreementId = model.Id;
                                lp.LokasiProyek = lokasiProyek.LokasiProyek;
                                _repository.SaveToLokasiProyek(lp);
                                isAnyNonEmpty = true;
                            }
                        }

                        if (!isAnyNonEmpty)
                        {
                            model.IsError = true;
                            model.ErrorMessage += "Minimum 1 data required for Project Location ";
                            model.ErrorMessage += "Minimum 1 data required for Project Location ";
                        }
                    }
                    else
                    {
                        model.IsError = true;
                        model.ErrorMessage += "Minimum 1 data required for Project Location ";
                    }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.InnerException.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        public AgreementViewModel Update(AgreementViewModel model, string userName)
        {
            try
            {
                AgreementNegaraMitraViewModel recordNegaraMitra = _repository.GetAgreementNegaraMitraById(model.Id);

                DateTime now = DateTime.Now;
                if (model.IsError)
                {
                    return model;
                }
                else
                {
                    model.IsError = false;
                    if (model.IsDraft == true)
                    {
                        model.KodeAgreement = GetNewRequestNumber();
                    }
                    if (model.PotentialRevenuePerYearToString != null)
                    {
                        model.PotentialRevenuePerYear =_repository.ParseRevenue(model.PotentialRevenuePerYearToString);
                    } 
                    
                    if (model.CapexToString  != null)
                    {
                        model.Capex =_repository.ParseRevenue(model.CapexToString);
                    } 
                    
                    if (model.ValuationToString != null)
                    {
                        model.Valuation =_repository.ParseRevenue(model.ValuationToString);
                    }
                    _repository.Update(model, userName);
                        //Dropdown multi
                        if (model.StreamBusinessIds != null)
                        {
                            _repository.DeleteExistingStreamBusiness(model.Id);
                            foreach (var stream in model.StreamBusinessIds)
                            {
                                AgreementStreamBusinessViewModel streamBusiness = new AgreementStreamBusinessViewModel();
                                streamBusiness.Id = Guid.NewGuid();
                                streamBusiness.CreateBy = userName;
                                streamBusiness.CreateDate = now;
                                streamBusiness.AgreementId = model.Id;
                                streamBusiness.StreamBusinessId = stream;
                                _repository.SaveStreamBusiness(streamBusiness);
                            }
                        }
                        if (model.PicFungsiId  != null)
                        {
                            _repository.DeleteExistingFungsiPic(model.Id);
                            
                                AgreementPicFungsiViewModel picFungsi = new AgreementPicFungsiViewModel();
                                picFungsi.Id = Guid.NewGuid();
                                picFungsi.CreateBy =userName;
                                picFungsi.CreateDate = now;
                                picFungsi.AgreementId = model.Id;
                                picFungsi.PicFungsiId = model.PicFungsiId;
                                picFungsi.PicLevelId = model.PicLevelId;
                                _repository.SavePic(picFungsi);
                        }
                        if (model.AgreementAddendums != null) {
                            if (model.AgreementAddendums.Count > 0)
                            {
                               
                                foreach (var item in model.AgreementAddendums)
                                {
                                    var getAddendumList = _repository.GetSequenceAgreementAddendumById(model.Id);
                                    AgreementAddendumViewModel addendum = new AgreementAddendumViewModel();
                                    addendum.Id = Guid.NewGuid();
                                    addendum.CreatedBy = userName;
                                    addendum.CreatedDate = now  ;
                                    addendum.AgreementId = model.Id;
                                    addendum.AddendumDate= item.AddendumDate;
                                    addendum.Sequence = getAddendumList +1 ;
                                    _repository.SaveAddendum(addendum);
                                }
                            }
                        }
                        if (model.AgreementPicFungsis != null)
                        {
                            if (model.AgreementPicFungsis.Count > 0)
                            {
                                foreach (var pic in model.AgreementPicFungsis)
                                {
                                    if(pic.PicFungsiId != null)
                                    {
                             
                                            AgreementPicFungsiViewModel picFungsiMember = new AgreementPicFungsiViewModel();
                                            picFungsiMember.Id = Guid.NewGuid();
                                            picFungsiMember.CreateBy =userName;
                                            picFungsiMember.CreateDate = now;
                                            picFungsiMember.AgreementId = model.Id;
                                            picFungsiMember.PicFungsiId = pic.PicFungsiId;
                                            picFungsiMember.PicLevelId = _repository.GetPicLevelMemberId();
                                            _repository.SavePic(picFungsiMember);
                                    }
                                }
                            }
                        }
                        if (model.EntitasPertaminaEvent != null)
                        {
                            _repository.DeleteExistingEntitasPertamina(model.Id);
                            foreach (var entitas in model.EntitasPertaminaEvent)
                            {
                                AgreementEntitasPertaminaViewModel entitasPertamina = new AgreementEntitasPertaminaViewModel();
                                entitasPertamina.Id = Guid.NewGuid();
                                entitasPertamina.CreateBy =userName;
                                entitasPertamina.CreateDate = now;
                                entitasPertamina.AgreementId = model.Id;
                                entitasPertamina.EntitasPertaminaId = entitas;
                                _repository.SaveEntitasPertamina(entitasPertamina);
                            }
                        }
                        if (model.NegaraMitraId != null)
                        {
                            _repository.DeleteExistingNegaraMitra(model.Id);
                            AgreementNegaraMitraViewModel negara = new AgreementNegaraMitraViewModel();
                            negara.Id = Guid.NewGuid();
                            negara.CreateDate = now;
                            negara.CreateBy = userName;
                            negara.AgreementId = model.Id;
                            negara.NegaraMitraId = model.NegaraMitraId;
                            _repository.SaveNegaraMitra(negara);
                        }
                        if (model.KementrianLembagaIds != null)
                        {
                            _repository.DeleteExistingLembaga(model.Id);
                            foreach (var lembagas in model.KementrianLembagaIds)
                            {
                                AgreementKementrianLembagaViewModel lembaga = new AgreementKementrianLembagaViewModel();
                                lembaga.Id = Guid.NewGuid();
                                lembaga.CreateDate = now;
                                lembaga.CreateBy = userName;
                                lembaga.AgreementId = model.Id;
                                lembaga.KementrianLembagaId = lembagas;
                                _repository.SaveLembaga(lembaga);
                            }
                        } 
                        else
                        {
                            _repository.DeleteExistingLembaga(model.Id);
                        }

                        if (model.AgreementLokasiProyeks != null && model.AgreementLokasiProyeks.Count > 0)
                        {
                            bool isAnyNonEmpty = false;
                            foreach (var entitas in model.AgreementLokasiProyeks)
                            {
                                if (!string.IsNullOrWhiteSpace(entitas.LokasiProyek))
                                {
                                    isAnyNonEmpty = true;
                                    break;
                                }
                            }

                            if (isAnyNonEmpty)
                            {
                                _repository.DeleteExistingLokasiProyek(model.Id);
                                foreach (var entitas in model.AgreementLokasiProyeks)
                                {
                                    if (!string.IsNullOrWhiteSpace(entitas.LokasiProyek))
                                    {
                                        AgreementLokasiProyekViewModel lokasi = new AgreementLokasiProyekViewModel();
                                        lokasi.Id = Guid.NewGuid();
                                        lokasi.CreateBy = userName;
                                        lokasi.CreateDate = model.CreateDate;
                                        lokasi.AgreementId = model.Id;
                                        lokasi.LokasiProyek = entitas.LokasiProyek;
                                        _repository.SaveToLokasiProyek(lokasi);
                                    }
                                }
                            }
                            else
                            {
                                model.IsError = true;
                                model.ErrorMessage += "Minimum 1 data required for Project Location ";
                            }
                        }
                        else
                        {
                            model.IsError = true;
                            model.ErrorMessage += "Minimum 1 data required for Project Location ";
                        }

                        if (model.AgreementPartners != null && model.AgreementPartners.Count > 0)
                        {
                            bool isAnyNonEmpty = false;
                            foreach (var entitas in model.AgreementPartners)
                            {
                                if (!string.IsNullOrWhiteSpace(entitas.PartnerName))
                                {
                                    isAnyNonEmpty = true;
                                    break;
                                }
                            }

                            if (isAnyNonEmpty)
                            {
                                _repository.DeleteExistingPartner(model.Id);
                                foreach (var entitas in model.AgreementPartners)
                                {
                                    if (!string.IsNullOrWhiteSpace(entitas.PartnerName))
                                    {
                                        AgreementPartnerViewModel partner = new AgreementPartnerViewModel();
                                        partner.Id = Guid.NewGuid();
                                        partner.CreateBy = model.CreateBy;
                                        partner.CreateDate = model.CreateDate;
                                        partner.AgreementId = model.Id;
                                        partner.PartnerName = entitas.PartnerName;
                                        _repository.SaveToPartner(partner);
                                    }
                                }
                            }
                            else
                            {
                                model.IsError = true;
                                model.ErrorMessage += "Minimum 1 data required for Partner(s) ";
                            }
                        }
                        else
                        {
                            model.IsError = true;
                            model.ErrorMessage += "Minimum 1 data required for Partner(s) ";
                        }
                    //}
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        public AgreementViewModel CreatePicFunction()
        {
            var model = new AgreementViewModel();
            
            model.PicLevelId =_repository.GetPicLevelLeadId();
            model.PicLevelMemberId =_repository.GetPicLevelMemberId();
            return model;
        }
        protected AgreementViewModel ValidationFormDelete(AgreementViewModel model)
        {
            if (_repository.IsExistInAgreement(model.Id))
            {
                model.IsError = true;
                model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Record";
            }
            if (_repository.IsExistInInitiative(model.Id))
            {
                model.IsError = true;
                model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Record";
            }
            return model;
        }
        protected AgreementViewModel ValidationForm(AgreementViewModel model)
        {
            if (model.JudulPerjanjian == null)
            {
                model.IsError = true;
                model.ErrorMessage = "Judul perjanjian wajib diisi sebelum menyimpan draft!";
            }
            return model;
        }
        public AgreementViewModel SaveDraft(AgreementViewModel model, string userName, string companyCode)
        {
            try
            {
                model = ValidationForm(model);
                if (model.IsError)
                {
                    return model;
                }
                DateTime now = DateTime.Now;
                model.Id = Guid.NewGuid();
                model.CreateBy = userName;
                model.CreateDate = now;
                model.IsDraft = true;
                model.CompanyCode = companyCode;
                
                if (model.PotentialRevenuePerYear != null)
                {
                    model.PotentialRevenuePerYear =_repository.ParseRevenue(model.PotentialRevenuePerYearToString);
                }
                
                if (model.Capex != null)
                {
                    model.Capex =_repository.ParseRevenue(model.CapexToString);
                }
                
                if (model.Valuation != null)
                {
                    model.Valuation =_repository.ParseRevenue(model.ValuationToString);
                }
           
                _repository.Save(model);

                if (model.KementrianLembaga != null)
                {
                    AgreementKementrianLembagaViewModel lembaga = new AgreementKementrianLembagaViewModel();
                    lembaga.Id = Guid.NewGuid();
                    lembaga.CreateDate = now;
                    lembaga.CreateBy = userName;
                    lembaga.AgreementId = model.Id;
                    lembaga.KementrianLembagaId = model.KementrianLembaga;
                    _repository.SaveLembaga(lembaga);
                }
                if (model.NegaraMitraId != null)
                {
                    AgreementNegaraMitraViewModel negara = new AgreementNegaraMitraViewModel();
                    negara.Id = Guid.NewGuid();
                    negara.CreateDate = now;
                    negara.CreateBy = userName;
                    negara.AgreementId = model.Id;
                    negara.NegaraMitraId = model.NegaraMitraId;
                    _repository.SaveNegaraMitra(negara);
                }
                if (model.FungsiPicId != null)
                {
                    foreach (var pic in model.FungsiPicId)
                    {
                        AgreementPicFungsiViewModel picFungsi = new AgreementPicFungsiViewModel();
                        picFungsi.Id = Guid.NewGuid();
                        picFungsi.CreateBy = model.CreateBy;
                        picFungsi.CreateDate = model.CreateDate;
                        picFungsi.AgreementId = model.Id;
                        picFungsi.PicFungsiId = pic;
                        _repository.SavePic(picFungsi);
                    }
                }
                if (model.AgreementPicFungsis != null)
                {
                    if (model.AgreementPicFungsis.Count > 0)
                    {
                        foreach (var pic in model.AgreementPicFungsis)
                        {
                            if (pic.PicFungsiId == model.PicFungsiId)
                            {
                                model.IsError = true;
                                model.ErrorMessage += "Pic function lead and member can't same ";
                            }
                            else
                            {
                                AgreementPicFungsiViewModel picFungsiMember = new AgreementPicFungsiViewModel();
                                picFungsiMember.Id = Guid.NewGuid();
                                picFungsiMember.CreateBy = model.CreateBy;
                                picFungsiMember.CreateDate = model.CreateDate;
                                picFungsiMember.AgreementId = model.Id;
                                picFungsiMember.PicFungsiId = pic.PicFungsiId;
                                picFungsiMember.PicLevelId = _repository.GetPicLevelMemberId();
                                _repository.SavePic(picFungsiMember);
                            }

                        }
                    }
                }
                
                if (model.EntitasPertaminaEvent != null)
                {
                    foreach (var entitas in model.EntitasPertaminaEvent)
                    {
                        AgreementEntitasPertaminaViewModel entitasPertamina = new AgreementEntitasPertaminaViewModel();
                        entitasPertamina.Id = Guid.NewGuid();
                        entitasPertamina.CreateBy = model.CreateBy;
                        entitasPertamina.CreateDate = model.CreateDate;
                        entitasPertamina.AgreementId = model.Id;
                        entitasPertamina.EntitasPertaminaId = entitas;
                        _repository.SaveEntitasPertamina(entitasPertamina);
                    }
                }
                if (model.KementrianLembagaIds != null)
                {
                    _repository.DeleteExistingLembaga(model.Id);
                    foreach (var lembagas in model.KementrianLembagaIds)
                    {
                        AgreementKementrianLembagaViewModel lembaga = new AgreementKementrianLembagaViewModel();
                        lembaga.Id = Guid.NewGuid();
                        lembaga.CreateDate = now;
                        lembaga.CreateBy = userName;
                        lembaga.AgreementId = model.Id;
                        lembaga.KementrianLembagaId = lembagas;
                        _repository.SaveLembaga(lembaga);
                    }
                }
                if (model.StreamBusinessIds != null)
                {
                    foreach (var stream in model.StreamBusinessIds)
                    {
                        StreamBusinessViewModel master = _repository.GetStreamBusinessById(stream);
                        AgreementStreamBusinessViewModel streamBusiness = new AgreementStreamBusinessViewModel();
                        streamBusiness.Id = Guid.NewGuid();
                        streamBusiness.CreateBy = model.CreateBy;
                        streamBusiness.CreateDate = model.CreateDate;
                        streamBusiness.AgreementId = model.Id;
                        streamBusiness.QueryStreamBusinessName = master.Name;
                        streamBusiness.StreamBusinessId = stream;
                        _repository.SaveStreamBusiness(streamBusiness);
                    }
                }
                if (model.AgreementPartners != null)
                {
                    foreach (var partner in model.AgreementPartners)
                    {
                        if (!string.IsNullOrEmpty(partner.PartnerName))
                        {
                            AgreementPartnerViewModel pr = new AgreementPartnerViewModel();
                            pr.Id = Guid.NewGuid();
                            pr.CreateDate = now;
                            pr.CreateBy = userName;
                            pr.AgreementId = model.Id;
                            pr.PartnerName = partner.PartnerName;
                            _repository.SaveToPartner(pr);
                        }
                    }
                }
                if (model.AgreementLokasiProyeks != null)
                {
                    foreach (var lokasiProyek in model.AgreementLokasiProyeks)
                    {
                        if (!string.IsNullOrEmpty(lokasiProyek.LokasiProyek))
                        {
                            AgreementLokasiProyekViewModel lp = new AgreementLokasiProyekViewModel();
                            lp.Id = Guid.NewGuid();
                            lp.CreateDate = now;
                            lp.CreateBy = userName;
                            lp.AgreementId = model.Id;
                            lp.LokasiProyek = lokasiProyek.LokasiProyek;
                            _repository.SaveToLokasiProyek(lp);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.InnerException.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        private string GetNewRequestNumber()
        {
            TblmSequenceCounter item = _sequenceCounterRepository.GetNewSequenceNumber("Agreement");
            return string.Format("{0:00000}-{1}", item.Sequence, item.Year);
        }
        public ExcelPackage ExportXLS(bool withData, string searchQuery, string statusBerlaku, string statusDiscussion, string agreementHolder, string entitasPertamina, string kawasanMitra, string negaraMitra, string jenisPerjanjian, string streamBusiness, string rangeCreateDate, string tanggalTtd, string tanggalBerakhir, bool draft, bool isG2G, bool isStrategicPartnerShip, bool isBdCoreBusiness, bool isGreenBusiness)
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "Agreement Download";
            package.Workbook.Properties.Author = "Pertamina";
            package.Workbook.Properties.Subject = "Export Data";
            package.Workbook.Properties.Keywords = "PIN - Pertamina";

            var worksheet = package.Workbook.Worksheets.Add("Agreement");

            // First: add the header
            worksheet.Cells[1, 1].Value = "NO.";
            worksheet.Cells[1, 2].Value = "AGREEMENT CODE";
            worksheet.Cells[1, 3].Value = "PARTNER(S)";
            worksheet.Cells[1, 4].Value = "PERTAMINA ENTITY";
            worksheet.Cells[1, 5].Value = "STREAM BUSINESS";
            worksheet.Cells[1, 6].Value = "HOLDING/SUB-HOLDING";
            worksheet.Cells[1, 7].Value = "PIC LEAD";
            worksheet.Cells[1, 8].Value = "PIC MEMBER";
            worksheet.Cells[1, 9].Value = "AGREEMENT TYPE";
            worksheet.Cells[1, 10].Value = "AGREEMENT TITLE";
            worksheet.Cells[1, 11].Value = "PARTNER REGION";
            worksheet.Cells[1, 12].Value = "COUNTRY REGION";
            worksheet.Cells[1, 13].Value = "PROJECT LOCATION";
            worksheet.Cells[1, 14].Value = "PROJECT VALUE";
            worksheet.Cells[1, 15].Value = "PROJECT COST";
            worksheet.Cells[1, 16].Value = "SIGNATURE DATE";
            worksheet.Cells[1, 17].Value = "END DATE";
            worksheet.Cells[1, 18].Value = "VALID STATUS";
            worksheet.Cells[1, 19].Value = "DISCUSSION STATUS";
            worksheet.Cells[1, 20].Value = "SCOPE/PROJECT REVIEW";
            worksheet.Cells[1, 21].Value = "PROGRESS";
            worksheet.Cells[1, 22].Value = "CONSTRAINT FACTOR";
            worksheet.Cells[1, 23].Value = "CONSTRAINT CLASSIFICATION";
            worksheet.Cells[1, 24].Value = "DESCRIPTION CLASSIFICATION";
            worksheet.Cells[1, 25].Value = "FOLLOW UP";
            worksheet.Cells[1, 26].Value = "REQUIRED SUPPORT FROM THE GOVERMENT";
            worksheet.Cells[1, 27].Value = "MINISTRY / AGENCY INVOLVEMENT";
            worksheet.Cells[1, 28].Value = "ESCALATION POTENTIAL";
            worksheet.Cells[1, 29].Value = "RELATED AGREEMENT";
            worksheet.Cells[1, 30].Value = "KODE RELATED AGREEMENT";
            worksheet.Cells[1, 31].Value = "ADDITIONAL NOTE";
            worksheet.Cells[1, 32].Value = "AMANDEMENT DATE";
            worksheet.Cells[1, 33].Value = "TRAFFIC LIGHT";
               
            worksheet.Cells["A1:ZZ"].Style.Font.Name = "Arial";
            worksheet.Cells["A1:ZZ"].Style.Font.Size = 12;
            worksheet.Cells["A1:ZZ1"].Style.Font.Size = 14; 

            int dataRowCount = worksheet.Dimension.End.Row - 1;
            if (dataRowCount > 0)
            {
                var dataContent = worksheet.Cells["A2:ZZ" + worksheet.Dimension.End.Row];
                dataContent.Style.Font.Size = 12; 
            }

            worksheet.Cells["A1:ZZ" + worksheet.Dimension.End.Row].AutoFitColumns();

            if (withData)
            {
                List<AgreementViewModel> datas = _repository.GetAllAgreementExcel().ToList();

                //filtering excluded
                var filterExcludedNegaraMitra = _repository.GetAllNegareMitraExcluded();

                List<AgreementViewModel> listFilterExcluded = new List<AgreementViewModel>();

                foreach (var item in filterExcludedNegaraMitra)
                {
                    string[] expiredArrayy = item.NegaraMitraId.ToString().ToUpper().Split(',');

                    listFilterExcluded.AddRange(
                        datas.Where(x => x.CellNegaraMitraId != expiredArrayy[0])
                    );
                }

                datas = listFilterExcluded.ToList();

                var filterExcludedExpired = _repository.GetStatusExpired();
                string[] expiredArray = filterExcludedExpired.Id.ToString().ToUpper().Split(',');
                List<AgreementViewModel> listFilterExpired = new List<AgreementViewModel>();
                if (filterExcludedExpired != null)
                {
                    listFilterExpired.AddRange(datas.Where(x => x.CellStatusBerlakuId != expiredArray[0]));
                }
                datas = listFilterExpired.ToList();

                if(!string.IsNullOrEmpty(agreementHolder))
                {
                    string[] agreementHolderArray = agreementHolder.Split(',');

                    if (agreementHolderArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellHshId != null && x.CellHshId.ToLower().Contains(agreementHolderArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<AgreementViewModel> filteredData = new List<AgreementViewModel>();

                        foreach (var item in agreementHolderArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellHshId != null && x.CellHshId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }
                if (!string.IsNullOrEmpty(entitasPertamina))
                {
                    string[] entitasArray = entitasPertamina.Split(',');

                    if (entitasArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellEntitasPertaminaId != null && x.CellEntitasPertaminaId.ToLower().Contains(entitasArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<AgreementViewModel> filteredData = new List<AgreementViewModel>();

                        foreach (var item in entitasArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellEntitasPertaminaId != null && x.CellEntitasPertaminaId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }

                if (!string.IsNullOrEmpty(jenisPerjanjian))
                {
                    string[] jenisPerjanjianArray = jenisPerjanjian.Split(',');

                    if (jenisPerjanjianArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellJenisPerjanjianId != null && x.CellJenisPerjanjianId.ToLower().Contains(jenisPerjanjianArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<AgreementViewModel> filteredData = new List<AgreementViewModel>();

                        foreach (var item in jenisPerjanjianArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellJenisPerjanjianId != null && x.CellJenisPerjanjianId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }

                if (!string.IsNullOrEmpty(kawasanMitra))
                {
                    string[] kawasanMitraArray = kawasanMitra.Split(',');

                    if (kawasanMitraArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellKawasanMitraId != null && x.CellKawasanMitraId.ToLower().Contains(kawasanMitraArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<AgreementViewModel> filteredData = new List<AgreementViewModel>();

                        foreach (var item in kawasanMitraArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellKawasanMitraId != null && x.CellKawasanMitraId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }

                if (!string.IsNullOrEmpty(negaraMitra) && string.IsNullOrEmpty(statusBerlaku))
                {
                    string[] negaraMitraArray = negaraMitra.Split(',');
                    datas = _repository.GetAllAgreementExcel().ToList();
                   
                    if (negaraMitraArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellNegaraMitraId != null && x.CellNegaraMitraId.ToLower().Contains(negaraMitraArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<AgreementViewModel> filteredData = new List<AgreementViewModel>();

                        foreach (var item in negaraMitraArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellNegaraMitraId != null && x.CellNegaraMitraId.Contains(item)));
                        }
                       
                        datas = filteredData.ToList();
                    }
                    List<AgreementViewModel> listFilterNegaraMitra = new List<AgreementViewModel>();
                    if (filterExcludedExpired != null)
                    {
                        listFilterNegaraMitra.AddRange(datas.Where(x => x.CellStatusBerlakuId != expiredArray[0]));
                    }
                    datas = listFilterNegaraMitra.ToList();
                }
                if (!string.IsNullOrEmpty(statusBerlaku) && string.IsNullOrEmpty(negaraMitra))
                {
                    string[] statusBerlakuArray = statusBerlaku.Split(',');
                    datas = _repository.GetAllAgreementExcel().ToList();
                    List<AgreementViewModel> listFilter = new List<AgreementViewModel>();
                    if (statusBerlakuArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellStatusBerlakuId != null && x.CellStatusBerlakuId.ToLower().Contains(statusBerlakuArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<AgreementViewModel> filteredData = new List<AgreementViewModel>();

                        foreach (var item in statusBerlakuArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellStatusBerlakuId != null && x.CellStatusBerlakuId.Contains(item)));
                        }
                        datas = filteredData.ToList();
                    }
                    foreach (var item in filterExcludedNegaraMitra)
                    {
                        string[] expiredArrayy = item.NegaraMitraId.ToString().ToUpper().Split(',');
                        
                        listFilter.AddRange(
                            datas.Where(x => x.CellNegaraMitraId != expiredArrayy[0])
                        );
                       
                    }
                    datas = listFilter.ToList();
                }
                if (!string.IsNullOrEmpty(statusBerlaku) && !string.IsNullOrEmpty(negaraMitra))
                {
                    datas = _repository.GetAllAgreementExcel().ToList();
                   
                    string[] statusBerlakuArray = statusBerlaku.Split(',');
                    if (statusBerlakuArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellStatusBerlakuId != null && x.CellStatusBerlakuId.ToLower().Contains(statusBerlakuArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<AgreementViewModel> filteredData = new List<AgreementViewModel>();

                        foreach (var item in statusBerlakuArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellStatusBerlakuId != null && x.CellStatusBerlakuId.Contains(item)));
                        }
                        datas = filteredData.ToList();
                    }

                    string[] negaraMitraArray = negaraMitra.Split(',');
                    if (negaraMitraArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellNegaraMitraId != null && x.CellNegaraMitraId.ToLower().Contains(negaraMitraArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<AgreementViewModel> filteredData = new List<AgreementViewModel>();

                        foreach (var item in negaraMitraArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellNegaraMitraId != null && x.CellNegaraMitraId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }

                if (!string.IsNullOrEmpty(streamBusiness))
                {
                    string[] streamBusinessArray = streamBusiness.Split(',');

                    if (streamBusinessArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellStreamBusinessId != null && x.CellStreamBusinessId.ToLower().Contains(streamBusinessArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<AgreementViewModel> filteredData = new List<AgreementViewModel>();

                        foreach (var item in streamBusinessArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellStreamBusinessId != null && x.CellStreamBusinessId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }

             

                if (!string.IsNullOrEmpty(statusDiscussion))
                {
                    string[] statusDiscussionArray = statusDiscussion.Split(',');

                    if (statusDiscussionArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellStatusDiskusiId != null && x.CellStatusDiskusiId.ToLower().Contains(statusDiscussionArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<AgreementViewModel> filteredData = new List<AgreementViewModel>();

                        foreach (var item in statusDiscussionArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellStatusDiskusiId != null && x.CellStatusDiskusiId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }

                if (!string.IsNullOrEmpty(rangeCreateDate))
                {
                    string[] createDateArray = rangeCreateDate.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    string startDateStr = createDateArray[0].Trim();
                    string endDateStr = createDateArray[1].Trim();

                    DateTime startDate;
                    DateTime endDate;

                    bool isStartDateValid = DateTime.TryParseExact(startDateStr, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                    bool isEndDateValid = DateTime.TryParseExact(endDateStr, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);


                    if (isStartDateValid && isEndDateValid)
                    {
                        datas = datas.Where(x =>
                        {
                            DateTime cellCreateDate;
                            bool isCellCreateDateValid = DateTime.TryParseExact(x.CellCreateDate, "yyyy-dd-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out cellCreateDate);
                            x.DeletedDate = null;
                            return isCellCreateDateValid && cellCreateDate >= startDate && cellCreateDate <= endDate;
                        }).ToList();
                    }
                }

                if (!string.IsNullOrEmpty(tanggalTtd))
                {
                    string[] tanggalTtdArray = tanggalTtd.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    string startDateStr = tanggalTtdArray[0].Trim();
                    string endDateStr = tanggalTtdArray[1].Trim();

                    DateTime startDate;
                    DateTime endDate;

                    bool isStartDateValid = DateTime.TryParseExact(startDateStr, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                    bool isEndDateValid = DateTime.TryParseExact(endDateStr, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                   
                    if (isStartDateValid && isEndDateValid)
                    {
                        datas = datas.Where(x =>
                        {
                            DateTime cellCreateDate;
                            bool isCellCreateDateValid = DateTime.TryParseExact(x.CellTanggalTTD, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out cellCreateDate);
                            x.DeletedDate = null;
                            return isCellCreateDateValid && cellCreateDate >= startDate && cellCreateDate <= endDate;
                        }).ToList();
                    }
                }


                if (!string.IsNullOrEmpty(tanggalBerakhir))
                {
                    string[] tanggalBerakhirArray = tanggalBerakhir.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    string startDateStr = tanggalBerakhirArray[0].Trim();
                    string endDateStr = tanggalBerakhirArray[1].Trim();

                    DateTime startDate;
                    DateTime endDate;

                    bool isStartDateValid = DateTime.TryParseExact(startDateStr, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                    bool isEndDateValid = DateTime.TryParseExact(endDateStr, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                    DateTime xy;
                    if (isStartDateValid && isEndDateValid)
                    {
                        datas = datas.Where(x =>
                        {
                            DateTime cellCreateDate;
                            bool isCellCreateDateValid = DateTime.TryParseExact(x.CellTanggalBerakhir, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out cellCreateDate);
                            x.DeletedDate = null;
                            return isCellCreateDateValid && cellCreateDate >= startDate && cellCreateDate <= endDate;
                        }).ToList();
                    }
                }
                if (draft)
                {
                    datas = datas.Where(x => x.IsDraft == true).ToList();
                }else
                {
                    datas = datas.Where(x => x.IsDraft == false || x.IsDraft == null).ToList();
                }
                if (isG2G)
                {
                    datas = datas.Where(x => x.IsGtg == true).ToList();
                } 
                if (isStrategicPartnerShip)
                {
                    //var guids = _repository.CheckIsStrategicPartnership(isStrategicPartnerShip);
                    datas= datas.Where(x => x.IsStrategicPartnerShipDecode == true).ToList();
                   
                }
                if (isBdCoreBusiness)
                {
                    //var guids = _repository.CheckBdCoreBusiness(isStrategicPartnerShip); 
                    datas = datas.Where(x => x.Valuation + x.PotentialRevenuePerYear+ x.Capex>= 500000000).ToList();
                }
                if (isGreenBusiness)
                {
                    //var guids = _repository.CheckIsGreenBusiness(isGreenBusiness); 
                    datas = datas.Where(x => x.IsGreenBusinessDecode== true).ToList();
                }

                //filtering all field
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    datas = datas.Where(x =>
                        (x.JudulPerjanjian != null && x.JudulPerjanjian.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.Valuation.HasValue && x.Valuation.Value.ToString().ToLower().Contains(searchQuery.ToLower())) ||
                        (x.Scope != null && x.Scope.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.Progress != null && x.Progress.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.DeskripsiKendala != null && x.DeskripsiKendala.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.SupportPemerintah != null && x.SupportPemerintah.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.PotensiEskalasi != null && x.PotensiEskalasi.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.CellKodeAgreement != null && x.CellKodeAgreement.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.CatatanTambahan != null && x.CatatanTambahan.ToLower().Contains(searchQuery.ToLower()))
                    ).ToList();
                }
                int x = 3;
                int y = 1;
                if (datas != null)
                {
                    foreach (var rec in datas)
                    {
                        worksheet.Cells[x, 1].Value = y;
                        worksheet.Cells[x, 2].Value = rec.CellKodeAgreement;
                        worksheet.Cells[x, 3].Value = rec.CellPartner;
                        worksheet.Cells[x, 4].Value = rec.CellEntitasPertamina;
                        worksheet.Cells[x, 5].Value = rec.CellStreamBusiness;
                        worksheet.Cells[x, 6].Value = rec.CellHsh;
                        worksheet.Cells[x, 7].Value = rec.CellFungsiPicLead;
                        worksheet.Cells[x, 8].Value = rec.CellFungsiPicMember;
                        worksheet.Cells[x, 9].Value = rec.CellJenisPerjanjian;
                        worksheet.Cells[x, 10].Value = rec.JudulPerjanjian;
                        worksheet.Cells[x, 11].Value = rec.CellKawasanMitra;
                        worksheet.Cells[x, 12].Value = rec.CellNegaraMitra;
                        worksheet.Cells[x, 13].Value = rec.CellLokasiProyek;
                        worksheet.Cells[x, 14].Value = rec.NilaiProyek;
                        worksheet.Cells[x, 15].Value = rec.CellProjectCost;
                        worksheet.Cells[x, 16].Value = rec.CellTanggalTTD;
                        worksheet.Cells[x, 17].Value = rec.CellTanggalBerakhir;
                        worksheet.Cells[x, 18].Value = rec.CellStatusBerlaku;
                        worksheet.Cells[x, 19].Value = rec.CellStatusDiskusi;
                        worksheet.Cells[x, 20].Value = rec.Scope;
                        worksheet.Cells[x, 21].Value = rec.Progress;
                        worksheet.Cells[x, 22].Value = rec.CellFaktorKendala;
                        worksheet.Cells[x, 23].Value = rec.CellKlasifikasiKendala;
                        worksheet.Cells[x, 24].Value = rec.DeskripsiKendala;
                        worksheet.Cells[x, 25].Value = rec.TindakLanjut;
                        worksheet.Cells[x, 26].Value = rec.SupportPemerintah;
                        worksheet.Cells[x, 27].Value = rec.CellLembaga;
                        worksheet.Cells[x, 28].Value = rec.PotensiEskalasi;
                        worksheet.Cells[x, 29].Value = rec.CellRelatedAgreement;
                        if (rec.CellKodeAgreementRelation != null && rec.CellRelatedAgreement != null)
                        {
                            worksheet.Cells[x, 30].Value = rec.CellKodeAgreementRelation + "_" + rec.CellRelatedAgreement;

                        }
                        else
                        {
                            worksheet.Cells[x, 30].Value = null;
                        }
                        worksheet.Cells[x, 31].Value = rec.CellCatatanTambahan;
                        worksheet.Cells[x, 32].Value = rec.CellAmandementDate;
                        worksheet.Cells[x, 33].Value = rec.CellTrafficLight;
                        x++;
                        y++;
                        
                        worksheet.Cells[1, 1, datas.Count + 1, 33].AutoFilter = true;
                    }
                }
            }

            return package;
        }
        public AgreementViewModel Delete(Guid guid, string userName)
        {
            AgreementViewModel model = new AgreementViewModel();
            model.Id = guid;
            try
            {
                model = ValidationFormDelete(model);
                if (model.IsError)
                {
                    return model;
                }
                else
                {
                    model.IsError = false;
                    _repository.Delete(guid, userName);
                    _repository.DeleteEntitas(guid, userName);
                    _repository.DeletePicFungsi(guid, userName);
                    _repository.DeletePartner(guid, userName);
                    _repository.DeleteNegaraMitra(guid, userName);
                    _repository.DeleteLokasiProyek(guid, userName);
                    _repository.DeleteStreamBusiness(guid, userName);
                    _repository.DeleteKementrianLembaga(guid, userName);
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        public async Task<AgreementViewModel> GetAgreementAsyncById(Guid guid)
        {

            AgreementViewModel model = new AgreementViewModel();

            try
            {
                if (guid != Guid.Empty)
                {
                    model = await _repository.GetAgreementAsyncById(guid);
                   
                    model.PicLevelId = _repository.GetPicLevelLeadId();
                    model.PicLevelMemberId = _repository.GetPicLevelMemberId();
                    model.AgreementEntitasPertamina = _repository.GetAgreementEntitasById(guid);
                    model.AgreementStreamBusiness = _repository.GetAgreementStreamBusinessById(guid);
                    model.AgreementNegaraMitra = _repository.GetAgreementNegaraMitraById(guid);
                    model.AgreementAddendums = _repository.GetAgreementAddendumById(guid);
                    model.AgreementPicFungsi = _repository.GetAgreementPicFungsiLeadById(guid, model.PicLevelId);
                    model.AgreementPicFungsis = _repository.GetAgreementPicFungsisById(guid, model.PicLevelMemberId);
                    model.AgreementLokasiProyeks = _repository.GetAgreementLokasiById(guid);
                    model.AgreementPartners = _repository.GetAgreementPartnerById(guid);
                    model.AgreementKementrianLembaga = _repository.GetAgreementLembagaById(guid);

                    if (model.StatusBerlakuId != null)
                    {
                        model.StatusBerlakuName = _repository.GetStatusBerlakuName(model.StatusBerlakuId).StatusName;
                    }
                    int? sequence = _repository.GetSequenceAmandement(guid);
                    if (sequence != null)
                    {
                        int sequenceValue = sequence.Value;
                        var sequenceAddendum = await _repository.GetAdendumSequence(guid,sequenceValue, false);
                        model.SequenceAmandement = sequenceAddendum.SequenceAmandement;
                    }
                    if (model.AgreementAddendums.Count > 0)
                    {

                        var zippedList = model.SequenceAmandement.Zip(model.AgreementAddendums, (sequence, addendum) => new{sequence,addendum});
                        foreach (var item in zippedList)
                        {
                            item.addendum.AddendumDateToString = item.addendum.AddendumDate.ToString("MM-dd-yyyy");
                            item.addendum.AmandementBySequence = item.sequence;
                        }
                    }
                    model.IsError = false;

                    //Read
                    model.JenisPerjanjianRead = _repository.GetJenisPerjanjianById(guid);
                    model.PicFungsiLeadRead = _repository.GetPicFungsiById(guid, model.PicLevelId).FirstOrDefault();
                    model.PicFungsiRead = _repository.GetPicFungsiById(guid, model.PicLevelMemberId).ToList();
                    model.EntitasPertaminaRead = _repository.GetEntitasPertaminaById(guid);
                    model.StatusBerlakuRead = _repository.GetStatusBerlakuById(guid);
                    model.StatusDiscussionRead = _repository.GetStatusDiscussionById(guid);
                    model.PartnerRead = _repository.GetPartnerById(guid);
                    model.NegaraMitraRead = _repository.GetNegaraMitraById(guid);
                    model.LokasiProyekRead = _repository.GetLokasiProyekById(guid);
                    model.StreamBusinessRead = _repository.GetAllStreamBusinessById(guid);
                    model.FaktorKendalaRead = _repository.GetFaktorKendalaById(guid);
                    model.KlasifikasiKendalaRead = _repository.GetKlasifikasiKendalaById(guid);
                    model.KementrianLembagaRead = _repository.GetKementrianLembagaById(guid);
                    model.RelatedAgreementRead = _repository.GetRelatedAgreementById(guid);
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        public async Task<InitiativeViewModel> GetInitiativeAsyncById(Guid guid)
        {
            InitiativeViewModel model = new InitiativeViewModel();

            try
            {
                if (guid != Guid.Empty)
                {
                    model = await _repository.GetInitiativeById(guid);
                    if (model != null)
                    {
                        model.InitiativeEntitasPertamina = _initiativeRepository.GetInitiativeEntitasById(guid);
                        model.InitiativeStreamBusiness = _initiativeRepository.GetInitiativeStreamBusinessById(guid);
                        model.InitiativeNegaraMitra = _initiativeRepository.GetInitiativeNegaraMitraById(guid);
                        model.InitiativePicFungsi = _initiativeRepository.GetInitiativePicFungsiById(guid);
                        model.InitiativeLokasiProyeks = _initiativeRepository.GetInitiativeLokasiById(guid);
                        model.InitiativePartners = _initiativeRepository.GetInitiativePartnerById(guid);
                    }
                    model.IsError = true;
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
            return model;
        }
        public async Task<OpportunityViewModel> GetOpportunityAsyncById(Guid guid)
        {
            OpportunityViewModel model = new OpportunityViewModel();

            try
            {
                if (guid != Guid.Empty)
                {
                    model = await _repository.GetOpportunityById(guid);
                    model.PICLevelLeadId = _repository.GetPicLevelLeadId();
                    model.OpEntitasPertamina = _opportunityRepository.GetOpportunityEntitasById(guid);
                    model.OpStreamBusiness = _opportunityRepository.GetOpportunityStreamBusinessById(guid);
                    model.OpPicFungsi = _opportunityRepository.GetOpportunityPicFungsiById(guid, model.PICLevelLeadId);
                    model.OpKesiapanProyek = _opportunityRepository.GetOpportunityKesiapanProyekById(guid);
                    model.OpNegaraMitra = _opportunityRepository.GetOpportunityNegaraMitraById(guid);
                    model.OpTargetMitra = _opportunityRepository.GetOpportunityTargetMitraById(guid);
                    model.OpPartners = _opportunityRepository.GetOpportunityPartnerById(guid);
                    model.OpLokasiProyeks = _opportunityRepository.GetOpportunityLokasiById(guid);
                    model.IsError = true;
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
            return model;
        }
        public async Task<AgreementViewModel> GetHubHeadByHubId(Guid hubId)
        {
            var result = await _repository.GetHubHeadByHubId(hubId);
            return result;
        } 
        public async Task<AgreementViewModel> GetAdendumSequence(Guid guid,int sequence, bool addRepeater)
        {
            var result = await _repository.GetAdendumSequence(guid,sequence, addRepeater);
            result.AgreementAddendum = _repository.GetAddendumLastById(guid);
            if(result.AgreementAddendum != null)
            {
                result.AgreementAddendum.AddendumDateToString =result.AgreementAddendum.AddendumDate.ToString("MM-dd-yyyy");
            }
            return result;
        }
        public async Task<AgreementViewModel> GetAddendumById(Guid hubId)
        {
            var result = await _repository.GetHubHeadByHubId(hubId);
            return result;
        }
    }
}
