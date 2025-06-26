using api.Data;
using api.Dtos.Visiteur;
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

        public async Task<Visiteur?> delete(long id)
        {
            var visiteur = await appDbContext.Visiteurs.FirstOrDefaultAsync(v=>v.Id==id);
            if (visiteur == null)
                return null;
            appDbContext.Visiteurs.Remove(visiteur);
            await appDbContext.SaveChangesAsync();
            return visiteur;
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

        public async Task<Visiteur> save(Visiteur addedVisiteur)
        {
            

            await appDbContext.Visiteurs.AddAsync(addedVisiteur);
            await appDbContext.SaveChangesAsync();
            return addedVisiteur;
        }
    }
}
