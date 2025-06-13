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
    }
}
