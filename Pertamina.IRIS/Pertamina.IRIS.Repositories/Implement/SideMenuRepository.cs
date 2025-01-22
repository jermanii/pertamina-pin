using AutoMapper;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class SideMenuRepository : BaseRepository<TblmFeature>, ISideMenuRepository
    {
        public SideMenuRepository(DB_PINMContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public SideMenuViewModel GetListFeature()
        {
            SideMenuViewModel result = new SideMenuViewModel();

            List<FeatureViewModel> featureViewModels = new List<FeatureViewModel>();

            List<FeatureViewModel> Features = new List<FeatureViewModel>();
            Features = AllMenuFeatures.Select(x => new FeatureViewModel
            {
                Id = x.Id,
                Code = x.Code,
                ParentFeatureCode = x.ParentFeatureCode,
                FeatureName = x.FeatureName,
                ControllerName = x.ControllerName,
                ActionName = x.ActionName,
                MenuIcon = x.MenuIcon,
                IsMenu = x.IsMenu,
                Description = x.Description,
                Sequence = x.Sequence ?? 0,
                SequenceChild = x.SequenceChild ?? 0,
                SubMenuIcon = x.SubMenuIcon,
                SubMenuDesc = x.SubMenuDesc,
                MenuLink = x.MenuLink

            }).OrderBy(x => x.Sequence).ToList();

            foreach (var Feature in Features)
            {
                Feature.ChildFeatures = new List<FeatureViewModel>();
                Feature.ChildFeatures = AllFeatures.Select(x => new FeatureViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    ParentFeatureCode = x.ParentFeatureCode,
                    FeatureName = x.FeatureName,
                    ControllerName = x.ControllerName,
                    ActionName = x.ActionName,
                    MenuIcon = x.MenuIcon,
                    IsMenu = x.IsMenu,
                    Description = x.Description,
                    Sequence = x.Sequence ?? 0,
                    SequenceChild = x.SequenceChild ?? 0,
                    SubMenuIcon = x.SubMenuIcon,
                    SubMenuDesc = x.SubMenuDesc,
                    MenuLink = x.MenuLink,
                }).Where(x => x.ParentFeatureCode == Feature.Code).OrderBy(x => x.SequenceChild).ToList();
                featureViewModels.Add(Feature);
            }

            result.Features = new List<FeatureViewModel>(featureViewModels);

            return result;
        }

        public SideMenuViewModel GetListFeature(List<RoleFeatureViewModel> roleFeature)
        {
            SideMenuViewModel result = new SideMenuViewModel();

            List<FeatureViewModel> featureViewModels = new List<FeatureViewModel>();

            List<FeatureViewModel> Features = new List<FeatureViewModel>();
            Features = AllMenuFeatures.Select(x => new FeatureViewModel
            {
                Id = x.Id,
                Code = x.Code,
                ParentFeatureCode = x.ParentFeatureCode,
                FeatureName = x.FeatureName,
                ControllerName = x.ControllerName,
                ActionName = x.ActionName,
                MenuIcon = x.MenuIcon,
                IsMenu = x.IsMenu,
                Description = x.Description,
                Sequence = x.Sequence ?? 0,
                SequenceChild = x.SequenceChild ?? 0,
                SubMenuIcon = x.SubMenuIcon,
                SubMenuDesc = x.SubMenuDesc,
                MenuLink = x.MenuLink

            }).OrderBy(x => x.Sequence).ToList();

            foreach (var Feature in Features)
            {
                bool isAvailable = false;

                foreach (var rec in roleFeature)
                {
                    if (Feature.Id == rec.FeatureId)
                        isAvailable = true;
                }

                if (isAvailable)
                {
                    Feature.ChildFeatures = new List<FeatureViewModel>();
                    Feature.ChildFeatures = AllFeatures.Select(x => new FeatureViewModel
                    {
                        Id = x.Id,
                        Code = x.Code,
                        ParentFeatureCode = x.ParentFeatureCode,
                        FeatureName = x.FeatureName,
                        ControllerName = x.ControllerName,
                        ActionName = x.ActionName,
                        MenuIcon = x.MenuIcon,
                        IsMenu = x.IsMenu,
                        Description = x.Description,
                        Sequence = x.Sequence ?? 0,
                        SequenceChild = x.SequenceChild ?? 0,
                        SubMenuIcon = x.SubMenuIcon,
                        SubMenuDesc = x.SubMenuDesc,
                        MenuLink = x.MenuLink,
                    }).Where(x => x.ParentFeatureCode == Feature.Code).OrderBy(x => x.SequenceChild).ToList();
                    featureViewModels.Add(Feature);
                }
            }

            result.Features = new List<FeatureViewModel>(featureViewModels);

            return result;
        }

        public List<RoleFeatureViewModel> GetRoleFeature(List<string> roleName)
        {
            List<RoleFeatureViewModel> result = new List<RoleFeatureViewModel>();

            List<Guid> roles = new List<Guid>();

            foreach (var role in roleName)
            {
                roles.Add(_context.TblmRoles.Where(x => x.Name == role).Select(x => x.Id).FirstOrDefault());
            }

            foreach (var role in roles)
            {
                result.AddRange(_mapper.Map<List<RoleFeatureViewModel>>(_context.TblmRoleFeatures.Where(x => x.DeletedDate == null && x.RoleId.Value == role).ToList()));
            }

            return result;
        }

        public List<RoleViewModel> GetRole()
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            result = _mapper.Map<List<RoleViewModel>>(_context.TblmRoles.ToList());
            return result;
        }

        private IQueryable<TblmFeature> AllMenuFeatures
        {
            get
            {
                return _context.TblmFeatures.Where(x => x.DeletedDate == null && x.IsMenu == true);
            }
        }

        private IQueryable<TblmFeature> AllFeatures
        {
            get
            {
                return _context.TblmFeatures.Where(x => x.DeletedDate == null);
            }
        }
    }
}
