using api.Data;
using api.Models;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class VisiteurRepository : IVisiteurRepository
    {
        private readonly AppDbContext appDbContext;

        public VisiteurRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<Visiteur>> getAllByOrganisateurId(long organisateurId)
        {
            var visiteurs = await appDbContext.Visiteurs
        .Include(v => v.Verifications)
            .ThenInclude(verif => verif.Evenement)
        .Where(v => v.Verifications.Any(verif => verif.Evenement != null && verif.Evenement.OrganisateurId == organisateurId))
        .ToListAsync();
            return visiteurs;
        }

        public async Task<Visiteur> save(Visiteur visiteur)
        {
            await appDbContext.Visiteurs.AddAsync(visiteur);
            await appDbContext.SaveChangesAsync();
            return visiteur;
        }
    }
}
