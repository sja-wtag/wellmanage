using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wellmanage.clientapp.Shared.Interfaces
{
    public interface IAppStorage
    {
        Task SetAsync(string key, string value);
        Task<string?> GetAsync(string key);
        Task RemoveAsync(string key);
    }

}
