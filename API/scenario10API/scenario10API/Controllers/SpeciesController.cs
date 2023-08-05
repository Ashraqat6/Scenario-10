using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scenario10API.models;
using scenario10API.models.DTOs;
using scenario10API.Repos.Speciess;

namespace scenario10API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesRepo _speciesRepo;

        public SpeciesController(ISpeciesRepo speciesRepo)
        {
            _speciesRepo = speciesRepo;
        }

        // GET: api/Species
        [HttpGet]
        public ActionResult<IEnumerable<SpeciesDTO>> GetSpecies()
        {
            var species = _speciesRepo.GetAll()
                .Select(s => new SpeciesDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    ScientificName = s.ScientificName,
                    Status = s.Status,
                    Img = s.Img
                })
                .ToList();

            if (!species.Any())
            {
                return NotFound();
            }

            return species;
        }

        [HttpGet("{id}")]
        public ActionResult<SpeciesDTO> GetSpecies(int id)
        {
            var species = _speciesRepo.GetById(id);

            if (species == null)
            {
                return NotFound();
            }

            var speciesDTO = new SpeciesDTO
            {
                Id = species.Id,
                Name = species.Name,
                ScientificName = species.ScientificName,
                Status = species.Status,
                Img = species.Img
            };

            return speciesDTO;
        }

        [HttpPut("{id}")]
        public IActionResult PutSpecies(int id, SpeciesDTO speciesDTO)
        {
            if (id != speciesDTO.Id)
            {
                return BadRequest();
            }

            var species = _speciesRepo.GetById(id);

            if (species == null)
            {
                return NotFound();
            }

            species.Name = speciesDTO.Name;
            species.ScientificName = speciesDTO.ScientificName;
            species.Status = speciesDTO.Status;
            species.Img = speciesDTO.Img;

            _speciesRepo.Update(species);
            _speciesRepo.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<SpeciesDTO> PostSpecies(SpeciesDTO speciesDTO)
        {
            var species = new Species
            {
                Name = speciesDTO.Name,
                ScientificName = speciesDTO.ScientificName,
                Status = speciesDTO.Status,
                Img = speciesDTO.Img
            };

            _speciesRepo.Add(species);
            _speciesRepo.SaveChanges();

            return CreatedAtAction("GetSpecies", new { id = species.Id }, speciesDTO);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSpecies(int id)
        {
            var species = _speciesRepo.GetById(id);
            if (species == null)
            {
                return NotFound();
            }

            _speciesRepo.Delete(species);
            _speciesRepo.SaveChanges();

            return NoContent();
        }

        private bool SpeciesExists(int id)
        {
            return _speciesRepo.SpeciesExists(id);
        }

    }

}
