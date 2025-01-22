using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class InternationalBusinessIntelligenceCategoryViewModel : ErrorBaseModel
    {
        public Guid? TypeStudyId { get; set; }
        public List<Guid> CountriesCoveredSelected { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public List<InternationalBusinessIntelligenceViewModel> IBICategoryOwn { get; set; }
        public List<InternationalBusinessIntelligenceViewModel> IBICategoryRecently { get; set; }
        public List<InternationalBusinessIntelligenceViewModel> IBICategoryMostView { get; set; }
        public List<NegaraMitraInformationViewModel> IBIMapView { get; set; }
        public List<InternationalBusinessIntelligenceViewModel> IBICategoryMyList { get; set; }
    } 
}
