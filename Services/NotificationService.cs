using System;
using System.Collections.Generic;

namespace CafeteriaApp.Services
{
    public class Notification
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public Notification(string message)
        {
            Message = message;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Timestamp}: {Message}";
        }
    }

    public static class NotificationService
    {
        private static List<string> notifications = new List<string>();

        public static void Notify(string message, string type = "Info")
        {
            string timestamp = DateTime.Now.ToString("G");
            string notification = $"{timestamp} [{type}]: {message}";
            notifications.Add(notification);
            Console.WriteLine($"Notification: {notification}");
        }

        public static void ListNotifications()
        {
            Console.WriteLine("Listing all notifications:");
            foreach (var notification in notifications)
            {
                Console.WriteLine(notification);
            }
        }
    }
}
