using Euro.Shared.In;
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
    public static class GroupServiceExtensions
    {
        public static async Task<IEnumerable<GroupResultApiModel>> HandleSuccessfullGetAllGroups(this HttpResponseMessage response)
        {
            var stream = await response.GetContentStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<IEnumerable<GroupResultApiModel>>(stream);

            return result;
        }
    }
}