using CafeteriaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Operations
{
    public class EmployeeOperations
    {
        private readonly RecommendationEngineClient _apiClient;

        public EmployeeOperations(RecommendationEngineClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task ViewNotifications()
        {
            Console.WriteLine("Notifications:");
            var notifications = await _apiClient.GetNotificationsAsync();
            foreach (var notification in notifications)
            {
                Console.WriteLine($"{notification.Message} - {notification.Timestamp}");
            }
        }

        public async Task SelectFoodItem()
        {
            Console.WriteLine("Select food item:");
            var menuItems = await _apiClient.GetMenuItemsAsync();
            foreach (var item in menuItems)
            {
                Console.WriteLine($"{item.Id}. {item.Name} - ${item.Price} - {item.IsAvailable}");
            }

            Console.WriteLine("Enter the ID of the item you want to select:");
            var id = int.Parse(Console.ReadLine());

            var selectedItem = await _apiClient.GetMenuItemByIdAsync(id);
            if (selectedItem != null)
            {
                Console.WriteLine($"You have selected: {selectedItem.Name}");
                // Add logic to save the selected food item
            }
            else
            {
                Console.WriteLine("Menu item not found.");
            }
        }

        public async Task FillFeedbackForm()
        {
            Console.WriteLine("Fill Feedback Form:");
            var menuItems = await _apiClient.GetMenuItemsAsync();
            foreach (var item in menuItems)
            {
                Console.WriteLine($"{item.Id} .  {item.Name}  - $ {item.Price}  -  {item.IsAvailable}");
            }

            Console.WriteLine("Enter the ID of the item you want to give feedback on:");
            var id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter your comment:");
            var comment = Console.ReadLine();
            Console.WriteLine("Enter your rating (1-5):");
            var rating = int.Parse(Console.ReadLine());

            var feedback = new Feedback
            {
                MenuItemId = id,
                Comment = comment,
                Rating = rating,
                Date = DateTime.Now
            };

            var response = await _apiClient.SubmitFeedbackAsync(feedback);
            if (response)
            {
                Console.WriteLine("Feedback submitted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to submit feedback.");
            }
        }
    }
}
