﻿using api.Data;
using api.Dtos.Evenement;
using api.Models;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EvenementRepository : IEvenementRepository
    {
        private readonly AppDbContext appDbContext;

        public EvenementRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Evenement?> delete(long id)
        {
            var evenement = await appDbContext.Evenements.FirstOrDefaultAsync(e => e.Id == id);
            if (evenement == null)
                return null;
            appDbContext.Evenements.Remove(evenement);
            await appDbContext.SaveChangesAsync();
            return evenement;
        }

        public async Task<List<Evenement>> findAll()
        {
            return await appDbContext.Evenements.ToListAsync();
        }

        

        public async Task<Evenement?> FindById(long id)
        {
            return await appDbContext.Evenements.FirstOrDefaultAsync(e=>e.Id==id);
        }

        public async Task<Evenement> save(Evenement evenement)
        {
            await appDbContext.Evenements.AddAsync(evenement);
            await appDbContext.SaveChangesAsync();

            evenement.Lien = $"http://localhost:4200/inscription?evenementId={evenement.Id}";
            appDbContext.Evenements.Update(evenement);
            await appDbContext.SaveChangesAsync();

            return evenement;
        }

        public async Task<Evenement?> update(CreateEvenement createEvenement,long id)
        {
            var evenement = await appDbContext.Evenements.FirstOrDefaultAsync(e => e.Id == id);
            if (evenement == null)
                return null;
            evenement.Titre = createEvenement.Titre;
            evenement.Lieu = createEvenement.Lieu;
            evenement.DateEvenement = createEvenement.DateEvenement;
            await appDbContext.SaveChangesAsync();
            return evenement;
        }

        public async Task<List<Evenement>> findAllByOrganisateurId(long id)
        {
            return await appDbContext.Evenements
                .Where(e=>e.OrganisateurId==id)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }
    }
}
