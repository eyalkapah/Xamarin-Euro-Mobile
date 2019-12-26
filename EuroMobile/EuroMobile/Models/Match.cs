using Euro.Shared.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobile.Models
{
    public class Match
    {
        public int GroupId { get; set; }

        public int GuestScored { get; set; }

        public int HostScored { get; set; }

        public int MatchId { get; set; }

        public DateTime PlayDateTime { get; set; }

        public TeamResultApiModel HomeTeam { get; set; }

        public TeamResultApiModel GuestTeam { get; set; }
    }
}