using api.Models;
using api.Repository.Interfaces;
using api.Services.Interfaces;

namespace api.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly IVerificationRepository verificationRepository;

        public VerificationService(IVerificationRepository verificationRepository)
        {
            this.verificationRepository = verificationRepository;
            
        }
        public async Task<List<Verification>> getAllByOrganisateurId(long organisateurId)
        {
            return await verificationRepository.getAllByOrganisateurId(organisateurId);
        }
    }
}
