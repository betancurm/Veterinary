using System.ComponentModel.DataAnnotations;

namespace Veterinary.web.Models
{
    public class Pet
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        [Required]
        public string Date { get; set; }
        [Required]
        public string PetName { get; set; }
        [Required]
        public string Allergy { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Race { get; set; }
        [Required]
        public string Weight { get; set; }
        [Required]
        public string Colour { get; set; }
        [Required]
        public string Remarks { get; set; }
    }
}

