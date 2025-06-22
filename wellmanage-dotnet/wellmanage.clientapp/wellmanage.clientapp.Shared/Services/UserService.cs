using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using wellmanage.clientapp.Shared.Interfaces;
using wellmanage.shared.Models;

namespace wellmanage.clientapp.Shared.Services
{
    public class UserService
    {
        private readonly HttpClient _http;
        public event Action<AttendanceStatus> OnAttendenceChanged;
        private readonly JwtAuthStateProvider _authStateProvider;
        private readonly IAppStorage _storage;
        public UserService(HttpClient http, AuthenticationStateProvider authStateProvider, IAppStorage storage)
        {
            _http = http;
            _authStateProvider = (JwtAuthStateProvider)authStateProvider;
            _storage = storage;
        }

        public async Task<EmployeeDto> GetEmployeeDetailsByUserId()
        {
            try
            {
                var user = await _authStateProvider.GetAuthenticatedUser();
                var response = await _http.GetAsync($"api/v1/user/{user.Id}/employee-details");
                var result = await response.Content.ReadFromJsonAsync<EmployeeDto>();
                _storage.SetAsync("employee-details", System.Text.Json.JsonSerializer.Serialize(result));
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
