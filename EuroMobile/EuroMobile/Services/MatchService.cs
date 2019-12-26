using Euro.Domain.ApiModels;
using Euro.Shared;
using EuroMobile.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EuroMobile.Services
{
    public class MatchService : IMatchService
    {
        public async Task<HttpResponseMessage> AddMatchAsync(int homeTeamId, int guestTeamId, DateTime matchDate, int groupId)
        {
            var jsonContent = JsonSerializer.Serialize(new MatchApiModel
            {
                HostTeamId = homeTeamId,
                GuestTeamId = guestTeamId,
                PlayDateTime = matchDate,
                GroupId = groupId
            });

            var response = await HttpClientExtensions.PostDefaultHttpClient(Routes.Matches, jsonContent.ToString());

            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}