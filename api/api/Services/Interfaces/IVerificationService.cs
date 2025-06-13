using api.Models;

namespace api.Services.Interfaces
{
    public interface IVerificationService
    {
        Task<List<Verification>> getAllByOrganisateurId(long organisateurId);
        Task<Verification?> sendEmail(long id);
        Task<Verification?> accepter(long id);
        Task<Verification?> refuser(long id);
    }
}
