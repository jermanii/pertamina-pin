using AutoMapper;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class HeaderMenuRepository : BaseRepository<TbltNotificationBell>, IHeaderMenuRepository
    {
        public HeaderMenuRepository(DB_PINMContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public void AddNotif(NotificationBellViewModel model)
        {
            string[] sentTo = model.SentTo.Split(',');
            foreach (var to in sentTo)
            {
                model.Id = Guid.NewGuid();
                model.SentTo = to;
                _context.Set<TbltNotificationBell>().Add(_mapper.Map<TbltNotificationBell>(model));
            }
            _context.SaveChanges();
        }
        public List<NotificationBellViewModel> GetListNotif(string userName)
        {
            List<NotificationBellViewModel> result = new List<NotificationBellViewModel>();

            result = _mapper.Map<List<NotificationBellViewModel>>(_context.TbltNotificationBells
                                                                .Where(x => x.SentTo == userName && x.IsRead == false)
                                                                .OrderByDescending(x => x.CreatedDate)
                                                                .ToList());

            return result;
        }

        public List<NotificationBellViewModel> GetListNotif(Guid transactionId, Guid formTransactionId, Guid statusTypeId)
        {
            List<NotificationBellViewModel> result = new List<NotificationBellViewModel>();            

            result = _mapper.Map<List<NotificationBellViewModel>>(_context.TbltNotificationBells
                                                                .Where(x => x.TransactionId == transactionId && x.FormTransactionId == formTransactionId && x.StatusTypeId == statusTypeId)
                                                                .OrderByDescending(x => x.CreatedDate)
                                                                .ToList());

            return result;
        }

        public FormTransactionViewModel GetFormTransaction(Guid guid)
        {
            FormTransactionViewModel result = new FormTransactionViewModel();

            result = _mapper.Map<FormTransactionViewModel>(_context.TblmFormTransactions.Where(x => x.Id == guid).FirstOrDefault());

            return result;
        }
        public FormTransactionViewModel GetFormTransaction(string name)
        {
            FormTransactionViewModel result = new FormTransactionViewModel();

            result = _mapper.Map<FormTransactionViewModel>(_context.TblmFormTransactions.Where(x => x.TransactionForm == name).FirstOrDefault());

            return result;
        }
        public TransactionStatusTypeViewModel GetStatusTransactionType(Guid guid)
        {
            TransactionStatusTypeViewModel result = new TransactionStatusTypeViewModel();

            result = _mapper.Map<TransactionStatusTypeViewModel>(_context.TblmStatusTypes.Where(x => x.Id == guid).FirstOrDefault());

            return result;
        }
        public TransactionStatusTypeViewModel GetStatusTransactionType(Guid guid, string type)
        {
            TransactionStatusTypeViewModel result = new TransactionStatusTypeViewModel();

            result = _mapper.Map<TransactionStatusTypeViewModel>(_context.TblmStatusTypes.Where(x => x.FormTransactionId == guid && x.Type == type).FirstOrDefault());

            return result;
        }

        public void SetReadNotif(Guid? guid, string userName)
        {
            List<TbltNotificationBell> existing = _context.Set<TbltNotificationBell>().Where(x => x.TransactionId == guid && x.SentTo == userName).ToList();
            if (existing.Count > 0)
            {
                foreach (var rec in existing)
                {
                    rec.IsRead = true;
                    _context.Entry(rec).CurrentValues.SetValues(rec);
                    _context.SaveChanges();
                }
            }
        }
    }
}
