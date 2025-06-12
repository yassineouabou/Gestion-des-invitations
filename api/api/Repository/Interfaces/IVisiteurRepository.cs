using api.Models;

namespace api.Repository.Interfaces
{
    public interface IVisiteurRepository
    {
        Task<Visiteur> save(Visiteur visiteur);
        Task<List<Visiteur>> getAllByOrganisateurId(long organisateurId);
    }
}
