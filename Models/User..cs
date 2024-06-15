using System.ComponentModel.DataAnnotations;

namespace CafeteriaApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }    
        public Role UserRole { get; set; }
    }
}
