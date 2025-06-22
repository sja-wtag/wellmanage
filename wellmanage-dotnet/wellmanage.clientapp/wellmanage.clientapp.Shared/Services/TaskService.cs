using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using wellmanage.shared.Enums;
using wellmanage.shared.Models;
using static System.Net.WebRequestMethods;

namespace wellmanage.clientapp.Shared.Services
{
    public class TaskService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/v1/tasks";

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProjectTaskDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProjectTaskDto>>(BaseUrl);
        }

        public async Task<ProjectTaskDto?> GetByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<ProjectTaskDto>($"{BaseUrl}/{id}");
        }

        public async Task<List<ProjectTaskDto>> GetTasksAsync(long? projectId = null, long? employeeId = null)
        {
            var url = $"{BaseUrl}";
            var queryParams = new List<string>();
            if (projectId.HasValue)
                queryParams.Add($"projectId={projectId.Value}");
            if (employeeId.HasValue)
                queryParams.Add($"employeeId={employeeId.Value}");

            if (queryParams.Count > 0)
                url += "?" + string.Join("&", queryParams);

            return await _httpClient.GetFromJsonAsync<List<ProjectTaskDto>>(url);
        }

        public async Task<ProjectTaskDto> CreateAsync(ProjectTaskDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProjectTaskDto>();
        }

        public async Task<bool> UpdateAsync(long id, ProjectTaskDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task UpdateTaskStatusAsync(long taskId, TaskStatusEnum newStatus)
        {
            var url = $"{BaseUrl}/{taskId}/status";
            var response = await _httpClient.PutAsJsonAsync(url, newStatus);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error - throw or show message
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error updating task status: {errorContent}");
            }
        }
    }
}
