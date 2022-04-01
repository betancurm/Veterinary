using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Veterinary.web.Models
{
    public class Owner
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} debe contener al menos un caracter")]
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Identification { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Neighborhood { get; set; }

        public ICollection<Pet> Pets { get; set; }
        [DisplayName("Departments Number")]
        public int DepartmentsNumber => Pets == null ? 0 : Pets.Count;

    }
}
