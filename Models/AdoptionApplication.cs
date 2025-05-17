using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AnimalShelter.Models
{
    public class AdoptionApplication
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        
        [Required]
        [Display(Name = "Animal Name")]
        public string AnimalName { get; set; } = string.Empty;

        [ForeignKey("AnimalId")]
        [ValidateNever]
        public Animal Animal { get; set; } = null!;

        [Required(ErrorMessage = "Please enter your full name")]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        [Display(Name = "Full Address")]
        public string? Address { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
    }
}