using Euro.Domain.ApiModels;
using Euro.Shared;
using EuroMobile.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobile.Services
{
    public class TeamService : ITeamService
    {
        public async Task<HttpResponseMessage> GetAllTeamsAsync()
        {
            var client = await HttpClientExtensions.HttpAuthenticatedClientAsync();

            var response = await client.GetAsync(Routes.GetAllTeams);

            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}