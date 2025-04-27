using Blazored.LocalStorage;
using Microsoft.JSInterop;
using wellmanage.clientapp.Shared.Interfaces;

namespace wellmanage.clientapp.Web.Client.Services.Storage
{
    public class WebAppStorage : IAppStorage
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IJSRuntime _jsRuntime;

        public WebAppStorage(ILocalStorageService localStorage, IJSRuntime jSRuntime)
        {
            _localStorage = localStorage;
            _jsRuntime = jSRuntime;
        }

        public async Task SetAsync(string key, string value)
        {
            if (IsJavaScriptAvailable())
            {
                await _localStorage.SetItemAsync(key, value);
            }
        }

        public async Task<string?> GetAsync(string key)
        {
            if (IsJavaScriptAvailable())
            {
                return await _localStorage.GetItemAsync<string>(key);
            }
            else
            {
                return null;
            }
        }

        public async Task RemoveAsync(string key)
        {
            if (IsJavaScriptAvailable())
            {                
                await _localStorage.RemoveItemAsync(key);
            }
        }

        private bool IsJavaScriptAvailable()
        {
            try
            {
                var _ = (IJSInProcessRuntime)_jsRuntime;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
