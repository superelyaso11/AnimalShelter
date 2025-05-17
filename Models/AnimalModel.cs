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

        [NotMapped] // This property won't be stored in the database
        public IFormFile? ImageFile { get; set; } // For file upload
        public string Image { get; set; } = string.Empty; // Will store the filename


        public bool IsAdopted { get; set; } = false;
    }
}