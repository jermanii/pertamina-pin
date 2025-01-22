using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IHeaderMenuRepository
    {
        void AddNotif(NotificationBellViewModel model);
        List<NotificationBellViewModel> GetListNotif(string userName);
        List<NotificationBellViewModel> GetListNotif(Guid transactionId, Guid formTransactionId, Guid statusTypeId);
        FormTransactionViewModel GetFormTransaction(Guid guid);
        FormTransactionViewModel GetFormTransaction(string name);
        TransactionStatusTypeViewModel GetStatusTransactionType(Guid guid);
        TransactionStatusTypeViewModel GetStatusTransactionType(Guid guid, string type);
        void SetReadNotif(Guid? guid, string userName);
    }
}
