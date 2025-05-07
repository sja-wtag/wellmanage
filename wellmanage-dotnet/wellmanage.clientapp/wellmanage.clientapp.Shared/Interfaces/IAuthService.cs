using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wellmanage.shared.Models;


namespace wellmanage.clientapp.Shared.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticatedUser?> Login(LoginRequest request);
        Task Register(RegistrationRequest request);
        Task Logout();
    }

}
