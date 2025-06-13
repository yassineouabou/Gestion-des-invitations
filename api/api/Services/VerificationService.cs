using api.Enums;
using api.Models;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly IVerificationRepository verificationRepository;
        private readonly IEmailServiceWithAI emailServiceWithAI;

        public VerificationService(IVerificationRepository verificationRepository, IEmailServiceWithAI emailServiceWithAI   )
        {
            this.verificationRepository = verificationRepository;
            this.emailServiceWithAI = emailServiceWithAI;
        }
        public async Task<List<Verification>> getAllByOrganisateurId(long organisateurId)
        {
            return await verificationRepository.getAllByOrganisateurId(organisateurId);
        }


        public async Task<Verification?> sendEmail(long id)
        {
            var verification = await verificationRepository.getById(id);
            if (verification == null || verification.Visiteur == null || verification.Evenement == null)
                return null;

            var result = await emailServiceWithAI.EnvoyerEmailAvecAI(verification.Visiteur,verification.Evenement);

            if (result)
            {
                verification.Etat = Enums.StatutVerification.ENVOYE;
                await verificationRepository.saveChange(verification);
            }
            
            return verification;
                
        }
    }
}
