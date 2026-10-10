using System.ComponentModel.DataAnnotations;

namespace firstMVC.Models
{
    public class User
    {
        [Key]
        public int id {get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
