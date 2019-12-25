using Euro.Domain.ApiModels;
using Euro.Shared.Out;
using EuroMobile.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EuroMobile.Services
{
    public static class TeamServiceExtensions
    {
        public static async Task<IEnumerable<TeamResultApiModel>> HandleSuccessfullGetAllTeams(this HttpResponseMessage response)
        {
            var stream = await response.GetContentStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<IEnumerable<TeamResultApiModel>>(stream);

            return result;
        }
    }
}