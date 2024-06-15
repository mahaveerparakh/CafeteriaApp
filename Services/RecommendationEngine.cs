using System;
using System.Collections.Generic;
using System.Linq;
using CafeteriaApp.Models;

namespace CafeteriaApp.Services
{
    public static class RecommendationEngine
    {
        public static double GetFoodRating(int menuItemId, List<Feedback> feedbacks)
        {
            var ratings = feedbacks.Where(f => f.MenuItemId == menuItemId).Select(f => f.Rating);
            if (ratings.Any())
            {
                return ratings.Average();
            }
            return 0;
        }

        public static double AnalyzeSentiment(string comment)
        {
            // Simplified sentiment analysis: positive words +1, negative words -1, neutral 0
            var positiveWords = new[] { "good", "great", "delicious", "nice", "excellent" };
            var negativeWords = new[] { "bad", "poor", "terrible", "awful", "disgusting" };

            int score = 0;
            var words = comment.ToLower().Split(' ');
            foreach (var word in words)
            {
                if (positiveWords.Contains(word)) score++;
                if (negativeWords.Contains(word)) score--;
            }
            return score;
        }
    }
}
