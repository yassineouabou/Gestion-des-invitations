using api.Data;
using api.Dtos.Organisateur;
using api.Models;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class OrganisateurRepository : IOrganisateurRepository
    {
        private readonly AppDbContext appDbContext;

        public OrganisateurRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Organisateur> createOrganisateur(Organisateur organisateur)
        {
            await appDbContext.organisateurs.AddAsync(organisateur);
            await appDbContext.SaveChangesAsync();
            return organisateur;
        }

        public async Task<Organisateur?> deleteOrganisateur(long organisateurId)
        {
            var organisateur = await appDbContext.organisateurs.FirstOrDefaultAsync(o=>o.Id==organisateurId);
            if (organisateur == null)
                return null;
            appDbContext.organisateurs.Remove(organisateur);
            await appDbContext.SaveChangesAsync();
            return organisateur;
        }

        public async Task<List<Organisateur>> getAllOrganisateurs()
        {
            return await appDbContext.organisateurs.ToListAsync();

        }

        public async Task<Organisateur?> GetOrganisateur(long organisateurId)
        {
            return await appDbContext.organisateurs.FindAsync(organisateurId);
        }

        public Task<Organisateur?> updateOrganisateur(long id, CreateOrganisateur createOrganisateur)
        {
            throw new NotImplementedException();
        }
    }
}
