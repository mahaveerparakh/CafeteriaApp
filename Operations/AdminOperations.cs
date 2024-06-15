using CafeteriaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Operations
{
    public class AdminOperations
    {
        private readonly RecommendationEngineClient _apiClient;

        public AdminOperations(RecommendationEngineClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task AddMenuItem()
        {
            Console.WriteLine("Enter Menu Item Name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter Price:");
            var price = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter Availability Status(Y/N):");
            var status = Console.ReadLine();

            var menuItem = new MenuItem
            {
                Name = name,
                Price = price,
                IsAvailable = status.ToUpper() == "Y" ? true : false
            };

            var response = await _apiClient.AddMenuItemAsync(menuItem);
            if (response)
            {
                Console.WriteLine("Menu item added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add menu item.");
            }
        }

        public async Task UpdateMenuItem()
        {
            Console.WriteLine("Enter Menu Item ID to Update:");
            var id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter New Name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter New Price:");
            var price = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter New Availability Status(Y/N):");
            var status = Console.ReadLine();

            var menuItem = new MenuItem
            {
                Id = id,
                Name = name,
                Price = price,
                IsAvailable = status.ToUpper() == "Y" ? true : false
            };

            var response = await _apiClient.UpdateMenuItemAsync(menuItem);
            if (response)
            {
                Console.WriteLine("Menu item updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update menu item.");
            }
        }

        public async Task DeleteMenuItem()
        {
            Console.WriteLine("Enter Menu Item ID to Delete:");
            var id = int.Parse(Console.ReadLine());

            var response = await _apiClient.DeleteMenuItemAsync(id);
            if (response)
            {
                Console.WriteLine("Menu item deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete menu item.");
            }
        }

        public async Task ListMenuItems()
        {
            var menuItems = await _apiClient.GetMenuItemsAsync();
            foreach (var item in menuItems)
            {
                Console.WriteLine($"{item.Id}. {item.Name} - ${item.Price} - {item.IsAvailable}");
            }
        }
    }
}
