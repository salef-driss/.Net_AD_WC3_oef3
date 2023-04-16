using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } 
        public DateTime Birthday { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int HouseNr { get; set; } 
        public List<Points>? Points { get; set; }
    }
}
