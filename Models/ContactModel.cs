using System.ComponentModel.DataAnnotations;

namespace AnimalShelter.Models
{
    public class Contact
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Please enter your name.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your message.")]
        public string? Message { get; set; }
    }
}