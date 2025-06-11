using api.Models;

namespace api.Repository.Interfaces
{
    public interface IVerificationRepository
    {
        Task<Verification> save(Verification verification);
    }
}
