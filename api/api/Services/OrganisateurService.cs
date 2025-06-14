using api.Dtos.Organisateur;
using api.Mappers;
using api.Models;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;

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
            createOrganisateur.Password = BCrypt.Net.BCrypt.HashPassword(createOrganisateur.Password);
            var organisateur = createOrganisateur.fromCreatOrganisateur();
            return await organisateurRepository.createOrganisateur(organisateur);
            
        }

        public async Task<Organisateur?> deleteOrganisateur(long id)
        {
            return await organisateurRepository.deleteOrganisateur(id);
        }

        public async Task<List<Organisateur>> getAllOrganisateur()
        {
            return await organisateurRepository.getAllOrganisateurs();
        }

        public async Task<Organisateur?> getOrganisateurById(long id)
        {
            return await organisateurRepository.GetOrganisateur(id);
        }

        public async Task<Organisateur?> login(LoginDto loginDto)
        {
            var organisateur = await organisateurRepository.findByEmail(loginDto.Email);
            if (organisateur == null)
                return null;
            else
            {
                Console.WriteLine($"Stored hash: {organisateur.Password}");
                bool isValid = BCrypt.Net.BCrypt.Verify(loginDto.Password,organisateur.Password);
                if (isValid)
                    return organisateur;
                else
                    return null;

            }
        }

        public async Task<Organisateur?> update(long id, CreateOrganisateur createOrganisateur)
        {
            return await organisateurRepository.updateOrganisateur(id,createOrganisateur);
        }
    }
}
