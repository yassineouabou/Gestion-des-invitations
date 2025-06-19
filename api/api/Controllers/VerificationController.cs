using api.Dtos.Verification;
using api.Mappers;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly IVerificationService verificationService;

        public VerificationController(IVerificationService verificationService)
        {
            this.verificationService = verificationService;
        }

        [HttpGet("organisateur/{organisateurId:long}")]
        public async Task<ActionResult<List<VerificationDto>>> GetAllByOrganisateurId([FromRoute] long organisateurId)
        {
            var verifications = await verificationService.getAllByOrganisateurId(organisateurId);

            var verificationDtos = verifications
                .Select(v => v.fromVerification())
                .ToList();

            return Ok(verificationDtos);
        }

        [HttpGet("envoyer-email/{id:long}")]
        public async Task<IActionResult> EnvoyerEmail(long id)
        {
            var verification = await verificationService.sendEmail(id);

            if (verification == null)
                return NotFound("Vérification introuvable ou informations incomplètes.");

            return Ok(new
            {
                Message = "E-mail envoyé avec succès"
            });
        }


        [HttpGet("accepter/{id:long}")]
        public async Task<IActionResult> Accepter(long id)
        {
            var verification = await verificationService.accepter(id);

            if (verification == null)
                return NotFound("Vérification non trouvée.");

            return Ok(verification.fromVerification());
        }

        [HttpGet("refuser/{id:long}")]
        public async Task<IActionResult> Refuser(long id)
        {
            var verification = await verificationService.refuser(id);

            if (verification == null)
                return NotFound("Vérification non trouvée.");

            return Ok(verification.fromVerification());
        }


        [HttpGet("{id:long}")]
        public async Task<IActionResult> getById(long id)
        {
            var verification = await verificationService.getById(id);

            if (verification == null)
                return NotFound("Vérification non trouvée.");

            return Ok(verification.fromVerification());
        }

    }
}
