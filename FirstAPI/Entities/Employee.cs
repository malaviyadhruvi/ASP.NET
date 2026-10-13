using System.ComponentModel.DataAnnotations;

namespace FirstAPI.Entities
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public DateTime? DOB { get; set; }
        public String position { get; set; }
        public String Department { get; set; }
        public String EmailAddress { get; set; }
    }
}

