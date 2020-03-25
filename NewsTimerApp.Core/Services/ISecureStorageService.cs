using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsTimerApp.Core.Services
{
    public interface ISecureStorageService
    {
        Task<string> GetAsync(string key);
        bool Remove(string key);
        void RemoveAll();
        Task SetAsync(string key, string value);
    }
}
