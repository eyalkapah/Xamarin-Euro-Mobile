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
    public class GroupService : IGroupService
    {
        public async Task<HttpResponseMessage> GetAllGroupsAsync()
        {
            var client = await HttpClientExtensions.HttpAuthenticatedClientAsync();

            var response = await client.GetAsync(Routes.Groups);

            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}