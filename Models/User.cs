using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Name { get; set; }

        [Range(0, 9999.99)]
        public decimal Age { get; set; }

        [Required, StringLength(100)]
        public string? Address { get; set; }
    }
}
