using api.Models;

namespace api.Repository.Interfaces
{
    public interface IVerificationRepository
    {
        Task<Verification> save(Verification verification);
        Task<List<Verification>> getAllByOrganisateurId(long organisateurId);

        Task<Verification?> getById(long id);

        Task<Verification> saveChange(Verification verification);
        
    }
}
