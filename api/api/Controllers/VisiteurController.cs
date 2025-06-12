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
        [Route("{id}")]
        public async Task<IActionResult> addVisiteur([FromBody] CreateVisiteur createVisiteur, [FromRoute] long id)
        {
            var visiteur = await visiteurService.save(createVisiteur, id);
            return Ok(visiteur);
        }
    }
}
