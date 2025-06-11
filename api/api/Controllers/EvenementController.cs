using api.Dtos.Evenement;
using api.Mappers;
using api.Models;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvenementController : ControllerBase
    {
        private readonly IEvenementService evenementService;

        public EvenementController(IEvenementService evenementService)
        {
            this.evenementService = evenementService;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<Evenement>>> GetAll()
        {
            var evenements = await evenementService.findAllEvenements();
            return Ok(evenements.Select(e=>e.fromEvenement()));
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Evenement>> GetById(long id)
        {
            var evenement = await evenementService.findEvenementById(id);
            if (evenement == null)
            {
                return NotFound();
            }
            return Ok(evenement.fromEvenement());
        }

       
        [HttpPost("{organisateurId}")]
        public async Task<ActionResult<Evenement>> Create(CreateEvenement createEvenement, long organisateurId)
        {
            var newEvenement = await evenementService.createEvenement(createEvenement, organisateurId);
            return CreatedAtAction(nameof(GetById), new { id = newEvenement.Id }, newEvenement.fromEvenement());
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult<Evenement>> Update(long id, CreateEvenement createEvenement)
        {
            var updatedEvenement = await evenementService.updateEvenement(createEvenement, id);
            if (updatedEvenement == null)
            {
                return NotFound();
            }
            return Ok(updatedEvenement.fromEvenement());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Evenement>> Delete(long id)
        {
            var deletedEvenement = await evenementService.deleteEvenement(id);
            if (deletedEvenement == null)
            {
                return NotFound();
            }
            return Ok(deletedEvenement.fromEvenement());
        }


    }
}
