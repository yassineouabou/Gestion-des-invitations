using api.Data;
using api.Models;
using api.Repository.Interfaces;

namespace api.Repository
{
    public class VerificationRepository : IVerificationRepository
    {
        private readonly AppDbContext appDbContext;

        public VerificationRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Verification> save(Verification verification)
        {
            await appDbContext.Verifications.AddAsync(verification);
            await appDbContext.SaveChangesAsync();
            return verification;
        }
    }
}
