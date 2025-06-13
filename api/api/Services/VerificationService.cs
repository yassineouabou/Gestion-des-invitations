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

        public async Task<Verification?> sendEmail(long id)
        {
            var visiteur = await verificationRepository.getById(id);
            if(visiteur != null)
            {
                visiteur.Etat = Enums.StatutVerification.ENVOYE;
                await verificationRepository.save(visiteur);

            }
            return visiteur;
                
        }
    }
}
