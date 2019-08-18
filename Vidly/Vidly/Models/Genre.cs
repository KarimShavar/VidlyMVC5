using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Genre
    {
        [Required]
        public short Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}