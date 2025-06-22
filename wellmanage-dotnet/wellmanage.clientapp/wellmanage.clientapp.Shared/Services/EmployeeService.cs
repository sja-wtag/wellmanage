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
    public class EmployeeService
    {
        private readonly HttpClient _http;
        private readonly IAppStorage _appStorage;
        public EmployeeService(HttpClient http, IAppStorage appStorage)
        {
            _http = http;
            _appStorage = appStorage;
        }

        public async Task<List<ProjectDto>> GetProjectsAssignedToEmployee()
        {
            try
            {
                var emp = System.Text.Json.JsonSerializer.Deserialize<EmployeeDto>(await _appStorage.GetAsync("employee-details"));
                var response = await _http.GetAsync($"api/v1/employee/{emp?.Id}/projects");
                var result = await response.Content.ReadFromJsonAsync<List<ProjectDto>>();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<List<ProjectTaskDto>> GetTasksAssignedToEmployee()
        {
            try
            {
                var emp = System.Text.Json.JsonSerializer.Deserialize<EmployeeDto>(await _appStorage.GetAsync("employee-details"));
                var response = await _http.GetAsync($"api/v1/employee/{emp?.Id}/tasks");
                var result = await response.Content.ReadFromJsonAsync<List<ProjectTaskDto>>();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ProjectDto>> GetAllProjects()
        {
            try
            {
                var response = await _http.GetAsync($"api/v1/projects");
                var result = await response.Content.ReadFromJsonAsync<List<ProjectDto>>();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EmployeeDto>> GetAllEmployees()
        {
            try
            {
                var response = await _http.GetAsync($"api/v1/employee");
                var result = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
