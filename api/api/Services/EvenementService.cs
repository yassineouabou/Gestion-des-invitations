using api.Dtos.Evenement;
using api.Mappers;
using api.Models;
using api.Repository.Interfaces;
using api.Services.Interfaces;

namespace api.Services
{
    public class EvenementService : IEvenementService
    {
        private readonly IEvenementRepository evenementRepository;

        public EvenementService(IEvenementRepository evenementRepository)
        {
            this.evenementRepository = evenementRepository;
        }
        public async Task<Evenement> createEvenement(CreateEvenement createEvenement, long organisateurId)
        {
            var evenement = createEvenement.toEvenementFromCreate(organisateurId);
            return await evenementRepository.save(evenement);
        }

        public async Task<Evenement?> deleteEvenement(long id)
        {
            return await evenementRepository.delete(id);
        }


        public async Task<List<Evenement>> findAllEvenements()
        {
            return await evenementRepository.findAll();
        }

        public async Task<Evenement?> findEvenementById(long id)
        {
            return await evenementRepository.FindById(id);
        }

        public async Task<Evenement?> updateEvenement(CreateEvenement createEvenement, long id)
        {
            return await evenementRepository.update(createEvenement,id);
        }


        public async Task<List<Evenement>> evenementsByOrganisateurId(long organisateurId)
        {
            return await evenementRepository.findAllByOrganisateurId(organisateurId);
        }

    }
}
