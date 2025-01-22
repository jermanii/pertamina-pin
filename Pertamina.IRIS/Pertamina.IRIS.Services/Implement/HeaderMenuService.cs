using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pertamina.IRIS.Services.Implement
{
    public class HeaderMenuService : IHeaderMenuService
    {
        private readonly IHeaderMenuRepository _headerMenuRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly IAgreementRepository _agreementRepository;
        private readonly IInitiativeRepository _initiativeRepository;
        private readonly IProjectsToOfferRepository _opportunityRepository;
        public HeaderMenuService(IHeaderMenuRepository headerMenuRepository, 
                                 IFeatureRepository featureRepository,
                                 IAgreementRepository agreementRepository,
                                 IInitiativeRepository initiativeRepository,
                                 IProjectsToOfferRepository opportunityRepository)
        {
            _headerMenuRepository = headerMenuRepository;
            _featureRepository = featureRepository;
            _agreementRepository = agreementRepository;
            _initiativeRepository = initiativeRepository;
            _opportunityRepository = opportunityRepository;
        }

        public void AddNotif(NotificationBellViewModel model)
        {
            var data = new NotificationBellViewModel();
            data = model;
            var formTransaction = _headerMenuRepository.GetFormTransaction(model.FormTransaction);
            var statusType = _headerMenuRepository.GetStatusTransactionType(formTransaction.Id, model.Status);

            var notifs = _headerMenuRepository.GetListNotif(model.TransactionId.Value, formTransaction.Id, statusType.Id);
            if(notifs.Count > 0 )
            {
                foreach(var item in notifs)
                {
                    if (!item.IsRead.Value)
                    {
                        _headerMenuRepository.SetReadNotif(item.TransactionId, item.SentTo);
                    }
                }
                data.Before = notifs.Select(x => x.Before).FirstOrDefault();
            }
            data.FormTransactionId = statusType.FormTransactionId;
            data.StatusTypeId = statusType.Id;

            _headerMenuRepository.AddNotif(data);
        }
        public HeaderMenuViewModel GetListNotifs(string userName)
        {
            HeaderMenuViewModel model = new HeaderMenuViewModel();

            model.NotificationBell = new List<NotificationBellViewModel>();
            model.NotificationBell = GetListNotif(userName);

            List<NotificationBellViewModel> notifs = new List<NotificationBellViewModel>();
            foreach (var rec in model.NotificationBell)
            {
                if ((DateTime.Now - rec.CreatedDate.Value).Minutes < 1)
                {
                    rec.NotifTime = "Just now";
                }
                else if ((DateTime.Now - rec.CreatedDate.Value).Minutes < 5 && (DateTime.Now - rec.CreatedDate.Value).Minutes > 1)
                {
                    rec.NotifTime = "5 minutes ago";
                }
                else
                {
                    rec.NotifTime = rec.CreatedDate.Value.ToString("dd/MM/yyyy");
                };
                var formTransaction =  _headerMenuRepository.GetFormTransaction(rec.FormTransactionId.Value);
                var feature = _featureRepository.GetFeatureById(formTransaction.FeatureId);
                var statusType = _headerMenuRepository.GetStatusTransactionType(rec.StatusTypeId.Value);
                rec.FormTransaction = formTransaction.TransactionForm;
                rec.RedirectUrl = $"/{feature.ControllerName}/{feature.ActionName}?guid={rec.TransactionId}";
                rec.Status = statusType.Type;
                rec.TransactionTitle = formTransaction.TransactionForm == EnumBaseModel.Agreement ? GetTransactionTitle(EnumBaseModel.Agreement, rec.TransactionId.Value) : 
                                       (formTransaction.TransactionForm == EnumBaseModel.Initiative ? GetTransactionTitle(EnumBaseModel.Initiative, rec.TransactionId.Value) : 
                                       GetTransactionTitle(EnumBaseModel.Opportunity, rec.TransactionId.Value));
                notifs.Add(rec);
            }

            model.NotificationBell = notifs;
            return model;
        }
        private List<NotificationBellViewModel> GetListNotif(string userName)
        {
            List<NotificationBellViewModel> result = new List<NotificationBellViewModel>();

            result = _headerMenuRepository.GetListNotif(userName).GroupBy(x => x.StatusTypeId).Select(g => g.First()).ToList();

            return result;
        }

        private string GetTransactionTitle(string formTransaction, Guid guid)
        {
            string title = string.Empty;

            switch(formTransaction)
            {
                case EnumBaseModel.Agreement:
                    title = _agreementRepository.GetAgreementById(guid).JudulPerjanjian;
                    break;
                case EnumBaseModel.Initiative:
                    title = _initiativeRepository.GetInitiativeById(guid).JudulInisiasi;
                    break;
                case EnumBaseModel.Opportunity:
                    title = _opportunityRepository.GetOpportunityById(guid).NamaProyek;
                    break;
            }

            return title;

        }
    }
}
