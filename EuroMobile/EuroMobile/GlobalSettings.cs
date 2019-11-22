using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobile
{
    public class GlobalSettings
    {
        private string _baseEndpoint;
        public static GlobalSettings Instance { get; } = new GlobalSettings();

        public string BaseEndpoint
        {
            get => _baseEndpoint;
            set
            {
                _baseEndpoint = value;
                UpdateEndpoint(_baseEndpoint);
            }
        }

        public string LogoutEndpoint { get; private set; }
        public string RegisterEndpoint { get; private set; }

        private void UpdateEndpoint(string baseEndpoint)
        {
            RegisterEndpoint = $"{baseEndpoint}/api/account/register";
            LogoutEndpoint = $"{baseEndpoint}/api/account/endsession";
        }
    }
}