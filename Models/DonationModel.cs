using System.ComponentModel.DataAnnotations;

namespace AnimalShelter.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter an amount.")]
        [Range(1, 10000, ErrorMessage = "Amount must be between $1 and $10,000.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        public string? DonorName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
    }
}