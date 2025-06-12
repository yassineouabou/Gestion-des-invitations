using api.Dtos.Visiteur;
using api.Models;

namespace api.Services.Interfaces
{
    public interface IVisiteurService
    {
        Task<Visiteur> save(CreateVisiteur createVisiteur, long evenementId);
        Task<List<Visiteur>> getAllByOrganisateurId(long organisateurId);
    }
}
