using api.Dtos.Organisateur;
using api.Mappers;
using api.Models;
using api.Repository.Interfaces;
using api.Services.Interfaces;


namespace api.Services
{
    public class OrganisateurService : IOrganisateurService
    {
        private readonly IOrganisateurRepository organisateurRepository;
        public OrganisateurService(IOrganisateurRepository organisateurRepository)
        {
            this.organisateurRepository = organisateurRepository;
        }
        public async Task<Organisateur> createOgranisateur(CreateOrganisateur createOrganisateur)
        {
            var organisateur = createOrganisateur.fromCreatOrganisateur();
            return await organisateurRepository.createOrganisateur(organisateur);
            
        }

        public async Task<List<Organisateur>> getAllOrganisateur()
        {
            return await organisateurRepository.getAllOrganisateurs();
        }

        public async Task<Organisateur?> getOrganisateurById(long id)
        {
            return await organisateurRepository.GetOrganisateur(id);
        }
    }
}
