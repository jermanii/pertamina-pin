using Newtonsoft.Json;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Models.Base;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Pertamina.IRIS.Services.Interfaces;
using Pertamina.IRIS.Models.Entities;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Pertamina.IRIS.Helpers;
using Pertamina.IRIS.Repositories.Interfaces;
using System.Collections.Generic;

namespace Pertamina.IRIS.Services.Implement
{
    public class IdamanService : IIdamanService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static string _email;
        private readonly AppSettingsBaseModel _appSettings;
        private readonly IIdamanRepository _idamanRepository;
        public IdamanService(IIdamanRepository idamanRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, AppSettingsBaseModel appSettings)
        {
            _config = configuration;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
            _idamanRepository = idamanRepository;
            _client = AuthHelper.ClientIdamanBearear(_httpContextAccessor, _appSettings);
            _email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        }
        public async Task<UserViewModel> GetUserAsync(string userId, string token)
        {
            TblmIdamanUrl idamanUrl = _idamanRepository.GetIdamanUrl(EnumBaseModel.GetUserAsync);
            var url = _appSettings.IdamanUrlApi + idamanUrl.EndPointUrl + userId;
            var client = new HttpClient();
            token = AuthHelper.GetTokenIdaman(client, _httpContextAccessor, _appSettings);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                _client.Dispose();
                string json = await response.Content.ReadAsStringAsync();
                UserViewModel value = JsonConvert.DeserializeObject<UserViewModel>(json);
                return value;
            }
            else
                _client.Dispose();

            return null;
        }

        public async Task<List<string>> GetRoleAsync(string userName, string token)
        {
            List<string> result = new List<string>();

            UserViewModel user = await GetUserAsync(userName, token);
            WhitelistViewModel whitelist = await GetWhitelistsAsync(user.id, token);

            if (whitelist.value != null)
            {
                foreach (var rec in whitelist.value)
                {
                    foreach (var role in rec.role)
                    {
                        if (role.application.applicationName == "IRIS")
                            result.Add(role.roleName);
                    }
                }
            }

            return result;
        }

        private async Task<WhitelistViewModel> GetWhitelistsAsync(string userId, string token)
        {
            TblmIdamanUrl idamanUrl = _idamanRepository.GetIdamanUrl(EnumBaseModel.GetWhitelistsAsync);
            var url = _appSettings.IdamanUrlApi + idamanUrl.EndPointUrl + userId + idamanUrl.ParameterTake;
            var client = new HttpClient();
            token = AuthHelper.GetTokenIdaman(client, _httpContextAccessor, _appSettings);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                _client.Dispose();
                string json = await response.Content.ReadAsStringAsync();
                WhitelistViewModel value = JsonConvert.DeserializeObject<WhitelistViewModel>(json);
                return value;
            }

            _client.Dispose();
            return null;
        }
    }
}
