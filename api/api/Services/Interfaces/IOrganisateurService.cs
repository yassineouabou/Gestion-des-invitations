using api.Dtos.Organisateur;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Services.Interfaces
{
    public interface IOrganisateurService
    {
        Task<Organisateur> createOgranisateur(CreateOrganisateur createOrganisateur);
        Task<Organisateur?> getOrganisateurById(long id);
        Task<List<Organisateur>> getAllOrganisateur();
        Task<Organisateur?> deleteOrganisateur(long id);
        Task<Organisateur?> login(LoginDto loginDto);
    }
}
