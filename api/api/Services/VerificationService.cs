using api.Models;
using api.Services.Interfaces;

namespace api.Services
{
    public class VerificationService : IVerificationService
    {
        public Task<List<Verification>> getAllByOrganisateurId(long organisateurId)
        {
            throw new NotImplementedException();
        }
    }
}
