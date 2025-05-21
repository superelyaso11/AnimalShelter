using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalShelter.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0, 50)]
        public int Age { get; set; }

        [Required]
        [StringLength(100)]
        public string Breed { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string Image { get; set; } = string.Empty;


        public bool IsAdopted { get; set; } = false;
    }
}