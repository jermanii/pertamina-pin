using OfficeOpenXml;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Services.Implement
{
    public class DropDownListService : IDropDownListService
    {
        private readonly IDropDownListRepository _repository;

        public DropDownListService(IDropDownListRepository repository)
        {
            _repository = repository;
        }

        public List<Select2BaseModel> DdlHsh(string query)
        {
            var result = _repository.DdlHsh(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlEntitasPertamina(string query)
        {
            var result = _repository.DdlEntitasPertamina(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlEntitasPertaminaGrid(string query)
        {
            var result = _repository.DdlEntitasPertaminaGrid(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlProvinsiIndonesia(string query)
        {
            var result = _repository.DdlProvinsiIndonesia(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlFungsi(string query)
        {
            var result = _repository.DdlFungsi(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlPic(string query)
        {
            var result = _repository.DdlPic(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlPicLevel(string query)
        {
            var result = _repository.DdlPicLevel(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlHubHead(string query)
        {
            var result = _repository.DdlHubHead(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlHub(string query)
        {
            var result = _repository.DdlHub(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlHshEntitasPertamina(string query)
        {
            var result = _repository.DdlHshEntitasPertamina(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlRelatedAgreement(string query)
        {
            var result = _repository.DdlRelatedAgreement(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlStreamBusiness(string query)
        {
            var result = _repository.DdlStreamBusiness(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlInterest(string query)
        {
            var result = _repository.DdlInterest(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlInitiativeStatus(string query)
        {
            var result = _repository.DdlInitiativeStatus(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlInitiativeTypes(string query)
        {
            var result = _repository.DdlInitiativeTypes(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlInitiativeStages(string query)
        {
            var result = _repository.DdlInitiativeStages(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlKawasanMitra(string query)
        {
            var result = _repository.DdlKawasanMitra(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlNegaraMitra(string query)
        {
            var result = _repository.DdlNegaraMitra(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlNegaraMitraOnlyNegara(string query)
        {
            var result = _repository.DdlNegaraMitraOnlyNegara(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlNegaraMitraWithoutKawasan(string query)
        {
            var result = _repository.DdlNegaraMitraWithoutKawasan(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlOpportunityEntitasPertamina(string query)
        {
            var result = _repository.DdlOpportunityEntitasPertamina(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlOpportunityPic(string query)
        {
            var result = _repository.DdlOpportunityPic(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlOpportunityKesiapanProyek(string query)
        {
            var result = _repository.DdlOpportunityKesiapanProyek(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlOpportunityNegara(string query)
        {
            var result = _repository.DdlOpportunityNegara(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlOpportunityTargetMitra(string query)
        {
            var result = _repository.DdlOpportunityTargetMitra(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlStatusBerlaku(string query)
        {
            var result = _repository.DdlStatusBerlaku(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        } 
        public List<Select2BaseModel> DdlTrafficLight(string query)
        {
            var result = _repository.DdlTrafficLight(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlInitiativeForm(string query)
        {
            var result = _repository.DdlInitiativeForm(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlAgreementForm(string query)
        {
            var result = _repository.DdlAgreementForm(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlNamaProyek(string query, string value)
        {
            var result = _repository.DdlNamaProyek(query, value).ToList();
            return result;
        }
        public List<Select2BaseModel> DdlNamaProyekInitiative(string query, string value)
        {
            var result = _repository.DdlNamaProyekInitiative(query, value).ToList();
            return result;
        }
        public List<Select2BaseModel> DdlStatusDiscussion(string query)
        {
            var result = _repository.DdlStatusDiscussion(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlJenisPerjanjian(string query)
        {
            var result = _repository.DdlJenisPerjanjian(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlExistingFootprintsSortingHighPriority(string query)
        {
            var result = _repository.DdlExistingFootprintsSortingHighPriority(query, EnumBaseModel.ExistingFootprintsSortingHighPriority).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlSignedAgreementSortingHighPriority(string query)
        {
            var result = _repository.DdlSignedAgreementSortingHighPriority(query, EnumBaseModel.SignedAgreementSortingHighPriority).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }

        public Select2BaseModel GetDdlFungsi(Guid guid)
        {
            var result = _repository.GetDdlFungsi(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlPicLevel(Guid guid)
        {
            var result = _repository.GetDdlPicLevel(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlPicFungsi(Guid guid)
        {
            var result = _repository.GetDdlPicFungsi(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlEntitasPertamina(Guid guid)
        {
            var result = _repository.GetDdlEntitasPertamina(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlHub(Guid guid)
        {
            var result = _repository.GetDdlHub(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlHsh(Guid guid)
        {
            var result = _repository.GetDdlHsh(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlInterest(Guid guid)
        {
            var result = _repository.GetDdlInterest(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlInitiativeStatus(Guid guid)
        {
            var result = _repository.GetDdlInitiativeStatus(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlInitiativeTypes(Guid guid)
        {
            var result = _repository.GetDdlInitiativeTypes(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlInitiativeStages(Guid guid)
        {
            var result = _repository.GetDdlInitiativeStages(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlKawasanMitra(Guid guid)
        {
            var result = _repository.GetDdlKawasanMitra(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlNegaraMitra(Guid guid)
        {
            var result = _repository.GetDdlNegaraMitra(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlNegaraMitraOnlyNegara(Guid guid)
        {
            var result = _repository.GetDdlNegaraMitraOnlyNegara(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlRelatedAgreement(Guid guid)
        {
            var result = _repository.GetDdlRelatedAgreement(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlOpportunity(Guid guid)
        {
            var result = _repository.GetDdlOpportunity(guid).FirstOrDefault();

            return result;
        }
        public Select2BaseModel GetDdlStreamBusiness(Guid guid)
        {
            var result = _repository.GetDdlStreamBusiness(guid).FirstOrDefault();

            return result;
        }
        public List<Select2BaseModel> GetDdlOpportunityStreamBusiness(Guid guid)
        {
            var result = _repository.GetDdlOpportunityStreamBusiness(guid).ToList();

            return result;
        }
        public Select2BaseModel GetDdlHshEntitasPertamina(Guid guid)
        {
            var result = _repository.GetDdlHshEntitasPertamina(guid).FirstOrDefault();

            return result;
        }
        public List<Select2BaseModel> GetDdlInitiativeEntitasPertamina(Guid guid)
        {
            var result = _repository.GetDdlInitiativeEntitasPertamina(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlOpportunityEntitasPertamina(Guid guid)
        {
            var result = _repository.GetDdlOpportunityEntitasPertamina(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlInitiativePicFungsi(Guid guid)
        {
            var result = _repository.GetDdlInitiativePicFungsi(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlInitiativeStreamBusiness(Guid guid)
        {
            var result = _repository.GetDdlInitiativeStreamBusiness(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlOpportunityPicFungsi(Guid guid)
        {
            var result = _repository.GetDdlOpportunityPic(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlOpportunityKesiapanProyek(Guid guid)
        {
            var result = _repository.GetDdlOpportunityKesiapanProyek(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlOpportunityNegara(Guid guid)
        {
            var result = _repository.GetDdlOpportunityNegara(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlOpportunityTargetMitra(Guid guid)
        {
            var result = _repository.GetDdlOpportunityTargetMitra(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> DdlFaktorKendala(string query)
        {
            var result = _repository.DdlFaktorKendala(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlKlasifikasiKendala(string query)
        {
            var result = _repository.DdlKlasifikasiKendala(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlKeterlibatanLembaga(string query)
        {
            var result = _repository.DdlKeterlibatanLembaga(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlYear(string query)
        {
            var result = _repository.DdlYear(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> GetDdlInitiativeNegara(Guid guid)
        {
            var result = _repository.GetDdlInitiativeNegara(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlAgreementStreamBusiness(Guid guid)
        {
            var result = _repository.GetDdlAgreementStreamBusiness(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlAgreementPicFungsi(Guid guid)
        {
            var result = _repository.GetDdlAgreementPicFungsi(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlAgreementEntitasPertamina(Guid guid)
        {
            var result = _repository.GetDdlAgreementEntitasPertamina(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlAgreementNegara(Guid guid)
        {
            var result = _repository.GetDdlAgreementNegara(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlAgreementJenisPerjanjian(Guid guid)
        {
            var result = _repository.GetDdlAgreementJenisPerjanjian(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlAgreementStatusDiscussion(Guid guid)
        {
            var result = _repository.GetDdlAgreementStatusDiscussion(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlAgreementFaktorKendala(Guid guid)
        {
            var result = _repository.GetDdlAgreementFaktorKendala(guid).ToList();

            return result;
        } 
        public List<Select2BaseModel> GetDdlAgreementTrafficLight(Guid guid)
        {
            var result = _repository.GetDdlAgreementTrafficLight(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlAgreementKlasifikasiKendala(Guid guid)
        {
            var result = _repository.GetDdlAgreementKlasifikasiKendala(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlAgreementKeterlibatanLembaga(Guid guid)
        {
            var result = _repository.GetDdlAgreementKeterlibatanLembaga(guid).ToList();

            return result;
        }
        public List<Select2BaseModel> GetDdlAgreementRelatedAgreement(Guid guid)
        {
            var result = _repository.GetDdlAgreementRelatedAgreement(guid).ToList();

            return result;
        }

        public List<Select2BaseModel> DdlTypeOfStudy(string query)
        {
            var result = _repository.DdlTypeOfStudy(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlConfidentiality(string query)
        {
            var result = _repository.DdlConfidentiality(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }
        public List<Select2BaseModel> DdlOwnerEntity(string query)
        {
            var result = _repository.DdlOwnerEntity(query).ToList();

            if (!(string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query)))
            {
                result = result.Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();
            }

            return result;
        }

        public List<Select2BaseModel> GetDdlInitiativeKeterlibatanLembaga(Guid guid)
        {
            var result = _repository.GetDdlInitiativeKeterlibatanLembaga(guid).ToList();

            return result;
        }
    }
}
