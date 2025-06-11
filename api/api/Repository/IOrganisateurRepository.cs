using api.Models;

namespace api.Repository
{
    public interface IOrganisateurRepository
    {
        Task<Organisateur> createOrganisateur(Organisateur organisateur);
        Task<List<Organisateur>> getAllOrganisateurs();
        Task<Organisateur?> GetOrganisateur(long organisateurId);
        Task<Organisateur?> deleteOrganisateur(long organisateurId);
        Task<Organisateur?> updateOrganisateur(long id);
    }
}
