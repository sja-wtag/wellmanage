using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using wellmanage.shared.Models;

namespace wellmanage.clientapp.Shared.Services
{
    public class AttendenceService
    {
        private readonly HttpClient _http;
        public event Action<AttendanceStatus> OnAttendenceChanged;

        public AttendenceService(HttpClient http)
        {
            _http = http;
        }

        public async Task<AttendanceStatus> GetAttendenceStatus()
        {
            var response = await _http.GetAsync("api/v1/user/attendence-status");
            var result = await response.Content.ReadFromJsonAsync<AttendanceStatus>();
            OnAttendenceChanged?.Invoke(result);
            return result;
        } 

        public async Task CheckInUser()
        {
            var response = await _http.PostAsync("api/v1/user/check-in", null);
        }

        public async Task CheckOutUser()
        {
            var response = await _http.PostAsync("api/v1/user/check-out", null);
        }
    }
}
