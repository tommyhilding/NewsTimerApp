using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NewsTimerApp.Core.Services
{
    public class SecureStorageService : ISecureStorageService
    {
        public Task<string> GetAsync(string key)
        {
            return SecureStorage.GetAsync(key);
        }

        public bool Remove(string key)
        {
            return SecureStorage.Remove(key);
        }

        public void RemoveAll()
        {
            SecureStorage.RemoveAll();
        }

        public Task SetAsync(string key, string value)
        {
            return SecureStorage.SetAsync(key, value);
        }
    }
}
