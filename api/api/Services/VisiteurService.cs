using api.Dtos.Verification;
using api.Dtos.Visiteur;
using api.Mappers;
using api.Models;
using api.Repository.Interfaces;
using api.Services.Interfaces;

namespace api.Services
{
    public class VisiteurService : IVisiteurService
    {

        private readonly IVisiteurRepository visiteurRepository;
        private readonly IVerificationRepository verificationRepository;

        public VisiteurService(IVisiteurRepository visiteurRepository, IVerificationRepository verificationRepository)
        {
            this.visiteurRepository = visiteurRepository;
            this.verificationRepository = verificationRepository;
        }

        public async Task<Visiteur?> deleteVisiteur(long id)
        {
            return await visiteurRepository.delete(id);
        }

        public async Task<List<Visiteur>> getAllByOrganisateurId(long organisateurId)
        {
            return await visiteurRepository.getAllByOrganisateurId(organisateurId);
        }

        public async Task<Visiteur> save(CreateVisiteur createVisiteur,long evenementId)
        {
            var visiteur = await visiteurRepository.save(createVisiteur.fromCreateVisiteur());
            var createVerification = new CreateVerification { Etat = Enums.StatutVerification.EN_ATTENTE };
            await verificationRepository.save(createVerification.fromCreateVerification(visiteur.Id, evenementId));
            return visiteur;
        }
    }
}
