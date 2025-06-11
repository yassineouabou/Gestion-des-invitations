using api.Data;
using api.Models;
using api.Repository.Interfaces;

namespace api.Repository
{
    public class VisiteurRepository : IVisiteurRepository
    {
        private readonly AppDbContext appDbContext;

        public VisiteurRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Visiteur> save(Visiteur visiteur)
        {
            await appDbContext.Visiteurs.AddAsync(visiteur);
            await appDbContext.SaveChangesAsync();
            return visiteur;
        }
    }
}
