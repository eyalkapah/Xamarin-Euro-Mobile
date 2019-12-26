using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobile.Services
{
    public interface IMatchService
    {
        Task<HttpResponseMessage> AddMatchAsync(int homeTeamId, int guestTeamId, DateTime matchDate, int groupId);
    }
}