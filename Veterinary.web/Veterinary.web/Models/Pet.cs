using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Veterinary.web.Models
{
    public class Pet
    {
        public int Id { get; set; }
        //[MaxLength(50, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        [Required]
        public string PetName { get; set; }
        [Required]
        public string Date { get; set; }
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
        public ICollection<Veterinarian> Veterinarians { get; set; }
        [DisplayName("Veterinarians Number")]
        public int VeterinariansNumber => Veterinarians == null ? 0 : Veterinarians.Count;

        [JsonIgnore] //lo ignora en la respuesta json
        [NotMapped] //no se crea en la base de datos
        public int IdOwner { get; set; }
    }
}

