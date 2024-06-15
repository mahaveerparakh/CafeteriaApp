using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeteriaApp.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int EmployeeId { get; set; }
        public int MenuItemId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // Rating out of 5
        public DateTime Date { get; set; }
    }
}
