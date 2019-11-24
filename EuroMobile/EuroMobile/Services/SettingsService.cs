using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EuroMobile.Services
{
    public class SettingsService : ISettingsService
    {
        private const string BaseUrl = "url_base";
        private static readonly string BaseUrlDefault = GlobalSettings.Instance.BaseEndpoint;

        public string UrlBase
        {
            get => GetValueOrDefault(BaseUrl, BaseUrlDefault);
            set => AddOrUpdateValue(BaseUrl, value);
        }

        public SettingsService()
        {
        }

        public Task AddOrUpdateValue(string key, bool value) => AddOrUpdateValueInternalAsync(key, value);

        public Task AddOrUpdateValue(string key, string value) => AddOrUpdateValueInternalAsync(key, value);

        public bool GetValueOrDefault(string key, bool defaultValue) => GetValueOrDefaultInternal(key, defaultValue);

        public string GetValueOrDefault(string key, string defaultValue) => GetValueOrDefaultInternal(key, defaultValue);

        private async Task AddOrUpdateValueInternalAsync<T>(string key, T value)
        {
            if (value == null)
            {
                await RemoveAsync(key);
            }

            Application.Current.Properties[key] = value;
            try
            {
                await Application.Current.SavePropertiesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to save: " + key, " Message: " + ex.Message);
            }
        }

        private T GetValueOrDefaultInternal<T>(string key, T defaultValue = default)
        {
            object value = null;
            if (Application.Current.Properties.ContainsKey(key))
            {
                value = Application.Current.Properties[key];
            }
            return null != value ? (T)value : defaultValue;
        }

        private async Task RemoveAsync(string key)
        {
            try
            {
                if (Application.Current.Properties[key] != null)
                {
                    Application.Current.Properties.Remove(key);
                    await Application.Current.SavePropertiesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to remove: " + key, " Message: " + ex.Message);
            }
        }
    }
}