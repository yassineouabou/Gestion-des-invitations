using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions options):base(options){}

        public DbSet<Organisateur> organisateurs { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Visiteur> Visiteurs { get; set; }
        public DbSet<Verification> Verifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration des relations
            modelBuilder.Entity<Evenement>()
                .HasOne(e => e.Organisateur)
                .WithMany(o => o.Evenements)
                .HasForeignKey(e => e.OrganisateurId);

            modelBuilder.Entity<Verification>()
                .HasOne(v => v.Visiteur)
                .WithMany(v => v.Verifications)
                .HasForeignKey(v => v.VisiteurId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Verification>()
                .HasOne(v => v.Evenement)
                .WithMany(e => e.Verifications)
                .HasForeignKey(v => v.EvenementId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
