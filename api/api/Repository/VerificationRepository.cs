﻿using api.Data;
using api.Enums;
using api.Models;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class VerificationRepository : IVerificationRepository
    {
        private readonly AppDbContext appDbContext;

        public VerificationRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<Verification>> getAllByOrganisateurId(long organisateurId)
        {
            var verifications = await appDbContext.Verifications
                .Include(v => v.Evenement)
                .Include(v => v.Visiteur)
                .Where(v => v.Evenement != null && v.Evenement.OrganisateurId == organisateurId)
                .ToListAsync();

            return verifications;
        }
        public async Task<Verification> save(Verification verification)
        {
            await appDbContext.Verifications.AddAsync(verification);
            await appDbContext.SaveChangesAsync();
            return verification;
        }

        public async Task<Verification?> getById(long id)
        {
            return await appDbContext.Verifications
                .Include(v => v.Evenement)
                    .ThenInclude(e => e.Organisateur)
                .Include(v => v.Visiteur)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Verification> saveChange(Verification verification)
        {
            appDbContext.Verifications.Update(verification);
            await appDbContext.SaveChangesAsync();
            return verification;
        }
    }
}
