using api.Dtos.Visiteur;
using api.Mappers;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        [Route("{organisateurId}")]
        public async Task<IActionResult> getAll([FromRoute] long organisateurId)
        {
            var visiteurs = await visiteurService.getAllByOrganisateurId(organisateurId);
            return Ok(visiteurs.Select(v=>v.ToVisiteurAvecEvenementsDto(organisateurId)));
        }
    }
}
