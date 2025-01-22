using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.Base
{
    public static class EnumBaseModel
    {
        public static string FormatUploadFileName = "File";

        //Role
        public const string RoleOwner = "OWNER";

        //HtmlTemplate

        //Feature Folder

        //Idaman Url
        public const string GetWhitelistsAsync = "GetWhitelistsAsync";
        public const string GetUserAsync = "GetUserAsync";
        public const string GetUserParentAsync = "GetUserParentAsync";
        public const string GetUserOrganizationsAsync = "GetUserOrganizationsAsync";
        public const string GetUserBySearchTextAsync = "GetUserBySearchTextAsync";
        public const string GetPositionsBySearchTextAsync = "GetPositionsBySearchTextAsync";
        public const string GetPositionUserAsync = "GetPositionUserAsync";
        public const string GetApplicationRolesAsync = "GetApplicationRolesAsync";
        public const string GetPositionByRoleIdAplicationAsync = "GetPositionByRoleIdAplicationAsync";

        //Transaction Name
        public const string Agreement = "Agreement";
        public const string Initiative = "Initiative";
        public const string Opportunity = "Opportunity";
        public const string UserManual = "UserManual";

        //Stored Procedure
        public const string ExistingFootprintsMetrics = "ExistingFootprintsMetrics";
        public const string PotentialInitiativeMetrics = "PotentialInitiativeMetrics";
        public const string SignedAgreementMetrics = "SignedAgreementMetrics";
        public const string ProjectsToOfferMetrics = "ProjectsToOfferMetrics";

        //Sorting High Priority
        public const string ExistingFootprintsSortingHighPriority = "ExistingFootprints";
        public const string PotentialInitiativeSortingHighPriority = "PotentialInitiative";
        public const string SignedAgreementSortingHighPriority = "SignedAgreement";
        public const string ProjectsToOfferSortingHighPriority = "ProjectsToOffer";
    }
}
