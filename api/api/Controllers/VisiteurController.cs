using api.Dtos.Visiteur;
using api.Mappers;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisiteurController : ControllerBase
    {
        private readonly IVisiteurService visiteurService;

        public VisiteurController(IVisiteurService visiteurService)
        {
            this.visiteurService = visiteurService;
        }

        [HttpPost]
        [Route("{evenementId}")]
        public async Task<IActionResult> addVisiteur([FromBody] CreateVisiteur createVisiteur, [FromRoute] long evenementId)
        {
            var visiteur = await visiteurService.save(createVisiteur, evenementId);
            return Ok(visiteur.fromVisiteur());
        }

        [HttpGet("organisateur/{organisateurId}")]
        public async Task<IActionResult> getAllByOrganisateurId([FromRoute] long organisateurId)
        {
            var visiteurs = await visiteurService.getAllByOrganisateurId(organisateurId);
            return Ok(visiteurs.Select(v=>v.ToVisiteurAvecEvenementsDto(organisateurId)));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> deleteVisiteur([FromRoute] long id)
        {
            var visiteur = await visiteurService.deleteVisiteur(id);
            if (visiteur == null)
                return NotFound("Visiteur Not Found");
            return Ok(visiteur.fromVisiteur());
        }

    }
}
