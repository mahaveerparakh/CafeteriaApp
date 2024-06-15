using CafeteriaApp.Models;
using CafeteriaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp
{
    public class RecommendationEngineClient
    {
        private readonly HttpClient _httpClient;

        public RecommendationEngineClient(string baseAddress)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress) // Replace with your actual API base URL
            };
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var credentials = new { Username = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/authenticate", credentials);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            return null;
        }

        public async Task<bool> AddMenuItemAsync(MenuItem menuItem)
        {
            var response = await _httpClient.PostAsJsonAsync("api/menuitems", menuItem);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateMenuItemAsync(MenuItem menuItem)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/menuitems/{menuItem.Id}", menuItem);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteMenuItemAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/menuitems/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsAsync()
        {
            var response = await _httpClient.GetAsync("api/menuitems");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<MenuItem>>();
        }

        public async Task<MenuItem> GetMenuItemByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/menuitems/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MenuItem>();
        }

        // Feedback methods
        public async Task<IEnumerable<Feedback>> GetFeedbackForTodayAsync()
        {
            var response = await _httpClient.GetAsync("api/feedback/today");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Feedback>>();
        }

        // Report methods
        public async Task<string> GetMonthlyReportAsync()
        {
            var response = await _httpClient.GetAsync("api/reports/monthly");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> SubmitFeedbackAsync(Feedback feedback)
        {
            var response = await _httpClient.PostAsJsonAsync("api/feedback", feedback);
            return response.IsSuccessStatusCode;
        }

        // Notification methods
        public async Task<IEnumerable<Notification>> GetNotificationsAsync()
        {
            var response = await _httpClient.GetAsync("api/notifications");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Notification>>();
        }
    }
}
