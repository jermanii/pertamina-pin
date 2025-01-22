using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IHeaderMenuService
    {
        void AddNotif(NotificationBellViewModel model);
        HeaderMenuViewModel GetListNotifs(string userName);
    }
}
