using api.Dtos.Visiteur;
using api.Models;
using api.Repository.Interfaces;
using api.Services.Interfaces;

namespace api.Services
{
    public class VisiteurService : IVisiteurService
    {

        private readonly IVisiteurRepository visiteurRepository;

        public VisiteurService(IVisiteurRepository visiteurRepository)
        {
            this.visiteurRepository = visiteurRepository;
        }
        public async Task<Visiteur> save(CreateVisiteur createVisiteur)
        {

            return await visiteurRepository.save();
        }
    }
}
