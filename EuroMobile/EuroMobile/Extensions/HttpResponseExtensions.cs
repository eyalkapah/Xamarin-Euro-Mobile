﻿using Euro.Shared.In;
using Euro.Shared.Out;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EuroMobile.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<Stream> GetContentStreamAsync(this HttpResponseMessage response)
        {
            return await response.Content.ReadAsStreamAsync();
        }

        public static async Task<string> GetResponseErrorAsync(this HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStreamAsync();

            var apiResponse = await JsonSerializer.DeserializeAsync<GeneralApiResponse<LoginResultApiModel>>(json);

            return !apiResponse.IsSucceeded ? apiResponse.Error : null;
        }

        public static async Task<List<ErrorApiModel>> GetResponseErrors(this HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new List<ErrorApiModel>
                {
                    new ErrorApiModel
                    {
                        Code = "Status Code",
                        Description = "Unauthorized."
                    }
                };
            }
            else
            {
                var json = await response.Content.ReadAsStreamAsync();

                var apiResponse = await JsonSerializer.DeserializeAsync<ApiResponse<RegisterCredentialsResultApiModel>>(json);

                if (!apiResponse.IsSucceeded)
                {
                    return apiResponse.Errors.Any() ? apiResponse.Errors : new List<ErrorApiModel>
                    {
                        new ErrorApiModel
                        {
                            Code = response.StatusCode.ToString(),
                            Description = $"Server responded with {response.StatusCode}"
                        }
                    };
                }

                return null;
            }
        }
    }
}