using api.Dtos.Evenement;
using api.Models;

namespace api.Repository.Interfaces
{
    public interface IEvenementRepository
    {
        Task<Evenement> sava(Evenement evenement);
        Task<Evenement?> FindById(long id);
        Task<Evenement?> delete(long id);
        Task<Evenement> update(long id, CreateEvenement createEvenement);
    }
}
