using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Operations
{
    public class ChefOperations
    {
        private readonly RecommendationEngineClient _apiClient;

        public ChefOperations(RecommendationEngineClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task SelectMenuForNextDay()
        {
            Console.WriteLine("Select items for next day:");
            var menuItems = await _apiClient.GetMenuItemsAsync();
            foreach (var item in menuItems)
            {
                Console.WriteLine($"{item.Id}. {item.Name} - ${item.Price} - {item.IsAvailable}");
            }

            Console.WriteLine("Enter the IDs of the items you want to select (comma separated):");
            var selectedIds = Console.ReadLine().Split(',');
            foreach (var id in selectedIds)
            {
                var menuItem = await _apiClient.GetMenuItemByIdAsync(int.Parse(id.Trim()));
                if (menuItem != null)
                {
                    Console.WriteLine($"Selected: {menuItem.Name}");
                    // Add logic to save the selected menu items for the next day
                }
                else
                {
                    Console.WriteLine($"Menu item with ID {id} not found.");
                }
            }
        }

        public async Task RollOutFeedbackForToday()
        {
            Console.WriteLine("Feedback for today:");
            var feedbacks = await _apiClient.GetFeedbackForTodayAsync();
            foreach (var feedback in feedbacks)
            {
                Console.WriteLine($"{feedback.Id}: {feedback.Comment} - Rating: {feedback.Rating}");
            }
        }

        public async Task GenerateMonthlyReport()
        {
            Console.WriteLine("Generating Monthly Report:");
            var report = await _apiClient.GetMonthlyReportAsync();
            Console.WriteLine(report);
        }
    }
}
