using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.clientapp.Shared.Interfaces;

namespace wellmanage.clientapp.Services
{
    public class MauiAppStorage : IAppStorage
    {
        public Task SetAsync(string key, string value)
        {
            Preferences.Set(key, value);
            return Task.CompletedTask;
        }

        public Task<string?> GetAsync(string key)
        {
            return Task.FromResult(Preferences.ContainsKey(key) ? Preferences.Get(key, null) : null);
        }

        public Task RemoveAsync(string key)
        {
            Preferences.Remove(key);
            return Task.CompletedTask;
        }
    }

}
