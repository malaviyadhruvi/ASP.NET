using System.ComponentModel.DataAnnotations;

namespace firstMVC.Models
{
    public class User
    {
        [Key]
        public int id {get; set; }
        public string name { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;

    }
}
