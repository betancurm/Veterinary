using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinary.web.Models;    

namespace Veterinary.web.Data
{
    public class SeedDb
    {
        private readonly ApplicationDbContext _context;
        public SeedDb(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
        }
        private async Task CheckCountriesAsync()
        {
            if (!_context.Owners.Any())
            {
                _context.Owners.Add(new Owner
                {
                    FullName = "Juan Manuel Betancur Mesa",
                    Identification = "1152451870",
                    Address = "Calle 31D # 89D 30",
                    Phone = "3053224270",
                    Email = "juanb9410@gmail.com",
                    Neighborhood = "Belen las Violetas",
                    Pets = new List<Pet>
                    {
                        new Pet
                        {
                            PetName = "Freya",
                            Date = "24/04/2022",
                            Allergy = "No tiene",
                            Age = "4",
                            Gender = "Hembra",
                            Race = "Boxer",
                            Weight = "15",
                            Colour = "Cafe",
                            Remarks = "Sin comentarios",
                            Veterinarians = new List<Veterinarian>
                            {
                                new Veterinarian { VeterinarianName = "Andres Betancur" },
                            }
                        },
                        new Pet
                        {
                            PetName = "Dargo",
                            Date = "22/04/2022",
                            Allergy = "No tiene",
                            Age = "6",
                            Gender = "Macho",
                            Race = "Boxer",
                            Weight = "18",
                            Colour = "Cafe",
                            Remarks = "Sin comentarios",
                            Veterinarians = new List<Veterinarian>
                            {
                                new Veterinarian { VeterinarianName = "Andres Betancur"},
                            },
                        }
                    }
                });
                _context.Owners.Add(new Owner
                {
                    FullName = "Holmes Cortéz",
                    Identification = "120545288",
                    Address = "Calle 1 sur # 48 30",
                    Phone = "3015689645",
                    Email = "holmescortez@hotmail.com",
                    Neighborhood = "Patio Bonito",
                    Pets = new List<Pet>
                    {
                        new Pet
                        {
                            PetName = "Maria Guadalupe",
                            Date = "27/04/2022",
                            Allergy = "Hongos",
                            Age = "3",
                            Gender = "Hembra",
                            Race = "Doberman",
                            Weight = "149",
                            Colour = "Cafe",
                            Remarks = "Preñada con un mes, revisión",
                            Veterinarians = new List<Veterinarian>
                            {
                                new Veterinarian { VeterinarianName = "Ricardo Chavarría" },
                            }
                        },
                        new Pet
                        {
                            PetName = "Manolo",
                            Date = "27/04/2022",
                            Allergy = "No tiene",
                            Age = "1",
                            Gender = "Macho",
                            Race = "Pincher",
                            Weight = "30",
                            Colour = "Cafe y negro",
                            Remarks = "Dolor abdominal",
                            Veterinarians = new List<Veterinarian>
                            {
                                new Veterinarian { VeterinarianName = "Ricardo Chavarría"},
                            },
                        }
                    }
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}

