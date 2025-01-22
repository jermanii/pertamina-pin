using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class UserViewModel
    {
        public string id { get; set; }
        public string userId { get; set; }
        public PositionUser position { get; set; }
        public string companyCode { get; set; }
        public string companyName { get; set; }
        public bool isActive { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string displayName { get; set; }
        public string employeeId { get; set; }
        public string lastName { get; set; }
        public string jobTitle { get; set; }
        public string email { get; set; }
        public string mobilePhone { get; set; }
        public string aboutMe { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string address { get; set; }
        public string photo { get; set; }
        public ExtensionAttributes extensionAttributes { get; set; }
        public string idp { get; set; }
        public string directoryId { get; set; }
        public string lastModified { get; set; }
        public string employeeNumber { get; set; }
        public string employeeType { get; set; }
        public string cultureInfo { get; set; }
        public string language { get; set; }
        public string dateFormat { get; set; }
        public string timeFormat { get; set; }
        public List<ApplicationParam> applicationParams { get; set; }
    }

    public class ApplicationUser
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ApplicationParam
    {
        public ApplicationUser application { get; set; }
        public List<CustomParameter> customParameters { get; set; }
    }

    public class CreatedBy
    {
        public List<object> additionalProp1 { get; set; }
        public List<object> additionalProp2 { get; set; }
        public List<object> additionalProp3 { get; set; }
    }

    public class CustomParameter
    {
        public List<object> additionalProp1 { get; set; }
        public List<object> additionalProp2 { get; set; }
        public List<object> additionalProp3 { get; set; }
    }

    public class ExtensionAttributes
    {
        public General General { get; set; }
        public Education Education { get; set; }
        public DocumentAccess DocumentAccess { get; set; }
        public Manager Manager { get; set; }
        public DTMonbehalf DTMonbehalf { get; set; }
        public IAM IAM { get; set; }
    }

    public class General
    {
        public string NIK { get; set; }
        public string NPWP { get; set; }
        public string Address { get; set; }
        public string TENANT { get; set; }
        public string WHATSAPP_NUMBER { get; set; }
        public string Email { get; set; }
    }

    public class Education
    {
        public string Scholar { get; set; }
        public string Graduated { get; set; }
    }
    public class DocumentAccess
    {
        public string companyCode { get; set; }
        public string positionId { get; set; }
        public string accessDurationStart { get; set; }
        public string accessDurationEnd { get; set; }
        public string documentDurationStart { get; set; }
        public string documentDurationEnd { get; set; }
    }

    public class Manager
    {
        public string employeeId { get; set; }
        public string assignmentNumber { get; set; }
    }
    public class DTMonbehalf
    {
        public string EmployeeIDassignment { get; set; }
        public string Assignmentposition { get; set; }
        public string AssignmentEmail { get; set; }
    }
    public class IAM
    {
        public string ADUsername { get; set; }
        public string AssigmentNumber { get; set; }
    }
    public class OrganizationUser
    {
        public string id { get; set; }
        public string name { get; set; }
        public string companyCode { get; set; }
    }

    public class PositionUser
    {
        public string id { get; set; }
        public string name { get; set; }
        public OrganizationUser organization { get; set; }
        public string kbo { get; set; }
        public CreatedBy createdBy { get; set; }
        public string lastModified { get; set; }
        public bool isPublished { get; set; }
        public bool isHead { get; set; }
        public bool isOwner { get; set; }
        public bool isChief { get; set; }
    }
}
