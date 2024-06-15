using System;
using System.Collections.Generic;
using CafeteriaApp.Models;  // User, Role, MenuItem, Feedback
using CafeteriaApp.Services; // NotificationService
using CafeteriaApp.Operations;

namespace CafeteriaApp
{
    public class Program
    {
        private static RecommendationEngineClient _apiClient;

        public static async Task Main(string[] args)
        {
            _apiClient = new RecommendationEngineClient("https://localhost:5001");

            var user = await AuthenticateUserAsync();
            if (user == null)
            {
                Console.WriteLine("Invalid credentials.");
                return;
            }

            Console.WriteLine($"Welcome {user.Username}!");
            ShowMenu(user);
        }

        private static async Task<User> AuthenticateUserAsync()
        {
            Console.WriteLine("Enter Username:");
            var username = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            var password = Console.ReadLine();

            return await _apiClient.AuthenticateAsync(username, password);
        }

        private static void ShowMenu(User user)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Please Select an option");
                switch (user.UserRole)
                {
                    case Role.Admin:
                        ShowAdminMenu();
                        break;
                    case Role.Chef:
                        ShowChefMenu();
                        break;
                    case Role.Employee:
                        ShowEmployeeMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid role.");
                        break;
                }
            }
        }

        private static void ShowAdminMenu()
        {
            var adminOperations = new AdminOperations(_apiClient);

            Console.WriteLine("Admin Menu:");
            Console.WriteLine("1. Add Menu Item");
            Console.WriteLine("2. Update Menu Item");
            Console.WriteLine("3. Delete Menu Item");
            Console.WriteLine("4. List Menu Items");
            Console.WriteLine("0. Exit");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    adminOperations.AddMenuItem().Wait();
                    break;
                case "2":
                    adminOperations.UpdateMenuItem().Wait();
                    break;
                case "3":
                    adminOperations.DeleteMenuItem().Wait();
                    break;
                case "4":
                    adminOperations.ListMenuItems().Wait();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void ShowChefMenu()
        {
            var chefOperations = new ChefOperations(_apiClient);

            Console.WriteLine("Chef Menu:");
            Console.WriteLine("1. Select Menu for next day");
            Console.WriteLine("2. Roll Out Feedback for today");
            Console.WriteLine("3. Generate Monthly Report");
            Console.WriteLine("0. Exit");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    chefOperations.SelectMenuForNextDay().Wait();
                    break;
                case "2":
                    chefOperations.RollOutFeedbackForToday().Wait();
                    break;
                case "3":
                    chefOperations.GenerateMonthlyReport().Wait();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void ShowEmployeeMenu()
        {
            var employeeOperations = new EmployeeOperations(_apiClient);

            Console.WriteLine("Employee Menu:");
            Console.WriteLine("1. View Notification");
            Console.WriteLine("2. Select Food Item");
            Console.WriteLine("3. Fill Feedback form");
            Console.WriteLine("0. Exit");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    employeeOperations.ViewNotifications().Wait();
                    break;
                case "2":
                    employeeOperations.SelectFoodItem().Wait();
                    break;
                case "3":
                    employeeOperations.FillFeedbackForm().Wait();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

}

