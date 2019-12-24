using Euro.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobile.Services
{
    public interface ITeamService
    {
        Task<HttpResponseMessage> GetAllTeamsAsync();
    }
}