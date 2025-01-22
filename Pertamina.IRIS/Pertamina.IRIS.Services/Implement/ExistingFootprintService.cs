using OfficeOpenXml;
using OfficeOpenXml.ConditionalFormatting;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Implement;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Implement
{
    public class ExistingFootprintService : IExistingFootprintService
    {
        private readonly IExistingFootprintRepository _repository;

        public ExistingFootprintService(IExistingFootprintRepository repository)
        {
            _repository = repository;
        }
        public async Task<PaginationBaseModel<ExistingFootprintViewModel>> GetListPaging(RequestFormDtBaseModel request, ExistingFootprintViewModel decodeModel)
        {
            var resultData = await _repository.GetList(request, decodeModel);
            if (resultData.Data != null)
            {
                var result = new PaginationBaseModel<ExistingFootprintViewModel>
                {
                    Data = resultData.Data,
                    RecordsFiltered = resultData.PageInfo.TotalRecord,
                    RecordsTotal = resultData.PageInfo.TotalRecord
                };
                return result;
            }
            return new PaginationBaseModel<ExistingFootprintViewModel>();
        }
        public async Task<PaginationBaseModel<ExistingFootprintsOperatingMetricViewModel>> GetListPagingOpenMetricById(RequestFormDtBaseModel request, Guid guid)
        {
            var resultData = await _repository.GetListOpenMetricById(request, guid);
            var result = new PaginationBaseModel<ExistingFootprintsOperatingMetricViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }
        public async Task<ExistingFootprintViewModel> GetHubHeadByHubId(Guid hubId)
        {
            var result = await _repository.GetHubHeadByHubId(hubId);
            return result;
        }
        public async Task<ExistingFootprintViewModel> GetExistingFootprintsById(Guid guid, Guid levelId)
        {
            var result = await _repository.GetExistingFootprintById(guid, levelId);
            if (result != null)
            {
                if (result.OperatingMetricss != null)
                {
                    foreach (var metric in result.OperatingMetricss)
                    {
                        metric.RevenueToString =metric.Revenue.HasValue ? metric.Revenue.Value.ToString("#,##0", new System.Globalization.CultureInfo("id-ID")) : "N/A";
                        metric.TotalAssetToString = metric.TotalAsset.HasValue ? metric.TotalAsset.Value.ToString("#,##0", new System.Globalization.CultureInfo("id-ID")) : "N/A";
                        metric.EbitdaToString = metric.Ebitda.HasValue ? metric.Ebitda.Value.ToString("#,##0", new System.Globalization.CultureInfo("id-ID")) : "N/A";
                    }
                }
            }

            return result;
        }
        public Select2BaseModel GetYearOperatingMetricById(Guid guid, int year)
        {
            var result = _repository.GetDdlYear(guid, year).FirstOrDefault();

            return result;
        }
        public ExistingFootprintViewModel SubmitOperatingMetric(ExistingFootprintViewModel model, string username, ExistingFootprintViewModel getEFModel)
        {

            try
            {
                if (model.HubId == Guid.Empty)
                {
                    model.HubId = _repository.GetHubId();
                    model.UpdatedDate = DateTime.Now;
                }
                List<ExistingFootprintsPICViewModel> PicFungsis = model.PicFungsis;
                List<ExistingFootprintsPartnersViewModel> partners = new List<ExistingFootprintsPartnersViewModel>();
                ExistingFootprintsHubHeadViewModel hd = new ExistingFootprintsHubHeadViewModel();
                ExistingFootprintsPICViewModel pic = new ExistingFootprintsPICViewModel();
                List<ExistingFootprintsPICViewModel> pics = new List<ExistingFootprintsPICViewModel>();
                ExistingFootprintsOperatingMetricViewModel om = new ExistingFootprintsOperatingMetricViewModel();
                if (getEFModel != null)
                {
                    if (getEFModel.CreatedBy != null)
                    {
                        om.CreatedBy = username;
                    }
                }
                om.Id = Guid.NewGuid();
                om.TotalAsset= model.OperatingMetrics.TotalAsset;
                om.Ebitda =model.OperatingMetrics.Ebitda;
                om.Revenue =model.OperatingMetrics.Revenue;
                om.ExistingFootprintsId =model.Id;
                om.Year =model.OperatingMetrics.Year;

                if (model.Partnerss != null)
                {
                    if (model.Partnerss.Count !=0)
                    {
                        foreach (var partner in model.Partnerss)
                        {
                            ExistingFootprintsPartnersViewModel ptr = new ExistingFootprintsPartnersViewModel();
                            ptr.Id = Guid.NewGuid();
                            ptr.ExistingFootPrintsId = model.Id;
                            ptr.Location = partner.Location;
                            ptr.Partners = partner.Partners;
                            partners.Add(ptr);
                        }
                    }
                }

                if (model.HubHeadId != Guid.Empty)
                {
                    hd.Id = Guid.NewGuid();
                    hd.ExistingFootprintsId = model.Id;
                    hd.HubHeadId = model.HubHeadId;
                }

                if (model.PicFungsiId != null)
                {
                    if (model.PicLevelId == null)
                    {
                        model.PicLevelId = _repository.GetPicLevelLeadId();
                    }
                    pic.Id = Guid.NewGuid();
                    pic.ExistingFootprintsId = model.Id;
                    pic.PicFungsiId= model.PicFungsiId;
                    pic.PicLevelId =model.PicLevelId;
                }

                if (PicFungsis != null)
                {
                    if (PicFungsis.Count != 0)
                    {
                        foreach (var picFungsi in model.PicFungsis)
                        {
                            if (picFungsi.PicLevelId == null)
                            {
                                picFungsi.PicLevelId=_repository.GetPicLevelMemberId();
                            }
                            if (picFungsi.PicFungsiId != null)
                            {
                                ExistingFootprintsPICViewModel picNew = new ExistingFootprintsPICViewModel();
                                picNew.Id = Guid.NewGuid();
                                picNew.ExistingFootprintsId = model.Id;
                                picNew.PicLevelId = picFungsi.PicLevelId;
                                picNew.PicFungsiId = picFungsi.PicFungsiId;
                                pics.Add(picNew);
                            }

                        }
                    }
                }


                bool result = _repository.SavePartial(model, partners, hd, pic, pics, om);
                if (result)
                {
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
        }
        public ExistingFootprintViewModel UpdateOperatingMetric(ExistingFootprintViewModel model, string username, ExistingFootprintViewModel getEFModel)
        {

            try
            {
                if (model.HubId == Guid.Empty)
                {
                    model.HubId = _repository.GetHubId();
                    model.UpdatedDate = DateTime.Now;
                }
                List<ExistingFootprintsPICViewModel> PicFungsis = model.PicFungsis;
                List<ExistingFootprintsPartnersViewModel> partners = new List<ExistingFootprintsPartnersViewModel>();
                ExistingFootprintsHubHeadViewModel hd = new ExistingFootprintsHubHeadViewModel();
                ExistingFootprintsPICViewModel pic = new ExistingFootprintsPICViewModel();
                List<ExistingFootprintsPICViewModel> pics = new List<ExistingFootprintsPICViewModel>();
                ExistingFootprintsOperatingMetricViewModel om = new ExistingFootprintsOperatingMetricViewModel();
                if (getEFModel.CreatedDate != null)
                {
                    om.CreatedBy = username;
                }
                om = _repository.GetExistingFootprintOperatingMetric(model.OperatingMetrics.Id);
                om.TotalAsset= model.OperatingMetrics.TotalAsset;
                om.Ebitda =model.OperatingMetrics.Ebitda;
                om.Revenue =model.OperatingMetrics.Revenue;
                om.ExistingFootprintsId =model.Id;
                om.Year =model.OperatingMetrics.Year;
                if (model.Partner != null)
                {
                    if (model.Partnerss.Count !=0)
                    {
                        foreach (var partner in model.Partnerss)
                        {
                            ExistingFootprintsPartnersViewModel ptr = new ExistingFootprintsPartnersViewModel();
                            ptr.Id = Guid.NewGuid();
                            ptr.ExistingFootPrintsId = model.Id;
                            ptr.Location = partner.Location;
                            ptr.Partners = partner.Partners;
                            partners.Add(ptr);
                        }
                    }
                }

                if (model.HubHeadId != Guid.Empty)
                {
                    hd.Id = Guid.NewGuid();
                    hd.ExistingFootprintsId = model.Id;
                    hd.HubHeadId = model.HubHeadId;
                    //hubHeads.Add(hd);
                }
                if (model.PicFungsiId != null)
                {
                    if (model.PicLevelId == null)
                    {
                        model.PicLevelId = _repository.GetPicLevelLeadId();
                    }
                    pic.Id = Guid.NewGuid();
                    pic.ExistingFootprintsId = model.Id;
                    pic.PicFungsiId= model.PicFungsiId;
                    pic.PicLevelId =model.PicLevelId;

                }
                if (PicFungsis != null)
                {
                    if (PicFungsis.Count != 0)
                    {
                        foreach (var picFungsi in model.PicFungsis)
                        {
                            if (picFungsi.PicLevelId == null)
                            {
                                picFungsi.PicLevelId = _repository.GetPicLevelMemberId();
                            }
                            if (picFungsi.PicFungsiId != null)
                            {
                                ExistingFootprintsPICViewModel picNew = new ExistingFootprintsPICViewModel();
                                picNew.Id = Guid.NewGuid();
                                picNew.ExistingFootprintsId = model.Id;
                                picNew.PicLevelId = picFungsi.PicLevelId;
                                picNew.PicFungsiId = picFungsi.PicFungsiId;
                                pics.Add(picNew);
                            }

                        }
                    }
                }

                bool result = _repository.UpdatePartial(model, partners, hd, pic, pics, om);
                if (result)
                {
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
        }
        public ExistingFootprintViewModel DeleteOperatingMetric(ExistingFootprintViewModel model)
        {
            try
            {
                if (model.HubId == Guid.Empty)
                {
                    model.HubId = _repository.GetHubId();
                    model.UpdatedDate = DateTime.Now;
                }
                List<ExistingFootprintsPICViewModel> PicFungsis = model.PicFungsis;
                List<ExistingFootprintsPartnersViewModel> partners = new List<ExistingFootprintsPartnersViewModel>();
                ExistingFootprintsHubHeadViewModel hd = new ExistingFootprintsHubHeadViewModel();
                ExistingFootprintsPICViewModel pic = new ExistingFootprintsPICViewModel();
                List<ExistingFootprintsPICViewModel> pics = new List<ExistingFootprintsPICViewModel>();
                ExistingFootprintsOperatingMetricViewModel om = new ExistingFootprintsOperatingMetricViewModel();


                om = _repository.GetExistingFootprintOperatingMetric(model.OperatingMetrics.Id);

                if (model.Partnerss != null)
                {
                    if (model.Partnerss.Count !=0)
                    {
                        foreach (var partner in model.Partnerss)
                        {
                            ExistingFootprintsPartnersViewModel ptr = new ExistingFootprintsPartnersViewModel();
                            ptr.Id = Guid.NewGuid();
                            ptr.ExistingFootPrintsId = model.Id;
                            ptr.Location = partner.Location;
                            ptr.Partners = partner.Partners;
                            partners.Add(ptr);
                        }
                    }
                }

                if (model.HubHeadId != Guid.Empty)
                {
                    hd.Id = Guid.NewGuid();
                    hd.ExistingFootprintsId = model.Id;
                    hd.HubHeadId = model.HubHeadId;
                    //hubHeads.Add(hd);
                }
                if (model.PicFungsiId != null)
                {
                    pic.Id = Guid.NewGuid();
                    pic.ExistingFootprintsId = model.Id;
                    pic.PicFungsiId= model.PicFungsiId;
                    pic.PicLevelId =model.PicLevelId;

                }
                if (PicFungsis!= null)
                {
                    if (PicFungsis.Count != 0)
                    {
                        foreach (var picFungsi in model.PicFungsis)
                        {
                            if (picFungsi.PicFungsiId != null)
                            {
                                ExistingFootprintsPICViewModel picNew = new ExistingFootprintsPICViewModel();
                                picNew.Id = Guid.NewGuid();
                                picNew.ExistingFootprintsId = model.Id;
                                picNew.PicLevelId = picFungsi.PicLevelId;
                                picNew.PicFungsiId = picFungsi.PicFungsiId;
                                pics.Add(picNew);
                            }

                        }
                    }
                }

                bool result = _repository.DeletePartial(model, partners, hd, pic, pics, om);
                if (result)
                {
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
        }
        public ExistingFootprintViewModel CreateExistingFootprint(Guid? guid)
        {
            var model = new ExistingFootprintViewModel();
            //if(guid == null)
            //{
            //    model.Id = Guid.NewGuid();
            //    model.HubId = _repository.GetHubId();
            //    model = _repository.CreateExistingFootprint(model);
            //}
            //else
            //{
            //    model.Id= guid.Value;
            //}
            model.PicLevelId =_repository.GetPicLevelLeadId();
            model.PicLevelMemberId =_repository.GetPicLevelMemberId();
            return model;
        }

        public decimal ParseRevenue(string revenueToParse)
        {
            string cleanedValue = revenueToParse.Replace(".", "");

            if (decimal.TryParse(cleanedValue, out decimal parsedRevenue))
            {
                return parsedRevenue;
            }

            throw new ArgumentException("Invalid revenue format");
        }
        public ExistingFootprintViewModel Submit(ExistingFootprintViewModel model, string username)
        {
            try
            {
                model.Id = Guid.NewGuid();
                model.CreatedBy=username;
                model.CreatedDate=DateTime.Now;
                //model.NegaraMitraId= _repository.GetMitraIdByHubId(model.HubId);
                //model.HubId = _repository.GetHubIdIdByHubId(model.HubId);

                //var getPartners = _repository.GetExistingFootprintPartners(model.Id);
                List<ExistingFootprintsPartnersViewModel> partners = new List<ExistingFootprintsPartnersViewModel>();
                foreach (var partner in model.Partnerss)
                {
                    //var existingPartner = getPartners.FirstOrDefault(p => p.Id == partner.Id || p.Partners == partner.Partners);
                    ExistingFootprintsPartnersViewModel ptr = new ExistingFootprintsPartnersViewModel();

                    ptr.Id = Guid.NewGuid();
                    ptr.CreatedDate = DateTime.Now;
                    ptr.CreatedBy = username;
                    ptr.ExistingFootPrintsId = model.Id;
                    ptr.Location = partner.Location;
                    ptr.Partners = partner.Partners;
                    partners.Add(ptr);
                }

                //var getHubHead = _repository.GetExistingFootprintHubHead(model.Id);

                ExistingFootprintsHubHeadViewModel hd = new ExistingFootprintsHubHeadViewModel();

                hd.Id = Guid.NewGuid();
                hd.CreatedDate = DateTime.Now;
                hd.CreatedBy = username;
                hd.ExistingFootprintsId = model.Id;
                hd.HubHeadId = model.HubHeadId;

                //var getPic = _repository.GetExistingFootprintPICLead(model.Id, model.PicLevelId);
                var picLevelLeadId = _repository.GetPicLevelLeadId();

                ExistingFootprintsPICViewModel pic = new ExistingFootprintsPICViewModel();

                pic.Id = Guid.NewGuid();
                pic.CreatedDate = DateTime.Now;
                pic.CreatedBy = username;
                pic.ExistingFootprintsId = model.Id;
                pic.PicFungsiId= model.PicFungsiId;
                pic.PicLevelId =picLevelLeadId;

                var getPicMember = _repository.GetExistingFootprintPICMember(model.Id, model.PicLevelMemberId);
                var picLevelMemberId = _repository.GetPicLevelMemberId();
                List<ExistingFootprintsPICViewModel> pics = new List<ExistingFootprintsPICViewModel>();
                foreach (var picMemberNew in model.PicFungsis)
                {
                    ExistingFootprintsPICViewModel picNew = new ExistingFootprintsPICViewModel();

                    picNew.Id = Guid.NewGuid();
                    picNew.CreatedDate = DateTime.Now;
                    picNew.CreatedBy = username;
                    picNew.ExistingFootprintsId = model.Id;
                    picNew.PicFungsiId = picMemberNew.PicFungsiId;
                    picNew.PicLevelId = picLevelMemberId;
                    pics.Add(picNew);
                }

                //var getOperatingMetrics = _repository.GetExistingFootprintOperatingMetrics(model.Id);
                List<ExistingFootprintsOperatingMetricViewModel> oms = new List<ExistingFootprintsOperatingMetricViewModel>();
                foreach (var operatingMetric in model.OperatingMetricss)
                {
                    operatingMetric.Revenue = ParseRevenue(operatingMetric.RevenueToString);
                    operatingMetric.TotalAsset = ParseRevenue(operatingMetric.TotalAssetToString);
                    operatingMetric.Ebitda = ParseRevenue(operatingMetric.EbitdaToString);
                    ExistingFootprintsOperatingMetricViewModel om = new ExistingFootprintsOperatingMetricViewModel();
                    om.Id = Guid.NewGuid();
                    om.CreatedDate = DateTime.Now;
                    om.CreatedBy = username;
                    om.ExistingFootprintsId = model.Id;
                    om.Year = operatingMetric.Year;
                    om.Revenue = operatingMetric.Revenue;
                    om.TotalAsset = operatingMetric.TotalAsset;
                    om.Ebitda = operatingMetric.Ebitda;
                    oms.Add(om);
                }
                bool result = _repository.Save(model, partners, hd, pic, pics, oms);
                if (result)
                {
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
        }

        public ExistingFootprintViewModel SaveDraft(ExistingFootprintViewModel model, string username)
        {
            try
            {
                model.CreatedBy=username;
                model.CreatedDate=DateTime.Now;
                model.Id=Guid.NewGuid();
                List<ExistingFootprintsPartnersViewModel> partners = new List<ExistingFootprintsPartnersViewModel>();
                List<ExistingFootprintsHubHeadViewModel> hubHeads = new List<ExistingFootprintsHubHeadViewModel>();
                List<ExistingFootprintsPICViewModel> picFungsis = new List<ExistingFootprintsPICViewModel>();
                if (model.Partnerss.Count > 0)
                {
                    foreach (var partner in model.Partnerss)
                    {
                        ExistingFootprintsPartnersViewModel ptr = new ExistingFootprintsPartnersViewModel();
                        ptr.Id = Guid.NewGuid();
                        ptr.CreatedDate = DateTime.Now;
                        ptr.CreatedBy = username;
                        ptr.ExistingFootPrintsId = model.Id;
                        ptr.Location = partner.Location;
                        ptr.Partners = partner.Partners;
                        partners.Add(ptr);
                    }
                }
                if (model.Heads.Count > 0)
                {
                    foreach (var head in model.Heads)
                    {
                        ExistingFootprintsHubHeadViewModel hd = new ExistingFootprintsHubHeadViewModel();
                        hd.Id = Guid.NewGuid();
                        hd.CreatedDate = DateTime.Now;
                        hd.CreatedBy = username;
                        hd.ExistingFootprintsId = model.Id;
                        hd.HubHeadId = model.HubHeadId;
                        hubHeads.Add(hd);
                    }
                }
                if (model.PicFungsis.Count > 0)
                {
                    foreach (var picFungsi in model.PicFungsis)
                    {
                        ExistingFootprintsPICViewModel pic = new ExistingFootprintsPICViewModel();
                        pic.Id = Guid.NewGuid();
                        pic.CreatedDate = DateTime.Now;
                        pic.CreatedBy = username;
                        pic.ExistingFootprintsId = model.Id;
                        pic.PicFungsiId= model.PicFungsiId;
                        pic.PicLevelId =model.PicFungsi.PicLevelId;
                        picFungsis.Add(pic);
                    }
                }
                bool result = _repository.SaveDraft(model, partners, hubHeads, picFungsis);
                if (result)
                {
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
        }
        #region crud
        public ExistingFootprintViewModel GetExistingFootprint(Guid guid)
        {
            var result = _repository.GetExistingFootprint(guid);
            return result;
        }
        public ExistingFootprintsOperatingMetricViewModel GetExistingFootprintOperatingMetric(Guid guid)
        {
            var result = _repository.GetExistingFootprintOperatingMetric(guid);
            result.TotalAssetToString =(result.TotalAsset).Value.ToString("0.##");
            result.EbitdaToString = (result.Ebitda).Value.ToString("0.##");
            result.RevenueToString = (result.Revenue).Value.ToString("0.##"); ;
            return result;
        }
        public ExistingFootprintViewModel IsExistingValidation(ExistingFootprintViewModel model)
        {
            var result = new ExistingFootprintViewModel();
            try
            {
                if (model.OperatingMetricss.Count> 0)
                {
                    foreach (var operatingMetric in model.OperatingMetricss)
                    {
                        if (operatingMetric.Year == null || operatingMetric.RevenueToString ==null || operatingMetric.TotalAssetToString == null|| operatingMetric.EbitdaToString == null)
                        {
                            result.IsError = true;
                            result.ErrorMessage += "Kindly complete the column for Operating Metrics";
                            return result;
                        }
                    }
                }
                else
                {
                    result.IsError = true;
                    result.ErrorMessage += "Minimum 1 data required for Operating Metric";
                    return result;
                }

                //Partners
                if (model.Partnerss.Count == 0 ||model.Partnerss == null)
                {
                    result.IsError = true;
                    result.ErrorMessage += "Minimum 1 data required for Partners";
                    return result;
                }
                if (model.Partnerss != null)
                {
                    foreach (var partner in model.Partnerss)
                    {
                        if (partner.Partners == null ||partner.Location == null)
                        {
                            result.IsError = true;
                            result.ErrorMessage += "Minimum 1 data required for Partners";
                            return result;
                        }
                        if (partner.Partners == null && partner.Location == null)
                        {
                            result.IsError = true;
                            result.ErrorMessage += "Please Complete the form";
                            return result;
                        }
                    }
                }
                //PIC
                if (model.PicFungsis == null || model.PicFungsis.Count < 0)
                {
                    result.IsError = true;
                    result.ErrorMessage += "Minimum 1 data required for Pic Member";
                    return result;
                }
                else
                {
                    List<Guid?> checkGuid = new List<Guid?>();
                    foreach (var picMember in model.PicFungsis)
                    {

                        if (picMember.PicFungsiId == null)
                        {
                            result.IsError = true;
                            result.ErrorMessage += "Minimum 1 data required for Pic";
                            return result;
                        }
                        if (picMember.PicFungsiId == model.PicFungsiId)
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

                        if (picMember.PicFungsiId == model.PicFungsiId)
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
        public ExistingFootprintViewModel IsOperatingMetricValidation(ExistingFootprintViewModel model)
        {
            var result = new ExistingFootprintViewModel();
            try
            {
                //Operating Metric
                result = _repository.IsCheckedYearOperatingMetric(model.Id, model.OperatingMetrics.Id);
                if (result.OperatingMetrics != null)
                {
                    if (result.OperatingMetrics.Year == model.OperatingMetrics.Year)
                    {
                        result.IsError = true;
                        result.ErrorMessage += "The year operating metric can't same";
                        return result;
                    }
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
        public List<ExistingFootprintsPartnersViewModel> GetExistingFootprintPartners(Guid guid)
        {
            var result = _repository.GetExistingFootprintPartners(guid);
            return result;
        }
        public List<ExistingFootprintsPICViewModel> GetExistingFootprintPicFungsiMember(Guid guid)
        {
            var picLevelMemberId = _repository.GetPicLevelMemberId();
            var result = _repository.GetExistingFootprintPICMember(guid, picLevelMemberId);
            if (result.Count >0)
            {
                foreach (var item in result)
                {
                    var picFungsi = _repository.GetPICFungsiName(item.PicFungsiId);
                    item.PicFungsiName = picFungsi.PicFungsiName;
                }
            }
            return result;
        }

        public ExistingFootprintViewModel GetExistingFootprintToRead(Guid guid)
        {
            ExistingFootprintViewModel model = new ExistingFootprintViewModel();
            model = _repository.GetExistingFootprint(guid);

            var negaraMitra = _repository.GetNegaraMitraById(model.NegaraMitraId);
            model.GridNegaraMitra = negaraMitra.GridNegaraMitra;
            model.Partnerss = _repository.GetExistingFootprintPartners(guid);
            model.PicFungsis = GetExistingFootprintPicFungsiMember(guid);
            model.PicFungsi = GetExistingFootprintPicFungsiLead(guid);
            model.GridProjectType = GetProjectTypeById(model.StreamBusinessId);

            var getHubHead = _repository.GetHubHeadByHubId(model.HubId);
            model.GridHubHeadName = getHubHead.Result.HubHeadName;
            model.GridHubName =getHubHead.Result.HubName;


            var getEntitas = _repository.GetEntitasById(model.EntitasPertaminaId.Value);
            model.GridEntitas = getEntitas.Result.GridEntitas;

            return model;
        }

        public ExistingFootprintViewModel GetNegaraMitraById(Guid? negaraMitraId)
        {
            var result = _repository.GetNegaraMitraById(negaraMitraId);
            return result;
        }
        public ExistingFootprintsPICViewModel GetExistingFootprintPicFungsiLead(Guid guid)
        {
            var picLevelLeadId = _repository.GetPicLevelLeadId();
            var result = _repository.GetExistingFootprintPICLead(guid, picLevelLeadId);
            if (result !=null)
            {
                var picFungsi = _repository.GetPICFungsiName(result.PicFungsiId);
                result.PicFungsiName = picFungsi.PicFungsiName;
            }
            return result;
        }
        public ExistingFootprintViewModel GetExistingToUpdate(Guid guid, Guid? picLevelLead, Guid? picLevelMember)
        {
            ExistingFootprintViewModel model = new ExistingFootprintViewModel();
            model = _repository.GetExistingFootprint(guid);
            model.Partnerss = _repository.GetExistingFootprintPartners(guid);
            model.PicFungsi = _repository.GetExistingFootprintPICLead(guid, picLevelLead);
            model.PicFungsis = _repository.GetExistingFootprintPICMember(guid, picLevelMember);
            return model;
        }
        public ExistingFootprintViewModel Delete(Guid guid, string username)
        {
            var model = _repository.GetExistingFootprint(guid);

            model.DeletedDate = DateTime.Now;
            model.UpdatedBy = username;
            model.UpdatedDate = DateTime.Now;
            List<ExistingFootprintsPartnersViewModel> partners = new List<ExistingFootprintsPartnersViewModel>();
            var ptr = _repository.GetExistingFootprintPartners(guid);
            foreach (var partner in ptr)
            {
                partner.Id = partner.Id;
                partner.CreatedDate = partner.CreatedDate;
                partner.CreatedBy = partner.CreatedBy;
                partner.ExistingFootPrintsId = partner.ExistingFootPrintsId;
                partner.Location = partner.Location;
                partner.Partners = partner.Partners;
                partner.DeletedDate = DateTime.Now;
                partner.UpdatedBy = username;
                partner.UpdatedDate = DateTime.Now;
                partners.Add(partner);
            }

            var hd = _repository.GetExistingFootprintHubHead(guid);
            hd.DeletedDate = DateTime.Now;
            hd.UpdatedBy = username;
            hd.UpdatedDate = DateTime.Now;

            var pic = _repository.GetExistingFootprintPIC(guid);
            pic.DeletedDate = DateTime.Now;
            pic.UpdatedBy = username;
            pic.UpdatedDate = DateTime.Now;

            var om = _repository.GetExistingFootprintOm(model.Id);
            om.UpdatedBy = username;
            om.UpdatedDate = DateTime.Now;
            om.DeletedDate= DateTime.Now;


            bool result = _repository.Delete(model, ptr, hd, pic, om);
            if (result)
            {
                return model;
            }
            return null;
        }

        public ExistingFootprintViewModel Update(ExistingFootprintViewModel model, string username)
        {
            try
            {

                var picLevelLeadId = _repository.GetPicLevelLeadId();
                model.PicLevelId = picLevelLeadId;
                var picLevelMemberId = _repository.GetPicLevelMemberId();
                model.PicLevelMemberId = picLevelMemberId;
                List<ExistingFootprintsOperatingMetricViewModel> oms = new List<ExistingFootprintsOperatingMetricViewModel>();
                if (model.OperatingMetricss.Count>0)
                {
                    foreach (var operatingMetric in model.OperatingMetricss)
                    {
                        operatingMetric.Revenue = ParseRevenue(operatingMetric.RevenueToString);
                        operatingMetric.TotalAsset = ParseRevenue(operatingMetric.TotalAssetToString);
                        operatingMetric.Ebitda = ParseRevenue(operatingMetric.EbitdaToString);
                        ExistingFootprintsOperatingMetricViewModel om = new ExistingFootprintsOperatingMetricViewModel();
                        om.ExistingFootprintsId = model.Id;
                        om.Year = operatingMetric.Year;
                        om.Ebitda = operatingMetric.Ebitda;
                        om.TotalAsset = operatingMetric.TotalAsset;
                        om.Revenue = operatingMetric.Revenue;
                        oms.Add(om);
                    }
                }

                bool result = _repository.Update(model, oms, username);
                if (result)
                {
                    return model;
                }
                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
        }

        public ExcelPackage ExportXLS(string searchGrid, string entitasPertaminaId,  string projectTypeId, string totalAssetsMinOp, string totalAssetsMaxOp, string revenueMinOp, string revenueMaxOp, string ebitdaMinOp, string ebitdaMaxOp, string yearOp, string partnerCountryId)

        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "Existing Download";
            package.Workbook.Properties.Author = "Pertamina";
            package.Workbook.Properties.Subject = "Export Data";
            package.Workbook.Properties.Keywords ="PIN - Pertamina";
            
            var worksheet = package.Workbook.Worksheets.Add("Existing Footprints");

            worksheet.Cells[1, 1].Value = "NO";
            worksheet.Cells[1, 2].Value = "NAME";
            worksheet.Cells[1, 3].Value = "PIC";
            worksheet.Cells[1, 4].Value = "HUB LEAD";
            worksheet.Cells[1, 5].Value = "PROJECT TYPE";
            worksheet.Cells[1, 6].Value = "TOTAL ASSETS";
            worksheet.Cells[1, 7].Value = "TOTAL REVENUE";
            worksheet.Cells[1, 8].Value = "EBITDA";
            worksheet.Cells[1, 9].Value = "YEAR";
            worksheet.Cells[1, 10].Value = "PARTNER COUNTRY";

            worksheet.Cells["A1:ZZ"].Style.Font.Name = "Arial";
            worksheet.Cells["A1 :ZZ"].Style.Font.Size = 12;
            worksheet.Cells["A1:ZZ1"].Style.Font.Size= 14;

            int dataRowCount = worksheet.Dimension.End.Row - 1;
            if (dataRowCount > 0)
            {
                var dataContent = worksheet.Cells["A2:ZZ" + worksheet.Dimension.End.Row];
                dataContent.Style.Font.Size = 12;
            }

            worksheet.Cells["A1:ZZ" + worksheet.Dimension.End.Row].AutoFitColumns();


            List<ExistingFootprintViewModel> datas = _repository.GetAllExistingFootprintsExcel().ToList();

            if (!string.IsNullOrEmpty(searchGrid))
            {
                datas = datas.Where(x =>
                (x.CellEntitas != null && x.CellEntitas.ToLower().Contains(searchGrid.ToLower())) ||
                (x.CellPicFungsiName != null && x.CellPicFungsiName.ToLower().Contains(searchGrid.ToLower())) ||
                (x.CellHubHeadName != null && x.CellHubHeadName.ToLower().Contains(searchGrid.ToLower())) ||
                (x.CellProjectType != null && x.CellProjectType.ToLower().Contains(searchGrid.ToLower())) ||
                (x.CellTotalAsset != null && x.CellTotalAsset.ToString().Contains(searchGrid.ToLower())) ||
                (x.CellRevenue != null && x.CellRevenue.ToString().Contains(searchGrid.ToLower())) ||
                (x.CellEbitda != null && x.CellEbitda.ToString().Contains(searchGrid.ToLower())) ||
                (x.CellYear != null && x.CellYear.ToString().Contains(searchGrid.ToLower())) ||
                (x.CellNegaraMitra != null && x.CellNegaraMitra.ToLower().Contains(searchGrid.ToLower()))
                ).ToList();
                
            }

            if (!string.IsNullOrEmpty(entitasPertaminaId))
            {
                string[] entitasArray = entitasPertaminaId.Split(',');
                if (entitasArray.Length<2)
                {
                    datas=datas.Where(x => x.CellEntitasId != null && x.CellEntitasId.ToLower().Contains(entitasArray[0].ToLower())).ToList();
                    //datas=datas.Where(x => x.CellEntitas != null && x.CellEntitas.ToLower().Contains(entitasArray[0].ToLower())).ToList();
                }
                else
                {
                    List<ExistingFootprintViewModel> filteredData = new List<ExistingFootprintViewModel>();
                    foreach(var item in entitasArray)
                    {
                        filteredData.AddRange(datas.Where(x => x.CellEntitasId != null && x.CellEntitasId.Contains(item)));
                    }
                    datas = filteredData.ToList();
                }
            }
            
            
            if (!string.IsNullOrEmpty(projectTypeId))
            {
                string[] projectTypeArray = projectTypeId.Split(',');
                if (projectTypeArray.Length<2)
                {
                    datas=datas.Where(x => x.CellProjectTypeId != null && x.CellProjectTypeId.ToLower().Contains(projectTypeArray[0].ToLower())).ToList();
                }
                else
                {
                    List<string> stringList = new List<string>();
                    foreach (var str in projectTypeArray)
                    {
                        stringList.Add(str);
                    }
                    datas = datas.Where(x => stringList.Contains(x.CellProjectTypeId)).ToList();
                }
            }
            
            if (!string.IsNullOrEmpty(totalAssetsMinOp))
            {
                if (decimal.TryParse(totalAssetsMinOp, out decimal resultRevenue))
                {
                    var totalAssetsMin = resultRevenue;
                    if (totalAssetsMin != null)
                    {
                        datas=datas.Where(x => x.CellTotalAsset != null && x.CellTotalAsset >= totalAssetsMin*1000000).ToList();
                    }
                }
                
            } 
            if (!string.IsNullOrEmpty(totalAssetsMaxOp))
            {
                if (decimal.TryParse(totalAssetsMaxOp, out decimal resultRevenue))
                {
                    var totalAssetsMax = resultRevenue;
                    if (totalAssetsMax != null)
                    {
                        datas=datas.Where(x => x.CellTotalAsset != null && x.CellTotalAsset <= totalAssetsMax*1000000).ToList();
                    }
                }
            } 

            if (!string.IsNullOrEmpty(revenueMinOp))
            {
                if (decimal.TryParse(revenueMinOp, out decimal resultRevenue))
                {
                    var revenueMin = resultRevenue;
                    if (revenueMin != null)
                    {
                        datas=datas.Where(x => x.CellRevenue != null && x.CellRevenue >= revenueMin*1000000).ToList();
                    }
                }
            }
            if (!string.IsNullOrEmpty(revenueMaxOp))
            {
                if (decimal.TryParse(revenueMaxOp, out decimal resultRevenue))
                {
                    var revenueMax = resultRevenue;
                    if (revenueMax != null)
                    {
                        datas=datas.Where(x => x.CellRevenue != null && x.CellRevenue <= revenueMax * 1000000).ToList();
                    }
                }
            }
            
            if (!string.IsNullOrEmpty(ebitdaMinOp))
            {
                if (decimal.TryParse(ebitdaMinOp, out decimal resultEbitda))
                {
                    var ebitdaMin = resultEbitda;
                    if (ebitdaMin != null)
                    {
                        datas=datas.Where(x => x.CellEbitda != null && x.CellEbitda >= ebitdaMin*1000000).ToList();
                    }
                }
            }
            if (!string.IsNullOrEmpty(ebitdaMaxOp))
            {
                if (decimal.TryParse(ebitdaMaxOp, out decimal resultEbitda))
                {
                    var ebitdaMax = resultEbitda;
                    if (ebitdaMax != null)
                    {
                        datas=datas.Where(x => x.CellEbitda != null && x.CellEbitda <= ebitdaMax*1000000).ToList();
                    }
                }
            }
            
            if (!string.IsNullOrEmpty(yearOp))
            {
                string[] yearArray = yearOp.Split(',');
                if (yearArray.Length<2)
                {
                    datas=datas.Where(x => x.CellYear != null && x.CellYear.ToString().Contains(yearArray[0].ToLower())).ToList();
                }
                else
                {
                    List<string> stringList = new List<string>();
                    foreach (var str in yearArray)
                    {
                        stringList.Add(str);
                    }
                    datas = datas.Where(x => stringList.Contains(x.CellYear.ToString())).ToList();
                }
            }
            
            if (!string.IsNullOrEmpty(partnerCountryId))
            {
                string[] partnerCountryArray = partnerCountryId.Split(',');
                if (partnerCountryArray.Length<2)
                {
                    datas=datas.Where(x => x.CellNegaraMitraId != null && x.CellNegaraMitraId==partnerCountryArray[0]).ToList();
                }
                else
                {
                    List<string> stringList = new List<string>();
                    foreach (var str in partnerCountryArray)
                    {
                        stringList.Add(str);
                    }
                    datas = datas.Where(x => stringList.Contains(x.CellNegaraMitraId)).ToList();
                }
            }

            int x = 3;
            int y = 1;
            if (datas != null )
            {
                if (datas.Count >0)
                {
                    foreach (var data in datas)
                    {
                        worksheet.Cells[x, 1].Value = y;
                        worksheet.Cells[x, 2].Value = data.CellEntitas;
                        worksheet.Cells[x, 3].Value = data.CellPicFungsiName;
                        worksheet.Cells[x, 4].Value = data.CellHubHeadName;
                        worksheet.Cells[x, 5].Value = data.CellProjectType;
                        worksheet.Cells[x, 6].Value = data.CellTotalAsset;
                        worksheet.Cells[x, 7].Value = data.CellRevenue;
                        worksheet.Cells[x, 8].Value = data.CellEbitda;
                        worksheet.Cells[x, 9].Value = data.CellYear;
                        worksheet.Cells[x, 10].Value = data.CellNegaraMitra;

                        x++;
                        y++;
                    }
                    worksheet.Cells[1, 1, datas.Count, 10].AutoFilter = true;
                }
               
            }
           
            return package;
        }

        public string GetProjectTypeById(Guid? streamBusinessId)
        {
            string result = string.Empty;

            result = _repository.GetProjectTypeById(streamBusinessId);

            return result;
        }
        #endregion

    }
}
