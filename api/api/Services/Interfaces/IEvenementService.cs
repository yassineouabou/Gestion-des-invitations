using api.Dtos.Evenement;
using api.Models;

namespace api.Services.Interfaces
{
    public interface IEvenementService
    {
        Task<Evenement> createEvenement(CreateEvenement createEvenement, long organisateurId);
        Task<Evenement?> findEvenementById(long id);
        Task<List<Evenement>> findAllEvenements();
        Task<Evenement?> deleteEvenement(long id);
        Task<Evenement?> updateEvenement(CreateEvenement createEvenement,long id);

        Task<List<Evenement>> evenementsByOrganisateurId(long organisateurId);
    }
}
