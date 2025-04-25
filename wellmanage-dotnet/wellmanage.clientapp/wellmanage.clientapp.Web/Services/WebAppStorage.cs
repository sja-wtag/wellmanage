using Blazored.LocalStorage;
using wellmanage.clientapp.Shared.Interfaces;

namespace wellmanage.clientapp.Web.Services
{
    public class WebAppStorage : IAppStorage
    {
        private readonly ILocalStorageService _localStorage;

        public WebAppStorage(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task SetAsync(string key, string value) => await _localStorage.SetItemAsync(key, value);
        public async Task<string?> GetAsync(string key) => await _localStorage.GetItemAsync<string>(key);
        public async Task RemoveAsync(string key) => await _localStorage.RemoveItemAsync(key);
    }

}
