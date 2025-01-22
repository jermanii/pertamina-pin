using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class ExternalContactViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string NamaCompany { get; set; }
        public string KoneksiCompany { get; set; }
        public string AlamatCompany { get; set; }
        public string EmailCompany { get; set; }
        public string NoTelpCompany { get; set; }
        public string NamaPerson { get; set; }
        public string JabatanPerson { get; set; }
        public string EmailPerson { get; set; }
        public string NoTelpPerson { get; set; }
        public List<string> NamaPersons { get; set; }
        public List<string> JabatanPersons { get; set; }
        public List<string> EmailPersons { get; set; }
        public List<string> NoTelpPersons { get; set; }
        public string Remark { get; set; }
        public List<PersonViewModel> Persons { get; set; }
        public PersonViewModel Person { get; set; }
        public class PersonViewModel
        {
            public string NamaPerson { get; set; }
            public string JabatanPerson { get; set; }
            public string EmailPerson { get; set; }
            public string NoTelpPerson { get; set; }
        }
        
    }
}
