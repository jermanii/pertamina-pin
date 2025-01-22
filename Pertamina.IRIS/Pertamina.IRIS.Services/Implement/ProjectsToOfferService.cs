using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
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
    public class ProjectsToOfferService : IProjectsToOfferService
    {
        private readonly IProjectsToOfferRepository _repository;

        public ProjectsToOfferService(IProjectsToOfferRepository repository)
        {
            _repository = repository;
        }

        public OpportunityViewModel Save(OpportunityViewModel model, string userName, string companyCode)
        {
            try
            {
                model.PICLevelLeadId = _repository.PicLevelLead();
                model.PICLevelMemberId = _repository.PicLevelMember();
                model.Id= Guid.NewGuid();
                DateTime now = DateTime.Now;
                model.IsDraft = false;
                model.CreateBy = userName;
                model.CompanyCode = companyCode;

                if (!string.IsNullOrEmpty(model.CapexToString))
                {
                    if (decimal.TryParse(model.CapexToString, NumberStyles.Number, new CultureInfo("id-ID"), out decimal parsedDecimal))
                    {
                        model.Capex = parsedDecimal;
                    }
                    else
                    {
                        model.Capex = null;
                    }
                }

                if (!string.IsNullOrEmpty(model.PotentialRevenuePerYearToString))
                {
                    if (decimal.TryParse(model.PotentialRevenuePerYearToString, NumberStyles.Number, new CultureInfo("id-ID"), out decimal parsedRevenue))
                    {
                        model.PotentialRevenuePerYear = parsedRevenue;
                    }
                    else
                    {
                        model.PotentialRevenuePerYear = null;
                    }
                }

                if (model.OpPicFungsis == null)
                {
                    model.IsError = false;
                    return model;
                }

                var result = _repository.SaveAllData(model);

            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        public OpportunityViewModel GetOpportunityById(Guid guid)
        {

            OpportunityViewModel model = new OpportunityViewModel();

            try
            {
                if (guid != Guid.Empty)
                {
                    model = _repository.GetOpportunityById(guid);

                    if (model.PotentialRevenuePerYear != null || model.Capex  != null)
                    {
                        model.PotentialRevenuePerYearToString = model.PotentialRevenuePerYear.HasValue ? model.PotentialRevenuePerYear.Value.ToString("#,##0", new System.Globalization.CultureInfo("id-ID")) : "N/A";
                        model.CapexToString = model.Capex.HasValue ? model.Capex.Value.ToString("#,##0", new System.Globalization.CultureInfo("id-ID")) : "N/A";
                        if (decimal.TryParse(model.NilaiProyek, out decimal ParsedValue))
                        {
                            model.NilaiProyek = ParsedValue.ToString("#,##0", new System.Globalization.CultureInfo("id-ID"));
                        }
                    }

                    model.PICLevelLeadId = _repository.PicLevelLead();
                    model.OpEntitasPertamina = _repository.GetOpportunityEntitasById(guid);
                    model.OpStreamBusiness = _repository.GetOpportunityStreamBusinessById(guid);
                    model.OpPicFungsi = _repository.GetOpportunityPicFungsiById(guid, model.PICLevelLeadId);
                    model.OpKesiapanProyek = _repository.GetOpportunityKesiapanProyekById(guid);
                    model.OpNegaraMitra = _repository.GetOpportunityNegaraMitraById(guid);
                    model.OpTargetMitra = _repository.GetOpportunityTargetMitraById(guid);
                    model.OpPartners = _repository.GetOpportunityPartnerById(guid);
                    model.OpLokasiProyeks = _repository.GetOpportunityLokasiById(guid);
                    model.PICLevelMemberId = _repository.PicLevelMember();
                    model.PICLevelLeadId = _repository.PicLevelLead();
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

        public async Task<OpportunityViewModel> GetOpportunityByIdAsync(Guid guid)
        {
            OpportunityViewModel model = new OpportunityViewModel();

            try
            {
                if (guid != Guid.Empty)
                {
                   
                    model = await _repository.GetOpportunityByIdAsync(guid);
                    model.PICLevelMemberId = _repository.PicLevelMember();
                    model.PICLevelLeadId = _repository.PicLevelLead();
                    model.OpEntitasPertamina = await _repository.GetOpportunityEntitasByIdAsync(guid);
                    model.OpStreamBusiness = await _repository.GetOpportunityStreamBusinessByIdAsync(guid);
                    model.OpPicFungsi = await _repository.GetOpportunityPicFungsiLeadByIdAsync(guid, model.PICLevelLeadId);
                    model.OpPicFungsis =  _repository.GetOpportunityPicFungsiMemberByIdAsync(guid, model.PICLevelMemberId);
                    model.OpKesiapanProyek = await _repository.GetOpportunityKesiapanProyekByIdAsync(guid);
                    model.OpNegaraMitra = await _repository.GetOpportunityNegaraMitraByIdAsync(guid);
                    model.OpTargetMitra = await _repository.GetOpportunityTargetMitraByIdAsync(guid);
                    model.OpPartners = await _repository.GetOpportunityPartnerByIdAsync(guid);
                    model.OpLokasiProyeks = await _repository.GetOpportunityLokasiByIdAsync(guid);
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

        public OpportunityViewModel Delete(Guid guid, string userName)
        {
            OpportunityViewModel model = new OpportunityViewModel();
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

                    _repository.DeleteStreamBusiness(guid, userName);

                    _repository.DeletePicFungsi(guid, userName);

                    _repository.DeleteKesiapanProyek(guid, userName);

                    _repository.DeleteNegaraMitra(guid, userName);

                    _repository.DeleteTargetMitra(guid, userName);

                    _repository.DeletePartner(guid, userName);

                    _repository.DeleteLokasiProyek(guid, userName);

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

        public OpportunityViewModel SaveDraft(OpportunityViewModel model, string userName, string companyCode)
        {
            try
            {
                model.PICLevelLeadId = _repository.PicLevelLead();
                model.PICLevelMemberId = _repository.PicLevelMember();
                model.Id= Guid.NewGuid();
                model.IsDraft = true;
                DateTime now = DateTime.Now;
                model.CreateBy = userName;
                model.CompanyCode = companyCode;

                if (!string.IsNullOrEmpty(model.CapexToString))
                {
                    if (decimal.TryParse(model.CapexToString, NumberStyles.Number, new CultureInfo("id-ID"), out decimal parsedDecimal))
                    {
                        model.Capex = parsedDecimal;
                    }
                    else
                    {
                        model.Capex = null;
                    }
                }

                if (!string.IsNullOrEmpty(model.PotentialRevenuePerYearToString))
                {
                    if (decimal.TryParse(model.PotentialRevenuePerYearToString, NumberStyles.Number, new CultureInfo("id-ID"), out decimal parsedRevenue))
                    {
                        model.PotentialRevenuePerYear = parsedRevenue;
                    }
                    else
                    {
                        model.PotentialRevenuePerYear = null;
                    }
                }

                if (model.OpPicFungsis == null)
                {
                    model.IsError = false;
                    return model;
                }

                var result = _repository.SaveAllData(model);

            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
      
        }
        public OpportunityViewModel IsExistingValidation(OpportunityViewModel model)
        {
            var result = new OpportunityViewModel();
            try
            {
                //Partners
                if (model.OpPartners.Count == 0 ||model.OpPartners == null)
                {
                    result.IsError = true;
                    result.ErrorMessage += "Minimum 1 data required for Partners";
                    return result;
                }
                //PIC
                if (model.OpPicFungsis != null || model.OpPicFungsis.Count < 0)
               
                {
                    List<Guid?> checkGuid = new List<Guid?>();
                    foreach (var picMember in model.OpPicFungsis)
                    {

                        //if (picMember.PicFungsiId == null)
                        //{
                        //    result.IsError = true;
                        //    result.ErrorMessage += "Minimum 1 data required for Pic";
                        //    return result;
                        //}
                        if (picMember.PicFungsiId == model.PicFungsiId[0])
                        {
                            result.IsError = true;
                            result.ErrorMessage += "The name PIC fungsi can't same";
                            return result;
                        }


                        if (checkGuid.Contains(picMember.PicFungsiId))
                        {
                            result.IsError = true;
                            result.ErrorMessage += "The name PIC fungsi can't same";
                            return result;
                        }
                        checkGuid.Add(picMember.PicFungsiId);

                        if (picMember.PicFungsiId == model.PicFungsiId[0])
                        {
                            result.IsError = true;
                            result.ErrorMessage += "The name PIC fungsi can't same";
                            return result;
                        }

                    }
                }
                if (model.PicFungsiId == null)
                {
                    result.IsError = true;
                    result.ErrorMessage += "Minimum 1 data required for Pic Lead";
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.IsError = true;
                return result;
            }

        }

        public OpportunityViewModel Update(OpportunityViewModel model, string userName)
        {
            try
            {
                model.PICLevelLeadId = _repository.PicLevelLead();
                model.PICLevelMemberId = _repository.PicLevelMember();
                model.CreateBy = userName;
                model.UpdateBy = userName;
                //OpportunityStreamBusinessViewModel recordStreamBusiness = _repository.GetOpportunityStreamBusinessById(model.Id);

                    //_repository.Update(model, userName);
                if (!string.IsNullOrEmpty(model.CapexToString))
                {
                    if (decimal.TryParse(model.CapexToString, NumberStyles.Number, new CultureInfo("id-ID"), out decimal parsedDecimal))
                    {
                        model.Capex = parsedDecimal;
                    }
                    else
                    {
                        model.Capex = null;
                    }
                }

                if (!string.IsNullOrEmpty(model.PotentialRevenuePerYearToString))
                {
                    if (decimal.TryParse(model.PotentialRevenuePerYearToString, NumberStyles.Number, new CultureInfo("id-ID"), out decimal parsedRevenue))
                    {
                        model.PotentialRevenuePerYear = parsedRevenue;
                    }
                    else
                    {
                        model.PotentialRevenuePerYear = null;
                    }
                }

                if (model.OpPicFungsis == null)
                {
                    model.IsError = false;
                    return model;
                }

                var result = _repository.UpdateAllData(model);

            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        protected OpportunityViewModel ValidationForm(OpportunityViewModel model)
        {
            if (model.NamaProyek == null)
            {
                model.IsError = true;
                model.ErrorMessage = "Nama Proyek wajib diisi sebelum menyimpan draft!";
            }

            return model;
        }

        public ExcelPackage ExportXLS(bool withData, string opportunityHolder, string negaraMitra, string streamBussiness, string entitasPertamina, string createDate, string searchQuery, bool isDraft)
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "Projects to Offer Download";
            package.Workbook.Properties.Author = "Pertamina";
            package.Workbook.Properties.Subject = "Data Export";
            package.Workbook.Properties.Keywords = "PIN - Pertamina";

            var worksheet = package.Workbook.Worksheets.Add("Projects to Offer");

            // First: add the header
            worksheet.Cells[1, 1].Value = "NO.";
            worksheet.Cells[1, 2].Value = "PROJECT NAME";
            worksheet.Cells[1, 3].Value = "DElIVERABLE";
            worksheet.Cells[1, 4].Value = "STREAM BUSSINESS";
            worksheet.Cells[1, 5].Value = "EXISTING PARTNER(S)";
            worksheet.Cells[1, 6].Value = "POTENTIAL REVENUE";
            worksheet.Cells[1, 7].Value = "CAPEX";
            worksheet.Cells[1, 8].Value = "TIMELINE";
            worksheet.Cells[1, 9].Value = "TYPE OF PARTNER NEEDED";
            worksheet.Cells[1, 10].Value = "PROJECT AREA - PARTNER LOCATION (COUNTRY)";
            worksheet.Cells[1, 11].Value = "PROJECT LOCATION (PROVINCE)";
            worksheet.Cells[1, 12].Value = "HOLDING/SUB-HOLDING - ENTITAS PERTAMINA";
            worksheet.Cells[1, 13].Value = "PIC LEAD";
            worksheet.Cells[1, 14].Value = "PIC MEMBER";
            worksheet.Cells[1, 15].Value = "PROJECT PROFILE";
            worksheet.Cells[1, 16].Value = "PROJECT READINESS";
            worksheet.Cells[1, 17].Value = "PROGRESS";
            worksheet.Cells[1, 18].Value = "COLLABORATION TARGET";
            worksheet.Cells[1, 19].Value = "ADDITIONAL NOTE";
            worksheet.Cells[1, 20].Value = "FOLLOW UP";

            worksheet.Cells["A1:ZZ1"].Style.Font.Name = "Arial";
            worksheet.Cells["A1:ZZ1"].Style.Font.Size = 14;
            worksheet.Cells["A1:ZZ"].Style.Font.Size = 12;

            int dataRowCount = worksheet.Dimension.End.Row - 1;
            if (dataRowCount > 0)
            {
                var dataContent = worksheet.Cells["A2:ZZ" + worksheet.Dimension.End.Row];
                dataContent.Style.Font.Size = 12;
            }

            worksheet.Cells["A1:ZZ" + worksheet.Dimension.End.Row].AutoFitColumns();

            if (withData)
            {
                List<OpportunityViewModel> datas = _repository.GetAllOpportunityExcel().OrderBy(x => x.NamaProyek).ToList();

                if (!string.IsNullOrEmpty(opportunityHolder))
                {
                    string[] opportunityHolderArray = opportunityHolder.Split(',');

                    if (opportunityHolderArray.Length < 2)
                    {
                        datas = datas.Where(x => x.CellHshId != null && x.CellHshId.ToLower().Contains(opportunityHolderArray[0].ToLower())).ToList();
                    }
                    else
                    {
                        List<OpportunityViewModel> filteredData = new List<OpportunityViewModel>();

                        foreach (var item in opportunityHolderArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellHshId != null && x.CellHshId.Contains(item)));
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
                        List<OpportunityViewModel> filteredData = new List<OpportunityViewModel>();

                        foreach (var item in negaraMitraArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellNegaraMitraId != null && x.CellNegaraMitraId.Contains(item)));
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
                        List<OpportunityViewModel> filteredData = new List<OpportunityViewModel>();

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
                        List<OpportunityViewModel> filteredData = new List<OpportunityViewModel>();

                        foreach (var item in entitasArray)
                        {
                            filteredData.AddRange(datas.Where(x => x.CellEntitasPertaminaId != null && x.CellEntitasPertaminaId.Contains(item)));
                        }

                        datas = filteredData.ToList();
                    }
                }

                if (!string.IsNullOrEmpty(createDate))
                {
                    string[] createDateArray = createDate.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

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
                            return isCellCreateDateValid && cellCreateDate >= startDate && cellCreateDate <= endDate ;
                        }).ToList();

                    }
                }

                if (isDraft)
                {
                    datas = datas.Where(x => x.IsDraft == true).ToList();
                }
                else
                {
                    datas = datas.Where(x => x.IsDraft == false || x.IsDraft == null).ToList();
                }
                if (datas != null)
                {
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        datas = datas.Where(x =>
                            (x.CellNamaProyek != null && x.CellNamaProyek.ToLower().Contains(searchQuery.ToLower())) ||
                            (x.CellProjectProfile != null && x.CellProjectProfile.ToLower().Contains(searchQuery.ToLower())) ||
                            (x.CellDeliverables != null && x.CellDeliverables.ToLower().Contains(searchQuery.ToLower())) ||
                            (x.CellNilaiProyek != null && x.CellNilaiProyek.ToLower().Contains(searchQuery.ToLower())) ||
                            (x.CellTimeline != null && x.CellTimeline.ToLower().Contains(searchQuery.ToLower())) ||
                            (x.CellProgress != null && x.CellProgress.ToLower().Contains(searchQuery.ToLower())) ||
                            (x.CellCatatanTambahan != null && x.CellCatatanTambahan.ToLower().Contains(searchQuery.ToLower()))
                         ).ToList();

                    }
                }

                int x = 3;
                int y = 1;
                if (datas != null)
                {
                    foreach (var rec in datas)
                    {
                        worksheet.Cells[x, 1].Value = y;
                        worksheet.Cells[x, 2].Value = rec.CellNamaProyek;
                        worksheet.Cells[x, 3].Value = rec.CellDeliverables;
                        worksheet.Cells[x, 4].Value = rec.CellStreamBusiness;
                        worksheet.Cells[x, 5].Value = rec.CellExistingPartner;
                        worksheet.Cells[x, 6].Value = rec.CellPotentialRevenue;
                        worksheet.Cells[x, 7].Value = rec.CellCapex;
                        worksheet.Cells[x, 8].Value = rec.CellTimeline;
                        worksheet.Cells[x, 9].Value = rec.CellTypeOfPartnerNeeded;
                        worksheet.Cells[x, 10].Value = rec.CellLokasiProyekNegara;
                        worksheet.Cells[x, 11].Value = rec.CellLokasiProyekProvinsi;
                        worksheet.Cells[x, 12].Value = rec.CellEntitasPertamina;
                        worksheet.Cells[x, 13].Value = rec.CellFungsiPic;
                        worksheet.Cells[x, 14].Value = rec.CellFungsiPicMember;
                        worksheet.Cells[x, 15].Value = rec.CellProjectProfile;
                        worksheet.Cells[x, 16].Value = rec.CellKesiapanProyek;
                        worksheet.Cells[x, 17].Value = rec.CellProgress;
                        worksheet.Cells[x, 18].Value = rec.CellTargetMitra;
                        worksheet.Cells[x, 19].Value = rec.CellCatatanTambahan;
                        worksheet.Cells[x, 20].Value = rec.CellTindakLanjut;

                        x++;
                        y++;
                    }
                }
                worksheet.Cells[1, 1, datas.Count + 1, 20].AutoFilter = true;
            }
            return package;
        }

        public OpportunityViewModel GetLevelPic()
        {
            var model = new OpportunityViewModel();
            model.PICLevelLeadId = _repository.PicLevelLead();
            model.PICLevelMemberId = _repository.PicLevelMember();
            return model;
        }

        #region Grid
        public async Task<PaginationBaseModel<OpportunityViewModel>> GetListPaging(RequestFormDtBaseModel request, OpportunityViewModel decodeModel)
        {
            var resultData = await _repository.GetList(request, decodeModel);

            var result = new PaginationBaseModel<OpportunityViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }
        public OpportunityViewModel GetOpportunityCount()
        {
            OpportunityViewModel model = new OpportunityViewModel();
            try
            {
                List<HshViewModel> allRecordHsh = _repository.GetAllDataHsh();
                List<HshViewModel> setRecordHsh = new List<HshViewModel>();
                foreach (var rec in allRecordHsh)
                {
                    rec.Count = _repository.GetCountHsh(rec.RelationSequence.Value);
                    setRecordHsh.Add(rec);
                }
                model.CountOpportunityHolder = setRecordHsh;
                model.CountOpportunity = _repository.GetCountRecordsOpportunity();
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
        public OpportunityViewModel GenerelizeDataOpportunity(OpportunityViewModel rec)
        {
            List<OpportunityPartnerViewModel> partners = new List<OpportunityPartnerViewModel>();
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

            List<OpportunityEntitasPertaminaViewModel> entitasPertaminas = new List<OpportunityEntitasPertaminaViewModel>();
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

            List<OpportunityStreamBusinessViewModel> streamBusinesses = new List<OpportunityStreamBusinessViewModel>();
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

            List<OpportunityNegaraMitraViewModel> negaraMitras = new List<OpportunityNegaraMitraViewModel>();
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

            List<OpportunityKesiapanProyekViewModel> kesiapanProyeks = new List<OpportunityKesiapanProyekViewModel>();
            kesiapanProyeks = _repository.GetKesiapanProyek(rec.Id);
            string strKesiapanProyeks = string.Empty;
            foreach (var kesiapanProyek in kesiapanProyeks)
            {
                if (kesiapanProyeks.Count() > 1)
                {
                    strKesiapanProyeks += kesiapanProyek.QueryKesiapanProyekName;
                    if (kesiapanProyek == kesiapanProyeks.LastOrDefault())
                    {
                    }
                    else
                    {
                        strKesiapanProyeks += ", ";
                    }
                }
                else
                {
                    strKesiapanProyeks += kesiapanProyek.QueryKesiapanProyekName;
                }
            }

            rec.RowStatus = _repository.GetStatusDraft(rec.Id);
            rec.RowNamaProyek = _repository.GetNamaProyek(rec.Id) ?? "N/A";
            rec.RowPartner = strPartner == "" ? "N/A": strPartner;
            rec.RowEntitasPertamina = strEntitasPertamina == "" ? "N/A" : strEntitasPertamina;
            rec.RowStreamBusiness = strStreamBusiness == "" ? "N/A" : strStreamBusiness;
            rec.RowNegaraMitra = strNegaraMitra == "" ? "N/A" : strNegaraMitra;
            rec.RowKawasanMitra = strKawasanMitra == "" ? "N/A" : strKawasanMitra;
            rec.RowKesiapanProyek = strKesiapanProyeks == "" ? "N/A" : strKesiapanProyeks;

            return rec;
        }
        public async Task<OpportunityViewModel> GetOpportunityCountWithFilter(OpportunityViewModel model)
        {
            try
            {
                IQueryable<OpportunityViewModel> query = await _repository.QueryOpportunityWithFilter(model);

                List<HshViewModel> allRecordHsh = await _repository.GetRecordsHsh();
                List<HshViewModel> setRecordHsh = new List<HshViewModel>();
                foreach (var rec in allRecordHsh)
                {
                    rec.Count = query.Where(x => x.QueryHshId == rec.Id).GroupBy(x => x.Id).Count();
                    setRecordHsh.Add(rec);
                }

                model.CountOpportunityHolder = setRecordHsh;
                model.CountOpportunity = query.GroupBy(x => x.Id).Count();
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

        public async Task<OpportunityViewModel> GetOpportunityCountWithFilter(OpportunityViewModel model, string strSearch)
        {
            try
            {
                IQueryable<OpportunityViewModel> query = await _repository.QueryOpportunityWithFilter(model);

                List<HshViewModel> allRecordHsh = await _repository.GetRecordsHsh();
                List<HshViewModel> setRecordHsh = new List<HshViewModel>();
                foreach (var rec in allRecordHsh)
                {
                    rec.Count = query.Where(x => x.QueryHshId == rec.Id && (x.QueryNamaProyek.Contains(strSearch)||
                                                                           x.Deliverables.Contains(strSearch)||
                                                                           x.NilaiProyek.Contains(strSearch)||
                                                                           x.Timeline.Contains(strSearch)||
                                                                           x.ProjectProfile.Contains(strSearch)||
                                                                           x.Progress.Contains(strSearch)||
                                                                           x.CatatanTambahan.Contains(strSearch))).GroupBy(x => x.Id).Count();
                    setRecordHsh.Add(rec);
                }

                model.CountOpportunityHolder = setRecordHsh;
                model.CountOpportunity = query.Where(x => x.QueryNamaProyek.Contains(strSearch) ||
                                                          x.Deliverables.Contains(strSearch) ||
                                                        x.NilaiProyek.Contains(strSearch) ||
                                                        x.Timeline.Contains(strSearch) ||
                                                        x.ProjectProfile.Contains(strSearch) ||
                                                        x.Progress.Contains(strSearch) ||
                                                        x.CatatanTambahan.Contains(strSearch)).GroupBy(x => x.Id).Count();
                model.CountNegaraMitra = _repository.GetRecordsNegaraMitraWithSearch(model, model.StrSearch).Result.Count();
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
        #endregion

        #region Read
        public OpportunityViewModel GetReadOpportunityById(Guid guid)
        {
            OpportunityViewModel model = new OpportunityViewModel();

            try
            {
                if (guid != Guid.Empty)
                {
                    model = _repository.GetOpportunityById(guid);

                    if (model.PotentialRevenuePerYear != null || model.Capex   != null || model.NilaiProyek != null)
                    {
                        model.PotentialRevenuePerYearToString = model.PotentialRevenuePerYear.HasValue ? model.PotentialRevenuePerYear.Value.ToString("#,##0", new System.Globalization.CultureInfo("id-ID")) : "N/A";
                        model.CapexToString = model.Capex.HasValue ? model.Capex.Value.ToString("#,##0", new System.Globalization.CultureInfo("id-ID")) : "N/A";
                        if (decimal.TryParse(model.NilaiProyek, out decimal ParsedValue))
                        {
                            model.NilaiProyek = ParsedValue.ToString("#,##0", new System.Globalization.CultureInfo("id-ID"));
                        }
                    }
                    model.ReadNamaProyek = model.NamaProyek;
                    model.ReadPartner = _repository.GetReadPartners(guid);
                    model.ReadPicFungsi = _repository.GetReadPicFungsiById(guid);
                    model.ReadPicFungsiLead = _repository.GetReadPicFungsiByIdLead(guid);
                    model.ReadEntitasPertamina = _repository.GetReadEntitasPertamina(guid);
                    model.ReadNegaraMitra = _repository.GetNegaraMitraById(guid);
                    model.ReadLokasiProyekProvinsi = _repository.GetLokasiProyekProvinsi(guid);
                    model.ReadNilaiProyek = model.NilaiProyek;
                    model.ReadTimeline = model.Timeline;
                    model.ReadStreamBusiness = _repository.GetReadStreamBusinessById(guid);
                    model.ReadProjectProfile = model.ProjectProfile;
                    model.ReadDeliverables = model.Deliverables;
                    model.ReadProgress = model.Progress;
                    model.ReadKesiapanProyek = _repository.GetReadKesiapanProyek(guid);
                    model.ReadTargetMitra = _repository.GetReadTargetMitra(guid);
                    model.ReadCatatanTambahan = model.CatatanTambahan;
                    model.ReadTindakLanjut = model.TindakLanjut;
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
        #endregion
    }
}