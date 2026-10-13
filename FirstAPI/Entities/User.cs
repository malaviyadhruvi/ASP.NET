using System.ComponentModel.DataAnnotations;
namespace FirstAPI.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
    }
}
