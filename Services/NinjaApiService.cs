using System.Net.Http.Json;
using FitnessApp.Models;

namespace FitnessApp.Services
{
    public class NinjaApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/ninjas";

        public NinjaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Workout[]> GetWorkoutsAsync(string? muscle = null, string? type = null, string? difficulty = null)
        {
            var queryParams = new List<string>();
            
            if (!string.IsNullOrEmpty(muscle))
                queryParams.Add($"muscle={Uri.EscapeDataString(muscle)}");
            
            if (!string.IsNullOrEmpty(type))
                queryParams.Add($"type={Uri.EscapeDataString(type)}");
            
            if (!string.IsNullOrEmpty(difficulty))
                queryParams.Add($"difficulty={Uri.EscapeDataString(difficulty)}");

            var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            var url = $"{BaseUrl}/workouts{query}";

            try
            {
                var workouts = await _httpClient.GetFromJsonAsync<Workout[]>(url);
                return workouts ?? Array.Empty<Workout>();
            }
            catch (Exception)
            {
                return Array.Empty<Workout>();
            }
        }

        public async Task<string[]> GetUniqueMusclesAsync()
        {
            try
            {
                var workouts = await GetWorkoutsAsync();
                return workouts
                    .Where(w => !string.IsNullOrEmpty(w.Muscle))
                    .Select(w => w.Muscle)
                    .Distinct()
                    .OrderBy(m => m)
                    .ToArray();
            }
            catch (Exception)
            {
                return Array.Empty<string>();
            }
        }

        public async Task<string[]> GetUniqueTypesAsync()
        {
            try
            {
                var workouts = await GetWorkoutsAsync();
                return workouts
                    .Where(w => !string.IsNullOrEmpty(w.Type))
                    .Select(w => w.Type)
                    .Distinct()
                    .OrderBy(t => t)
                    .ToArray();
            }
            catch (Exception)
            {
                return Array.Empty<string>();
            }
        }

        public async Task<string[]> GetUniqueDifficultiesAsync()
        {
            try
            {
                var workouts = await GetWorkoutsAsync();
                return workouts
                    .Where(w => !string.IsNullOrEmpty(w.Difficulty))
                    .Select(w => w.Difficulty)
                    .Distinct()
                    .OrderBy(d => d)
                    .ToArray();
            }
            catch (Exception)
            {
                return Array.Empty<string>();
            }
        }
    }
}
