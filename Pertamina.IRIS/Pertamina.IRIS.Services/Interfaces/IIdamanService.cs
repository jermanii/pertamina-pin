using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IIdamanService
    {
        Task<UserViewModel> GetUserAsync(string userId, string token);
        Task<List<string>> GetRoleAsync(string positionId, string token);
    }
}