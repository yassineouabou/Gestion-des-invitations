using api.Dtos.Organisateur;
using api.Mappers;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisateurController : ControllerBase
    {
        private readonly IOrganisateurService organisateurService;
        public OrganisateurController(IOrganisateurService organisateurService)
        {
            this.organisateurService = organisateurService;
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> getOrganisateur([FromRoute] long id)
        {
            var organisateur = await organisateurService.getOrganisateurById(id);
            if (organisateur == null)
                return NotFound("Organisateur Not Found !");
            return Ok(organisateur.fromOrganisateur());
        }


        [HttpPost("register")]
        public async Task<IActionResult> createOrganisateur([FromBody] CreateOrganisateur createOrganisateur)
        {
            var organisateur = await organisateurService.createOgranisateur(createOrganisateur);            
            return CreatedAtAction(nameof(getOrganisateur), new { Id = organisateur.Id }, organisateur.fromOrganisateur());
        }


        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginDto loginDto)
        {
            var organisateur = await organisateurService.login(loginDto);
            if (organisateur == null)
                return NotFound("Organisateur Not Found !");
            return Ok(organisateur);
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> deleteOrganisateur([FromRoute] long id)
        {
            var organisateur = await organisateurService.deleteOrganisateur(id);
            if (organisateur == null)
                return NotFound("Organisateur Not Found !");
            return Ok("Organisateur Deleted.");
        }
    }
}
