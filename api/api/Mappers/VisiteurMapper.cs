using api.Dtos.Visiteur;
using api.Models;

namespace api.Mappers
{
    public static class VisiteurMapper
    {
        public static VisiteurDto fromVisiteur(this Visiteur visiteur)
        {
            return new VisiteurDto
            {
                Id = visiteur.Id,
                Nom = visiteur.Nom,
                Email = visiteur.Email
            };
        }

        public static Visiteur fromCreateVisiteur(this CreateVisiteur createVisiteur)
        {
            return new Visiteur
            {
                Nom = createVisiteur.Nom,
                Email = createVisiteur.Email
            };
        } 
    }
}
