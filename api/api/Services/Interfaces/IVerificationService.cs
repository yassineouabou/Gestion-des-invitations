using api.Models;

namespace api.Services.Interfaces
{
    public interface IVerificationService
    {
        Task<List<Verification>> getAllByOrganisateurId(long organisateurId);
    }
}
