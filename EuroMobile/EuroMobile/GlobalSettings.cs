using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobile
{
    public class GlobalSettings
    {
        public const string DefaultBaseUrl = "https://localhost:44340";
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

        public GlobalSettings()
        {
            BaseEndpoint = DefaultBaseUrl;
        }

        private void UpdateEndpoint(string baseEndpoint)
        {
            RegisterEndpoint = $"{baseEndpoint}/api/register";
            LogoutEndpoint = $"{baseEndpoint}/api/account/endsession";
        }
    }
}