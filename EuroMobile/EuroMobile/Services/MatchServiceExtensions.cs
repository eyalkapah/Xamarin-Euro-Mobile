using Euro.Shared.Out;
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
    public static class MatchServiceExtensions
    {
        public static async Task<MatchResultApiModel> HandleSuccessfullAddMatchAsync(this HttpResponseMessage response)
        {
            try
            {
                var stream = await response.GetContentStreamAsync(); ;

                var result = await JsonSerializer.DeserializeAsync<MatchResultApiModel>(stream);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<IEnumerable<MatchResultApiModel>> HandleSuccessfullGetAllMatches(this HttpResponseMessage response)
        {
            var stream = await response.GetContentStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<IEnumerable<MatchResultApiModel>>(stream);

            return result;
        }
    }
}