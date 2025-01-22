using OfficeOpenXml;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Implement
{
    public class InitiativeService : IInitiativeService
    {
        private readonly IInitiativeRepository _repository;
        private readonly IProjectsToOfferRepository _opportunityRepository;

        public InitiativeService(IInitiativeRepository repository, IProjectsToOfferRepository opportunityRepository)
        {
            _repository = repository;
            _opportunityRepository = opportunityRepository;
        }

        public async Task<PaginationBaseModel<InitiativeViewModel>> GetListPaging(RequestFormDtBaseModel request, InitiativeViewModel decodeModel)
        {
            var resultData = await _repository.GetList(request, decodeModel);

            var result = new PaginationBaseModel<InitiativeViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }
        public InitiativeViewModel GenerelizeDataInitiative(InitiativeViewModel rec)
        {
            List<InitiativePartnerViewModel> partners = new List<InitiativePartnerViewModel>();
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

            List<InitiativeEntitasPertaminaViewModel> entitasPertaminas = new List<InitiativeEntitasPertaminaViewModel>();
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

            List<InitiativeStreamBusinessViewModel> streamBusinesses = new List<InitiativeStreamBusinessViewModel>();
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

            List<InitiativeNegaraMitraViewModel> negaraMitras = new List<InitiativeNegaraMitraViewModel>();
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
            rec.RowJudulInitiative = _repository.GetJudulInitiative(rec.Id);
            rec.RowPartner = strPartner;
            rec.RowEntitasPertamina = strEntitasPertamina;
            rec.RowStreamBusiness = strStreamBusiness;
            rec.RowInitiativeStage = _repository.GetInitiativeStage(rec.Id);
            rec.RowInitiativeStatus = _repository.GetInitiativeStatus(rec.Id);
            rec.RowNegaraMitra = strNegaraMitra;
            rec.RowKawasanMitra = strKawasanMitra;

            return rec;
        }
        public InitiativeViewModel Save(InitiativeViewModel model, string userName, string companyCode)
        {
            try
            {
                if (model.InitiativePartners == null || string.IsNullOrEmpty(model.InitiativePartners.FirstOrDefault().PartnerName))
                {
                    model.IsError = true;
                    model.ErrorMessage += "Minimum 1 data required for Partner(s) ";
                }
                else if (model.InitiativeLokasiProyeks == null || string.IsNullOrEmpty(model.InitiativeLokasiProyeks.FirstOrDefault().LokasiProyek))
                {
                    model.IsError = true;
                    model.ErrorMessage += "Minimum 1 data required for Lokasi Proyek ";
                }
                else
                {
                    model.IsError = false;
                    DateTime now = DateTime.Now;
                    model.Id = Guid.NewGuid();
                    model.CreateBy = userName;
                    model.CreateDate = now;
                    model.IsDraft = false;
                    model.CompanyCode = companyCode;
                    _repository.Save(model);

                    InitiativeNegaraMitraViewModel negara = new InitiativeNegaraMitraViewModel();
                    negara.Id = Guid.NewGuid();
                    negara.CreateDate = now;
                    negara.CreateBy = userName;
                    negara.InitiativeId = model.Id;
                    negara.NegaraMitraId = model.NegaraMitraId;
                    _repository.SaveNegaraMitra(negara);

                    foreach (var pic in model.FungsiPicId)
                    {
                        InitiativePicFungsiViewModel picFungsi = new InitiativePicFungsiViewModel();
                        picFungsi.Id = Guid.NewGuid();
                        picFungsi.CreateBy = model.CreateBy;
                        picFungsi.CreateDate = model.CreateDate;
                        picFungsi.InitiativeId = model.Id;
                        picFungsi.PicFungsiId = pic;
                        _repository.SavePic(picFungsi);
                    }
                    foreach (var entitas in model.EntitasPertaminaId)
                    {
                        InitiativeEntitasPertaminaViewModel entitasPertamina = new InitiativeEntitasPertaminaViewModel();
                        entitasPertamina.Id = Guid.NewGuid();
                        entitasPertamina.CreateBy = model.CreateBy;
                        entitasPertamina.CreateDate = model.CreateDate;
                        entitasPertamina.InitiativeId = model.Id;
                        entitasPertamina.EntitasPertaminaId = entitas;
                        _repository.SaveEntitasPertamina(entitasPertamina);
                    }
                    foreach (var stream in model.StreamBusinessId)
                    {
                        InitiativeStreamBusinessViewModel streamBusiness = new InitiativeStreamBusinessViewModel();
                        streamBusiness.Id = Guid.NewGuid();
                        streamBusiness.CreateBy = model.CreateBy;
                        streamBusiness.CreateDate = model.CreateDate;
                        streamBusiness.InitiativeId = model.Id;
                        streamBusiness.StreamBusinessId = stream;
                        _repository.SaveStreamBusiness(streamBusiness);
                    }
                    if (model.InitiativePartners != null && model.InitiativePartners.Count > 0 )
                    {
                        bool isAnyNonEmpty = false;
                        foreach (var partner in model.InitiativePartners)
                        {
                            if (!string.IsNullOrEmpty(partner.PartnerName))
                            {
                                InitiativePartnerViewModel pr = new InitiativePartnerViewModel();
                                pr.Id = Guid.NewGuid();
                                pr.CreateDate = now;
                                pr.CreateBy = userName;
                                pr.InitiativeId = model.Id;
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

                    if (model.InitiativeLokasiProyeks != null && model.InitiativeLokasiProyeks.Count > 0)
                    {
                        bool isAnyNonEmpty = false;
                        foreach (var lokasiProyek in model.InitiativeLokasiProyeks)
                        {
                            if (!string.IsNullOrEmpty(lokasiProyek.LokasiProyek))
                            {
                                InitiativeLokasiProyekViewModel lp = new InitiativeLokasiProyekViewModel();
                                lp.Id = Guid.NewGuid();
                                lp.CreateDate = now;
                                lp.CreateBy = userName;
                                lp.InitiativeId = model.Id;
                                lp.LokasiProyek = lokasiProyek.LokasiProyek;
                                _repository.SaveToLokasiProyek(lp);
                                isAnyNonEmpty = true;
                            }
                        }

                        if (!isAnyNonEmpty)
                        {
                            model.IsError = true;
                            model.ErrorMessage += "Minimum 1 data required for Lokasi Proyek ";
                        }
                    }
                    else
                    {
                        model.IsError = true;
                        model.ErrorMessage += "Minimum 1 data required for Lokasi Proyek ";
                    }
                    if (model.KementrianLembagaId.Count > 0)
                    {
                        foreach (var kementrianId in model.KementrianLembagaId)
                        {
                            InitiativeKementrianLembagaViewModel kementrianLembaga = new InitiativeKementrianLembagaViewModel();
                            kementrianLembaga.Id = Guid.NewGuid();
                            kementrianLembaga.CreatedBy = model.CreateBy;
                            kementrianLembaga.CreatedDate = model.CreateDate;
                            kementrianLembaga.InitiativeId = model.Id;
                            kementrianLembaga.KementrianLembagaId = kementrianId;
                            _repository.SaveKementrianLembaga(kementrianLembaga);
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
        protected InitiativeViewModel ValidationForm(InitiativeViewModel model)
        {
            if (model.JudulInisiasi == null)
            {
                model.IsError = true;
                model.ErrorMessage = "Judul inisiasi wajib diisi sebelum menyimpan draft!";
            }

            return model;
        }
        public InitiativeViewModel SaveDraft(InitiativeViewModel model, string userName, string companyCode)
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
                _repository.Save(model);
                if (model.NegaraMitraId != null)
                {
                    InitiativeNegaraMitraViewModel negara = new InitiativeNegaraMitraViewModel();
                    negara.Id = Guid.NewGuid();
                    negara.CreateDate = now;
                    negara.CreateBy = userName;
                    negara.InitiativeId = model.Id;
                    negara.NegaraMitraId = model.NegaraMitraId;
                    _repository.SaveNegaraMitra(negara);
                }
                if (model.FungsiPicId != null)
                {
                    foreach (var pic in model.FungsiPicId)
                    {
                        InitiativePicFungsiViewModel picFungsi = new InitiativePicFungsiViewModel();
                        picFungsi.Id = Guid.NewGuid();
                        picFungsi.CreateBy = model.CreateBy;
                        picFungsi.CreateDate = model.CreateDate;
                        picFungsi.InitiativeId = model.Id;
                        picFungsi.PicFungsiId = pic;
                        _repository.SavePic(picFungsi);
                    }
                }
                if (model.EntitasPertaminaId != null)
                {
                    foreach (var entitas in model.EntitasPertaminaId)
                    {
                        InitiativeEntitasPertaminaViewModel entitasPertamina = new InitiativeEntitasPertaminaViewModel();
                        entitasPertamina.Id = Guid.NewGuid();
                        entitasPertamina.CreateBy = model.CreateBy;
                        entitasPertamina.CreateDate = model.CreateDate;
                        entitasPertamina.InitiativeId = model.Id;
                        entitasPertamina.EntitasPertaminaId = entitas;
                        _repository.SaveEntitasPertamina(entitasPertamina);
                    }
                }
                if (model.StreamBusinessId != null)
                {
                    foreach (var stream in model.StreamBusinessId)
                    {
                        InitiativeStreamBusinessViewModel streamBusiness = new InitiativeStreamBusinessViewModel();
                        streamBusiness.Id = Guid.NewGuid();
                        streamBusiness.CreateBy = model.CreateBy;
                        streamBusiness.CreateDate = model.CreateDate;
                        streamBusiness.InitiativeId = model.Id;
                        streamBusiness.StreamBusinessId = stream;
                        _repository.SaveStreamBusiness(streamBusiness);
                    }
                }
                if (model.InitiativePartners != null)
                {
                    foreach (var partner in model.InitiativePartners)
                    {
                        if (!string.IsNullOrEmpty(partner.PartnerName))
                        {
                            InitiativePartnerViewModel pr = new InitiativePartnerViewModel();
                            pr.Id = Guid.NewGuid();
                            pr.CreateDate = now;
                            pr.CreateBy = userName;
                            pr.InitiativeId = model.Id;
                            pr.PartnerName = partner.PartnerName;
                            _repository.SaveToPartner(pr);
                        }
                    }
                }
                if (model.InitiativeLokasiProyeks != null)
                {
                    foreach (var lokasiProyek in model.InitiativeLokasiProyeks)
                    {
                        if (!string.IsNullOrEmpty(lokasiProyek.LokasiProyek))
                        {
                            InitiativeLokasiProyekViewModel lp = new InitiativeLokasiProyekViewModel();
                            lp.Id = Guid.NewGuid();
                            lp.CreateDate = now;
                            lp.CreateBy = userName;
                            lp.InitiativeId = model.Id;
                            lp.LokasiProyek = lokasiProyek.LokasiProyek;
                            _repository.SaveToLokasiProyek(lp);
                        }
                    }
                }
                if (model.KementrianLembagaId.Count > 0)
                {
                    foreach (var kementrianId in model.KementrianLembagaId)
                    {
                        InitiativeKementrianLembagaViewModel kementrianLembaga = new InitiativeKementrianLembagaViewModel();
                        kementrianLembaga.Id = Guid.NewGuid();
                        kementrianLembaga.CreatedBy = model.CreateBy;
                        kementrianLembaga.CreatedDate = model.CreateDate;
                        kementrianLembaga.InitiativeId = model.Id;
                        kementrianLembaga.KementrianLembagaId = kementrianId;
                        _repository.SaveKementrianLembaga(kementrianLembaga);
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
        public async Task<InitiativeViewModel> GetInitiativeByIdAsync(Guid guid)
        {

            InitiativeViewModel model = new InitiativeViewModel();

            try
            {
                if (guid != Guid.Empty)
                {
                    model = await _repository.GetInitiativeAsyncById(guid);
                    if (model != null)
                    {
                        model.InitiativeEntitasPertamina = _repository.GetInitiativeEntitasById(guid);
                        model.InitiativeStreamBusiness = _repository.GetInitiativeStreamBusinessById(guid);
                        model.InitiativeNegaraMitra = _repository.GetInitiativeNegaraMitraById(guid);
                        model.InitiativePicFungsi = _repository.GetInitiativePicFungsiById(guid);
                        model.InitiativeLokasiProyeks = _repository.GetInitiativeLokasiById(guid);
                        model.InitiativePartners = _repository.GetInitiativePartnerById(guid);
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
        public InitiativeViewModel GetInitiativeById(Guid guid)
        {

            InitiativeViewModel model = new InitiativeViewModel();

            try
            {
                if (guid != Guid.Empty)
                {
                    model = _repository.GetInitiativeById(guid);
                    if (model != null)
                    {
                        model.InitiativeEntitasPertamina = _repository.GetInitiativeEntitasById(guid);
                        model.InitiativeStreamBusiness = _repository.GetInitiativeStreamBusinessById(guid);
                        model.InitiativeNegaraMitra = _repository.GetInitiativeNegaraMitraById(guid);
                        model.InitiativePicFungsi = _repository.GetInitiativePicFungsiById(guid);
                        model.InitiativeLokasiProyeks = _repository.GetInitiativeLokasiById(guid);
                        model.InitiativePartners = _repository.GetInitiativePartnerById(guid);
                        model.InitiativeKementrianLembaga = _repository.GetInitiativeKementrianLembagaById(guid);
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
        public InitiativeViewModel Update(InitiativeViewModel model, string userName)
        {
            try
            {
                InitiativeNegaraMitraViewModel recordNegaraMitra = _repository.GetInitiativeNegaraMitraById(model.Id);

                DateTime now = DateTime.Now;
                if (model.IsError)
                {
                    return model;
                }
                else
                {
                    if (model.InitiativePartners == null || string.IsNullOrEmpty(model.InitiativePartners.FirstOrDefault().PartnerName))
                    {
                        model.IsError = true;
                        model.ErrorMessage += "Minimum 1 data required for Partner(s) ";
                    }
                    else if (model.InitiativeLokasiProyeks == null || string.IsNullOrEmpty(model.InitiativeLokasiProyeks.FirstOrDefault().LokasiProyek))
                    {
                        model.IsError = true;
                        model.ErrorMessage += "Minimum 1 data required for Lokasi Proyek ";
                    }
                    else
                    {
                        model.IsError = false;
                        _repository.Update(model, userName);
                        //Dropdown multi
                        if (model.StreamBusinessId != null)
                        {
                            _repository.DeleteExistingStreamBusiness(model.Id);
                            foreach (var stream in model.StreamBusinessId)
                            {
                                InitiativeStreamBusinessViewModel streamBusiness = new InitiativeStreamBusinessViewModel();
                                streamBusiness.Id = Guid.NewGuid();
                                streamBusiness.CreateBy = model.CreateBy;
                                streamBusiness.CreateDate = model.CreateDate;
                                streamBusiness.InitiativeId = model.Id;
                                streamBusiness.StreamBusinessId = stream;
                                _repository.SaveStreamBusiness(streamBusiness);
                            }
                        }
                        if (model.FungsiPicId != null)
                        {
                            _repository.DeleteExistingFungsiPic(model.Id);
                            foreach (var pic in model.FungsiPicId)
                            {
                                InitiativePicFungsiViewModel picFungsi = new InitiativePicFungsiViewModel();
                                picFungsi.Id = Guid.NewGuid();
                                picFungsi.CreateBy = model.CreateBy;
                                picFungsi.CreateDate = model.CreateDate;
                                picFungsi.InitiativeId = model.Id;
                                picFungsi.PicFungsiId = pic;
                                _repository.SavePic(picFungsi);
                            }
                        }
                        if (model.EntitasPertaminaId != null)
                        {
                            _repository.DeleteExistingEntitasPertamina(model.Id);
                            foreach (var entitas in model.EntitasPertaminaId)
                            {
                                InitiativeEntitasPertaminaViewModel entitasPertamina = new InitiativeEntitasPertaminaViewModel();
                                entitasPertamina.Id = Guid.NewGuid();
                                entitasPertamina.CreateBy = model.CreateBy;
                                entitasPertamina.CreateDate = model.CreateDate;
                                entitasPertamina.InitiativeId = model.Id;
                                entitasPertamina.EntitasPertaminaId = entitas;
                                _repository.SaveEntitasPertamina(entitasPertamina);
                            }
                        }
                        if (model.NegaraMitraId != null)
                        {
                            _repository.DeleteExistingNegaraMitra(model.Id);
                            InitiativeNegaraMitraViewModel negara = new InitiativeNegaraMitraViewModel();
                            negara.Id = Guid.NewGuid();
                            negara.CreateDate = now;
                            negara.CreateBy = userName;
                            negara.InitiativeId = model.Id;
                            negara.NegaraMitraId = model.NegaraMitraId;
                            _repository.SaveNegaraMitra(negara);
                        }
                        if (model.InitiativeLokasiProyeks != null && model.InitiativeLokasiProyeks.Count > 0)
                        {
                            bool isAnyNonEmpty = false;
                            foreach (var entitas in model.InitiativeLokasiProyeks)
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
                                foreach (var entitas in model.InitiativeLokasiProyeks)
                                {
                                    if (!string.IsNullOrWhiteSpace(entitas.LokasiProyek))
                                    {
                                        InitiativeLokasiProyekViewModel lokasi = new InitiativeLokasiProyekViewModel();
                                        lokasi.Id = Guid.NewGuid();
                                        lokasi.CreateBy = model.CreateBy;
                                        lokasi.CreateDate = model.CreateDate;
                                        lokasi.InitiativeId = model.Id;
                                        lokasi.LokasiProyek = entitas.LokasiProyek;
                                        _repository.SaveToLokasiProyek(lokasi);
                                    }
                                }
                            }
                            else
                            {
                                model.IsError = true;
                                model.ErrorMessage += "Minimum 1 data required for Lokasi Proyek ";
                            }
                        }
                        else
                        {
                            model.IsError = true;
                            model.ErrorMessage += "Minimum 1 data required for Lokasi Proyek ";
                        }


                        if (model.InitiativePartners != null && model.InitiativePartners.Count > 0)
                        {
                            bool isAnyNonEmpty = false;
                            foreach (var entitas in model.InitiativePartners)
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
                                foreach (var entitas in model.InitiativePartners)
                                {
                                    if (!string.IsNullOrWhiteSpace(entitas.PartnerName))
                                    {
                                        InitiativePartnerViewModel partner = new InitiativePartnerViewModel();
                                        partner.Id = Guid.NewGuid();
                                        partner.CreateBy = model.CreateBy;
                                        partner.CreateDate = model.CreateDate;
                                        partner.InitiativeId = model.Id;
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
                        if (model.KementrianLembagaId.Count > 0)
                        {
                            _repository.DeleteExistingKementrianLembaga(model.Id);
                            foreach (var kementrianId in model.KementrianLembagaId)
                            {
                                InitiativeKementrianLembagaViewModel kementrianLembaga = new InitiativeKementrianLembagaViewModel();
                                kementrianLembaga.Id = Guid.NewGuid();
                                kementrianLembaga.CreatedBy = model.CreateBy;
                                kementrianLembaga.CreatedDate = model.CreateDate;
                                kementrianLembaga.InitiativeId = model.Id;
                                kementrianLembaga.KementrianLembagaId = kementrianId;
                                _repository.SaveKementrianLembaga(kementrianLembaga);
                            }
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
        public InitiativeViewModel GetInitiativeCount()
        {
            InitiativeViewModel model = new InitiativeViewModel();
            try
            {
                List<InitiativeStageViewModel> allRecordInitiativeStage = _repository.GetRecordsInitiativeStage();
                List<InitiativeStageViewModel> setRecordInitiativeStage = new List<InitiativeStageViewModel>();
                foreach (var rec in allRecordInitiativeStage)
                {
                    rec.Count = _repository.GetCountInitiativeStage(rec.RelationSequence.Value);
                    setRecordInitiativeStage.Add(rec);
                }

                List<InitiativeStatusViewModel> allRecordInitiativeStatus = _repository.GetRecordsInitiativeStatus();
                List<InitiativeStatusViewModel> setRecordInitiativeStatus = new List<InitiativeStatusViewModel>();
                foreach (var rec in allRecordInitiativeStatus)
                {
                    rec.Count = _repository.GetCountInitiativeStatus(rec.RelationSequence.Value);
                    setRecordInitiativeStatus.Add(rec);
                }

                List<HshViewModel> allRecordHsh = _repository.GetRecordsHsh();
                List<HshViewModel> setRecordHsh = new List<HshViewModel>();
                foreach (var rec in allRecordHsh)
                {
                    rec.Count = _repository.GetCountHsh(rec.RelationSequence.Value);
                    setRecordHsh.Add(rec);
                }

                model.CountInitiativeStage = setRecordInitiativeStage;
                model.CountInitiativeStatus = setRecordInitiativeStatus;
                model.CountInitiativeHolder = setRecordHsh;
                model.CountInitiative = _repository.GetCountRecordsInitiative();
                model.CountNegaraMitra = _repository.GetCountRecordsNegaraMitra();
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
        public async Task<InitiativeViewModel> GetInitiativeCountWithFilter(InitiativeViewModel model)
        {
            try
            {
                IQueryable<InitiativeViewModel> query = await _repository.QueryInitiativeWithFilter(model);
                List<InitiativeStageViewModel> allRecordInitiativeStage = _repository.GetRecordsInitiativeStage();
                List<InitiativeStageViewModel> setRecordInitiativeStage = new List<InitiativeStageViewModel>();
                foreach (var rec in allRecordInitiativeStage)
                {
                    rec.Count = query.Where(x => x.QueryInitiativeStageId == rec.Id).GroupBy(x => x.Id).Count();
                    setRecordInitiativeStage.Add(rec);
                }


                List<InitiativeStatusViewModel> allRecordInitiativeStatus = _repository.GetRecordsInitiativeStatus();
                List<InitiativeStatusViewModel> setRecordInitiativeStatus = new List<InitiativeStatusViewModel>();
                foreach (var rec in allRecordInitiativeStatus)
                {
                    rec.Count = query.Where(x => x.QueryInitiativeStatusId == rec.Id).GroupBy(x => x.Id).Count();
                    setRecordInitiativeStatus.Add(rec);
                }

                List<HshViewModel> allRecordHsh = _repository.GetRecordsHsh();
                List<HshViewModel> setRecordHsh = new List<HshViewModel>();
                foreach (var rec in allRecordHsh)
                {
                    rec.Count = query.Where(x => x.QueryHshId == rec.Id).GroupBy(x => x.Id).Count();
                    setRecordHsh.Add(rec);
                }

                model.CountInitiativeStage = setRecordInitiativeStage;
                model.CountInitiativeStatus = setRecordInitiativeStatus;
                model.CountInitiativeHolder = setRecordHsh;
                model.CountInitiative = query.GroupBy(x => x.Id).Count();
                model.CountNegaraMitra = _repository.GetRecordsNegaraMitra(model).Result.Count();
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
        public async Task<StatusBerlakuViewModel> StatusBerlakuCounting(DateTime endDate)
        {

            StatusBerlakuViewModel model = new StatusBerlakuViewModel();
            DateTime today = DateTime.Now;
            DateTime sixMonthsBefore = endDate.AddMonths(-6);

            if (DateTime.Parse(today.ToString("MM-dd-yyyy")) > (DateTime.Parse(endDate.ToString("MM-dd-yyyy"))))
            {
                model.RelationSequence = 3;
            }
            else if (DateTime.Parse(today.ToString("MM-dd-yyyy")) >= (DateTime.Parse(sixMonthsBefore.ToString("MM-dd-yyyy"))))
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
        public async Task<InitiativeViewModel> GetInitiativeCountWithFilter(InitiativeViewModel model, string strSearch)
        {
            try
            {
                IQueryable<InitiativeViewModel> query = await _repository.QueryInitiativeWithFilter(model);
                List<InitiativeStageViewModel> allRecordInitiativeStage = _repository.GetRecordsInitiativeStage();
                List<InitiativeStageViewModel> setRecordInitiativeStage = new List<InitiativeStageViewModel>();
                foreach (var rec in allRecordInitiativeStage)
                {
                    rec.Count = query.Where(x => x.QueryInitiativeStageId == rec.Id && (x.JudulInisiasi.Contains(strSearch) ||
                                                                                        x.NilaiProyek.Contains(strSearch) ||
                                                                                        x.TargetWaktuProyek.Contains(strSearch) ||
                                                                                        x.Scope.Contains(strSearch) ||
                                                                                        x.Progress.Contains(strSearch) ||
                                                                                        x.IsuKendala.Contains(strSearch) ||
                                                                                        x.Referal.Contains(strSearch) ||
                                                                                        x.PotensiEskalasi.Contains(strSearch) ||
                                                                                        x.CatatanTambahan.Contains(strSearch) ||
                                                                                        x.SupportPemerintah.Contains(strSearch) ||
                                                                                        x.ValueForIndonesia.Contains(strSearch))).GroupBy(x => x.Id).Count();
                                                                                        
                    setRecordInitiativeStage.Add(rec);
                }

                List<InitiativeStatusViewModel> allRecordInitiativeStatus = _repository.GetRecordsInitiativeStatus();
                List<InitiativeStatusViewModel> setRecordInitiativeStatus = new List<InitiativeStatusViewModel>();
                foreach (var rec in allRecordInitiativeStatus)
                {
                    rec.Count = query.Where(x => x.QueryInitiativeStatusId == rec.Id && (x.JudulInisiasi.Contains(strSearch) ||
                                                                                        x.NilaiProyek.Contains(strSearch) ||
                                                                                        x.TargetWaktuProyek.Contains(strSearch) ||
                                                                                        x.Scope.Contains(strSearch) ||
                                                                                        x.Progress.Contains(strSearch) ||
                                                                                        x.IsuKendala.Contains(strSearch) ||
                                                                                        x.Referal.Contains(strSearch) ||
                                                                                        x.PotensiEskalasi.Contains(strSearch) ||
                                                                                        x.CatatanTambahan.Contains(strSearch) ||
                                                                                        x.SupportPemerintah.Contains(strSearch) ||
                                                                                        x.ValueForIndonesia.Contains(strSearch))).GroupBy(x => x.Id).Count();
                    setRecordInitiativeStatus.Add(rec);
                }

                List<HshViewModel> allRecordHsh = _repository.GetRecordsHsh();
                List<HshViewModel> setRecordHsh = new List<HshViewModel>();
                foreach (var rec in allRecordHsh)
                {
                    rec.Count = query.Where(x => x.QueryHshId == rec.Id && (x.JudulInisiasi.Contains(strSearch) ||
                                                                            x.NilaiProyek.Contains(strSearch) ||
                                                                            x.TargetWaktuProyek.Contains(strSearch) ||
                                                                            x.Scope.Contains(strSearch) ||
                                                                            x.Progress.Contains(strSearch) ||
                                                                            x.IsuKendala.Contains(strSearch) ||
                                                                            x.Referal.Contains(strSearch) ||
                                                                            x.PotensiEskalasi.Contains(strSearch) ||
                                                                            x.CatatanTambahan.Contains(strSearch) ||
                                                                            x.SupportPemerintah.Contains(strSearch) ||
                                                                            x.ValueForIndonesia.Contains(strSearch))).GroupBy(x => x.Id).Count();
                    setRecordHsh.Add(rec);
                }

                model.CountInitiativeStage = setRecordInitiativeStage;
                model.CountInitiativeStatus = setRecordInitiativeStatus;
                model.CountInitiativeHolder = setRecordHsh;
                model.CountInitiative = query.Where(x => x.JudulInisiasi.Contains(strSearch) ||
                                                                            x.NilaiProyek.Contains(strSearch) ||
                                                                            x.TargetWaktuProyek.Contains(strSearch) ||
                                                                            x.Scope.Contains(strSearch) ||
                                                                            x.Progress.Contains(strSearch) ||
                                                                            x.IsuKendala.Contains(strSearch) ||
                                                                            x.Referal.Contains(strSearch) ||
                                                                            x.PotensiEskalasi.Contains(strSearch) ||
                                                                            x.CatatanTambahan.Contains(strSearch) ||
                                                                            x.SupportPemerintah.Contains(strSearch) ||
                                                                            x.ValueForIndonesia.Contains(strSearch)).GroupBy(x => x.Id).Count();

                model.CountNegaraMitra = _repository.GetRecordsNegaraMitraWithSearch(model, strSearch).Result.Count();
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
        public ExcelPackage ExportXLS(bool withData, string initiativeStage, string initiativeStatus, string initiativeHolder, string negaraMitra, string kawasanMitra, string streamBussiness, string entitasPertamina, string searchQuery, string rangeCreateDate, bool draft)
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "Initiative Download";
            package.Workbook.Properties.Author = "Pertamina";
            package.Workbook.Properties.Subject = "Data Eksport";
            package.Workbook.Properties.Keywords = "IRIS - Pertamina";

            var worksheet = package.Workbook.Worksheets.Add("Initiative");

            // First: add the header
            worksheet.Cells[1, 1].Value = "NO.";
            worksheet.Cells[1, 2].Value = "PARTNER(S)";
            worksheet.Cells[1, 3].Value = "ENTITAS PERTAMINA";
            worksheet.Cells[1, 4].Value = "STREAM BUSINESS";
            worksheet.Cells[1, 5].Value = "HOLDING/SUB-HOLDING";
            worksheet.Cells[1, 6].Value = "FUNGSI PIC";
            worksheet.Cells[1, 7].Value = "JUDUL INITIATIVE";
            worksheet.Cells[1, 8].Value = "INTEREST";
            worksheet.Cells[1, 9].Value = "INITIATIVE STATUS";
            worksheet.Cells[1, 10].Value = "INITIATIVE TYPE";
            worksheet.Cells[1, 11].Value = "INITIATIVE STAGE";
            worksheet.Cells[1, 12].Value = "KAWASAN MITRA";
            worksheet.Cells[1, 13].Value = "NEGARA MITRA";
            worksheet.Cells[1, 14].Value = "LOKASI PROYEK";
            worksheet.Cells[1, 15].Value = "NILAI PROYEK";
            worksheet.Cells[1, 16].Value = "TARGET WAKTU PROYEK";
            worksheet.Cells[1, 17].Value = "SCOPE/PROJECT OVERVIEW";
            worksheet.Cells[1, 18].Value = "PROGRESS";
            worksheet.Cells[1, 19].Value = "VALUE FOR INDONESIA";
            worksheet.Cells[1, 20].Value = "ISU KENDALA";
            worksheet.Cells[1, 21].Value = "TINDAK LANJUT";
            worksheet.Cells[1, 22].Value = "DUKUNGAN YANG DIPERLUKAN DARI PEMERINTAH";
            worksheet.Cells[1, 23].Value = "REFERRAL";
            worksheet.Cells[1, 24].Value = "POTENSIAL ESKALASI";
            worksheet.Cells[1, 25].Value = "RELATED AGREEMENT";
            worksheet.Cells[1, 26].Value = "CATATAN TAMBAHAN";
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
                List<InitiativeViewModel> datas = _repository.GetAllInitiativeExcel()
                    .OrderBy(x => x?.CellJudulInisiasi) 
                    .ToList();

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    datas = datas.Where(x =>
                                            (x.CellJudulInisiasi != null && x.CellJudulInisiasi.ToLower().Contains(searchQuery.ToLower())) ||
                                            (x.CellNilaiProyek != null && x.CellNilaiProyek.ToLower().Contains(searchQuery.ToLower())) ||
                                            (x.CellTargetProyek != null && x.CellTargetProyek.ToLower().Contains(searchQuery.ToLower())) ||
                                            (x.CellLokasiProyek != null && x.CellLokasiProyek.ToLower().Contains(searchQuery.ToLower())) ||
                                            (x.CellScope != null && x.CellScope.ToLower().Contains(searchQuery.ToLower())) ||
                                            (x.CellProgress != null && x.CellProgress.ToLower().Contains(searchQuery.ToLower())) ||
                                            (x.CellIsuKendala != null && x.CellIsuKendala.ToLower().Contains(searchQuery.ToLower())) ||
                                            (x.CellReferral != null && x.CellReferral.ToLower().Contains(searchQuery.ToLower())) ||
                                            (x.CellPotensiEkskalasi != null && x.CellPotensiEkskalasi.ToLower().Contains(searchQuery.ToLower())) ||
                                            (x.CellCatatanTambahan != null && x.CellCatatanTambahan.ToLower().Contains(searchQuery.ToLower())) ||
                                            (x.CellSupportPemerintah != null && x.CellSupportPemerintah.ToLower().Contains(searchQuery.ToLower())) ||
                                            (x.CellValue != null && x.CellValue.ToLower().Contains(searchQuery.ToLower())) 
                    ).ToList();
                }

                if (!string.IsNullOrEmpty(initiativeStage))
                {
                    string[] initiativeStageArray = initiativeStage.Split(',');

                    if (initiativeStageArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellInitiativeStageId != null && x.CellInitiativeStageId.ToLower().Contains(initiativeStageArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<InitiativeViewModel> filteredData = new List<InitiativeViewModel>();

                        foreach (var item in initiativeStageArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellInitiativeStageId != null && x.CellInitiativeStageId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }

                if (!string.IsNullOrEmpty(initiativeStatus))
                {
                    string[] initiativeStatusArray = initiativeStatus.Split(',');

                    if (initiativeStatusArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellInitiativeStatusId != null && x.CellInitiativeStatusId.ToLower().Contains(initiativeStatusArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<InitiativeViewModel> filteredData = new List<InitiativeViewModel>();

                        foreach (var item in initiativeStatusArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellInitiativeStatusId != null && x.CellInitiativeStatusId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }

                if (!string.IsNullOrEmpty(initiativeHolder))
                {
                    string[] initiativeHolderArray = initiativeHolder.Split(',');

                    if (initiativeHolderArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellInitiativeHolderId != null && x.CellInitiativeHolderId.ToLower().Contains(initiativeHolderArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<InitiativeViewModel> filteredData = new List<InitiativeViewModel>();

                        foreach (var item in initiativeHolderArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellInitiativeHolderId != null && x.CellInitiativeHolderId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }

                if (!string.IsNullOrEmpty(negaraMitra))
                {
                    string[] negaraMitraArray = negaraMitra.Split(',');

                    if (negaraMitraArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellNegaraMitraId != null && x.CellNegaraMitraId.ToLower().Contains(negaraMitraArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<InitiativeViewModel> filteredData = new List<InitiativeViewModel>();

                        foreach (var item in negaraMitraArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellNegaraMitraId != null && x.CellNegaraMitraId.Contains(item)));
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
                        List<InitiativeViewModel> filteredData = new List<InitiativeViewModel>();

                        foreach (var item in kawasanMitraArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellKawasanMitraId != null && x.CellKawasanMitraId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }

                if (!string.IsNullOrEmpty(streamBussiness))
                {
                    string[] streamBusinessArray = streamBussiness.Split(',');

                    if (streamBusinessArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellStreamBusinessId != null && x.CellStreamBusinessId.ToLower().Contains(streamBusinessArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<InitiativeViewModel> filteredData = new List<InitiativeViewModel>();

                        foreach (var item in streamBusinessArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellStreamBusinessId != null && x.CellStreamBusinessId.Contains(item)));
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
                        List<InitiativeViewModel> filteredData = new List<InitiativeViewModel>();

                        foreach (var item in entitasArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellEntitasPertaminaId != null && x.CellEntitasPertaminaId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }

                if(!string.IsNullOrEmpty(rangeCreateDate))
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
                if (draft)
                {
                    datas = datas.Where(x => x.IsDraft == true).ToList();
                }
                else
                {
                    datas = datas.Where(x => x.IsDraft == false || x.IsDraft == null).ToList();
                }

                int x = 3;
                int y = 1;
                if (datas != null)
                {
                    foreach (var rec in datas)
                    {
                        worksheet.Cells[x, 1].Value = y;
                        worksheet.Cells[x, 2].Value = rec.CellPartner;
                        worksheet.Cells[x, 3].Value = rec.CellEntitasPertamina;
                        worksheet.Cells[x, 4].Value = rec.CellStreamBusiness;
                        worksheet.Cells[x, 5].Value = rec.CellHsh;
                        worksheet.Cells[x, 6].Value = rec.CellFungsiPic;
                        worksheet.Cells[x, 7].Value = rec.CellJudulInisiasi;
                        worksheet.Cells[x, 8].Value = rec.CellInterest;
                        worksheet.Cells[x, 9].Value = rec.CellInitiativeStatus;
                        worksheet.Cells[x, 10].Value = rec.CellInitiativeType;
                        worksheet.Cells[x, 11].Value = rec.CellInitiativeStage;
                        worksheet.Cells[x, 12].Value = rec.CellKawasanMitra;
                        worksheet.Cells[x, 13].Value = rec.CellNegaraMitra;
                        worksheet.Cells[x, 14].Value = rec.CellLokasiProyek;
                        worksheet.Cells[x, 15].Value = rec.CellNilaiProyek;
                        worksheet.Cells[x, 16].Value = rec.CellTargetProyek;
                        worksheet.Cells[x, 17].Value = rec.CellScope;
                        worksheet.Cells[x, 18].Value = rec.CellProgress;
                        worksheet.Cells[x, 19].Value = rec.CellValue;
                        worksheet.Cells[x, 20].Value = rec.CellIsuKendala;
                        worksheet.Cells[x, 21].Value = rec.CellTindakLanjut;
                        worksheet.Cells[x, 22].Value = rec.CellSupportPemerintah;
                        worksheet.Cells[x, 23].Value = rec.CellReferral;
                        worksheet.Cells[x, 24].Value = rec.CellPotensiEkskalasi;
                        if (rec.CellKodeAgreement != null && rec.CellRelatedAgreement != null)
                        {
                            worksheet.Cells[x, 25].Value = rec.CellKodeAgreement + "_" + rec.CellRelatedAgreement;

                        }
                        else
                        {
                            worksheet.Cells[x, 25].Value = null;
                        }
                        worksheet.Cells[x, 26].Value = rec.CellCatatanTambahan;
                        x++;
                        y++;
                    }
                }
            }

            // Set the autofilter for the data range
            if (withData)
            {
                // Get the range of data (including headers)
                var dataRange = worksheet.Cells[2, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];

                // Set autofilter on the data range
                dataRange.AutoFilter = false;
            }

            return package;
        }
        public InitiativeViewModel Delete(Guid guid, string userName)
        {
            InitiativeViewModel model = new InitiativeViewModel();
            model.Id = guid;
            try
            {
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
        public InitiativeViewModel GetReadInitiativeById(Guid guid)
        {
            InitiativeViewModel model = new InitiativeViewModel();

            try
            {
                if (guid != Guid.Empty)
                {
                    model = _repository.GetInitiativeById(guid);
                    model.ReadJudulInitiative = model.JudulInisiasi;
                    model.ReadInterest = _repository.GetReadInitiativeInterestById(guid);
                    model.ReadInitiativeStatus = _repository.GetReadInitiativeStatusById(guid);
                    model.ReadInitiativeType = _repository.GetReadInitiativeTypesById(guid);
                    model.ReadInitiativeStage = _repository.GetReadInitiativeStageById(guid);
                    model.ReadPicFungsi = _repository.GetReadPicFungsiById(guid);
                    model.ReadEntitasPertamina = _repository.GetReadEntitasPertamina(guid);
                    model.ReadPartner = _repository.GetReadPartners(guid);
                    model.ReadNegaraMitra = _repository.GetNegaraMitraById(guid);
                    model.ReadLokasiProyek = _repository.GetReadLokasiProyekById(guid);
                    model.ReadNilaiProyek = model.NilaiProyek;
                    model.ReadTargetWaktuProyek = model.TargetWaktuProyek;
                    model.ReadStreamBusiness = _repository.GetReadStreamBusinessById(guid);
                    model.ReadProgress = model.Progress;
                    model.ReadIsuKendala = model.IsuKendala;
                    model.ReadTindakLanjut = model.TindakLanjut;
                    model.ReadSupportPemerintah = model.SupportPemerintah;
                    model.ReadValueForIndonesia = model.ValueForIndonesia;
                    model.ReadRelatedAgreement = _repository.GetReadRelatedAgreementById(guid);
                    model.ReadReferal = model.Referal;
                    model.ReadPotensiEskalasi = model.PotensiEskalasi;
                    model.ReadCatatanTambahan = model.CatatanTambahan;
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
        public async Task<OpportunityViewModel> GetOpportunityById(Guid guid)
        {
            OpportunityViewModel model = new OpportunityViewModel();

            try
            {
                if (guid != Guid.Empty)
                {
                    model = await _opportunityRepository.GetOpportunityByIdAsync(guid);
                    model.OpEntitasPertamina = await _opportunityRepository.GetOpportunityEntitasByIdAsync(guid);
                    model.OpStreamBusiness = await _opportunityRepository.GetOpportunityStreamBusinessByIdAsync(guid);
                    model.OpPicFungsi = await _opportunityRepository.GetOpportunityPicFungsiByIdAsync(guid);
                    model.OpKesiapanProyek = await _opportunityRepository.GetOpportunityKesiapanProyekByIdAsync(guid);
                    model.OpNegaraMitra = await _opportunityRepository.GetOpportunityNegaraMitraByIdAsync(guid);
                    model.OpTargetMitra = await _opportunityRepository.GetOpportunityTargetMitraByIdAsync(guid);
                    model.OpPartners = await _opportunityRepository.GetOpportunityPartnerByIdAsync(guid);
                    model.OpLokasiProyeks = await _opportunityRepository.GetOpportunityLokasiByIdAsync(guid);
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
    }
}