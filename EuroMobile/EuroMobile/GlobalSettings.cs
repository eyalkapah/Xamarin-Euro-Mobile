using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EuroMobile
{
    public class GlobalSettings
    {
        //public const string DefaultBaseUrl = "https://localhost:44340";
        //public const string DefaultBaseUrl = "http://10.164.71.176:5001";

        public static string DefaultBaseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";

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
        public string LogInEndpoint { get; private set; }

        public GlobalSettings()
        {
            BaseEndpoint = DefaultBaseUrl;
        }

        private void UpdateEndpoint(string baseEndpoint)
        {
            RegisterEndpoint = $"{baseEndpoint}/api/register";
            LogInEndpoint = $"{BaseEndpoint}/api/login";
            LogoutEndpoint = $"{baseEndpoint}/api/account/endsession";
        }
    }
}